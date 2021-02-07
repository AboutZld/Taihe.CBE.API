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
    /// 审核类型
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuditTypeController : BaseController
    {
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<AuditTypeController> _logger;

        /// <summary>
        /// 审核类型接口
        /// </summary>
        private readonly ICfgAuditTypeService _audittypeService;


        public AuditTypeController(TokenManager tokenManager, ICfgAuditTypeService audittypeService, ILogger<AuditTypeController> logger)
        {
            _tokenManager = tokenManager;
            _audittypeService = audittypeService;
            _logger = logger;
        }


        /// <summary>
        /// 查询列表（分页）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Query([FromBody] AuditTypeQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Cfg_AuditType>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.QueryText), m => m.AuditTypeCode.Contains(parm.QueryText) || m.AuditTypeName.Contains(parm.QueryText));

            var response = _audittypeService.GetPages(predicate.ToExpression(), parm);

            return toResponse(response);
        }

        /// <summary>
        /// 查询列表（全部）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult QueryAll([FromBody] AuditTypeQueryAllDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Cfg_AuditType>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.QueryText), m => m.AuditTypeCode.Contains(parm.QueryText) || m.AuditTypeName.Contains(parm.QueryText));

            var response = _audittypeService.GetWhere(predicate.ToExpression());

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
            return toResponse(_audittypeService.GetId(id));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_SYSTEMTYPE_CREATE")]
        //[Authorization]
        public IActionResult Create([FromBody] AuditTypeCreateDto parm)
        {
            if (_audittypeService.Any(m => m.AuditTypeCode == parm.AuditTypeCode))
            {
                return toResponse(StatusCodeType.Error, $"添加类别编码 {parm.AuditTypeCode} 已存在，不能重复！");
            }
            if (_audittypeService.Any(m => m.AuditTypeName == parm.AuditTypeName))
            {
                return toResponse(StatusCodeType.Error, $"添加类别名称 {parm.AuditTypeName} 已存在，不能重复！");
            }
            //从 Dto 映射到 实体
            var options = parm.Adapt<Cfg_AuditType>().ToCreate(_tokenManager.GetSessionInfo());

            return toResponse(_audittypeService.Add(options));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_SYSTEMTYPE_UPDATE")]
        //[Authorization]
        public IActionResult Update([FromBody] AuditTypeUpdateDto parm)
        {
            var userSession = _tokenManager.GetSessionInfo();

            if (string.IsNullOrEmpty(parm.ID))
            {
                if (_audittypeService.Any(m => m.AuditTypeCode == parm.AuditTypeCode))
                {
                    return toResponse(StatusCodeType.Error, $"添加类别编码 {parm.AuditTypeCode} 已存在，不能重复！");
                }
                if (_audittypeService.Any(m => m.AuditTypeName == parm.AuditTypeName))
                {
                    return toResponse(StatusCodeType.Error, $"添加类别名称 {parm.AuditTypeName} 已存在，不能重复！");
                }
                //从 Dto 映射到 实体
                var options = parm.Adapt<Cfg_AuditType>().ToCreate(_tokenManager.GetSessionInfo());

                return toResponse(_audittypeService.Add(options));
            }
            else
            {
                return toResponse(_audittypeService.Update(m => m.ID == parm.ID, m => new Cfg_AuditType()
                {
                    AuditTypeCode = parm.AuditTypeCode,
                    AuditTypeName = parm.AuditTypeName,
                    SortIndex = parm.SortIndex,
                    AuditTypeNameEN = parm.AuditTypeNameEN,
                    Remark = parm.Remark,
                    Enabled = parm.Enabled,
                    UpdateID = userSession.UserID,
                    UpdateName = userSession.UserName,
                    UpdateTime = DateTime.Now
                }));
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_SYSTEMTYPE_DELETE")]
        //[Authorization]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "删除 Id 不能为空");
            }

            var response = _audittypeService.Delete(id);

            return toResponse(response);
        }
    }
}
