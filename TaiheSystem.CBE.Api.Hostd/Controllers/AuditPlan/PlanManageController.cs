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
using System.Collections.Generic;
using TaiheSystem.CBE.Api.Model.View.AuditPlan;
using TaiheSystem.CBE.Api.Model.View.Standard;
using TaiheSystem.CBE.Api.Model.View;
using TaiheSystem.CBE.Api.GWF;

namespace TaiheSystem.CBE.Api.Hostd.Controllers.Basic
{
    /// <summary>
    /// 待派人计划
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlanManageController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<PlanManageController> _logger;
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 合同子项目接口
        /// </summary>
        private readonly IBizContractItemSubService _contractitemsubService;

        /// <summary>
        /// 项目接口
        /// </summary>
        private readonly IBizContractItemService  _contractitemService;

        /// <summary>
        /// 任务安排
        /// </summary>
        private readonly IBizContractPlanService _contractitemplanService;

        /// <summary>
        /// 审核员
        /// </summary>
        private readonly IBizContractPlanAuditorService _contractplanauditorService;

        /// <summary>
        /// 审核员项目信息
        /// </summary>
        private readonly IBizContractPlanAuditorItemService _contractplanauditoritemService;


        public PlanManageController(ILogger<PlanManageController> logger, TokenManager tokenManager, IBizContractItemSubService contractitemsubService, IBizContractItemService contractitemService, IBizContractPlanService contractitemplanService, IBizContractPlanAuditorService contractplanauditorService, IBizContractPlanAuditorItemService contractplanauditoritemService)
        {
            _logger = logger;
            _tokenManager = tokenManager;
            _contractitemsubService = contractitemsubService;
            _contractitemService = contractitemService;
            _contractitemplanService = contractitemplanService;
            _contractplanauditorService = contractplanauditorService;
            _contractplanauditoritemService = contractplanauditoritemService;
        }


        /// <summary>
        /// 查询审核安排列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Query([FromBody] PlanManageDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<view_PlanVM>();

            //switch(parm.status)
            //{
            //    case 0:
            //        predicate = predicate.And(m => m.status == 30000);
            //        break;
            //    case 1:
            //        predicate = predicate.And(m => m.status == 30010);
            //        break;
            //    case 3:
            //        predicate = predicate.And(m => m.status == 30020);
            //        break;
            //    case 4:
            //        predicate = predicate.And(m => m.status == 39999);
            //        break;
            //    default:
            //        return toResponse(StatusCodeType.Error, "状态匹配失败,请核对！");
            //        //break;
            //}

            predicate = predicate.And(m => m.status == 30000); //待派人状态

            //项目编号
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ContractItemNo), m => m.ContractItemNos.Contains(parm.ContractItemNo));
            //客户名称
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.khmc), m => m.zzmc.Contains(parm.khmc));
            //合作伙伴
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.hzhb), m => m.mc.Contains(parm.hzhb));
            //受理日期开始
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.AcceptDateStart), m => m.CreateTime >= DateTime.Parse(parm.AcceptDateStart));
            //受理日期结束
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.AcceptDateEnd), m => m.CreateTime <= DateTime.Parse(parm.AcceptDateEnd));

            //var response = _contractitemsubService.GetPages(predicate.ToExpression(), parm);

            var response = Core.DbContext.CurrentDB.SqlQueryable<view_PlanVM>(@"
SELECT  p.*, k.zzmc, h.mc,  
                   [dbo].[GetNodeName](p.status) AS StatusName, stuff
                       ((SELECT  '  ' + ItemName + ':'
                         FROM       Biz_ContractItem AS t
						 inner join Biz_ContractItem_Sub s on s.ContractItemID = t.ID
                         WHERE    s .ContractPlanID = p.ID AND t .deleted = 0 FOR xml path('')), 1, 2, '') AS StandardNos, stuff
                       ((SELECT  '  ' + ContractItemNo
                         FROM       Biz_ContractItem AS t
                         inner join Biz_ContractItem_Sub s on s.ContractItemID = t.ID
                         WHERE    s .ContractPlanID = p.ID AND t .deleted = 0 FOR xml path('')), 1, 2, '') AS ContractNos, stuff
                       ((SELECT  '  ' + AuditTypeName
                         FROM       Biz_ContractItem AS t
                         inner join Biz_ContractItem_Sub s on s.ContractItemID = t.ID
                         WHERE    s .ContractPlanID = p.ID AND t .deleted = 0 FOR xml path('')), 1, 2, '') AS AuditTypeNames
FROM      Biz_Contract_Plan p LEFT JOIN
uf_khxx k ON k.ID = p.CustomerID LEFT JOIN
uf_hzhb h ON k.hzdwID = h.ID
").ToPage<view_PlanVM>(predicate.ToExpression(), parm);

            return toResponse(response);
        }

        /// <summary>
        /// 获取计划任务信息
        /// Power = PRIV_PLANMANAGE_DETAIL
        /// </summary>
        /// <param name="parm">ids</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_PLANMANAGE_DETAIL")]
        public IActionResult GetPlanDetail(string id = null)
        {
            if(string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "id不允许为空！");
            }
            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Biz_Contract_Plan plan = _contractitemplanService.GetId(id);
                if (plan == null)
                {
                    return toResponse(StatusCodeType.Error, "id不允许为空！");
                }

                //任务信息
                ItemPlanVM plandata = Api.Common.Helpers.ComHelper.Mapper<ItemPlanVM, Biz_Contract_Plan>(plan);
                List<Biz_ContractItem_Sub> subItemList = _contractitemsubService.GetWhere(m => m.ContractPlanID == plandata.ID);

                //审核员信息
                List<Biz_Contract_PlanAuditor> planauditorlist = _contractplanauditorService.GetWhere(m => m.ContractPlanID == plandata.ID);
                List<PlanAuditorVM> planauditorviewlist = new List<PlanAuditorVM>();

                foreach(var planauditor in planauditorlist)
                {
                    PlanAuditorVM planauditorview = Api.Common.Helpers.ComHelper.Mapper<PlanAuditorVM, Biz_Contract_PlanAuditor>(planauditor);

                    planauditorview.PlanAuditItemList = _contractplanauditoritemService.GetWhere(m=>m.PlanAuditorID == planauditorview.ID);

                    planauditorviewlist.Add(planauditorview);
                }
                plandata.ContractItem_SubList = subItemList;
                plandata.PlanAuditorList = planauditorviewlist;

                return toResponse(plandata);
            }
        }

        /// <summary>
        /// 保存计划信息
        /// Power = PRIV_PLANMANAGE_UPDATE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_PLANMANAGE_UPDATE")]
        public IActionResult Update([FromBody] ItemPlanVM parm)
        {
            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                db.Ado.BeginTran();
                try
                {
                    //合同信息
                    Biz_Contract_Plan plan = Api.Common.Helpers.ComHelper.Mapper<Biz_Contract_Plan, ItemPlanVM>(parm);

                    db.Updateable<Biz_Contract_Plan>().SetColumns(m => new Biz_Contract_Plan()
                    {
                        PlanStartDate = parm.PlanStartDate,
                        PlanEndDate = parm.PlanEndDate,
                        PlanRemark = parm.PlanRemark,
                        Remark = parm.Remark,
                    }).Where(m => m.ID == parm.ID).ExecuteCommand();

                    //体系项目信息
                    List<Biz_ContractItem_Sub> subitemlist = parm.ContractItem_SubList;
                    foreach(var subitem in subitemlist)
                    {
                        //subitem.ContractPlanID = plan.ID;

                        db.Updateable<Biz_ContractItem_Sub>().SetColumns(m => new Biz_ContractItem_Sub()
                        {
                            ContractPlanID = plan.ID,
                            MultiSiteAddDays = subitem.MultiSiteAddDays,
                            AuditCombinDegree = subitem.AuditCombinDegree,
                            DeletionScale = subitem.DeletionScale,
                            PlanTotalDays = subitem.PlanTotalDays,
                            OffSiteDays = subitem.OffSiteDays,
                            FirstDays = subitem.FirstDays,
                            SecondDays = subitem.SecondDays,
                            MultiSitetTravelDays = subitem.MultiSitetTravelDays,
                            OtherAddDays = subitem.OtherAddDays,
                            TrueFirstDays = subitem.TrueFirstDays,
                            TrueSecondDays = subitem.TrueSecondDays,
                        }).Where(m => m.ID == subitem.ID).ExecuteCommand();
                    }

                    //审核员信息
                    List<PlanAuditorVM> auditorlist_insert = parm.PlanAuditorList_insert; //插入
                    List<PlanAuditorVM> auditorlist_update = parm.PlanAuditorList_update; //更新
                    List<PlanAuditorVM> auditorlist_delete = parm.PlanAuditorList_delete; //删除

                    //删除信息
                    string[] auditorids = auditorlist_delete.Select(x => x.ID).ToArray();

                    db.Deleteable<Biz_Contract_PlanAuditor_Item>().Where(m => auditorids.Contains(m.PlanAuditorID)).ExecuteCommand();//删除审核人项目
                    db.Deleteable<Biz_Contract_PlanAuditor>().Where(m => auditorids.Contains(m.ID)).ExecuteCommand();//删除审核人

                    //插入审核员信息
                    foreach (var auditorVM in auditorlist_insert)
                    {
                        Biz_Contract_PlanAuditor auditor = Api.Common.Helpers.ComHelper.Mapper<Biz_Contract_PlanAuditor, PlanAuditorVM>(auditorVM);
                        auditor = auditor.Adapt<Biz_Contract_PlanAuditor>().ToCreate(_tokenManager.GetSessionInfo());
                        auditor.ContractPlanID = parm.ID;
                        db.Insertable(auditor).AS("Biz_Contract_PlanAuditor").ExecuteCommand();

                        foreach (var auditoritem in auditorVM.PlanAuditItemList)
                        {
                            auditoritem.ID = Guid.NewGuid().ToString();
                            auditoritem.PlanAuditorID = auditor.ID;
                            auditoritem.ContractPlanID = plan.ID;

                            db.Insertable(auditoritem).AS("Biz_Contract_PlanAuditor_Item").ExecuteCommand();
                        }
                    }

                    //更新审核员信息
                    foreach (var auditorVM in auditorlist_update)
                    {
                        db.Updateable<Biz_Contract_PlanAuditor>().SetColumns(m => new Biz_Contract_PlanAuditor()
                        {
                            UserID = auditorVM.UserID,
                        }).Where(m => m.ID == auditorVM.ID).ExecuteCommand();

                        foreach (var auditoritem in auditorVM.PlanAuditItemList)
                        {
                            db.Updateable<Biz_Contract_PlanAuditor_Item>().SetColumns(m => new Biz_Contract_PlanAuditor_Item()
                            {
                                GroupIdentityID = auditoritem.GroupIdentityID,
                                GroupIdentityName = auditoritem.GroupIdentityName,
                                WitnessTypeID = auditoritem.WitnessTypeID,
                                WitnessTypeName = auditoritem.WitnessTypeName,
                                WitnessTypeUserName = auditoritem.WitnessTypeUserName,
                                GroupCode = auditoritem.GroupCode,
                                ProfessionCode = auditoritem.ProfessionCode,
                            }).Where(m => m.ID == auditoritem.ID).ExecuteCommand();
                        }
                    }
                    db.Ado.CommitTran();

                    return toResponse(plan.ID);
                }
                catch (Exception ex)
                {
                    db.Ado.RollbackTran();
                    return toResponse(StatusCodeType.Error, ex.Message);
                }
            }
        }

        /// <summary>
        /// 提交派人
        /// Power = PRIV_PLANMANAGE_SEND
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_PLANMANAGE_SEND")]
        public IActionResult SubmitSend(string id = null)
        {
            if(string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "提交id不能为空，请核对");
            }
            var userinfo = _tokenManager.GetSessionInfo();
            var maincontract = _contractitemplanService.GetId(id);

            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    List<SugarParameter> parameters = new List<SugarParameter>();
                    parameters.Add(new SugarParameter("UserID", userinfo.UserID));
                    parameters.Add(new SugarParameter("UserName", userinfo.UserName));

                    Step.Submit(db, maincontract, "Biz_Contract_Plan", "ID", "status", "301", parameters, UpdatePlanAfterSubmitted, "终止审核");
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

        public static Action<SqlSugarClient, List<SugarParameter>> UpdatePlanAfterSubmitted = (SqlSugarClient db, List<SugarParameter> paramters) =>
        {
            if (db.Ado.ExecuteCommand(@"UPDATE Biz_Contract_Plan SET Status = @Node_To,SendSubmitTime=getdate(),SendSubmitID=@UserID,SendSubmitName=@UserName
WHERE ID = @Biz_Contract_Plan_ID AND Status = @Node_From", paramters) == 0)
            {
                throw new Exception(GWF.Step.DIRTY_DATA_PROMPT);
            }
        };

        /// <summary>
        /// 删除安排任务
        /// Power=PRIV_PLANMANAGE_DELETE
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorization(Power = "PRIV_PLANMANAGE_DELETE")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "删除 Id 不能为空");
            }
            var userinfo = _tokenManager.GetSessionInfo();
            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    var contract = _contractitemplanService.GetId(id);
                    db.Deleteable<Biz_Contract_Plan>()
                        .Where(it => it.ID == id).ExecuteCommand();

                    //删除任务子项目信息
                    List<Biz_ContractItem_Sub> subitmlist = db.Queryable<Biz_ContractItem_Sub>().Where(it => it.ContractPlanID == contract.ID).ToList();
                    foreach(var subitem in subitmlist)
                    {
                        List<SugarParameter> parameters = new List<SugarParameter>();
                        parameters.Add(new SugarParameter("UserID", userinfo.UserID));
                        parameters.Add(new SugarParameter("UserName", userinfo.UserName));

                        Step.Cancel(db, subitem, "Biz_ContractItem_Sub", "ID", "status", "201", parameters, UpdateBizEntityAfterCancelled, "撤销安排");
                    }

                    //删除审核员
                    db.Deleteable<Biz_Contract_PlanAuditor>().Where(m => m.ContractPlanID == contract.ID).ExecuteCommand();

                    //删除审核员详细信息
                    db.Deleteable<Biz_Contract_PlanAuditor_Item>().Where(m => m.ContractPlanID == contract.ID).ExecuteCommand();

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

        public static Action<SqlSugarClient, List<SugarParameter>> UpdateBizEntityAfterCancelled = (SqlSugarClient db, List<SugarParameter> paramters) =>
        {
            if (db.Ado.ExecuteCommand(@"UPDATE Biz_ContractItem_Sub SET Status = @Node_To,ContractPlanID=NULL
WHERE ID = @Biz_ContractItem_Sub_ID AND Status = @Node_From", paramters) == 0)
            {
                throw new Exception(GWF.Step.DIRTY_DATA_PROMPT);
            }
        };
    }
}
