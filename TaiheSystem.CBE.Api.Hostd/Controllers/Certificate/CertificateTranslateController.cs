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

namespace TaiheSystem.CBE.Api.Hostd.Controllers.Certificate
{
    /// <summary>
    /// 证书翻译
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CertificateTranslateController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<CertificateTranslateController> _logger;
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 项目证书信息
        /// </summary>
        private readonly IBizContractItemSubCertificateService _cretifcateService;

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


        public CertificateTranslateController(ILogger<CertificateTranslateController> logger, TokenManager tokenManager, IBizContractItemSubService contractitemsubService, IBizContractItemService contractitemService, IBizContractPlanService contractitemplanService, IBizContractPlanAuditorService contractplanauditorService, IBizContractPlanAuditorItemService contractplanauditoritemService)
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
            var predicate = Expressionable.Create<view_ItemCert>();

            //状态 0-未翻译 1-已翻译
            switch (parm.status)
            {
                case 0:
                    predicate = predicate.And(m => m.status == 50000); 
                    break;
                case 1:
                    predicate = predicate.And(m => m.status == 50010);
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

            var response = Core.DbContext.CurrentDB.SqlQueryable<view_ItemCert>(@"
select c.status,m.ContractNo,ci.ItemName,ci.ContractItemNo,cis.ContractItemSubTypeCode,k.zzmc,h.mc
,cis.CNAS
from Biz_ContractItem_Sub_Certificate c
inner join Biz_MainContract m on m.ID = c.MainContractID
inner join uf_khxx k on k.ID = c.CustomerID
inner join uf_hzhb h on h.ID = k.hzdwID
inner join Biz_ContractItem_Sub cis on cis.ID = c.ContractSubItemID
inner join Biz_ContractItem ci on c.ContractItemID = ci.ID
").ToPage<view_ItemCert>(predicate.ToExpression(), parm);

            return toResponse(response);
        }

        /// <summary>
        /// 获取评定结论详细
        /// Power = PRIV_EVALUATION_DETAIL
        /// </summary>
        /// <param name="parm">ids</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_EVALUATION_DETAIL")]
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
                return toResponse(plandata);
            }
        }

        /// <summary>
        /// 获取评定结论问题
        /// Power = PRIV_EVALUATION_PROBLEM
        /// </summary>
        /// <param name="parm">ids</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_EVALUATION_PROBLEM")]
        public IActionResult GetPlanProblem(string id = null)
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
                return toResponse(plandata);
            }
        }

        /// <summary>
        /// 提交问题整改
        /// Power = PRIV_EVALUATION_PROBLEM
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_EVALUATION_PROBLEM")]
        public IActionResult SubmitProblem(EvaluationProblemDto parm)
        {
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

                    //插入问题
                    foreach (var problem in parm.EvaluationProblemList.Where(m=>string.IsNullOrEmpty(m.ID)))
                    {
                        var probleminsert = problem.Adapt<Biz_Contract_Plan_EvaluationProblem>().ToCreate(_tokenManager.GetSessionInfo());
                        probleminsert.status = 0;
                        db.Insertable<Biz_Contract_Plan_EvaluationProblem>(probleminsert).ExecuteCommand();

                        db.Ado.ExecuteCommand("update Biz_Contract_PlanAuditor set ProblemFlag = 1 where ID = @auditorid", new { auditorid = probleminsert.PlanAuditorID});
                    }

                    List<Biz_ContractItem_Sub> subitemlist = db.Queryable<Biz_ContractItem_Sub>().Where(m => m.ContractPlanID == parm.ID).ToList();

                    //提交项目至整改状态
                    foreach (var item in subitemlist)
                    {
                        Step.Submit(db, item, "Biz_ContractItem_Sub", "ID", "status", "202", parameters, UpdateItemAfterSubmitted, "提交问题整改");
                    }

                    //提交任务至整改状态
                    Step.Submit(db, contractplan, "Biz_Contract_Plan", "ID", "status", "307", parameters, UpdatePlanAfterSubmitted, "提交问题整改");

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

        /// <summary>
        /// 关闭问题
        /// Power = PRIV_EVALUATION_CLOSEPROBLEM
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_EVALUATION_CLOSEPROBLEM")]
        public IActionResult CloseProblem(List<Biz_Contract_Plan_EvaluationProblem> parm)
        {
            var userinfo = _tokenManager.GetSessionInfo();

            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    foreach (var problem in parm)
                    {
                        db.Ado.ExecuteCommand("update Biz_Contract_Plan_EvaluationProblem set status = 1 where ID = @id", new { id = problem.ID });
                    }
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
        /// 保存评定人员信息
        /// Power = PRIV_EVALUATION_SAVEEVALUATIONNAMES
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_EVALUATION_SAVEEVALUATIONNAMES")]
        public IActionResult SaveEvaluationNames([FromBody] EvaluationSaveDto parm)
        {
            var userinfo = _tokenManager.GetSessionInfo();

            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    foreach (var problem in parm.ContractItemSubList)
                    {
                        db.Updateable<Biz_ContractItem_Sub>().SetColumns(m => new Biz_ContractItem_Sub()
                        {
                            EvaluationNames = problem.EvaluationNames
                        }).Where(m => m.ID == problem.ID).ExecuteCommand();
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
        /// 保存评定信息
        /// Power = PRIV_EVALUATION_SAVE
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_EVALUATION_SAVE")]
        public IActionResult SaveEvaluation([FromBody] EvaluationSaveDto parm)
        {
            var userinfo = _tokenManager.GetSessionInfo();

            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    foreach (var problem in parm.ContractItemSubList)
                    {
                        db.Updateable<Biz_ContractItem_Sub>().SetColumns(m => new Biz_ContractItem_Sub()
                        {
                            CNAS = problem.CNAS,
                            AuditScope = problem.AuditScope,
                            EvaluationDate = problem.EvaluationDate,
                            CertificatesIssue = problem.CertificatesIssue,
                            NotificationIssue = problem.NotificationIssue,
                            EvaluationRemark = problem.EvaluationRemark
                        }).Where(m => m.ID == problem.ID).ExecuteCommand();
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
        /// Power = PRIV_EVALUATION_SUBMIT
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_EVALUATION_SUBMIT")]
        public IActionResult SubmitItem(string id = null)
        {
            if(string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "id不允许为空！");
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

                    List<Biz_ContractItem_Sub> subitemlist = db.Queryable<Biz_ContractItem_Sub>().Where(m => m.ContractPlanID == id).ToList();

                    //提交项目至认证决定
                    foreach (var item in subitemlist)
                    {
                        Step.Submit(db, item, "Biz_ContractItem_Sub", "ID", "status", "204", parameters, UpdateItemAfterSubmitted, "提交认证决定");
                    }

                    Step.Submit(db, maincontract, "Biz_Contract_Plan", "ID", "status", "310", parameters, UpdatePlanAfterSubmitted, "提交认证决定");

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
        /// 评定未通过重新提交
        /// Power = PRIV_EVALUATION_SUBMIT
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_EVALUATION_SUBMIT")]
        public IActionResult SubmitAgain(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "id不允许为空！");
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

                    List<Biz_ContractItem_Sub> subitemlist = db.Queryable<Biz_ContractItem_Sub>().Where(m => m.ContractPlanID == id).ToList();

                    //提交未通过项目至认证决定
                    foreach (var item in subitemlist.Where(m=>m.CertificationDecisionResult ==0))
                    {
                        Step.Submit(db, item, "Biz_ContractItem_Sub", "ID", "status", "207", parameters, UpdateItemAfterSubmitted, "重新提交认证决定");
                    }

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
