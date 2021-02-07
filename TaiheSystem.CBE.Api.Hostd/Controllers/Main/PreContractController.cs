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
using Newtonsoft.Json.Linq;
using TaiheSystem.CBE.Api.Model.View.Main;
using System.Collections.Generic;
using TaiheSystem.CBE.Api.Model.View;
using TaiheSystem.CBE.Api.Model.View.System;

namespace TaiheSystem.CBE.Api.Hostd.Controllers.Main
{
    /// <summary>
    /// OA合同管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PreContractController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<PreContractController> _logger;
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// OA合同接口
        /// </summary>
        private readonly IBizPreContractService _precontractService;

        /// <summary>
        /// 
        /// </summary>
        private readonly IufkhxxService _customerService;

        /// <summary>
        /// 外部服务接入验证
        /// </summary>
        private readonly ISysExtAccessService _accessService;


        public PreContractController(ILogger<PreContractController> logger, TokenManager tokenManager, IBizPreContractService precontractService, IufkhxxService customerService, ISysExtAccessService accessService)
        {
            _logger = logger;
            _tokenManager = tokenManager;
            _precontractService = precontractService;
            _customerService = customerService;
            _accessService = accessService;
        }


        /// <summary>
        /// 查询OA合同管理信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Query([FromBody] PreContractQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Biz_PreContract>();

            //登记状态
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.status), m => m.Status.ToString() == parm.status);
            //合同编号
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ContractNo), m => m.ContractNo.Contains(parm.ContractNo));
            //服务项目
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ServiceItem), m => m.ServiceItem.Contains(parm.ServiceItem));
            //客户名称
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.khmc), m => m.khmc.Contains(parm.khmc));
            //联系人
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.lxr), m => m.lxr.Contains(parm.lxr));

            var response = _precontractService.GetPages(predicate.ToExpression(), parm);

            return toResponse(response);
        }


        /// <summary>
        /// 合同登记初始信息
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult GetRegisterDetail(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "Id 不能为空");
            }
            if (!_precontractService.Any(m => m.ID == id && m.Status == 0))
            {
                return toResponse(StatusCodeType.Error, "当前合同信息不存在,请核对");
            }
            var oacontrcat = _precontractService.GetId(id);
            if (oacontrcat.ID != null)
            {
                MainContractCreateDto maincontract = new MainContractCreateDto();
                maincontract.PreContractID = oacontrcat.ID;
                maincontract.CustomerID = oacontrcat.khid;
                maincontract.ContractNo = oacontrcat.ContractNo;
                maincontract.ApplyDate = DateTime.Today;

                maincontract.ContractCustomerInfo = _customerService.GetId(oacontrcat.khid);
                maincontract.ContractItemList = new List<ContractItemVM>(); //合同项目结构
                maincontract.ContractfcsList = new List<FcsVM>();//分场所信息
                maincontract.ContractFileList = new List<FileVM>();//附件信息

                return toResponse(maincontract);
            }
            else
            {
                return toResponse(StatusCodeType.Error, "当前合同信息不存在,请核对");
            }
        }

        /// <summary>
        /// OA推送合同信息(单个)
        /// </summary>
        /// <param name="ExtName">分配账号名称</param>
        /// <param name="AuthToken">分配账号密钥</param>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost]
        //[Authorization(Power = "PRIV_PRECONTRACT_REGISTER")]
        public IActionResult AcceptContractSingle(string ExtName, string AuthToken, [FromBody] object parm)
        {
            var ret = _accessService.GetFirst(o => o.ExtName == ExtName);
            if (ret == null)
            {
                return toResponse(StatusCodeType.Error, "当前接入账号不存在");
            }

            if (ret.KeyValue != AuthToken)
            {
                return toResponse(StatusCodeType.Error, "当前接入账号AuthToken验证失败，请联系管理员！");
            }
            

            JObject contractjson = null;
            try
            {
                contractjson = parm as JObject;
            }
            catch
            {
                contractjson = Api.Common.Helpers.JsonHelpers.DeserializeJson<JObject>(parm.ToString());
            }

            if (_precontractService.Any(m => m.ContractNo == contractjson["htmc"].ToString()))
            {
                return toResponse(StatusCodeType.Error, $"添加合同编号 {contractjson["htmc"].ToString()} 已存在，不能重复！");
            }

            //生成客户信息实体
            Biz_PreContract contract = new Biz_PreContract();
            contract.ID = Guid.NewGuid().ToString();
            contract.ContractNo = contractjson["htmc"].ToString();

            contract.CreateTime = DateTime.Now;
            contract.CreateName = ExtName;

            return toResponse(_precontractService.Add(contract));
        }



        /// <summary>
        /// 删除OA合同
        /// Power = PRIV_PRECONTRACT_DELETE
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_PRECONTRACT_DELETE")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "删除公司 Id 不能为空");
            }

            if (_precontractService.Any(m => m.ID == id && m.Status == 1))
            {
                return toResponse(StatusCodeType.Error, "该合同已经登记，无法删除");
            }

            var response = _precontractService.Delete(id);

            return toResponse(response);
        }
    }
}
