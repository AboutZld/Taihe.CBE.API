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

namespace TaiheSystem.CBE.Api.Hostd.Controllers.Standard
{
    /// <summary>
    /// 管理体系审核时间
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuditTimeController : BaseController
    {
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<AuditTimeController> _logger;

        /// <summary>
        /// 体系审核时间
        /// </summary>
        private readonly ISysAuditTimeService _audittimeService;


        public AuditTimeController(TokenManager tokenManager, ISysAuditTimeService audittimeService, ILogger<AuditTimeController> logger)
        {
            _tokenManager = tokenManager;
            _audittimeService = audittimeService;
            _logger = logger;
        }


        /// <summary>
        /// 查询列表（分页）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Query([FromBody] AuditTimeQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Sys_AuditTime>();

            //体系编号
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.SystemTypeID), m => m.SystemTypeID == parm.SystemTypeID);

            var response = _audittimeService.GetPages(predicate.ToExpression(), parm);

            return toResponse(response);
        }


        /// <summary>
        /// 查询详细
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "id不能为空");
            }
            return toResponse(_audittimeService.GetId(id));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_BIZCLASS_CREATE")]
        [Authorization]
        public IActionResult Create([FromBody] AuditTimeCreateDto parm)
        {
            //从 Dto 映射到 实体
            var options = parm.Adapt<Sys_AuditTime>().ToCreate(_tokenManager.GetSessionInfo());

            return toResponse(_audittimeService.Add(options));
        }

        /// <summary>
        /// 更新
        /// Power = PRIV_AUDITTIME_UPDATE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_AUDITTIME_UPDATE")]
        //[Authorization]
        public IActionResult Update([FromBody] AuditTimeUpdateDto parm)
        {
            var userSession = _tokenManager.GetSessionInfo();

            if (string.IsNullOrEmpty(parm.ID))
            {
                //从 Dto 映射到 实体
                var options = parm.Adapt<Sys_AuditTime>().ToCreate(_tokenManager.GetSessionInfo());

                return toResponse(_audittimeService.Add(options));
            }
            else
            {
                return toResponse(_audittimeService.Update(m => m.ID == parm.ID, m => new Sys_AuditTime()
                {
                    SystemTypeID = parm.SystemTypeID,
                    SystemTypeCode = parm.SystemTypeCode,
                    SystemTypeName = parm.SystemTypeName,
                    DownLimt = parm.DownLimt,
                    UpLimit = parm.UpLimit,
                    RiskRegisterID = parm.RiskRegisterID,
                    RiskRegister = parm.RiskRegister,
                    AuditDays = parm.AuditDays
                }));
            }
        }

        /// <summary>
        /// 删除
        /// Power = PRIV_AUDITTIME_DELETE
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_AUDITTIME_DELETE")]
        //[Authorization]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "删除 Id 不能为空");
            }

            var response = _audittimeService.Delete(id);

            return toResponse(response);
        }
    }
}
