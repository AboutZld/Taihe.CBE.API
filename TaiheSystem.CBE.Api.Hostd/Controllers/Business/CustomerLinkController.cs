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
using TaiheSystem.CBE.Api.Model.View;

namespace TaiheSystem.CBE.Api.Hostd.Controllers.Business
{
    /// <summary>
    /// 客户联系人信息
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerLinkController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<CustomerLinkController> _logger;
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 客户联系人接口
        /// </summary>
        private readonly Iufkhxxdt1Service _customerlinkService;


        public CustomerLinkController(ILogger<CustomerLinkController> logger, TokenManager tokenManager, Iufkhxxdt1Service customerlinkService)
        {
            _logger = logger;
            _tokenManager = tokenManager;
            _customerlinkService = customerlinkService;
        }

        /// <summary>
        /// 查询客户联系人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult GetByCustomerID(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, " Id 不能为空");
            }

            return toResponse(_customerlinkService.GetWhere(m => m.ParentUID == id));
        }

        /// <summary>
        /// 根据 Id 查询联系人信息
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult Get(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error," Id 不能为空");
            }
            return toResponse(_customerlinkService.GetId(id));
        }



        /// <summary>
        /// 添加客户联系人信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Create([FromBody] CustomerLinkCreateDto parm)
        {

            //从 Dto 映射到 实体
            var company = parm.Adapt<uf_khxx_dt1>().ToCreate(_tokenManager.GetSessionInfo());

            return toResponse(_customerlinkService.Add(company));
        }

        /// <summary>
        /// 更新客户联系人信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Update([FromBody] CustomerLinkUpdateDto parm)
        {
            var userSession = _tokenManager.GetSessionInfo();
            if (string.IsNullOrEmpty(parm.ID))
            {
                //从 Dto 映射到 实体
                var company = parm.Adapt<uf_khxx_dt1>().ToCreate(_tokenManager.GetSessionInfo());

                return toResponse(_customerlinkService.Add(company));
            }
            else
            {
                return toResponse(_customerlinkService.Update(m => m.ID == parm.ID, m => new uf_khxx_dt1()
                {
                    lxr = parm.lxr,
                    bm = parm.bm,
                    zw = parm.zw,
                    zj = parm.zj,
                    sj = parm.sj,
                    yx = parm.yx,
                    bz = parm.bz,
                    UpdateTime = DateTime.Now
                }));
            }
        }

        /// <summary>
        /// 删除客户联系人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "删除 Id 不能为空");
            }

            var response = _customerlinkService.Delete(id);

            return toResponse(response);
        }
    }
}
