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
    /// 客户分场所信息
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerSiteController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<CustomerSiteController> _logger;
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 客户分场所接口
        /// </summary>
        private readonly IuffcsxxService _customersiteervice;


        public CustomerSiteController(ILogger<CustomerSiteController> logger, TokenManager tokenManager, IuffcsxxService customersiteService)
        {
            _logger = logger;
            _tokenManager = tokenManager;
            _customersiteervice = customersiteService;
        }

        /// <summary>
        /// 查询客户分场所信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult GetByCustomerID(CustomerSiteDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<uf_fcsxx>();

            predicate = predicate.And( m => m.ParentUID == parm.ParentUID);

            var response = _customersiteervice.GetWhere(predicate.ToExpression()).OrderBy(s => s.fcslx);

            return toResponse(response);
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
            return toResponse(_customersiteervice.GetId(id));
        }



        /// <summary>
        /// 添加客户分场所信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Create([FromBody] CustomerSiteCreateDto parm)
        {

            //从 Dto 映射到 实体
            var company = parm.Adapt<uf_fcsxx>().ToCreate(_tokenManager.GetSessionInfo());

            return toResponse(_customersiteervice.Add(company));
        }

        /// <summary>
        /// 更新客户分场所信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Update([FromBody] CustomerSiteUpdateDto parm)
        {
            var userSession = _tokenManager.GetSessionInfo();

            if(string.IsNullOrEmpty(parm.ID))
            {
                //从 Dto 映射到 实体
                var company = parm.Adapt<uf_fcsxx>().ToCreate(_tokenManager.GetSessionInfo());

                return toResponse(_customersiteervice.Add(company));
            }

            return toResponse(_customersiteervice.Update(m => m.ID == parm.ID, m => new uf_fcsxx()
            {
                zgsmc = parm.zgsmc,
                fcslx = parm.fcslx,
                fcslxmc = parm.fcslxmc,
                fcsmc = parm.fcsmc,
                fcsmcy = parm.fcsmcy,
                dz = parm.dz,
                dzy = parm.dzy,
                lxdh = parm.lxdh,
                cz = parm.cz,
                lxr = parm.lxr,
                lxrsj = parm.lxrsj,
                fxcrs = parm.fxcrs,
                jzbjl = parm.jzbjl,
                znbm = parm.znbm,
                fcshd = parm.fcshd,
                bz = parm.bz,
                UpdateID = userSession.ID,
                UpdateName = userSession.UserName,
                UpdateTime = DateTime.Now
            }));
        }

        /// <summary>
        /// 删除客户分场所信息
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

            var response = _customersiteervice.Delete(id);

            return toResponse(response);
        }
    }
}
