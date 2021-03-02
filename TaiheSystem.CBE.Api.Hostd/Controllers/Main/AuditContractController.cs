using Mapster;
using TaiheSystem.CBE.Api.Hostd.Authorization;
using TaiheSystem.CBE.Api.Hostd.Extensions;
using TaiheSystem.CBE.Api.Interfaces;
using TaiheSystem.CBE.Api.Model;
using TaiheSystem.CBE.Api.Model.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TaiheSystem.CBE.Api.Model.View.Main;
using TaiheSystem.CBE.Api.Model.View;
using TaiheSystem.CBE.Api.Model.View.System;
using TaiheSystem.CBE.Api.GWF;
using System.Data;

namespace TaiheSystem.CBE.Api.Hostd.Controllers.Main
{
    /// <summary>
    /// 合同评审接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuditContractController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<AuditContractController> _logger;
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// OA合同接口
        /// </summary>
        private readonly IBizPreContractService _precontractService;

        /// <summary>
        /// 合同登记接口
        /// </summary>
        private readonly IBizMainContractService _maincontractService;

        /// <summary>
        /// 客户信息接口
        /// </summary>
        private readonly IufkhxxService _customerService;

        /// <summary>
        /// 合同项目标准项目信息
        /// </summary>
        private readonly IBizContractItemService _contractitemService;

        /// <summary>
        /// 合同分场所管理
        /// </summary>
        private readonly IBizContractFcsService _contractfcsService;

        /// <summary>
        /// 附件管理
        /// </summary>
        private readonly IBizContractFileService _contractfileService;


        private readonly IViewMainContractService _vmcontractService;


        public AuditContractController(ILogger<AuditContractController> logger, TokenManager tokenManager, IBizMainContractService maincontractService, IufkhxxService customerService, IBizContractItemService contractitemService, IBizContractFcsService contractfcsService, IBizContractFileService contractfileService, IBizPreContractService precontractService
            , IViewMainContractService vmcontractService)
        {
            _logger = logger;
            _tokenManager = tokenManager;
            _maincontractService = maincontractService;
            _customerService = customerService;
            _contractitemService = contractitemService;
            _contractfcsService = contractfcsService;
            _contractfileService = contractfileService;
            _precontractService = precontractService;
            _vmcontractService = vmcontractService;
        }


        /// <summary>
        /// 查询合同管理信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Query([FromBody] MainContractQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<View_MainContract>();

            //predicate = predicate.And(m=>m.deleted == 0);
            //登记状态
            if(parm.status == 0) //待评审
            {
                predicate = predicate.And(m => m.status == 10010);
            }
            else if (parm.status == 1) //已审批
            {
                predicate = predicate.And(m => m.status == 10020);
            }
            else if (parm.status == 2) //退回
            {
                predicate = predicate.And(m => m.status == 10015);
            }
            else if (parm.status == 3) //终止
            {
                predicate = predicate.And(m => m.status == 10090);
            }
            //合同编号
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ContractNo), m => m.ContractNo.Contains(parm.ContractNo));
            //客户名称
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.khmc), m => m.zzmc.Contains(parm.khmc));
            //合作伙伴
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.hzhb), m => m.mc.Contains(parm.hzhb));
            //受理日期开始
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.AcceptDateStart), m => m.CreateTime >= DateTime.Parse(parm.AcceptDateStart));
            //受理日期结束
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.AcceptDateEnd), m => m.CreateTime <= DateTime.Parse(parm.AcceptDateEnd));

            var response = _vmcontractService.GetPages(predicate.ToExpression(), parm);

            return toResponse(response);
        }

        /// <summary>
        /// 获取合同项目初审基础人日信息
        /// </summary>
        /// <param name="ContractItemID">合同项目ID</param>
        /// <param name="RiskIndex">选项index</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult GetBaseMandays(string ID,string RiskIndex)
        {
            if(string.IsNullOrEmpty(ID))
            {
                return toResponse(StatusCodeType.Error, "合同项目ID不能为空");
            }
            if(string.IsNullOrEmpty(RiskIndex))
            {
                return toResponse(StatusCodeType.Error, "风险等级不能为空");
            }
            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                DataTable contrcatitem = db.Ado.GetDataTable("select PropleNum,SystemTypeID from Biz_ContractItem b inner join Abi_SysStandard c on b.ItemStandardID = c.ID where b.ID  =@ID", new { ID = ID }); //获取体系人数
                if(contrcatitem.Rows.Count == 0)
                {
                    return toResponse(StatusCodeType.Error, "项目信息获取失败");
                }
                object auditdays = db.Ado
                    .GetDecimal(@"select AuditDays from Sys_AuditTime where SystemTypeID = @SystemTypeID and (isnull(RiskRegisterID,0) = 0 or (RiskRegisterID=@RiskRegisterID))
                    and (isnull(DownLimt,0) < @PropleNum and isnull(UpLimit,99999) > @PropleNum)", new { SystemTypeID = contrcatitem.Rows[0]["SystemTypeID"].ToString(), RiskRegisterID  = RiskIndex , PropleNum = contrcatitem.Rows[0]["PropleNum"].ToString() });
                if(auditdays == null)
                {
                    return toResponse(StatusCodeType.Error, "匹配基础人日信息失败，请核对！");
                }
                BaseMandaysDto basedata = new BaseMandaysDto();
                basedata.FirstTrialBaseDays = (decimal)auditdays;
                basedata.SupervisionBaseDays = Math.Round(((decimal)auditdays/3),2);
                basedata.RecertificationBaseDays = Math.Round(((decimal)auditdays / 3) * 2, 2);

                return toResponse(basedata);
            }
        }

        /// <summary>
        /// 根据 Id 获取合同信息
        /// Power = PRIV_CONTRACT_DETAIL
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_CONTRACT_DETAIL")]
        public IActionResult Get(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "Id 不能为空");
            }
            return toResponse(_maincontractService.GetId(id));
        }


        /// <summary>
        /// 提交审核
        /// Power = PRIV_AUDITCONTRACT_APPROVE
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_AUDITCONTRACT_APPROVE")]
        public IActionResult SubmitApprove(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "Id 不能为空");
            }
            var userinfo = _tokenManager.GetSessionInfo();
            var maincontract = _maincontractService.GetId(id);

            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    List<SugarParameter> parameters = new List<SugarParameter>();
                    parameters.Add(new SugarParameter("UserID", userinfo.UserID));
                    parameters.Add(new SugarParameter("UserName", userinfo.UserName));
                    Step.Submit(db, maincontract, "Biz_MainContract", "ID", "status", "102", parameters, UpdateBizEntityAfterSubmitted, "提交审核");
                    Core.DbContext.CommitTran();
                    return toResponse("提交成功");
                }
                catch (Exception ex)
                {
                    Core.DbContext.RollbackTran();
                    return toResponse(StatusCodeType.Error, ex.Message);
                }
            }
        }


        public static Action<SqlSugarClient, List<SugarParameter>> UpdateBizEntityAfterSubmitted = (SqlSugarClient db, List<SugarParameter> paramters) =>
        {
            if (db.Ado.ExecuteCommand(@"UPDATE Biz_MainContract SET Status = @Node_To,SubmitAuditTime=getdate(),AuditID=@UserID,AuditName=@UserName
WHERE ID = @Biz_MainContract_ID AND Status = @Node_From", paramters) == 0)
            {
                throw new Exception(GWF.Step.DIRTY_DATA_PROMPT);
            }
        };

        /// <summary>
        /// 评审退回登记
        /// Power = PRIV_AUDITCONTRACT_RETURN
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_AUDITCONTRACT_RETURN")]
        public IActionResult AuditReturn(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "Id 不能为空");
            }
            var userinfo = _tokenManager.GetSessionInfo();
            var maincontract = _maincontractService.GetId(id);

            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    List<SugarParameter> parameters = new List<SugarParameter>();
                    parameters.Add(new SugarParameter("UserID", userinfo.UserID));
                    parameters.Add(new SugarParameter("UserName", userinfo.UserName));
                    Step.Submit(db, maincontract, "Biz_MainContract", "ID", "status", "106", parameters, UpdateBizEntityAfterSubmitted, "退回登记");
                    Core.DbContext.CommitTran();
                    return toResponse("提交成功");
                }
                catch (Exception ex)
                {
                    Core.DbContext.RollbackTran();
                    return toResponse(StatusCodeType.Error, ex.Message);
                }
            }
        }

        /// <summary>
        /// 根据 Id 获取合同及明细全部信息
        /// Power = PRIV_CONTRACT_DETAIL
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_CONTRACT_DETAIL")]
        public IActionResult GetDetail(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "Id 不能为空");
            }
            var userinfo = _tokenManager.GetSessionInfo();
            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                var contract = _maincontractService.GetId(id);
                if (contract == null || contract.deleted == 1)
                {
                    return toResponse(StatusCodeType.Error, "当前合同信息不存在");
                }

                AuditContractVM maincotract = Api.Common.Helpers.ComHelper.Mapper<AuditContractVM, Biz_MainContract>(contract);

                //获取客户信息
                maincotract.ContractCustomerInfo = _customerService.GetId(maincotract.CustomerID);
                //获取合同体系项目
                List<Biz_ContractItem> itemlist = _contractitemService.GetWhere(m => m.MainContractID == maincotract.ID && m.deleted == 0);
                List<ContractItemVM> ItemVMList = new List<ContractItemVM>();
                foreach (var item in itemlist)
                {
                    ContractItemVM itemVM = Api.Common.Helpers.ComHelper.Mapper<ContractItemVM, Biz_ContractItem>(item);

                    //默认赋值基础人日
                    DataTable contrcatitem = db.Ado.GetDataTable("select PropleNum,SystemTypeID from Biz_ContractItem b inner join Abi_SysStandard c on b.ItemStandardID = c.ID where b.ID  =@ID", new { ID = item.ID }); //获取体系人数
                    if (contrcatitem.Rows.Count > 0 && (item.FirstTrialBaseDays == null || item.FirstTrialBaseDays == 0))
                    {
                        //return toResponse(StatusCodeType.Error, "项目信息获取失败");
                        object auditdays = db.Ado
        .GetDecimal(@"select AuditDays from Sys_AuditTime where SystemTypeID = @SystemTypeID and (isnull(RiskRegisterID,0) = 0)
                    and (isnull(DownLimt,0) < @PropleNum and isnull(UpLimit,99999) > @PropleNum)", new { SystemTypeID = contrcatitem.Rows[0]["SystemTypeID"].ToString(), PropleNum = contrcatitem.Rows[0]["PropleNum"].ToString() });
                        if (auditdays == null)
                        {
                            return toResponse(StatusCodeType.Error, "匹配基础人日信息失败，请核对！");
                        }

                        itemVM.FirstTrialBaseDays = (decimal)auditdays;//初审基础人日
                        itemVM.SupervisionBaseDays = Math.Round(((decimal)auditdays / 3), 2); //监督基础人日
                        itemVM.RecertificationBaseDays = Math.Round(((decimal)auditdays / 3) * 2, 2); //再认证基础人日
                    }

                    
                    //增人日依据
                    itemVM.ContractItemAddList = Core.DbContext.Db.Queryable<Biz_ContractItem_Add>().Where(m => m.ContractItemID == item.ID).ToList();

                    //减人日依据
                    itemVM.ContractItemMinusList = Core.DbContext.Db.Queryable<Biz_ContractItem_Minus>().Where(m => m.ContractItemID == item.ID).ToList();

                    //体系业务代码
                    itemVM.ContractItemBizClassificationList = Core.DbContext.Db.Queryable<Biz_ContractItem_BizClassification, Abi_BizClassification>((a, b) => new object[] {
                    JoinType.Left,a.BizClassificationID == b.ID}).Where((a, b) => a.ContractItemID == item.ID && b.Enabled == true).Select((a, b) => new ContractItemBizClassVM
                    {
                        ID = a.ID,
                        ContractItemID = item.ID,
                        BizClassificationID = a.BizClassificationID,
                        SystemTypeID = b.SystemTypeID,
                        SystemTypeCode = b.SystemTypeCode,
                        SystemTypeName = b.SystemTypeName,
                        ClassificationCode = b.ClassificationCode,
                        ClassificationReportCode = b.ClassificationReportCode,
                        ClassificationName = b.ClassificationName,
                        ClassificationNameEN = b.ClassificationNameEN,
                        Industry = b.Industry,
                        RiskRegister = b.RiskRegister,
                        CNAS = b.CNAS
                    }).ToList();


                    ItemVMList.Add(itemVM);
                }
                maincotract.ContractItemList = ItemVMList;
                //获取合同分场所信息
                maincotract.ContractfcsList = Core.DbContext.Db.Queryable<Biz_ContractFcs, uf_fcsxx>((a, b) => new object[] {
        JoinType.Left,a.fcsID==b.fcsID}).OrderBy((a, b) => b.fcslx).Where((a, b) => a.MainContractID == maincotract.ID)
          .Select((a, b) => new FcsVM { ID = a.ID, fcsID = (int)a.fcsID, fcslx = b.fcslx, fcslxmc = b.fcslxmc, fcsmc = b.fcsmc, fcsmcy = b.fcsmcy, dz = b.dz, dzy = b.dzy, lxdh = b.lxdh, cz = b.cz, lxr = b.lxr, lxrsj = b.lxrsj, fxcrs = b.fxcrs, jzbjl = b.jzbjl, znbm = b.znbm, fcshd = b.fcshd, bz = b.bz }).ToList(); //固定场所排序显示
                                                                                                                                                                                                                                                                                                                            //获取合同附件信息
                maincotract.ContractFileList = _contractfileService.GetWhere(m => m.MainContractID == maincotract.ID);

                //支持方式
                if (maincotract.SupportWay != null)
                {
                    string[] IDs = maincotract.SupportWay.Split(',');
                    maincotract.SupportWayList = db.Queryable<Frm_SelectItemObj>().Where(m => IDs.Contains(m.SelectItemIndex.ToString())).ToList();
                }
                return toResponse(maincotract);
            }
        }


        /// <summary>
        /// 保存合同信息
        /// Power = PRIV_AUDITCONTRACT_UPDATE
        /// </summary>
        /// <param name="parm">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_AUDITCONTRACT_UPDATE")]
        public IActionResult Update(AuditContractUpdateDto parm)
        {
            var userinfo = _tokenManager.GetSessionInfo();

            if (_maincontractService.Any(m => m.PreContractID == parm.PreContractID && m.status >=10020))
            {
                return toResponse(StatusCodeType.Error, $"当前合同已经审批通过，不允许修改！");
            }
            var contract = _maincontractService.GetId(parm.ID);
            if (contract == null || contract.deleted == 1)
            {
                return toResponse(StatusCodeType.Error, "当前合同信息不存在");
            }

            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    string SupportWay = "";
                    if (parm.SupportWayList != null)
                    {
                        SupportWay = string.Join(",", parm.SupportWayList.Select(m => m.SelectItemIndex));
                    }
                    //从 Dto 映射到 实体
                    _maincontractService.Update(m => m.ID == parm.ID, m => new Biz_MainContract()
                    {
                        MultiSiteDays = parm.MultiSiteDays,
                        ApplyAuditID = parm.ApplyAuditID,
                        ApplyAuditName = parm.ApplyAuditName,
                        SupportUserID = parm.SupportUserID,
                        SupportUserName = parm.SupportUserName,
                        MarketAuditRemark = parm.MarketAuditRemark,
                        AuditPlanRemark = parm.AuditPlanRemark,
                        AuditTeamRemark = parm.AuditTeamRemark,
                        AuditRemark = parm.AuditRemark,
                        IntegrationLevel = parm.IntegrationLevel,
                        SupportWay = SupportWay,
                        UpdateID = userinfo.ID,
                        UpdateName=userinfo.UserName,
                        UpdateTime = DateTime.Now
                    });
                    //更新合同关联项目信息
                    List<ContractItemVM> ItemList = parm.ContractItemList;
                    if (ItemList != null)
                    {
                        foreach (var Item in ItemList)
                        {
                            //增人日依据
                            List<Biz_ContractItem_Add> addlist_update = Item.ContractItemAddList.Where(m => m.AutoID > 0).ToList(); //更新
                            List<Biz_ContractItem_Add> addlist_insert = Item.ContractItemAddList.Where(m => m.AutoID == 0).ToList(); //插入
                            List<Biz_ContractItem_Add> addlist_delete = Item.ContractItemAddList; //删除
                            //删除增人日依据
                            string[] addids = addlist_delete.Where(x => !string.IsNullOrEmpty(x.ID)).Select(x => x.ID).ToArray();
                            db.Deleteable<Biz_ContractItem_Add>().Where(m => m.ContractItemID == Item.ID && !addids.Contains(m.ID)).ExecuteCommand();
                            if(addlist_update != null)
                            {
                                foreach (var add in addlist_update) //更新
                                {
                                    db.Updateable<Biz_ContractItem_Add>().SetColumns(m => new Biz_ContractItem_Add()
                                    {
                                        AddIndex = add.AddIndex,
                                        AddName = add.AddName,
                                        AddPri = add.AddPri
                                    }).Where(m => m.ID == add.ID).ExecuteCommand();
                                }
                            }
                            if (addlist_insert != null)
                            {
                                foreach (var add in addlist_insert)
                                {
                                    add.ID = Guid.NewGuid().ToString();
                                    add.ContractItemID = Item.ID;
                                    db.Insertable<Biz_ContractItem_Add>(add).ExecuteCommand();
                                }
                            }
                            //减人日依据
                            List<Biz_ContractItem_Minus> minuslist_update = Item.ContractItemMinusList.Where(m => m.AutoID > 0).ToList(); //更新
                            List<Biz_ContractItem_Minus> minuslist_insert = Item.ContractItemMinusList.Where(m => m.AutoID == 0).ToList(); //插入
                            List<Biz_ContractItem_Minus> minuslist_delete = Item.ContractItemMinusList; //删除
                            //删除减人日依据
                            string[] minusids = minuslist_delete.Where(x => !string.IsNullOrEmpty(x.ID)).Select(x => x.ID).ToArray();
                            db.Deleteable<Biz_ContractItem_Minus>().Where(m => m.ContractItemID == Item.ID && !minusids.Contains(m.ID)).ExecuteCommand();
                            if (minuslist_update != null)
                            {
                                foreach (var minus in minuslist_update) //更新
                                {
                                    db.Updateable<Biz_ContractItem_Minus>().SetColumns(m => new Biz_ContractItem_Minus()
                                    {
                                        MinusIndex = minus.MinusIndex,
                                        MinusName = minus.MinusName,
                                        MinusPri = minus.MinusPri
                                    }).Where(m => m.ID == minus.ID).ExecuteCommand();
                                }
                            }
                            if (minuslist_insert != null)
                            {
                                foreach (var minus in minuslist_insert)
                                {
                                    minus.ID = Guid.NewGuid().ToString();
                                    minus.ContractItemID = Item.ID;
                                    db.Insertable<Biz_ContractItem_Minus>(minus).ExecuteCommand();
                                }
                            }
                            //业务类别处理
                            List<ContractItemBizClassVM> classlist_update = Item.ContractItemBizClassificationList.Where(m => !string.IsNullOrEmpty(m.ID)).ToList();//更新
                            List<ContractItemBizClassVM> classlist_insert = Item.ContractItemBizClassificationList.Where(m => string.IsNullOrEmpty(m.ID)).ToList();//插入
                            List<ContractItemBizClassVM> classlist_delete = Item.ContractItemBizClassificationList.Where(m => !string.IsNullOrEmpty(m.ID)).ToList();//删除
                            //删除管理业务类别
                            string[] classids = classlist_delete.Where(x => !string.IsNullOrEmpty(x.ID)).Select(x => x.ID).ToArray();
                            db.Deleteable<Biz_ContractItem_BizClassification>().Where(m => m.ContractItemID == Item.ID && !classids.Contains(m.ID)).ExecuteCommand();
                            if (classlist_update != null)
                            {
                                foreach (var cl in classlist_update)
                                {
                                    db.Updateable<Biz_ContractItem_BizClassification>().SetColumns(m => new Biz_ContractItem_BizClassification()
                                    {
                                        BizClassificationID = cl.BizClassificationID
                                    }).Where(m => m.ID == cl.ID).ExecuteCommand();
                                }
                            }
                            if (classlist_insert != null)
                            {
                                foreach (var cl in classlist_insert)
                                {
                                    Biz_ContractItem_BizClassification bc = Api.Common.Helpers.ComHelper.Mapper<Biz_ContractItem_BizClassification, ContractItemBizClassVM>(cl);
                                    bc.ID = Guid.NewGuid().ToString();
                                    bc.ContractItemID = Item.ID;
                                    db.Insertable<Biz_ContractItem_BizClassification>(bc).ExecuteCommand();
                                }
                            }
                            decimal AddPris = (decimal)addlist_update.Sum(x=>x.AddPri) + (decimal)addlist_insert.Sum(x => x.AddPri); //增人日比例汇总
                            decimal MinusPris = (decimal)minuslist_update.Sum(x => x.MinusPri) + (decimal)minuslist_insert.Sum(x => x.MinusPri); //减人日比例汇总

                            decimal TotalbasePris = AddPris - MinusPris;

                            decimal basePris = 1+ (TotalbasePris / 100);//增减比例

                            decimal firstTrialTotalDays = 0;//初审总人日计算(基础人日*（1+增减比例）)
                            decimal supervisionTotalDays = 0; //监督总人日
                            decimal recertificationTotalDays = 0;//再认证总人日
                            if (Item.FirstTrialBaseDays != null)
                            {
                                firstTrialTotalDays = Math.Round((decimal)Item.FirstTrialBaseDays * basePris, 2);
                                supervisionTotalDays = Math.Round((decimal)Item.SupervisionBaseDays * basePris, 2);
                                recertificationTotalDays = Math.Round((decimal)Item.RecertificationBaseDays * basePris, 2); 
                            }
                            //保存项目信息
                            {
                                var standard = db.Queryable<Abi_SysStandard>().First(m => m.ID == Item.ItemStandardID);
                                string itemno = parm.ContractNo + standard.SysStandardNo;
                                db.Updateable<Biz_ContractItem>().SetColumns(m => new Biz_ContractItem()
                                {
                                    IsSiteWork = Item.IsSiteWork,
                                    NotSiteWorkReasonID = Item.NotSiteWorkReasonID,
                                    NotSiteWorkReason = Item.NotSiteWorkReason,
                                    RiskRegisterID = Item.RiskRegisterID,
                                    RiskRegister = Item.RiskRegister,
                                    IncompatibilityClause = Item.IncompatibilityClause,
                                    CNAS = Item.CNAS,
                                    AuditScope = Item.AuditScope,
                                    AuditScopeEN = Item.AuditScopeEN,
                                    TotalBasePris = TotalbasePris,
                                    FirstTrialBaseDays = Item.FirstTrialBaseDays,
                                    SupervisionBaseDays = Item.SupervisionBaseDays,
                                    RecertificationBaseDays = Item.RecertificationBaseDays,
                                    FirstTrialTotalDays = firstTrialTotalDays,
                                    SupervisionTotalDays = supervisionTotalDays,
                                    RecertificationTotalDays = recertificationTotalDays,
                                    UpdateID = userinfo.ID,
                                    UpdateName = userinfo.UserName,
                                    UpdateTime = DateTime.Now
                                }).Where(m => m.ID == Item.ID).ExecuteCommand();
                            }
                        }
                    }

                    Core.DbContext.CommitTran();

                    return toResponse("保存成功");
                }
                catch (Exception ex)
                {
                    Core.DbContext.RollbackTran();
                    return toResponse(StatusCodeType.Error, ex.Message);
                }
            }
        }

        /// <summary>
        /// 删除合同
        /// Power = PRIV_AUDITCONTRACT_DELETE
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_AUDITCONTRACT_DELETE")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "Id 不能为空");
            }
            using (SqlSugarClient db = Core.DbContext.Current)
            {
                Core.DbContext.BeginTran();
                try
                {
                    db.Updateable<Biz_MainContract>()
                        .SetColumns(it => new Biz_MainContract() { deleted = 1 })
                        .Where(it => it.ID == id).ExecuteCommand();

                    //删除体系项目
                    db.Updateable<Biz_ContractItem>()
                        .SetColumns(it => new Biz_ContractItem() { deleted = 1 })
                        .Where(it => it.MainContractID == id).ExecuteCommand();

                    //删除分场所
                    db.Deleteable<Biz_ContractFcs>().Where(m => m.MainContractID == id).ExecuteCommand();

                    //删除文件
                    //2021-01-21 上传文件资料跟客户绑定，暂时不删除
                    //db.Deleteable<Biz_ContractFile>().Where(m => m.MainContractID == id).ExecuteCommand();

                    Core.DbContext.CommitTran();

                    return toResponse("删除成功");
                }
                catch (Exception ex)
                {
                    Core.DbContext.RollbackTran();
                    return toResponse(StatusCodeType.Error, ex.Message);
                }
            }
        }
    }
}
