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
    /// 合作伙伴
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PartnersController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<PartnersController> _logger;
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 合作伙伴接口
        /// </summary>
        private readonly IufhzhbService _partnerService;

        /// <summary>
        /// 数据关系接口
        /// </summary>
        private readonly IufkhxxService  _ufkhxxService;


        public PartnersController(ILogger<PartnersController> logger, TokenManager tokenManager, IufhzhbService partnerService, IufkhxxService dataRelationService)
        {
            _logger = logger;
            _tokenManager = tokenManager;
            _partnerService = partnerService;
            _ufkhxxService = dataRelationService;
        }


        /// <summary>
        /// 查询合作伙伴列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Query([FromBody] PartnerQueryDto parm)
        {
            var response = _partnerService.QueryPartnerPages(parm);

            return toResponse(response);
        }


        /// <summary>
        /// 根据 Id 查询合作伙伴
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
            return toResponse(_partnerService.GetId(id));
        }

        /// <summary>
        /// 查询所有合作伙伴
        /// </summary>
        /// <param name="enable">是否启用（不传返回所有）</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult GetAll(bool? enable = null)
        {
            var predicate = Expressionable.Create<uf_hzhb>();

            //predicate = predicate.AndIF(enable != null, m => m.Enable == enable);

            return toResponse(_partnerService.GetAll());
        }

        /// <summary>
        /// 添加合作伙伴
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_PARTNER_CREATE")]
        public IActionResult Create([FromBody] PartnerCreateDto parm)
        {
            if (_partnerService.Any(m => m.bm == parm.bm))
            {
                return toResponse(StatusCodeType.Error, $"添加合作伙伴编码 {parm.mc} 已存在，不能重复！");
            }
            if (_partnerService.Any(m => m.mc == parm.mc))
            {
                return toResponse(StatusCodeType.Error, $"添加合作伙伴名称 {parm.mc} 已存在，不能重复！");
            }

            //从 Dto 映射到 实体
            var company = parm.Adapt<uf_hzhb>().ToCreate(_tokenManager.GetSessionInfo());

            return toResponse(_partnerService.Add(company));
        }

        /// <summary>
        /// 更新合作伙伴
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_PARTNER_UPDATE")]
        public IActionResult Update([FromBody] PartnerUpdateDto parm)
        {
            if (_partnerService.Any(m => m.bm == parm.bm))
            {
                return toResponse(StatusCodeType.Error, $"更新合作伙伴编码{parm.mc} 已存在，不能重复！");
            }
            if (_partnerService.Any(m => m.mc == parm.mc && m.ID != parm.ID))
            {
                return toResponse(StatusCodeType.Error, $"更新合作伙伴名称 {parm.mc} 已存在，不能重复！");
            }

            var userSession = _tokenManager.GetSessionInfo();

            return toResponse(_partnerService.Update(m => m.ID == parm.ID, m => new uf_hzhb()
            {
                mc = parm.mc,
                UpdateTime = DateTime.Now
            }));
        }

        /// <summary>
        /// 删除合作伙伴
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_PARTNER_DELETE")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "删除 Id 不能为空");
            }

            if (_ufkhxxService.Any(m => m.hzdw == id))
            {
                return toResponse(StatusCodeType.Error, "该合作伙伴已被客户信息关联，无法删除，若要请先删除关联");
            }

            var response = _partnerService.Delete(id);

            return toResponse(response);
        }
    }
}
