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

namespace TaiheSystem.CBE.Api.Hostd.Controllers.Main
{
    /// <summary>
    /// 合同登记
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainContractController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<MainContractController> _logger;
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


        public MainContractController(ILogger<MainContractController> logger, TokenManager tokenManager, IBizMainContractService maincontractService, IufkhxxService customerService, IBizContractItemService contractitemService, IBizContractFcsService contractfcsService, IBizContractFileService contractfileService, IBizPreContractService precontractService
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
            if(parm.status == 0) //已添加
            {
                predicate = predicate.And(m => m.status == 10000);
            }
            else if(parm.status == 1) //待评审
            {
                predicate = predicate.And(m => m.status == 10010);
            }
            else if (parm.status == 2) //待审批
            {
                predicate = predicate.And(m => m.status == 10020);
            }
            else if (parm.status == 3) //已审批
            {
                predicate = predicate.And(m => m.status == 10015);
            }
            else if (parm.status == 4) //退回
            {
                predicate = predicate.And(m => m.status == 10015);
            }
            else if (parm.status == 5) //终止
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
        /// 申请受理合同信息
        /// Power = PRIV_CONTRACT_APPLY
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_CONTRACT_APPLY")]
        [Authorization]
        public IActionResult Apply([FromBody] MainContractCreateDto parm)
        {
            var userinfo = _tokenManager.GetSessionInfo();
            if (!_precontractService.Any(m => m.ID == parm.PreContractID && m.Status == 0))
            {
                return toResponse(StatusCodeType.Error, $"不存在当前未登记OA合同，请核对！");
            }
            if (_maincontractService.Any(m => m.PreContractID == parm.PreContractID && m.deleted == 0))
            {
                using (SqlSugarClient db = Core.DbContext.Current)
                {
                    //更新状态为已登记
                    var i = db.Updateable<Biz_PreContract>()
                        .SetColumns(it => new Biz_PreContract() { Status = 1, RegisterTime = DateTime.Now, RegisterID = userinfo.ID, RegisterName = userinfo.UserName })
                        .Where(it => it.ID == parm.PreContractID).ExecuteCommand();
                }
                return toResponse(StatusCodeType.Error, $"当前合同已经受理，不能重复！");
            }


            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    //从 Dto 映射到 实体
                    var options = parm.Adapt<Biz_MainContract>().ToCreate(_tokenManager.GetSessionInfo());
                    options.status = 10000;
                    if (db.Insertable<Biz_MainContract>(options).ExecuteCommand() == 0)
                    {
                        return toResponse(StatusCodeType.Error, "受理失败，不能重复！");
                    }
                    //添加合同关联项目信息
                    List<ContractItemVM> ItemList = parm.ContractItemList_insert;
                    if (ItemList != null)
                    {
                        foreach (var Item in ItemList)
                        {
                            Biz_ContractItem contractitem = new Biz_ContractItem();
                            contractitem.ID = Guid.NewGuid().ToString();
                            contractitem.MainContractID = options.ID;
                            contractitem.ItemStandardID = Item.ItemStandardID;  //标准ID
                            contractitem.ItemStandardCode = Item.ItemStandardCode;//标准code
                            contractitem.ItemName = Item.ItemName;              //标准名称
                            contractitem.AuditTypeID = Item.AuditTypeID;        //审核类型
                            contractitem.AuditTypeName = Item.AuditTypeName;              //审核类型名称
                            contractitem.PropleNum = Item.PropleNum;            //体系人数
                            contractitem.ApplyRange = Item.ApplyRange;          //申请范围
                            contractitem.ApplyRangeEN = Item.ApplyRangeEN;          //申请范围英文
                            contractitem.OrganizationIn = Item.OrganizationIn;          //是否机构转入
                            contractitem.ReevaluateNum = Item.ReevaluateNum;
                            contractitem.CreateTime = DateTime.Now;
                            contractitem.CreateID = userinfo.ID;
                            contractitem.CreateName = userinfo.UserName;
                            contractitem.deleted = 0;

                            var standard = db.Queryable<Abi_SysStandard>().First(m => m.SysStandardID == int.Parse(Item.ItemStandardID));
                            contractitem.ContractItemNo = options.ContractNo + standard.SysStandardNo;

                            db.Insertable<Biz_ContractItem>(contractitem).ExecuteCommand();
                        }
                    }
                    //添加合同多场所信息
                    List<FcsVM> FcsList = parm.ContractfcsList_insert;
                    if (FcsList != null)
                    {
                        foreach (var fcs in FcsList)
                        {
                            Biz_ContractFcs contractfcs = new Biz_ContractFcs();
                            contractfcs.ID = Guid.NewGuid().ToString();
                            contractfcs.fcsID = fcs.fcsID;
                            contractfcs.MainContractID = options.ID;
                            contractfcs.CustomerID = options.CustomerID;
                            contractfcs.CreateTime = DateTime.Now;
                            contractfcs.CreateID = userinfo.ID;
                            contractfcs.CreateName = userinfo.UserName;

                            db.Insertable<Biz_ContractFcs>(contractfcs).ExecuteCommand();
                        }
                    }
                    //评审附件
                    List<FileVM> FileList = parm.ContractFileList_insert;
                    if (FileList != null)
                    {
                        foreach (var file in FileList)
                        {
                            Biz_ContractFile contractfile = new Biz_ContractFile();
                            contractfile.ID = Guid.NewGuid().ToString();
                            contractfile.FileID = file.FileID;
                            contractfile.FileName = file.FileName;
                            contractfile.Name = file.FileName;
                            contractfile.MainContractID = options.ID;
                            contractfile.CustomerID = options.CustomerID;
                            contractfile.CreateTime = DateTime.Now;
                            contractfile.CreateID = userinfo.ID;
                            contractfile.CreateName = userinfo.UserName;

                            db.Insertable<Biz_ContractFile>(contractfile).ExecuteCommand();
                        }
                    }
                    //更新状态为已登记
                    var i = db.Updateable<Biz_PreContract>()
                        .SetColumns(it => new Biz_PreContract() { Status = 1, RegisterTime = DateTime.Now , RegisterID = userinfo.ID, RegisterName =userinfo.UserName})
                        .Where(it => it.ID == options.PreContractID).ExecuteCommand();

                    Core.DbContext.CommitTran();

                    return toResponse(options.ID);
                }
                catch(Exception ex)
                {
                    Core.DbContext.RollbackTran();
                    return toResponse(StatusCodeType.Error, ex.Message);
                }
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
        /// 提交评审
        /// Power = PRIV_CONTRACT_SUBMIT
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_CONTRACT_SUBMIT")]
        public IActionResult SubmitAudit(string id = null)
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
                    Step.Submit(db, maincontract, "Biz_MainContract", "ID", "status", "101", parameters, UpdateBizEntityAfterSubmitted, "提交评审");
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
            if (db.Ado.ExecuteCommand(@"UPDATE Biz_MainContract SET Status = @Node_To,SubmitAuditTime=getdate(),SubmitAuditID=@UserID,SubmitAuditName=@UserName
WHERE ID = @Biz_MainContract_ID AND Status = @Node_From", paramters) == 0)
            {
                throw new Exception(GWF.Step.DIRTY_DATA_PROMPT);
            }
        };

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
            var contract = _maincontractService.GetId(id);
            if(contract == null || contract.deleted == 1)
            {
                return toResponse(StatusCodeType.Error, "当前合同信息不存在");
            }

            MainContractVM maincotract = Api.Common.Helpers.ComHelper.Mapper<MainContractVM,Biz_MainContract>(contract);

            //获取客户信息
            maincotract.ContractCustomerInfo = _customerService.GetFirst(m=>m.khxxID == maincotract.CustomerID);
            //获取合同体系项目
            maincotract.ContractItemList = _contractitemService.GetWhere(m=>m.MainContractID == maincotract.ID && m.deleted == 0);
            //获取合同分场所信息
            maincotract.ContractfcsList = Core.DbContext.Db.Queryable<Biz_ContractFcs, uf_fcsxx>((a, b) => new object[] {
        JoinType.Left,a.fcsID == b.fcsID}).OrderBy((a,b) => b.fcslx,OrderByType.Desc).Where((a, b) =>a.MainContractID == maincotract.ID)
      .Select((a, b) => new FcsVM { ID = a.ID, fcsID = (int)a.fcsID,fcslx = b.fcslx, fcslxmc=b.fcslxmc, fcsmc=b.fcsmc, fcsmcy=b.fcsmcy, dz=b.dz, dzy=b.dzy, lxdh=b.lxdh, cz=b.cz, lxr=b.lxr, lxrsj=b.lxrsj, fxcrs=b.fxcrs, jzbjl=b.jzbjl, znbm=b.znbm, fcshd=b.fcshd, bz=b.bz }).ToList(); //按类型排序显示
            //获取合同附件信息
            maincotract.ContractFileList = _contractfileService.GetWhere(m=>m.MainContractID == maincotract.ID);

            return toResponse(maincotract);
        }


        /// <summary>
        /// 更新合同信息
        /// Power = PRIV_CONTRACT_UPDATE
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_CONTRACT_UPDATE")]
        public IActionResult Update(MainContractUpdateDto parm)
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
                    //从 Dto 映射到 实体
                    _maincontractService.Update(m => m.ID == parm.ID, m => new Biz_MainContract()
                    {
                        ConcernExtent = parm.ConcernExtent,
                        ConcernExtentName = parm.ConcernExtentName,
                        AuditPlanDate = parm.AuditPlanDate,
                        JudgeReuqest = parm.JudgeReuqest,
                        AuditPlanRequest = parm.AuditPlanRequest,
                        AuditGroupRequest = parm.AuditGroupRequest,
                        DecisionRequest = parm.DecisionRequest,
                        Remark = parm.Remark,
                        UpdateID = userinfo.ID,
                        UpdateName=userinfo.UserName,
                        UpdateTime = DateTime.Now
                    });
                    //添加合同关联项目信息
                    List<ContractItemVM> ItemList_update = parm.ContractItemList_update;//更新
                    List<ContractItemVM> ItemList_insert = parm.ContractItemList_insert;//插入
                    List<ContractItemVM> ItemList_delete = parm.ContractItemList_delete;//删除

                    //删除合同体系项目
                    string[] ids = ItemList_delete.Where(x => !string.IsNullOrEmpty(x.ID)).Select(x => x.ID).ToArray();
                    db.Updateable<Biz_ContractItem>().SetColumns(m=> new Biz_ContractItem { deleted = 1 }).Where(m => m.MainContractID == parm.ID && ids.Contains(m.ID)).ExecuteCommand();
                    if (ItemList_update != null) //更新列表
                    {
                        foreach (var Item in ItemList_update)
                        {
                            var standard = db.Queryable<Abi_SysStandard>().First(m => m.SysStandardID == int.Parse(Item.ItemStandardID));
                            string itemno = parm.ContractNo + standard.SysStandardNo;
                            db.Updateable<Biz_ContractItem>().SetColumns(m => new Biz_ContractItem()
                            {
                                ItemStandardID = Item.ItemStandardID,
                                ItemName = Item.ItemName,
                                AuditTypeID = Item.AuditTypeID,
                                AuditTypeName = Item.AuditTypeName,
                                PropleNum = Item.PropleNum,
                                ApplyRange = Item.ApplyRange,
                                ApplyRangeEN = Item.ApplyRangeEN,
                                OrganizationIn = Item.OrganizationIn,
                                ReevaluateNum = Item.ReevaluateNum,
                                UpdateID = userinfo.ID,
                                UpdateName = userinfo.UserName,
                                UpdateTime = DateTime.Now
                            }).Where(m => m.ID == parm.ID).ExecuteCommand();
                        }
                    }
                    if(ItemList_insert != null) //插入新数据
                    {
                        foreach(var Item in ItemList_insert)
                        {
                            Biz_ContractItem contractitem = new Biz_ContractItem();
                            contractitem.ID = Guid.NewGuid().ToString();
                            contractitem.MainContractID = parm.ID;
                            contractitem.ItemStandardID = Item.ItemStandardID;  //标准ID
                            contractitem.ItemStandardCode = Item.ItemStandardCode;//标准code
                            contractitem.ItemName = Item.ItemName;              //标准名称
                            contractitem.AuditTypeID = Item.AuditTypeID;        //审核类型
                            contractitem.AuditTypeName = Item.ItemName;              //审核类型名称
                            contractitem.PropleNum = Item.PropleNum;            //体系人数
                            contractitem.ApplyRange = Item.ApplyRange;          //申请范围
                            contractitem.ApplyRangeEN = Item.ApplyRangeEN;          //申请范围英文
                            contractitem.OrganizationIn = Item.OrganizationIn;          //是否机构转入
                            contractitem.ReevaluateNum = Item.ReevaluateNum; //复评次数
                            contractitem.CreateTime = DateTime.Now;
                            contractitem.CreateID = userinfo.ID;
                            contractitem.CreateName = userinfo.UserName;
                            contractitem.deleted = 0;

                            var standard = db.Queryable<Abi_SysStandard>().First(m => m.SysStandardID == int.Parse(Item.ItemStandardID));
                            contractitem.ContractItemNo = parm.ContractNo + standard.SysStandardNo;

                            db.Insertable<Biz_ContractItem>(contractitem).ExecuteCommand();
                        }
                    }
                    //添加合同多场所信息
                    List<FcsVM> FcsList_update = parm.ContractfcsList_update;//更新
                    List<FcsVM> FcsList_insert = parm.ContractfcsList_insert;//插入
                    List<FcsVM> FcsList_delete = parm.ContractfcsList_delete;//删除
                    //删除合同体系项目
                    string[] fcsids = FcsList_delete.Where(x => !string.IsNullOrEmpty(x.ID)).Select(x => x.ID).ToArray();
                    db.Deleteable<Biz_ContractFcs>().Where(m => m.MainContractID == parm.ID && fcsids.Contains(m.ID)).ExecuteCommand();
                    if (FcsList_update != null) //更新数据
                    {
                        foreach (var fcs in FcsList_update)
                        {
                            db.Updateable<Biz_ContractFcs>().SetColumns(m => new Biz_ContractFcs()
                            { 
                                fcsID = fcs.fcsID
                            }).Where(m => m.ID == parm.ID).ExecuteCommand();
                        }
                    }
                    if(FcsList_insert != null)
                    {
                        foreach(var fcs in FcsList_insert)
                        {
                            Biz_ContractFcs contractfcs = new Biz_ContractFcs();
                            contractfcs.ID = Guid.NewGuid().ToString();
                            contractfcs.fcsID = fcs.fcsID;
                            contractfcs.MainContractID = parm.ID;
                            contractfcs.CustomerID = parm.CustomerID;
                            contractfcs.CreateTime = DateTime.Now;
                            contractfcs.CreateID = userinfo.ID;
                            contractfcs.CreateName = userinfo.UserName;

                            db.Insertable<Biz_ContractFcs>(contractfcs).ExecuteCommand();
                        }
                    }
                    //评审附件
                    List<FileVM> FileList_update = parm.ContractFileList_update;//更新
                    List<FileVM> FileList_insert = parm.ContractFileList_insert;//插入
                    List<FileVM> FileList_delete = parm.ContractFileList_delete;//删除
                    //删除合同体系项目
                    string[] fileids = FileList_delete.Where(x => !string.IsNullOrEmpty(x.ID)).Select(x => x.ID).ToArray();
                    db.Deleteable<Biz_ContractFile>().Where(m => m.MainContractID == parm.ID && fileids.Contains(m.ID)).ExecuteCommand();
                    if (FileList_update != null)//更新
                    {
                        foreach (var file in FileList_update)
                        {
                            db.Updateable<Biz_ContractFile>().SetColumns(m => new Biz_ContractFile()
                            {
                                FileID = file.FileID,
                                FileName = file.FileName,
                                Name = file.FileName,
                            }).Where(m => m.ID == parm.ID).ExecuteCommand();
                        }
                    }
                    if(FileList_insert !=null) //插入
                    {
                        foreach (var file in FileList_insert)
                        {
                            Biz_ContractFile contractfile = new Biz_ContractFile();
                            contractfile.ID = Guid.NewGuid().ToString();
                            contractfile.FileID = file.FileID;
                            contractfile.FileName = file.FileName;
                            contractfile.Name = file.FileName;
                            contractfile.MainContractID = parm.ID;
                            contractfile.CustomerID = parm.CustomerID;
                            contractfile.CreateTime = DateTime.Now;
                            contractfile.CreateID = userinfo.ID;
                            contractfile.CreateName = userinfo.UserName;

                            db.Insertable<Biz_ContractFile>(contractfile).ExecuteCommand();
                        }
                    }
                    //更新状态为已登记
                    var i = db.Updateable<Biz_MainContract>()
                        .SetColumns(it => new Biz_MainContract() { UpdateTime = DateTime.Now, UpdateID = userinfo.ID, UpdateName = userinfo.UserName })
                        .Where(it => it.ID == parm.ID).ExecuteCommand();

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

        /// <summary>
        /// 删除合同
        /// Power = PRIV_CONTRACT_DELETE
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_CONTRACT_DELETE")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "Id 不能为空");
            }
            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    var contract = _maincontractService.GetId(id);
                    db.Updateable<Biz_MainContract>()
                        .SetColumns(it => new Biz_MainContract() { deleted = 1 })
                        .Where(it => it.ID == id).ExecuteCommand();

                    //删除体系项目
                    db.Updateable<Biz_ContractItem>()
                        .SetColumns(it => new Biz_ContractItem() { deleted = 1 })
                        .Where(it => it.MainContractID == id).ExecuteCommand();

                    //删除分场所
                    db.Deleteable<Biz_ContractFcs>().Where(m => m.MainContractID == id).ExecuteCommand();

                    //重置OA合同状态
                    db.Updateable<Biz_PreContract>()
                        .SetColumns(it => new Biz_PreContract() { Status = 0 })
                        .Where(it => it.ID == contract.PreContractID).ExecuteCommand();

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
