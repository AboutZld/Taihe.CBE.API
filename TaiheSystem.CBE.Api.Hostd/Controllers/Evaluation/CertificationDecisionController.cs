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

namespace TaiheSystem.CBE.Api.Hostd.Controllers.Evaluation
{
    /// <summary>
    /// 认证决定
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CertificationDecisionController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<CertificationDecisionController> _logger;
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
        private readonly IBizContractItemService _contractitemService;

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


        public CertificationDecisionController(ILogger<CertificationDecisionController> logger, TokenManager tokenManager, IBizContractItemSubService contractitemsubService, IBizContractItemService contractitemService, IBizContractPlanService contractitemplanService, IBizContractPlanAuditorService contractplanauditorService, IBizContractPlanAuditorItemService contractplanauditoritemService)
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
        /// 查询评定阅卷列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Query([FromBody] EvaluationDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<view_dataRecovery>();

            //状态 0-未审批 1- 已审批 
            switch (parm.status)
            {
                case 0:
                    predicate = predicate.And(m => m.ItemStatus == 20019 && m.status == 30038); 
                    break;
                case 1:
                    predicate = predicate.And(m => m.status == 30040 );
                    break;
                default:
                    return toResponse(StatusCodeType.Error, "状态匹配失败,请核对！");
                    //break;
            }


            //项目编号
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ContractItemNo), m => m.ContractItemNo.Contains(parm.ContractItemNo));
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

            var response = Core.DbContext.CurrentDB.SqlQueryable<view_dataRecovery>(@"
select cp.*,k.zzmc,h.mc,c.ContractNo,ci.AuditTypeName,ci.ItemName,ci.ContractItemNo,p.UserName,cis.status as ItemStatus
from Biz_Contract_Plan cp
inner join Biz_ContractItem_Sub cis on cis.ContractPlanID = cp.ID
inner join Biz_ContractItem ci on ci.ID = cis.ContractItemID
inner join Biz_MainContract c on c.ID = ci.MainContractID
inner join uf_khxx k on k.ID = cp.CustomerID
left join uf_hzhb h ON k.hzdwID = h.ID
outer apply(select top 1 * from Biz_Contract_PlanAuditor p 
where p.ContractPlanID = cp.ID and exists(select 1 from Biz_Contract_PlanAuditor_Item i where i.GroupIdentityName = '组长' and i.PlanAuditorID = p.ID)) p
where  cis.ContractItemSubType > 0
").ToPage<view_dataRecovery>(predicate.ToExpression(), parm);

            return toResponse(response);
        }

        /// <summary>
        /// 获取认证决定详细
        /// Power = PRIV_CERTIFICATIONDECISION_DETAIL
        /// </summary>
        /// <param name="parm">ids</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_CERTIFICATIONDECISION_DETAIL")]
        public IActionResult GetPlanDetail(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "id不允许为空！");
            }
            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Biz_Contract_Plan plan = db.Queryable<Biz_Contract_Plan>().First(m => m.ID == id);
                if (plan == null)
                {
                    return toResponse(StatusCodeType.Error, "查询不到相关项目信息，请核对！");
                }

                //任务信息
                DataRecoveryDetail plandata = Api.Common.Helpers.ComHelper.Mapper<DataRecoveryDetail, Biz_Contract_Plan>(plan);
                //客户信息列表
                plandata.CustomerInfo = db.Queryable<uf_khxx>().First(m => m.ID == plandata.CustomerID);
                //分场所
                plandata.CustomerParterList = db.Queryable<uf_fcsxx>().Where(m => m.ParentUID == plandata.CustomerID).ToList();
                //项目信息
                plandata.ContractItemSubList = db.SqlQueryable<ContractItemSubVM>(@$"select cis.*,s.SystemTypeName from Biz_ContractItem_Sub cis
inner join Biz_ContractItem ci on cis.ContractItemID = ci.ID
inner join Abi_SysStandard s on s.ID = ci.ItemStandardID where cis.ContractPlanID = '{plandata.ID}' ").ToList();

                plandata.SystemTypeNames = string.Join(",", plandata.ContractItemSubList.Select(m => m.SystemTypeName)); //体系认证类型
                plandata.AuditTypeNames = string.Join(",", plandata.ContractItemSubList.Select(m => m.ContractItemSubTypeCode)); //审核类型

                //附件信息
                plandata.ContractsubitemFileList = db.Queryable<Biz_ContractItem_Sub_File>().Where(m => m.ContractPlanID == plandata.ID).ToList();

                //评定问题列表
                plandata.Biz_Contract_Plan_EvaluationProblemList = db.Queryable<Biz_Contract_Plan_EvaluationProblem>().Where(m => m.ContractPlanID == plandata.ID).ToList();

                return toResponse(plandata);
            }
        }

        /// <summary>
        /// 保存评定信息
        /// Power = PRIV_CERTIFICATIONDECISION_SAVE
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_CERTIFICATIONDECISION_SAVE")]
        public IActionResult Save([FromBody] CertificationDecisionSaveDto parm)
        {
            var userinfo = _tokenManager.GetSessionInfo();

            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    foreach (var item in parm.ContractItemSubList)
                    {
                        db.Updateable<Biz_ContractItem_Sub>().SetColumns(m => new Biz_ContractItem_Sub()
                        {
                            CNAS = item.CNAS,
                            AuditScope = item.AuditScope,
                            GMApproveTime = item.GMApproveTime,
                            CertificatesIssue = item.CertificatesIssue,
                            NotificationIssue = item.NotificationIssue,
                            CertificationDecisionResult = item.CertificationDecisionResult,
                            CertificationDecisionRemark = item.CertificationDecisionRemark
                        }).Where(m => m.ID == item.ID).ExecuteCommand();
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
        /// 提交
        /// Power = PRIV_CERTIFICATIONDECISION_SUBMIT
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_CERTIFICATIONDECISION_SUBMIT")]
        public IActionResult SubmitItem([FromBody] CertificationDecisionSaveDto parm)
        {
            if(string.IsNullOrEmpty(parm.ID))
            {
                return toResponse(StatusCodeType.Error, "id不允许为空！");
            }
            var userinfo = _tokenManager.GetSessionInfo();
            var contractplan = _contractitemplanService.GetId(parm.ID);

            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    List<SugarParameter> parameters = new List<SugarParameter>();
                    parameters.Add(new SugarParameter("UserID", userinfo.UserID));
                    parameters.Add(new SugarParameter("UserName", userinfo.UserName));

                    

                    //提交项目至认证决定
                    foreach (var item in parm.ContractItemSubList)
                    {
                        db.Updateable<Biz_ContractItem_Sub>().SetColumns(m => new Biz_ContractItem_Sub()
                        {
                            CNAS = item.CNAS,
                            AuditScope = item.AuditScope,
                            GMApproveTime = item.GMApproveTime,
                            CertificatesIssue = item.CertificatesIssue,
                            NotificationIssue = item.NotificationIssue,
                            CertificationDecisionResult = item.CertificationDecisionResult,
                            CertificationDecisionRemark = item.CertificationDecisionRemark
                        }).Where(m => m.ID == item.ID).ExecuteCommand();

                        if (item.CertificationDecisionResult == 1)
                        {
                            //生成证书记录
                            Biz_ContractItem_Sub_Certificate cert = new Biz_ContractItem_Sub_Certificate();
                            cert.MainContractID = item.MainContractID;
                            cert.CustomerID = contractplan.CustomerID;
                            cert.ContractItemID = item.ContractItemID;
                            cert.ContractSubItemID = item.ID;

                            if(item.ContractItemSubType != 1) //项目类型不等于二阶段时
                            {

                            }

                            db.Insertable<Biz_ContractItem_Sub_Certificate>(cert).ExecuteCommand();

                            Step.Submit(db, item, "Biz_ContractItem_Sub", "ID", "status", "205", parameters, UpdateItemAfterSubmitted, "认证决定通过");
                        }
                        else if (item.CertificationDecisionResult == 0)
                        {
                            Step.Submit(db, item, "Biz_ContractItem_Sub", "ID", "status", "206", parameters, UpdateItemAfterSubmitted, "认证决定不通过");
                        }
                    }

                    Step.Submit(db, contractplan, "Biz_Contract_Plan", "ID", "status", "311", parameters, UpdatePlanAfterSubmitted, "提交认证决定");

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

        public static Action<SqlSugarClient, List<SugarParameter>> UpdateItemAfterSubmitted = (SqlSugarClient db, List<SugarParameter> paramters) =>
        {
            if (db.Ado.ExecuteCommand(@"UPDATE Biz_ContractItem_Sub SET Status = @Node_To
WHERE ID = @Biz_ContractItem_Sub_ID AND Status = @Node_From", paramters) == 0)
            {
                throw new Exception(GWF.Step.DIRTY_DATA_PROMPT);
            }
        };

        public static Action<SqlSugarClient, List<SugarParameter>> UpdatePlanAfterSubmitted = (SqlSugarClient db, List<SugarParameter> paramters) =>
        {
            if (db.Ado.ExecuteCommand(@"UPDATE Biz_Contract_Plan SET Status = @Node_To
WHERE ID = @Biz_Contract_Plan_ID AND Status = @Node_From", paramters) == 0)
            {
                throw new Exception(GWF.Step.DIRTY_DATA_PROMPT);
            }
        };
    }
}
