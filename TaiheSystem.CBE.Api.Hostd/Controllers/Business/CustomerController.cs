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
    /// 客户信息
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<CustomerController> _logger;
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 客户信息接口
        /// </summary>
        private readonly IufkhxxService _customerService;

        /// <summary>
        /// 联系人接口
        /// </summary>
        private readonly Iufkhxxdt1Service  _customerlinkService;


        public CustomerController(ILogger<CustomerController> logger, TokenManager tokenManager, IufkhxxService customerService, Iufkhxxdt1Service dataRelationService)
        {
            _logger = logger;
            _tokenManager = tokenManager;
            _customerService = customerService;
            _customerlinkService = dataRelationService;
        }


        /// <summary>
        /// 查询客户信息列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[Authorization]
        public IActionResult Query([FromBody] CustomerQueryDto parm)
        {
            var response = _customerService.QueryCustomerPages(parm);

            return toResponse(response);
        }


        /// <summary>
        /// 根据 Id 查询客户信息
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpGet]
        //[Authorization]
        public IActionResult Get(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error," Id 不能为空");
            }
            return toResponse(_customerService.GetId(id));
        }

        /// <summary>
        /// 查询所有客户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult GetAll()
        {
            var predicate = Expressionable.Create<uf_hzhb>();

            return toResponse(_customerService.GetAll());
        }

        /// <summary>
        /// 添加客户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_CUSTOMER_CREATE")]
        public IActionResult Create([FromBody] CustomerCreateDto parm)
        {
            if (_customerService.Any(m => m.bm == parm.zzmc))
            {
                return toResponse(StatusCodeType.Error, $"客户组织名称 {parm.zzmc} 已存在，不能重复！");
            }

            //从 Dto 映射到 实体
            var company = parm.Adapt<uf_khxx>().ToCreate(_tokenManager.GetSessionInfo());

            return toResponse(_customerService.Add(company));
        }

        /// <summary>
        /// 更新客户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_CUSTOMER_UPDATE")]
        public IActionResult Update([FromBody] CustomerUpdateDto parm)
        {
            if (_customerService.Any(m => m.bm == parm.zzmc))
            {
                return toResponse(StatusCodeType.Error, $"客户组织名称{parm.zzmc} 已存在，不能重复！");
            }

            var userSession = _tokenManager.GetSessionInfo();
            _customerService.Update(m => m.ID == parm.ID, m => new uf_khxx()
            {
                zzmc = parm.zzmc,
                UpdateTime = DateTime.Now
            });

            //删除联系人
            string[] ids = parm.CustomerLinkList.Where(x => !string.IsNullOrEmpty(x.ID)).Select(x => x.ID).ToArray();
            _customerlinkService.Delete(m=>m.ParentUID == parm.ID && !ids.Contains(m.ID));
            //修改联系人信息
            foreach (var link in parm.CustomerLinkList)
            {
                //判断是否是新增
                if(string.IsNullOrEmpty(link.ID))
                {
                    var customerlink = link.Adapt<uf_khxx_dt1>().ToCreate(_tokenManager.GetSessionInfo());
                    _customerlinkService.Add(customerlink);
                }
                else
                {
                    _customerlinkService.Update(m=>m.ID == link.ID,m=>new uf_khxx_dt1() {
                        lxr = link.lxr,
                        bm = link.bm,
                        zw = link.zw,
                        zj = link.zj,
                        sj = link.sj,
                        yx = link.yx,
                        bz = link.bz,
                        UpdateTime = DateTime.Now
                    });
                }
            }

            return toResponse("更新成功");
        }

        /// <summary>
        /// 删除客户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_CUSTOMER_DELETE")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "删除 Id 不能为空");
            }

            var response = _customerService.Delete(id);

            //删除关联的联系人信息
            _customerlinkService.Delete(m => m.ParentUID == id);

            return toResponse(response);
        }
    }
}
