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

namespace TaiheSystem.CBE.Api.Hostd.Controllers.Gwf
{

    /// <summary>
    /// 流程日志查询接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FlowStepController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<FlowStepController> _logger;
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 日志查询接口
        /// </summary>
        private readonly IGwfStepService _stepService;


        public FlowStepController(ILogger<FlowStepController> logger, TokenManager tokenManager, IGwfStepService stepService, ISysDataRelationService dataRelationService)
        {
            _logger = logger;
            _tokenManager = tokenManager;
            _stepService = stepService;
        }


        /// <summary>
        /// 查询操作流程日志记录
        /// </summary>
        /// <param name="id">查询id</param>
        /// <param name="flowcode">流程代号 10-合同流程 20-项目流程 30-任务流程 40-证书流程</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult GetAll(string id = null, string flowcode = null)
        {
            using (SqlSugarClient db = Core.DbContext.Current)
            {
                string sql = @"select top 20 *,dbo.GetNodeName(Node_From) as Node_FromName,dbo.GetNodeName(Node_To) as Node_ToName,[dbo].[GetOperationName](OperationCode) as OperationName from Gwf_Step where 1=1 ";
                string Sortby = " Order by CreateTime DESC";

                string sqlWhere = "";
                switch (flowcode)
                {
                    case "10":
                        sqlWhere += $" and Biz_MainContract_ID = '{id}'";
                        break;
                    case "20":
                        sqlWhere += $" and Biz_ContractItem_Sub_ID = '{id}'";
                        break;
                    case "30":
                        sqlWhere += $" and Biz_Contract_Plan_ID = '{id}'";
                        break;
                    case "40":
                        sqlWhere += $" and Biz_Contract_PlanAuditor_Item_ID = '{id}'";
                        break;
                }

                var response = db.SqlQueryable<object>(sql + sqlWhere + Sortby).ToList();

                return toResponse(response);
            }
        }

        
    }
}
