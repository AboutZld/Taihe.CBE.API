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
    /// 审核员计划确认
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuditorPlanConfirmController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<AuditorPlanConfirmController> _logger;
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


        public AuditorPlanConfirmController(ILogger<AuditorPlanConfirmController> logger, TokenManager tokenManager, IBizContractItemSubService contractitemsubService, IBizContractItemService contractitemService, IBizContractPlanService contractitemplanService, IBizContractPlanAuditorService contractplanauditorService, IBizContractPlanAuditorItemService contractplanauditoritemService)
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
        public IActionResult Query([FromBody] AuditorPlanConfirmDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<view_AuditorItem>();

            switch (parm.status)
            {
                case 0:
                    predicate = predicate.And(m => m.status == 40000);
                    break;
                case 1:
                    predicate = predicate.And(m => m.status == 40010);
                    break;
                case 3:
                    predicate = predicate.And(m => m.status == 40005);
                    break;
                case 4:
                    predicate = predicate.And(m => m.status == 40020);
                    break;
                default:
                    return toResponse(StatusCodeType.Error, "状态匹配失败,请核对！");
                    //break;
            }


            //项目编号
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ContractItemNo), m => m.ContractItemNos.Contains(parm.ContractItemNo));
            //客户名称
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.khmc), m => m.zzmc.Contains(parm.khmc));
            //审核开始起
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.AcceptStartDateStart), m => m.PlanStartDate >= DateTime.Parse(parm.AcceptStartDateStart));
            //审核开始止
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.AcceptStartDateEnd), m => m.PlanStartDate <= DateTime.Parse(parm.AcceptStartDateEnd));

            //审核结束起
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.AcceptEndDateStart), m => m.PlanEndDate >= DateTime.Parse(parm.AcceptEndDateStart));
            //审核结束止
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.AcceptEndDateEnd), m => m.PlanEndDate <= DateTime.Parse(parm.AcceptEndDateEnd));

            //var response = _contractitemsubService.GetPages(predicate.ToExpression(), parm);

            var response = Core.DbContext.CurrentDB.SqlQueryable<view_AuditorItem>(@"
select cpa.*,cp.PlanStartDate,cp.PlanEndDate,k.zzmc, stuff
                       ((SELECT  '  ' + ItemName 
                         FROM       Biz_ContractItem AS t
						 inner join Biz_ContractItem_Sub s on s.ContractItemID = t.ID
                         WHERE    s .ContractPlanID = cp.ID AND t .deleted = 0 FOR xml path('')), 1, 2, '') AS StandardNos, stuff
                       ((SELECT  '  ' + ContractItemNo
                         FROM       Biz_ContractItem AS t
                         inner join Biz_ContractItem_Sub s on s.ContractItemID = t.ID
                         WHERE    s .ContractPlanID = cp.ID AND t .deleted = 0 FOR xml path('')), 1, 2, '') AS ContractItemNos, stuff
                       ((SELECT  '  ' + AuditTypeName
                         FROM       Biz_ContractItem AS t
                         inner join Biz_ContractItem_Sub s on s.ContractItemID = t.ID
                         WHERE    s .ContractPlanID = cp.ID AND t .deleted = 0 FOR xml path('')), 1, 2, '') AS AuditTypeNames
from Biz_Contract_PlanAuditor cpa 
inner join Biz_Contract_Plan cp on cp.ID = cpa.ContractPlanID
inner join uf_khxx k on k.ID = cp.CustomerID
").ToPage<view_AuditorItem>(predicate.ToExpression(), parm);

            return toResponse(response);
        }

        /// <summary>
        /// 获取编制计划详情
        /// Power = PRIV_PLANDRAW_DETAIL
        /// </summary>
        /// <param name="parm">ids</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_PLANDRAW_DETAIL")]
        public IActionResult GetPlanDetail(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "id不允许为空！");
            }
            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Biz_Contract_PlanAuditor plan = db.Queryable<Biz_Contract_PlanAuditor>().First(m => m.ID == id);
                if (plan == null)
                {
                    return toResponse(StatusCodeType.Error, "查询不到相关项目信息，请核对！");
                }

                //已编制的任务信息
                AuditorDrawUpdateDto plandata = Api.Common.Helpers.ComHelper.Mapper<AuditorDrawUpdateDto, Biz_Contract_PlanAuditor>(plan);
                //审核员项目列表
                plandata.ContractsubitemList = db.Queryable<Biz_Contract_PlanAuditor_Item>().Where(m => m.PlanAuditorID == plandata.ID).ToList();
                //编制计划列表信息
                plandata.PlanAuditorDrawList = db.Queryable<Biz_Contract_PlanAuditor_Draw>().Where(m => m.PlanAuditorID == plandata.ID).ToList();
                //附件信息
                plandata.ContractsubitemFileList = db.Queryable<Biz_ContractItem_Sub_File>().Where(m => m.PlanAuditorID == plandata.ID).ToList();
                return toResponse(plandata);
            }
        }

        /// <summary>
        /// 提交确认
        /// Power = PRIV_AUDITORPLAN_CONFIRM
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_AUDITORPLAN_CONFIRM")]
        public IActionResult SubmitConfirm(AuditorConfrmSubmitDto parm)
        {
            var userinfo = _tokenManager.GetSessionInfo();
            var maincontract = _contractplanauditorService.GetId(parm.id);

            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    List<SugarParameter> parameters = new List<SugarParameter>();
                    parameters.Add(new SugarParameter("UserID", userinfo.UserID));
                    parameters.Add(new SugarParameter("UserName", userinfo.UserName));
                    parameters.Add(new SugarParameter("PlanRemark", parm.PlanRemark));

                    Step.Submit(db, maincontract, "Biz_Contract_PlanAuditor", "ID", "status", "402", parameters, UpdatePlanAfterSubmitted, "提交确认");
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
            if (db.Ado.ExecuteCommand(@"UPDATE Biz_Contract_PlanAuditor SET Status = @Node_To,PlanRemark=@PlanRemark
WHERE ID = @Biz_Contract_PlanAuditor_ID AND Status = @Node_From", paramters) == 0)
            {
                throw new Exception(GWF.Step.DIRTY_DATA_PROMPT);
            }
        };

        /// <summary>
        /// 已编制退回调整
        /// Power = PRIV_AUDITORPLAN_RETURN
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_AUDITORPLAN_RETURN")]
        public IActionResult SubmitReturn(AuditorConfrmSubmitDto parm)
        {
            var userinfo = _tokenManager.GetSessionInfo();
            var maincontract = _contractplanauditorService.GetId(parm.id);

            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    List<SugarParameter> parameters = new List<SugarParameter>();
                    parameters.Add(new SugarParameter("UserID", userinfo.UserID));
                    parameters.Add(new SugarParameter("UserName", userinfo.UserName));
                    parameters.Add(new SugarParameter("PlanRemark", parm.PlanRemark));

                    Step.Submit(db, maincontract, "Biz_Contract_PlanAuditor", "ID", "status", "403", parameters, UpdatePlanAfterSubmitted, "退回调整");
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
    }
}
