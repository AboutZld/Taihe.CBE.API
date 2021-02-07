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
    /// 合同审批接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApproveContractController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<ApproveContractController> _logger;
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


        public ApproveContractController(ILogger<ApproveContractController> logger, TokenManager tokenManager, IBizMainContractService maincontractService, IufkhxxService customerService, IBizContractItemService contractitemService, IBizContractFcsService contractfcsService, IBizContractFileService contractfileService, IBizPreContractService precontractService
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
            if (parm.status == 0) //待审批
            {
                predicate = predicate.And(m => m.status == 10020);
            }
            else if (parm.status == 1) //已审批
            {
                predicate = predicate.And(m => m.status == 10030);
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
        /// 根据 Id 获取合同信息
        /// Power = PRIV_APPROVECONTRACT_DETAIL
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_APPROVECONTRACT_DETAIL")]
        public IActionResult Get(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "Id 不能为空");
            }
            return toResponse(_maincontractService.GetId(id));
        }


        /// <summary>
        /// 提交已审批
        /// Power = PRIV_APPROVECONTRACT_APPROVE
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_APPROVECONTRACT_APPROVE")]
        public IActionResult ApprovePass(ApproveContractSubmitDto param)
        {
            var userinfo = _tokenManager.GetSessionInfo();
            var maincontract = _maincontractService.GetId(param.ID);
            if (maincontract.status != 10020)
            {
                return toResponse(StatusCodeType.Error, "当前合同不在当前状态，请核对");
            }
            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    List<SugarParameter> parameters = new List<SugarParameter>();
                    parameters.Add(new SugarParameter("UserID", userinfo.UserID));
                    parameters.Add(new SugarParameter("UserName", userinfo.UserName));

                    parameters.Add(new SugarParameter("ContractApproveTime", param.ContractApproveTime));//合同审批日期
                    parameters.Add(new SugarParameter("ApproveRemark", param.ApproveRemark));//批准备注

                    Step.Submit(db, maincontract, "Biz_MainContract", "ID", "status", "103", parameters, UpdateBizEntityAfterSubmitted, "审批通过");

                    #region 生成子项目信息

                    List<Biz_ContractItem> itemList = _contractitemService.GetWhere(m => m.MainContractID == param.ID && m.deleted == 0);

                    foreach (var Item in itemList)
                    {
                        var AuditType = db.Queryable<Cfg_AuditType>().First(m => m.AuditTypeID == int.Parse(Item.AuditTypeID));
                        if ((bool)AuditType.NeedFirstStage)
                        {
                            ContractItemSubCreateDto itemsub1 = new ContractItemSubCreateDto();
                            itemsub1.MainContractID = Item.MainContractID;
                            itemsub1.ContractItemID = Item.ID;
                            itemsub1.ContractItemSubType = 0; //一阶段
                            itemsub1.ContractItemSubTypeCode = string.Format("一阶段({0})", (Item.IsSiteWork != null && (bool)Item.IsSiteWork) ? "现" : "非");

                            ContractItemSubCreateDto itemsub2 = new ContractItemSubCreateDto();
                            itemsub2.MainContractID = Item.MainContractID;
                            itemsub2.ContractItemID = Item.ID;
                            itemsub2.ContractItemSubType = 1; //二阶段
                            itemsub2.ContractItemSubTypeCode = "二阶段";

                            db.Insertable(itemsub1).AS("Biz_ContractItem_Sub").ExecuteCommand();
                            db.Insertable(itemsub2).AS("Biz_ContractItem_Sub").ExecuteCommand();

                        }
                        else
                        {
                            ContractItemSubCreateDto itemsub = new ContractItemSubCreateDto();
                            itemsub.MainContractID = Item.MainContractID;
                            itemsub.ContractItemID = Item.ID;
                            itemsub.ContractItemSubType = 3;
                            itemsub.ContractItemSubTypeCode = AuditType.AuditTypeName;

                            db.Insertable(itemsub).AS("Biz_ContractItem_Sub").ExecuteCommand();
                        }
                    }
                    #endregion
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
        /// 退回待评审
        /// Power = PRIV_APPROVECONTRACT_RETURN
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_APPROVECONTRACT_RETURN")]
        public IActionResult ReturnAudit(ApproveContractSubmitDto param)
        {
            var userinfo = _tokenManager.GetSessionInfo();
            var maincontract = _maincontractService.GetId(param.ID);
            if (maincontract.status != 10020)
            {
                return toResponse(StatusCodeType.Error, "当前合同不在当前状态，请核对");
            }
            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    List<SugarParameter> parameters = new List<SugarParameter>();
                    parameters.Add(new SugarParameter("UserID", userinfo.UserID));
                    parameters.Add(new SugarParameter("UserName", userinfo.UserName));

                    parameters.Add(new SugarParameter("ContractApproveTime", param.ContractApproveTime));//合同审批日期
                    parameters.Add(new SugarParameter("ApproveRemark", param.ApproveRemark));//批准备注

                    Step.Submit(db, maincontract, "Biz_MainContract", "ID", "status", "104", parameters, UpdateBizEntityAfterSubmitted, "退回评审");
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
        /// 合同终止
        /// Power = PRIV_APPROVECONTRACT_END
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_APPROVECONTRACT_END")]
        public IActionResult ContractEnd(ApproveContractSubmitDto param)
        {
            var userinfo = _tokenManager.GetSessionInfo();
            var maincontract = _maincontractService.GetId(param.ID);

            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    List<SugarParameter> parameters = new List<SugarParameter>();
                    parameters.Add(new SugarParameter("UserID", userinfo.UserID));
                    parameters.Add(new SugarParameter("UserName", userinfo.UserName));

                    parameters.Add(new SugarParameter("ContractApproveTime", param.ContractApproveTime));//合同审批日期
                    parameters.Add(new SugarParameter("ApproveRemark", param.ApproveRemark));//批准备注

                    Step.Submit(db, maincontract, "Biz_MainContract", "ID", "status", "105", parameters, UpdateBizEntityAfterSubmitted, "退回评审");
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
            if (db.Ado.ExecuteCommand(@"UPDATE Biz_MainContract SET Status = @Node_To,ContractApproveTime=@ContractApproveTime,ApproveRemark=@ApproveRemark,ApproveTime=getdate(),ApproveID=@UserID,ApproveName=@UserName
WHERE ID = @Biz_MainContract_ID AND Status = @Node_From", paramters) == 0)
            {
                throw new Exception(GWF.Step.DIRTY_DATA_PROMPT);
            }
        };


        /// <summary>
        /// 撤销
        /// Power = PRIV_APPROVECONTRACT_CANCEL
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_APPROVECONTRACT_CANCEL")]
        public IActionResult ApproveCancel(string id = null)
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
                    //判断时候存在已经审核安排的体系子项目
                    if (db.Ado.GetScalar("select 1 from Biz_ContractItem_Sub where MainContractID=@MainContractID and status > 20000", new { MainContractID = id }) != null)
                    {
                        return toResponse(StatusCodeType.Error, "当前合同已经有项目完成审核安排，不允许撤销");
                    }

                    List<SugarParameter> parameters = new List<SugarParameter>();
                    parameters.Add(new SugarParameter("UserID", userinfo.UserID));
                    parameters.Add(new SugarParameter("UserName", userinfo.UserName));

                    Step.Cancel(db, maincontract, "Biz_MainContract", "ID", "status", "103", parameters, UpdateBizEntityAfterCancelled, "撤销审批");

                    db.Ado.ExecuteCommand("delete from Biz_ContractItem_Sub where MainContractID=@MainContractID", new { MainContractID = id });

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

        public static Action<SqlSugarClient, List<SugarParameter>> UpdateBizEntityAfterCancelled = (SqlSugarClient db, List<SugarParameter> paramters) =>
        {
            if (db.Ado.ExecuteCommand(@"UPDATE Biz_MainContract SET Status = @Node_To,ApproveTime=NULL,ApproveID=NULL,ApproveName=NULL
WHERE ID = @Biz_MainContract_ID AND Status = @Node_From", paramters) == 0)
            {
                throw new Exception(GWF.Step.DIRTY_DATA_PROMPT);
            }
        };

        /// <summary>
        /// 根据 Id 获取合同及明细全部信息
        /// Power = PRIV_APPROVECONTRACT_DETAIL
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_APPROVECONTRACT_DETAIL")]
        public IActionResult GetDetail(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "Id 不能为空");
            }
            var userinfo = _tokenManager.GetSessionInfo();
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
      .Select((a, b) => new FcsVM { ID = a.ID, fcsID = (int)a.fcsID, fcslx = b.fcslx, fcslxmc = b.fcslxmc, fcsmc=b.fcsmc, fcsmcy = b.fcsmcy, dz = b.dz, dzy = b.dzy, lxdh = b.lxdh, cz = b.cz, lxr = b.lxr, lxrsj = b.lxrsj, fxcrs = b.fxcrs, jzbjl = b.jzbjl, znbm = b.znbm, fcshd = b.fcshd, bz = b.bz }).ToList(); //固定场所排序显示
            //获取合同附件信息
            maincotract.ContractFileList = _contractfileService.GetWhere(m => m.MainContractID == maincotract.ID);

            return toResponse(maincotract);
        }


        /// <summary>
        /// 保存合同信息
        /// Power = PRIV_APPROVECONTRACT_UPDATE
        /// </summary>
        /// <param name="parm">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_APPROVECONTRACT_UPDATE")]
        public IActionResult Update(AuditContractUpdateDto parm)
        {
            var userinfo = _tokenManager.GetSessionInfo();

            if (_maincontractService.Any(m => m.PreContractID == parm.PreContractID && m.status >= 10020))
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
                        UpdateID = userinfo.ID,
                        UpdateName = userinfo.UserName,
                        UpdateTime = DateTime.Now
                    });
                    //添加合同关联项目信息
                    List<ContractItemVM> ItemList = parm.ContractItemList;
                    if (ItemList != null)
                    {
                        foreach (var Item in ItemList)
                        {
                            //增人日依据
                            List<Biz_ContractItem_Add> addlist = Item.ContractItemAddList;
                            //删除增人日依据
                            string[] addids = addlist.Where(x => !string.IsNullOrEmpty(x.ID)).Select(x => x.ID).ToArray();
                            db.Deleteable<Biz_ContractItem_Add>().Where(m => m.ContractItemID == Item.ID && !addids.Contains(m.ID)).ExecuteCommand();
                            foreach (var add in addlist)
                            {
                                if (string.IsNullOrEmpty(add.ID))
                                {
                                    add.ID = Guid.NewGuid().ToString();
                                    add.ContractItemID = Item.ID;
                                    db.Insertable<Biz_ContractItem_Add>(add).ExecuteCommand();
                                }
                                else
                                {
                                    db.Updateable<Biz_ContractItem_Add>().SetColumns(m => new Biz_ContractItem_Add()
                                    {
                                        AddIndex = add.AddIndex,
                                        AddName = add.AddName,
                                        AddPri = add.AddPri
                                    }).Where(m => m.ID == add.ID).ExecuteCommand();
                                }
                            }
                            //减人日依据
                            List<Biz_ContractItem_Minus> minuslist = Item.ContractItemMinusList;
                            //删除减人日依据
                            string[] minusids = minuslist.Where(x => !string.IsNullOrEmpty(x.ID)).Select(x => x.ID).ToArray();
                            db.Deleteable<Biz_ContractItem_Minus>().Where(m => m.ContractItemID == Item.ID && !minusids.Contains(m.ID)).ExecuteCommand();
                            foreach (var minus in minuslist)
                            {
                                if (string.IsNullOrEmpty(minus.ID))
                                {
                                    minus.ID = Guid.NewGuid().ToString();
                                    minus.ContractItemID = Item.ID;
                                    db.Insertable<Biz_ContractItem_Minus>(minus).ExecuteCommand();
                                }
                                else
                                {
                                    db.Updateable<Biz_ContractItem_Minus>().SetColumns(m => new Biz_ContractItem_Minus()
                                    {
                                        MinusIndex = minus.MinusIndex,
                                        MinusName = minus.MinusName,
                                        MinusPri = minus.MinusPri
                                    }).Where(m => m.ID == minus.ID).ExecuteCommand();
                                }
                            }
                            //业务类别处理
                            List<ContractItemBizClassVM> classlist = Item.ContractItemBizClassificationList;
                            //删除管理业务类别
                            string[] classids = classlist.Where(x => !string.IsNullOrEmpty(x.ID)).Select(x => x.ID).ToArray();
                            db.Deleteable<Biz_ContractItem_BizClassification>().Where(m => m.ContractItemID == Item.ID && !classids.Contains(m.ID)).ExecuteCommand();
                            foreach (var cl in classlist)
                            {
                                if (string.IsNullOrEmpty(cl.ID))
                                {
                                    Biz_ContractItem_BizClassification bc = Api.Common.Helpers.ComHelper.Mapper<Biz_ContractItem_BizClassification, ContractItemBizClassVM>(cl);
                                    bc.ID = Guid.NewGuid().ToString();
                                    bc.ContractItemID = Item.ID;
                                    db.Insertable<Biz_ContractItem_BizClassification>(bc).ExecuteCommand();
                                }
                                else
                                {
                                    db.Updateable<Biz_ContractItem_BizClassification>().SetColumns(m => new Biz_ContractItem_BizClassification()
                                    {
                                        BizClassificationID = cl.BizClassificationID
                                    }).Where(m => m.ID == cl.ID).ExecuteCommand();
                                }
                            }
                            decimal AddPris = (decimal)addlist.Sum(x => x.AddPri); //增人日比例汇总
                            decimal MinusPris = (decimal)minuslist.Sum(x => x.MinusPri); //减人日比例汇总

                            decimal basePris = 1 + ((AddPris - MinusPris) / 100);//增减比例

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
                                var standard = db.Queryable<Abi_SysStandard>().First(m => m.SysStandardID == int.Parse(Item.ItemStandardID));
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

                    return toResponse(parm.ID);
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
