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
    /// 计划审批
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlanApproveController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<PlanApproveController> _logger;
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


        public PlanApproveController(ILogger<PlanApproveController> logger, TokenManager tokenManager, IBizContractItemSubService contractitemsubService, IBizContractItemService contractitemService, IBizContractPlanService contractitemplanService, IBizContractPlanAuditorService contractplanauditorService, IBizContractPlanAuditorItemService contractplanauditoritemService)
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
        public IActionResult Query([FromBody] PlanApproveDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<view_PlanVM>();

            switch (parm.status)
            {
                case 0:
                    predicate = predicate.And(m => m.status == 30010);
                    break;
                case 1:
                    predicate = predicate.And(m => m.status == 30020);
                    break;
                case 2:
                    predicate = predicate.And(m => m.status == 39999);
                    break;
                default:
                    return toResponse(StatusCodeType.Error, "状态匹配失败,请核对！");
                    //break;
            }

            //predicate = predicate.And(m => m.status == 30000); //待派人状态

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
        /// 审批通过
        /// Power = PRIV_PLANAPPROVE_PASS
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_PLANAPPROVE_PASS")]
        public IActionResult ApprovePass(PlanApproveSubmitDto parm)
        {
            if(string.IsNullOrEmpty(parm.ID))
            {
                return toResponse(StatusCodeType.Error, "提交id不能为空，请核对");
            }
            var userinfo = _tokenManager.GetSessionInfo();
            var maincontract = _contractitemplanService.GetId(parm.ID);

            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    List<SugarParameter> parameters = new List<SugarParameter>();
                    parameters.Add(new SugarParameter("UserID", userinfo.UserID));
                    parameters.Add(new SugarParameter("UserName", userinfo.UserName));

                    parameters.Add(new SugarParameter("ApproveTime", parm.ApproveTime));//审批日期
                    parameters.Add(new SugarParameter("ApproveRemark", parm.ApproveRemark));//批准备注

                    Step.Submit(db, maincontract, "Biz_Contract_Plan", "ID", "status", "302", parameters, UpdateBizPlanAfterSubmitted, "审批通过");
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

        public static Action<SqlSugarClient, List<SugarParameter>> UpdateBizPlanAfterSubmitted = (SqlSugarClient db, List<SugarParameter> paramters) =>
        {
            if (db.Ado.ExecuteCommand(@"UPDATE Biz_Contract_Plan SET Status = @Node_To,ApproveRemark=@ApproveRemark,ApproveTime=@ApproveTime,SendSubmitID=@UserID,SendSubmitName=@UserName
WHERE ID = @Biz_Contract_Plan_ID AND Status = @Node_From", paramters) == 0)
            {
                throw new Exception(GWF.Step.DIRTY_DATA_PROMPT);
            }
        };

        /// <summary>
        /// 提交终止审核
        /// Power = PRIV_PLANAPPROVE_END
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_PLANAPPROVE_END")]
        public IActionResult ApproveEnd(PlanApproveSubmitDto parm)
        {
            if (string.IsNullOrEmpty(parm.ID))
            {
                return toResponse(StatusCodeType.Error, "提交id不能为空，请核对");
            }
            var userinfo = _tokenManager.GetSessionInfo();
            var maincontract = _contractitemplanService.GetId(parm.ID);

            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    List<SugarParameter> parameters = new List<SugarParameter>();
                    parameters.Add(new SugarParameter("UserID", userinfo.UserID));
                    parameters.Add(new SugarParameter("UserName", userinfo.UserName));

                    parameters.Add(new SugarParameter("ApproveTime", parm.ApproveTime));//审批日期
                    parameters.Add(new SugarParameter("ApproveRemark", parm.ApproveRemark));//批准备注

                    Step.Submit(db, maincontract, "Biz_Contract_Plan", "ID", "status", "309", parameters, UpdateBizPlanAfterSubmitted, "终止审核");
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
        ///撤销审批
        /// Power = PRIV_PLANAPPROVE_CANCEL
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_PLANAPPROVE_CANCEL")]
        public IActionResult Cancel(PlanApproveSubmitDto parm)
        {
            if (string.IsNullOrEmpty(parm.ID))
            {
                return toResponse(StatusCodeType.Error, "提交id不能为空，请核对");
            }
            var userinfo = _tokenManager.GetSessionInfo();
            var maincontract = _contractitemplanService.GetId(parm.ID);

            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    List<SugarParameter> parameters = new List<SugarParameter>();
                    parameters.Add(new SugarParameter("UserID", userinfo.UserID));
                    parameters.Add(new SugarParameter("UserName", userinfo.UserName));

                    Step.Cancel(db, maincontract, "Biz_Contract_Plan", "ID", "status", "302", parameters, UpdatePlanAfterCancelled, "撤销审批");
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
        /// 退回派人
        /// Power = PRIV_PLANAPPROVE_RETURN
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_PLANAPPROVE_RETURN")]
        public IActionResult Return(PlanApproveSubmitDto parm)
        {
            if (string.IsNullOrEmpty(parm.ID))
            {
                return toResponse(StatusCodeType.Error, "提交id不能为空，请核对");
            }
            var userinfo = _tokenManager.GetSessionInfo();
            var maincontract = _contractitemplanService.GetId(parm.ID);

            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    List<SugarParameter> parameters = new List<SugarParameter>();
                    parameters.Add(new SugarParameter("UserID", userinfo.UserID));
                    parameters.Add(new SugarParameter("UserName", userinfo.UserName));

                    Step.Cancel(db, maincontract, "Biz_Contract_Plan", "ID", "status", "301", parameters, UpdatePlanAfterCancelled, "审批通过");
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

        public static Action<SqlSugarClient, List<SugarParameter>> UpdatePlanAfterCancelled = (SqlSugarClient db, List<SugarParameter> paramters) =>
        {
            if (db.Ado.ExecuteCommand(@"UPDATE Biz_Contract_Plan SET Status = @Node_To,ApproveRemark=NULL,ApproveTime=NULL,SendSubmitID=NULL,SendSubmitName=NULL
WHERE ID = @Biz_Contract_Plan_ID AND Status = @Node_From", paramters) == 0)
            {
                throw new Exception(GWF.Step.DIRTY_DATA_PROMPT);
            }
        };

        /// <summary>
        /// 终止审核重新提交审批
        /// Power = PRIV_PLANAPPROVE_RESET
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_PLANAPPROVE_RESET")]
        public IActionResult ApproveReset(PlanApproveSubmitDto parm)
        {
            if (string.IsNullOrEmpty(parm.ID))
            {
                return toResponse(StatusCodeType.Error, "提交id不能为空，请核对");
            }
            var userinfo = _tokenManager.GetSessionInfo();
            var maincontract = _contractitemplanService.GetId(parm.ID);

            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    List<SugarParameter> parameters = new List<SugarParameter>();
                    parameters.Add(new SugarParameter("UserID", userinfo.UserID));
                    parameters.Add(new SugarParameter("UserName", userinfo.UserName));

                    parameters.Add(new SugarParameter("ApproveTime", null));//审批日期
                    parameters.Add(new SugarParameter("ApproveRemark", null));//批准备注

                    Step.Submit(db, maincontract, "Biz_Contract_Plan", "ID", "status", "303", parameters, UpdateBizPlanAfterSubmitted, "审批通过");
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
