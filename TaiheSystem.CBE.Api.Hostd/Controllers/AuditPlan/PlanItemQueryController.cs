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
    /// 审核项目查询
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlanItemQueryController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<PlanItemQueryController> _logger;
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 任务项目试图表
        /// </summary>
        private readonly IviewItemPlanVMService _itemplanService;

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


        public PlanItemQueryController(ILogger<PlanItemQueryController> logger, TokenManager tokenManager, IBizContractItemSubService contractitemsubService, IBizContractItemService contractitemService, IBizContractPlanService contractitemplanService, IBizContractPlanAuditorService contractplanauditorService, IBizContractPlanAuditorItemService contractplanauditoritemService, IviewItemPlanVMService itemplanService)
        {
            _logger = logger;
            _tokenManager = tokenManager;
            _contractitemsubService = contractitemsubService;
            _contractitemService = contractitemService;
            _contractitemplanService = contractitemplanService;
            _contractplanauditorService = contractplanauditorService;
            _contractplanauditoritemService = contractplanauditoritemService;
            _itemplanService = itemplanService;
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
            var predicate = Expressionable.Create<view_ItemPlanVM>();

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

            //项目编号
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ContractItemNo), m => m.ContractItemNo.Contains(parm.ContractItemNo));
            //客户名称
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.khmc), m => m.zzmc.Contains(parm.khmc));
            //合作伙伴
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.hzhb), m => m.mc.Contains(parm.hzhb));
            //受理日期开始
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.AcceptDateStart), m => m.CreateTime >= DateTime.Parse(parm.AcceptDateStart));
            //受理日期结束
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.AcceptDateEnd), m => m.CreateTime <= DateTime.Parse(parm.AcceptDateEnd));

            //var response = _contractitemsubService.GetPages(predicate.ToExpression(), parm);

            var response = Core.DbContext.CurrentDB.SqlQueryable<view_ItemPlanVM>(@"
SELECT  m.ID, m.ContractNo, m.CustomerID, m.CreateTime, k.zzmc, k.xzqhmc, h.mc, s.ID AS ContractItemSubID, s.MainContractID, 
                   s.ContractItemID, s.ContractItemSubType, s.ContractItemSubTypeCode, c.ItemName, c.ContractItemNo,p.ContractPlanNo,
				   p.PlanStartDate,p.PlanEndDate,p.ApproveTime,
				   dbo.GetNodeName(p.status) as StatusName
FROM      dbo.Biz_ContractItem_Sub AS s 
inner join Biz_Contract_Plan p on s.ContractPlanID = p.ID
INNER JOIN
                   dbo.Biz_MainContract AS m ON m.ID = s.MainContractID INNER JOIN
                   dbo.Biz_ContractItem AS c ON c.ID = s.ContractItemID LEFT OUTER JOIN
                   dbo.uf_khxx AS k ON k.ID = m.CustomerID LEFT OUTER JOIN
                   dbo.uf_hzhb AS h ON k.hzdwID = h.ID
WHERE   (m.deleted = 0)
").ToPage<view_ItemPlanVM>(predicate.ToExpression(), parm);

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
                    return toResponse(StatusCodeType.Error, "查询不到相关项目信息，请核对！");
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
    }
}
