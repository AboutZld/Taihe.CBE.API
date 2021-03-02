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
    /// 模板文件维护管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TMPController : BaseController
    {
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<TMPController> _logger;

        /// <summary>
        /// 模板类别接口
        /// </summary>
        private readonly IDocTMPTYPEService _tmptypeService;

        /// <summary>
        /// 模板接口
        /// </summary>
        private readonly IDocTMPService _tmpService;

        public TMPController(TokenManager tokenManager, IDocTMPTYPEService tmptypeService, ILogger<TMPController> logger, IDocTMPService tmpService)
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
        public IActionResult Query([FromBody] TMPQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Doc_TMP>();

            predicate = predicate.And(m => (bool)m.deleted == false);

            //按照模板类型查询
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.TMPTypeID), m => m.TMPTypeID == parm.TMPTypeID);
            //按照关键字查询
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.QueryText), m => m.TMP.Contains(parm.QueryText));

            var response = _tmpService.GetWhere(predicate.ToExpression()).OrderBy(m => m.SortIndex == null ? 0 : (int)m.SortIndex).ToList();

            return toResponse(response);
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
                return toResponse(_tmpService.GetId(id));
            }
            return toResponse(_tmpService.GetAll());
        }

        /// <summary>
        /// 添加模板
        /// Poewr = PRIV_TMP_CREATE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_TMP_CREATE")]
        //[Authorization]
        public IActionResult Create([FromBody] TMPCreateDto parm)
        {

            if (_tmpService.Any(m => m.TMP == parm.TMP))
            {
                return toResponse(StatusCodeType.Error, $"添加模板名称 {parm.TMP} 已存在，不能重复！");
            }

            //从 Dto 映射到 实体
            var options = parm.Adapt<Doc_TMP>().ToCreate(_tokenManager.GetSessionInfo());

            return toResponse(_tmpService.Add(options));
        }

        /// <summary>
        /// 更新模板文件
        /// Power = PRIV_TMP_UPDATE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_TMP_UPDATE")]
        //[Authorization]
        public IActionResult Update([FromBody] TMPUpdateDto parm)
        {
            var userSession = _tokenManager.GetSessionInfo();
            
            //判断是更新还是新增
            if(string.IsNullOrEmpty(parm.ID))
            {
                if (_tmpService.Any(m => m.TMP == parm.TMP))
                {
                    return toResponse(StatusCodeType.Error, $"添加模板名称 {parm.TMP} 已存在，不能重复！");
                }

                //从 Dto 映射到 实体
                var options = parm.Adapt<Doc_TMP>().ToCreate(_tokenManager.GetSessionInfo());
                return toResponse(_tmpService.Add(options));
            }

            return toResponse(_tmpService.Update(m => m.ID == parm.ID, m => new Doc_TMP()
            {
                TMP = parm.TMP,
                TMPID = parm.TMPID,
                Remark = parm.Remark,
                SortIndex = parm.SortIndex,
                UpdateID = userSession.ID,
                UpdateName = userSession.UserName,
                UpdateTime = DateTime.Now
            }));
        }

        /// <summary>
        /// 删除模板文件
        /// Power = PRIV_TMP_DELETE
        /// </summary>
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorization(Power = "PRIV_TMP_DELETE")]
        //[Authorization]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "删除 Id 不能为空");
            }

            var response = _tmpService.Update(m => m.ID == id, m => new Doc_TMP()
            {
                deleted = true
            }) ;

            return toResponse(response);
        }
    }
}
