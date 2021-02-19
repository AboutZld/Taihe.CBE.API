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
    /// 选项类型管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SelectItemController : BaseController
    {
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<SelectItemController> _logger;

        /// <summary>
        /// 选项定义接口
        /// </summary>
        private readonly IFrmSelectItemService _itemService;

        public SelectItemController(TokenManager tokenManager, IFrmSelectItemService optionService, ILogger<SelectItemController> logger)
        {
            _tokenManager = tokenManager;
            _itemService = optionService;
            _logger = logger;
        }


        /// <summary>
        /// 查询选项类型列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Query([FromBody] SelectItemsQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Frm_SelectItem>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.QueryText), m => m.SelectItemName.Contains(parm.QueryText) || m.SelectItemCode.Contains(parm.QueryText));

            var response = _itemService.GetWhere(predicate.ToExpression()).OrderBy(s=>s.SortIndex);

            return toResponse(response);
        }


        /// <summary>
        /// 查询选项类型
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult Get(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return toResponse(_itemService.GetId(id));
            }
            return toResponse(_itemService.GetAll());
        }

        /// <summary>
        /// 添加选项类型
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_SELECTITEM_CREATE")]
        //[Authorization]
        public IActionResult Create([FromBody] SelectItemCreateDto parm)
        {
            if (_itemService.Any(m => m.SelectItemCode == parm.SelectItemCode ))
            {
                return toResponse(StatusCodeType.Error, $"添加选项类型编码 {parm.SelectItemCode} 已存在，不能重复！");
            }

            if (_itemService.Any(m => m.SelectItemName == parm.SelectItemName))
            {
                return toResponse(StatusCodeType.Error, $"添加选项类型名称 {parm.SelectItemName} 已存在，不能重复！");
            }

            //从 Dto 映射到 实体
            var options = parm.Adapt<Frm_SelectItem>().ToCreate(_tokenManager.GetSessionInfo());

            return toResponse(_itemService.Add(options));
        }

        /// <summary>
        /// 更新选项类型
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_SELECTITEM_UPDATE")]
        //[Authorization]
        public IActionResult Update([FromBody] SelectItemUpdateDto parm)
        {
            var userSession = _tokenManager.GetSessionInfo();
            
            //判断是更新还是新增
            if(!string.IsNullOrEmpty(parm.ID))
            {
                if (_itemService.Any(m => m.SelectItemCode == parm.SelectItemCode))
                {
                    return toResponse(StatusCodeType.Error, $"添加选项类型编码 {parm.SelectItemCode} 已存在，不能重复！");
                }

                if (_itemService.Any(m => m.SelectItemName == parm.SelectItemName))
                {
                    return toResponse(StatusCodeType.Error, $"添加选项类型名称 {parm.SelectItemName} 已存在，不能重复！");
                }

                //从 Dto 映射到 实体
                var options = parm.Adapt<Frm_SelectItem>().ToCreate(_tokenManager.GetSessionInfo());

                return toResponse(_itemService.Add(options));
            }

            return toResponse(_itemService.Update(m => m.ID == parm.ID, m => new Frm_SelectItem()
            {
                SelectItemName = parm.SelectItemName,
                SelectItemCode = parm.SelectItemCode,
                SelectItemDecr = parm.SelectItemDecr,
                SortIndex = parm.SortIndex,
                UpdateID = userSession.ID,
                UpdateName = userSession.UserName,
                UpdateTime = DateTime.Now
            }));
        }

        /// <summary>
        /// 删除选项类型
        /// </summary>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_SELECTITEM_DELETE")]
        //[Authorization]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "删除 Id 不能为空");
            }

            var response = _itemService.Delete(id);

            return toResponse(response);
        }
    }
}
