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

namespace TaiheSystem.CBE.Api.Hostd.Controllers.Auditor
{
    /// <summary>
    /// 审核员任务管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuditorPlanController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<AuditorPlanController> _logger;
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


        public AuditorPlanController(ILogger<AuditorPlanController> logger, TokenManager tokenManager, IBizContractItemSubService contractitemsubService, IBizContractItemService contractitemService, IBizContractPlanService contractitemplanService)
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
        public IActionResult Query([FromBody] AuditorPlanDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<view_AuditorPlanVM>();

            switch (parm.status)
            {
                case 0:
                    predicate = predicate.And(m => m.status == 40000);
                    break;
                case 1:
                    predicate = predicate.And(m => m.status == 40010);
                    break;
                case 2:
                    predicate = predicate.And(m => m.status == 40020);
                    break;
                case 3:
                    predicate = predicate.And(m => m.status == 40015);
                    break;
                case 4:
                    predicate = predicate.And(m => m.status == 40013);
                    break;
                default:
                    return toResponse(StatusCodeType.Error, "状态匹配失败,请核对！");
                    //break;
            }

            //合同编号
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ContractItemNo), m => m.ContractItemNo.Contains(parm.ContractItemNo));
            //客户名称
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.khmc), m => m.zzmc.Contains(parm.khmc));
            //合作伙伴
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.hzhb), m => m.mc.Contains(parm.hzhb));
            //受理日期开始
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.AcceptDateStart), m => m.AcceptTime >= DateTime.Parse(parm.AcceptDateStart));
            //受理日期结束
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.AcceptDateEnd), m => m.AcceptTime <= DateTime.Parse(parm.AcceptDateEnd));

            //var response = _contractitemsubService.GetPages(predicate.ToExpression(), parm);

            var response = Core.DbContext.CurrentDB.SqlQueryable<view_AuditorPlanVM>(@"
SELECT  m.ContractNo, m.CustomerID, m.CreateTime as AcceptTime, k.zzmc, k.xzqhmc, h.mc, s.MainContractID, 
                   s.ContractItemID, s.ContractItemSubType, s.ContractItemSubTypeCode, c.ItemName, c.ContractItemNo,p.ContractPlanNo,
				   p.PlanStartDate,p.PlanEndDate,p.ApproveTime,
				   dbo.GetNodeName(pai.status) as ItemStatusName,dbo.GetNodeName(p.status) planStatusName
				   , CASE WHEN ConcernExtentName = '加急发证' THEN 1 ELSE 0 END UrgentFlag,pai.*
FROM      Biz_Contract_PlanAuditor pa 
inner join Biz_Contract_PlanAuditor_Item pai on pa.ID = pai.PlanAuditorID
inner join  dbo.Biz_ContractItem_Sub s on s.ID = pai.ContractItemSubID
inner join Biz_Contract_Plan p on s.ContractPlanID = p.ID
INNER JOIN
                   dbo.Biz_MainContract AS m ON m.ID = s.MainContractID INNER JOIN
                   dbo.Biz_ContractItem AS c ON c.ID = s.ContractItemID LEFT OUTER JOIN
                   dbo.uf_khxx AS k ON k.ID = m.CustomerID LEFT OUTER JOIN
                   dbo.uf_hzhb AS h ON k.hzdwID = h.ID
WHERE   (m.deleted = 0)
").ToPage<view_AuditorPlanVM>(predicate.ToExpression(), parm);

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
                Biz_Contract_PlanAuditor_Item plan = db.Queryable<Biz_Contract_PlanAuditor_Item>().First(m => m.ID == id);
                if (plan == null)
                {
                    return toResponse(StatusCodeType.Error, "查询不到相关项目信息，请核对！");
                }

                //已编制的任务信息
                AuditorDrawUpdateDto plandata = Api.Common.Helpers.ComHelper.Mapper<AuditorDrawUpdateDto, Biz_Contract_PlanAuditor_Item>(plan);
                plandata.PlanAuditorDrawList = db.Queryable<Biz_Contract_PlanAuditor_Draw>().Where(m => m.PlanAuditorItemID == plandata.ID).ToList();

                return toResponse(plandata);
            }
        }


        /// <summary>
        /// 编制任务计划
        /// Power = PRIV_PLANDRAW_REGISTER
        /// </summary>
        /// <param name="parm">ids</param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_PLANDRAW_REGISTER")]
        public IActionResult RegisterPlan([FromBody] AuditorDrawUpdateDto parm)
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

                    //if (db.Ado.GetScalar("select 1 from Biz_Contract_PlanAuditor_Item where status > 40000 and PlanAuditorItemID = @id", new { id = parm.ID }) != null)
                    //{
                    //    throw new Exception("当前计划已经提交编制，请核对！");
                    //}

                    //修改计划任务信息
                    List<Biz_Contract_PlanAuditor_Draw> PlanDrawList_update = parm.PlanAuditorDrawList_update;//更新
                    List<Biz_Contract_PlanAuditor_Draw> PlanDrawList_insert = parm.PlanAuditorDrawList_insert;//插入
                    List<Biz_Contract_PlanAuditor_Draw> PlanDrawList_delete = parm.PlanAuditorDrawList_delete;//删除
                    //删除计划任务
                    string[] drawids = PlanDrawList_delete.Where(x => !string.IsNullOrEmpty(x.ID)).Select(x => x.ID).ToArray();
                    db.Deleteable<Biz_Contract_PlanAuditor_Draw>().Where(m => drawids.Contains(m.ID)).ExecuteCommand();
                    if (PlanDrawList_update != null) //更新数据
                    {
                        foreach (var draw in PlanDrawList_update)
                        {
                            db.Updateable<Biz_Contract_PlanAuditor_Draw>().SetColumns(m => new Biz_Contract_PlanAuditor_Draw()
                            {
                                DrawStartTime = draw.DrawStartTime,
                                DrawEndTime = draw.DrawEndTime,
                                Department = draw.Department,
                                DrawContent = draw.DrawContent,
                                DrawClause = draw.DrawClause,
                            }).Where(m => m.ID == draw.ID).ExecuteCommand();
                        }
                    }
                    if (PlanDrawList_insert != null)
                    {
                        foreach (var draw in PlanDrawList_insert)
                        {
                            var ret = draw.ToCreate<Biz_Contract_PlanAuditor_Draw>(userinfo);
                            db.Insertable<Biz_Contract_PlanAuditor_Draw>(ret).ExecuteCommand();
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
