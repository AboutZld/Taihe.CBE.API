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
    /// 未安排计划管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlanController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<PlanController> _logger;
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


        public PlanController(ILogger<PlanController> logger, TokenManager tokenManager, IBizContractItemSubService contractitemsubService, IBizContractItemService contractitemService, IBizContractPlanService contractitemplanService)
        {
            _logger = logger;
            _tokenManager = tokenManager;
            _contractitemsubService = contractitemsubService;
            _contractitemService = contractitemService;
            _contractitemplanService = contractitemplanService;
        }


        /// <summary>
        /// 查询未安排列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Query([FromBody] PlanQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<view_MakePlan>();

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

            //var response = _contractitemsubService.GetPages(predicate.ToExpression(), parm);

            var response = Core.DbContext.CurrentDB.SqlQueryable<view_MakePlan>(@"
SELECT  m.ID,m.ContractNo,m.CustomerID,m.CreateTime, k.zzmc,k.xzqhmc, h.mc,s.ID as ContractItemSubID, s.MainContractID, s.ContractItemID,ContractItemSubType, ContractItemSubTypeCode
				   ,c.ItemName,c.ContractItemNo
FROM   Biz_ContractItem_Sub s
inner join Biz_MainContract m on m.ID = s.MainContractID
inner join Biz_ContractItem c on c.ID = s.ContractItemID
LEFT JOIN uf_khxx k ON k.ID = m.CustomerID 
LEFT JOIN uf_hzhb h ON k.hzdwID = h.ID
WHERE   m.deleted = 0 and s.status = 20000
").ToPage<view_MakePlan>(predicate.ToExpression(), parm);

            return toResponse(response);
        }


        /// <summary>
        /// 生成计划安排信息
        /// Power = PRIV_PLAN_REGISTER
        /// </summary>
        /// <param name="parm">ids</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_PLAN_REGISTER")]
        public IActionResult GetPlanDetail([FromBody] PlanInitDto parm)
        {
            string[] subitemids = parm.Ids.Split(',');
            List<Biz_ContractItem_Sub> subItemlist = _contractitemsubService.GetWhere(m => subitemids.Contains(m.ID));

            if (subItemlist.Any(m => m.status > 20000))
            {
                return toResponse(StatusCodeType.Error, "当前项目已经提交安排，请刷新列表再试！");
            }

            if (subItemlist.Any(m=>m.ContractItemSubType==0) && subItemlist.Any(m => m.ContractItemSubType == 1))
            {
                return toResponse(StatusCodeType.Error, "不允许同时安排一阶段跟二阶段项目，请核对！");
            }

            string ids = "'" + string.Join("','", subitemids) +"'";

            var customerid = Core.DbContext.Db.Ado.SqlQuery<string>(string.Format("select distinct CustomerID from Biz_MainContract where ID in (select MainContractID from Biz_ContractItem_Sub where ID in ({0}))", ids)).ToList();
            if (customerid .Count() > 1)
            {
                return toResponse(StatusCodeType.Error, "不同客户下项目不允许一起安排，请核对！");
            }

            //任务信息
            ItemPlanVM plandata = new ItemPlanVM();
            plandata.CustomerID = customerid.First().ToString();
            plandata.ContractItem_SubList = subItemlist; //添加体系项目

            //审核员信息
            List<PlanAuditorVM> planlist = new List<PlanAuditorVM>();

            PlanAuditorVM plan = new PlanAuditorVM();
            
            List<Biz_Contract_PlanAuditor_Item> AuditorItemList = new List<Biz_Contract_PlanAuditor_Item>();
            foreach (var subItem in subItemlist)
            {
                Biz_Contract_PlanAuditor_Item planauditor_item = new Biz_Contract_PlanAuditor_Item(); //审核员项目信息
                planauditor_item.ContractItemSubID = subItem.ID;
                AuditorItemList.Add(planauditor_item);
            }
            plan.PlanAuditItemList = AuditorItemList;
            planlist.Add(plan);
            plandata.PlanAuditorList = planlist;

            return toResponse(plandata);
        }

        /// <summary>
        /// 生成任务信息
        /// Power = PRIV_PLAN_CREATE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_PLAN_CREATE")]
        public IActionResult Create([FromBody] ItemPlanVM parm)
        {
            var userinfo = _tokenManager.GetSessionInfo();
            using (SqlSugarClient db = Core.DbContext.CurrentDB)
            {
                Core.DbContext.BeginTran();
                try
                {
                    List<SugarParameter> parameters = new List<SugarParameter>();
                    parameters.Add(new SugarParameter("UserID", userinfo.UserID));
                    parameters.Add(new SugarParameter("UserName", userinfo.UserName));

                    //体系项目信息
                    List<Biz_ContractItem_Sub> subitemlist = parm.ContractItem_SubList;
                    if (subitemlist.Count == 0)
                    {
                        throw new Exception("提交数据不包含体系项目信息，请核对！");
                    }
                    string IDs = string.Join(",", subitemlist.Select(m => m.ContractItemSubID));

                    if (db.Ado.GetScalar("select 1 from Biz_ContractItem_Sub where status > 20000 and ContractItemSubID in (@ids)", new { ids = IDs }) != null)
                    {
                        throw new Exception( "提交项目包含已经安排项目，请核对！");
                    }

                    //匹配任务信息
                    Biz_Contract_Plan plan = new Biz_Contract_Plan();
                    Api.Common.Helpers.ComHelper.MapperMatch<Biz_Contract_Plan, ItemPlanVM>(plan, parm,"ID","status");

                    var helper = new Common.SerialNoHelper();
                    var serialno = helper.Generate("N20"); //生成任务编号

                    plan.ContractPlanNo = serialno;

                    plan = plan.Adapt<Biz_Contract_Plan>().ToCreate(_tokenManager.GetSessionInfo());
                    int ContractPlanID = db.Insertable<Biz_Contract_Plan>(plan).ExecuteReturnIdentity();

                    //处理项目信息
                    foreach (var subitem in subitemlist)
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

                        //提交
                        Step.Submit(db, subitem, "Biz_ContractItem_Sub", "ID", "status", "201", parameters, UpdateBizEntityAfterSubmitted, "提交安排");
                    }

                    //审核员信息
                    List<PlanAuditorVM> auditorlist = parm.PlanAuditorList;
                    foreach(var auditorVM in auditorlist)
                    {
                        Biz_Contract_PlanAuditor auditor = Api.Common.Helpers.ComHelper.Mapper<Biz_Contract_PlanAuditor, PlanAuditorVM>(auditorVM);
                        auditor = auditor.Adapt<Biz_Contract_PlanAuditor>().ToCreate(_tokenManager.GetSessionInfo());
                        auditor.ContractPlanID = plan.ID;
                        db.Insertable(auditor).AS("Biz_Contract_PlanAuditor").ExecuteCommand();

                        foreach(var auditoritem in auditorVM.PlanAuditItemList)
                        {
                            auditoritem.ID = Guid.NewGuid().ToString();
                            auditoritem.PlanAuditorID = auditor.ID;
                            auditoritem.ContractPlanID = plan.ID;

                            db.Insertable(auditoritem).AS("Biz_Contract_PlanAuditor_Item").ExecuteCommand();
                        }
                    }
                    Core.DbContext.CommitTran();

                    return toResponse(plan.ID);
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
            if (db.Ado.ExecuteCommand(@"UPDATE Biz_ContractItem_Sub SET Status = @Node_To 
WHERE ID = @Biz_ContractItem_Sub_ID AND Status = @Node_From", paramters) == 0)
            {
                throw new Exception(GWF.Step.DIRTY_DATA_PROMPT);
            }
        };
    }
}
