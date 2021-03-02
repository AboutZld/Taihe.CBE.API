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

namespace TaiheSystem.CBE.Api.Hostd.Controllers.Sys
{
    /// <summary>
    /// 模板类别维护管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TMPTypeController : BaseController
    {
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<TMPTypeController> _logger;

        /// <summary>
        /// 模板类别接口
        /// </summary>
        private readonly IDocTMPTYPEService _tmptypeService;

        /// <summary>
        /// 模板接口
        /// </summary>
        private readonly IDocTMPService _tmpService;

        public TMPTypeController(TokenManager tokenManager, IDocTMPTYPEService tmptypeService, ILogger<TMPTypeController> logger, IDocTMPService tmpService)
        {
            _tokenManager = tokenManager;
            _tmptypeService = tmptypeService;
            _tmpService = tmpService;
            _logger = logger;
        }


        /// <summary>
        /// 查询模板类别列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Query([FromBody] TMPTypeQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Doc_TMPTYPE>();

            predicate = predicate.And(m => (bool)m.deleted == false);

           predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.QueryText), m => m.TMPTypeCode.Contains(parm.QueryText) || m.TMPTypeName.Contains(parm.QueryText));

            var response = _tmptypeService.GetWhere(predicate.ToExpression()).OrderBy(m => m.SortIndex == null ? 0 : (int)m.SortIndex).ToList();

            return toResponse(ResolveTMPTypeTree(response));
        }


        /// <summary>
        /// 查询模板类别详细
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult Get(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return toResponse(_tmptypeService.GetId(id));
            }
            return toResponse(_tmptypeService.GetAll());
        }

        /// <summary>
        /// 添加模板类别
        /// Poewr = PRIV_TMPTYPE_CREATE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_TMPTYPE_CREATE")]
        //[Authorization]
        public IActionResult Create([FromBody] TMPTypeCreateDto parm)
        {
            if (_tmptypeService.Any(m => m.TMPTypeCode == parm.TMPTypeCode ))
            {
                return toResponse(StatusCodeType.Error, $"添加模板类别编码 {parm.TMPTypeCode} 已存在，不能重复！");
            }

            if (_tmptypeService.Any(m => m.TMPTypeName == parm.TMPTypeName))
            {
                return toResponse(StatusCodeType.Error, $"添加模板类别名称 {parm.TMPTypeName} 已存在，不能重复！");
            }

            //从 Dto 映射到 实体
            var options = parm.Adapt<Doc_TMPTYPE>().ToCreate(_tokenManager.GetSessionInfo());

            return toResponse(_tmptypeService.Add(options));
        }

        /// <summary>
        /// 更新模板类别
        /// Power = PRIV_TMPTYPE_UPDATE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_TMPTYPE_UPDATE")]
        //[Authorization]
        public IActionResult Update([FromBody] TMPTypeUpdateDto parm)
        {
            var userSession = _tokenManager.GetSessionInfo();

            //判断是更新还是新增
            if (string.IsNullOrEmpty(parm.ID))
            {
                if (_tmptypeService.Any(m => m.TMPTypeCode == parm.TMPTypeCode))
                {
                    return toResponse(StatusCodeType.Error, $"添加模板类别编码 {parm.TMPTypeCode} 已存在，不能重复！");
                }

                if (_tmptypeService.Any(m => m.TMPTypeName == parm.TMPTypeName))
                {
                    return toResponse(StatusCodeType.Error, $"添加模板类别名称 {parm.TMPTypeName} 已存在，不能重复！");
                }

                //从 Dto 映射到 实体
                var options = parm.Adapt<Doc_TMPTYPE>().ToCreate(_tokenManager.GetSessionInfo());
                return toResponse(_tmptypeService.Add(options));
            }
            else
            {
                return toResponse(_tmptypeService.Update(m => m.ID == parm.ID, m => new Doc_TMPTYPE()
                {
                    TMPTypeCode = parm.TMPTypeCode,
                    TMPTypeName = parm.TMPTypeName,
                    Remark = parm.Remark,
                    SortIndex = parm.SortIndex,
                    UpdateID = userSession.ID,
                    UpdateName = userSession.UserName,
                    UpdateTime = DateTime.Now
                }));
            }
        }

        /// <summary>
        /// 删除模板类别
        /// Power = PRIV_TMPTYPE_DELETE
        /// </summary>
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorization(Power = "PRIV_TMPTYPE_DELETE")]
        //[Authorization]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "删除 Id 不能为空");
            }
            if(_tmptypeService.Any(m=>m.ParentUID == id && (bool)m.deleted == false))
            {
                return toResponse(StatusCodeType.Error, "当前模板类别存在下级，请删除后再试");
            }    
            if(_tmpService.Any(m=>m.TMPTypeID == id))
            {
                return toResponse(StatusCodeType.Error, "当前模板类别存在模板，请删除后再试");
            }

            var response = _tmptypeService.Update(m => m.ID == id, m => new Doc_TMPTYPE()
            {
                deleted = true
            }) ;

            return toResponse(response);
        }
    }
}
