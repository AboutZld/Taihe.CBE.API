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
    /// 选项管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SelectItemObjController : BaseController
    {
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<SelectItemObjController> _logger;

        /// <summary>
        /// 字典定义接口
        /// </summary>
        private readonly IFrmSelectItemObjService _itemobjService;

        public SelectItemObjController(TokenManager tokenManager, IFrmSelectItemObjService optionService, ILogger<SelectItemObjController> logger)
        {
            _tokenManager = tokenManager;
            _itemobjService = optionService;
            _logger = logger;
        }


        /// <summary>
        /// 查询选项列表（分页）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Query([FromBody] SelectItemObjQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Frm_SelectItemObj>();

            predicate = predicate.And(m=>m.SelectItemCode == parm.SelectItemCode).AndIF(!string.IsNullOrEmpty(parm.QueryText), m => m.SelectItemObjCode.Contains(parm.QueryText) || m.SelectItemObjName.Contains(parm.QueryText));

            var response = _itemobjService.GetPages(predicate.ToExpression(), parm);

            return toResponse(response);
        }

        /// <summary>
        /// 查询选项列表（全部）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult QueryAll([FromBody] SelectItemObjQueryAllDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Frm_SelectItemObj>();

            predicate = predicate.And(m => m.SelectItemCode == parm.SelectItemCode).AndIF(!string.IsNullOrEmpty(parm.QueryText), m => m.SelectItemObjCode.Contains(parm.QueryText) || m.SelectItemObjName.Contains(parm.QueryText));

            var response = _itemobjService.GetWhere(predicate.ToExpression());

            return toResponse(response);
        }

        /// <summary>
        /// 查询多个SelectItemCode对应选项列表(只返回已启用)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult QueryMutilate([FromBody] SelectItemObjsQueryDto parm)
        {
            //开始拼装查询条件
            var spiltcode = parm.SelectItemCodes.Split(',').ToArray();

            using (SqlSugarClient db = Core.DbContext.Current)
            {
                var response = db.Queryable<Frm_SelectItemObj>().Where(m => spiltcode.Contains(m.SelectItemCode) && m.Enabled == true).OrderBy(m => m.SelectItemCode).OrderBy(m => m.SortIndex).ToList();

                return toResponse(response);
            }
        }


        /// <summary>
        /// 查询字典
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
            return toResponse(_itemobjService.GetId(id));
        }

        /// <summary>
        /// 添加选项
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_SLECTITEMOBJ_CREATE")]
        //[Authorization]
        public IActionResult Create([FromBody] SelectItemObjCreateDto parm)
        {
            if (_itemobjService.Any(m => m.SelectItemCode == parm.SelectItemCode && m.SelectItemObjCode == parm.SelectItemObjCode))
            {
                return toResponse(StatusCodeType.Error, $"添加选项编码 {parm.SelectItemObjCode} 已存在，不能重复！");
            }
            //从 Dto 映射到 实体
            var options = parm.Adapt<Frm_SelectItemObj>().ToCreate(_tokenManager.GetSessionInfo());

            return toResponse(_itemobjService.Add(options));
        }

        /// <summary>
        /// 更新选项
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_SLECTITEMOBJ_UPDATE")]
        //[Authorization]
        public IActionResult Update([FromBody] SelectItemObjUpdateDto parm)
        {
            var userSession = _tokenManager.GetSessionInfo();

            //判断是新增还是更新
            if (!string.IsNullOrEmpty(parm.ID))
            {
                if (_itemobjService.Any(m => m.SelectItemCode == parm.SelectItemCode && m.SelectItemObjCode == parm.SelectItemObjCode))
                {
                    return toResponse(StatusCodeType.Error, $"添加选项编码 {parm.SelectItemObjCode} 已存在，不能重复！");
                }
                //从 Dto 映射到 实体
                var options = parm.Adapt<Frm_SelectItemObj>().ToCreate(_tokenManager.GetSessionInfo());

                return toResponse(_itemobjService.Add(options));
            }
            else
            {
                return toResponse(_itemobjService.Update(m => m.ID == parm.ID, m => new Frm_SelectItemObj()
                {
                    SelectItemObjCode = parm.SelectItemObjCode,
                    SelectItemObjName = parm.SelectItemObjName,
                    SortIndex = parm.SortIndex,
                    SelectItmeObjDecr = parm.SelectItmeObjDecr,
                    Enabled = parm.Enabled,
                    UpdateID = userSession.UserID,
                    UpdateName = userSession.UserName,
                    UpdateTime = DateTime.Now
                }));
            }
        }

        /// <summary>
        /// 删除选项
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_SLECTITEMOBJ_DELETE")]
        //[Authorization]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "删除选项 Id 不能为空");
            }

            var response = _itemobjService.Delete(id);

            return toResponse(response);
        }
    }
}
