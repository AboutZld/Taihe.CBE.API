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
    /// 体系标准管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SysStandardController : BaseController
    {
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<SysStandardController> _logger;

        /// <summary>
        /// 体系标准接口
        /// </summary>
        private readonly IAbiSysStandardService _sysstandarService;

        public SysStandardController(TokenManager tokenManager, IAbiSysStandardService sysstandardService, ILogger<SysStandardController> logger)
        {
            _tokenManager = tokenManager;
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
            var predicate = Expressionable.Create<Abi_SysStandard>();

            //关键字搜索匹配：编码、上报编码、简称
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.QueryText), m => m.SysStandardCode.Contains(parm.QueryText) || m.SysStandardReportCode.Contains(parm.QueryText) || m.SysStandardShortName.Contains(parm.QueryText));

            var response = _sysstandarService.GetPages(predicate.ToExpression(), parm);

            return toResponse(response);
        }

        /// <summary>
        /// 查询列表（全部启用）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult QueryAll([FromBody] SystemTypeQueryAllDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Abi_SysStandard>();

            predicate = predicate.And(m => m.Enabled == true);
            //关键字搜索匹配：编码、上报编码、简称
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.QueryText), m => m.SysStandardCode.Contains(parm.QueryText) || m.SysStandardReportCode.Contains(parm.QueryText) || m.SysStandardShortName.Contains(parm.QueryText));

            var response = _sysstandarService.GetWhere(predicate.ToExpression()).OrderBy(m=>m.SortIndex);

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
            return toResponse(_sysstandarService.GetId(id));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_SYSSTANDARD_CREATE")]
        //[Authorization]
        public IActionResult Create([FromBody] SysStandardCreateDto parm)
        {
            if (_sysstandarService.Any(m => m.SysStandardCode == parm.SysStandardCode))
            {
                return toResponse(StatusCodeType.Error, $"添加编码 {parm.SysStandardCode} 已存在，不能重复！");
            }
            if (_sysstandarService.Any(m => m.SysStandardReportCode == parm.SysStandardReportCode))
            {
                return toResponse(StatusCodeType.Error, $"添加上报编 {parm.SysStandardReportCode} 已存在，不能重复！");
            }
            if (_sysstandarService.Any(m => m.SysStandardShortName == parm.SysStandardShortName))
            {
                return toResponse(StatusCodeType.Error, $"添加简称 {parm.SysStandardShortName} 已存在，不能重复！");
            }
            //从 Dto 映射到 实体
            var options = parm.Adapt<Abi_SysStandard>().ToCreate(_tokenManager.GetSessionInfo());

            return toResponse(_sysstandarService.Add(options));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_SYSSTANDARD_UPDATE")]
        //[Authorization]
        public IActionResult Update([FromBody] SysStandardUpdateDto parm)
        {
            var userSession = _tokenManager.GetSessionInfo();

            return toResponse(_sysstandarService.Update(m => m.ID == parm.ID, m => new Abi_SysStandard()
            {
                SystemTypeID = parm.SystemTypeID,
                SystemTypeCode = parm.SystemTypeCode,
                SystemTypeName = parm.SystemTypeName,
                SysStandardCode = parm.SysStandardCode,
                SysStandardReportCode = parm.SysStandardReportCode,
                SysStandardShortName = parm.SysStandardShortName,
                SysStandardName = parm.SysStandardName,
                SysStandardNo = parm.SysStandardNo,
                Remark = parm.Remark,
                RemarkEN = parm.RemarkEN,
                DeadLine = parm.DeadLine,
                Enabled = parm.Enabled,
                SortIndex = parm.SortIndex,
                UpdateID = userSession.UserID,
                UpdateName = userSession.UserName,
                UpdateTime = DateTime.Now
            }));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_SYSSTANDARD_DELETE")]
        //[Authorization]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "删除 Id 不能为空");
            }

            var response = _sysstandarService.Delete(id);

            return toResponse(response);
        }
    }
}
