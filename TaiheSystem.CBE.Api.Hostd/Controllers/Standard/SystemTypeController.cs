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
    /// 体系认证类别管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SystemTypeController : BaseController
    {
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<SystemTypeController> _logger;

        /// <summary>
        /// 体系类别接口
        /// </summary>
        private readonly ICfgSystemTypeService _systypeService;

        /// <summary>
        /// 体系标准接口
        /// </summary>
        private readonly IAbiSysStandardService _sysstandarService;

        public SystemTypeController(TokenManager tokenManager, ICfgSystemTypeService systypeService, ILogger<SystemTypeController> logger, IAbiSysStandardService sysstandardService)
        {
            _tokenManager = tokenManager;
            _systypeService = systypeService;
            _sysstandarService = sysstandardService;
            _logger = logger;
        }


        /// <summary>
        /// 查询列表（分页）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Query([FromBody] SystemTypeQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Cfg_SystemType>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.QueryText), m => m.TypeCode.Contains(parm.QueryText) || m.TypeName.Contains(parm.QueryText));

            var response = _systypeService.GetPages(predicate.ToExpression(), parm);

            return toResponse(response);
        }

        /// <summary>
        /// 查询列表（全部）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult QueryAll([FromBody] SystemTypeQueryAllDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Cfg_SystemType>();

            predicate = predicate.And(m=>m.Enabled == true);

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.QueryText), m => m.TypeCode.Contains(parm.QueryText) || m.TypeName.Contains(parm.QueryText));

            var response = _systypeService.GetWhere(predicate.ToExpression()).OrderBy(m=>m.SortIndex);

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
            return toResponse(_systypeService.GetId(id));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_SYSTEMTYPE_CREATE")]
        //[Authorization]
        public IActionResult Create([FromBody] SystemTypeCreateDto parm)
        {
            if (_systypeService.Any(m => m.TypeCode == parm.TypeCode))
            {
                return toResponse(StatusCodeType.Error, $"添加类别编码 {parm.TypeCode} 已存在，不能重复！");
            }
            if (_systypeService.Any(m => m.TypeName == parm.TypeName))
            {
                return toResponse(StatusCodeType.Error, $"添加类别名称 {parm.TypeName} 已存在，不能重复！");
            }
            //从 Dto 映射到 实体
            var options = parm.Adapt<Cfg_SystemType>().ToCreate(_tokenManager.GetSessionInfo());

            return toResponse(_systypeService.Add(options));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_SYSTEMTYPE_UPDATE")]
        //[Authorization]
        public IActionResult Update([FromBody] SystemTypeUpdateDto parm)
        {
            var userSession = _tokenManager.GetSessionInfo();

            if (string.IsNullOrEmpty(parm.ID))
            {
                if (_systypeService.Any(m => m.TypeCode == parm.TypeCode))
                {
                    return toResponse(StatusCodeType.Error, $"添加类别编码 {parm.TypeCode} 已存在，不能重复！");
                }
                if (_systypeService.Any(m => m.TypeName == parm.TypeName))
                {
                    return toResponse(StatusCodeType.Error, $"添加类别名称 {parm.TypeName} 已存在，不能重复！");
                }
                //从 Dto 映射到 实体
                var options = parm.Adapt<Cfg_SystemType>().ToCreate(_tokenManager.GetSessionInfo());

                return toResponse(_systypeService.Add(options));
            }
            else
            {
                return toResponse(_systypeService.Update(m => m.ID == parm.ID, m => new Cfg_SystemType()
                {
                    TypeCode = parm.TypeCode,
                    TypeName = parm.TypeName,
                    SortIndex = parm.SortIndex,
                    TypeNameEN = parm.TypeNameEN,
                    TypeNameDescr = parm.TypeNameDescr,
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

            if (_sysstandarService.Any(m => m.SystemTypeID == id))
            {
                return toResponse(StatusCodeType.Error, $"添加体系类别编码已经被体系标准引用，请先删除标准！");
            }

            var response = _systypeService.Delete(id);

            return toResponse(response);
        }
    }
}
