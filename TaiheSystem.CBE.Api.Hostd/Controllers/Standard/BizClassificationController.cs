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
    /// 业务类别
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BizClassificationController : BaseController
    {
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<BizClassificationController> _logger;

        /// <summary>
        /// 业务代码接口
        /// </summary>
        private readonly IAbiBizClassificationService _bizclassService;


        public BizClassificationController(TokenManager tokenManager, IAbiBizClassificationService bizclaseeService, ILogger<BizClassificationController> logger)
        {
            _tokenManager = tokenManager;
            _bizclassService = bizclaseeService;
            _logger = logger;
        }


        /// <summary>
        /// 查询列表（分页）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Query([FromBody] BizClassificationQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Abi_BizClassification>();

            //体系编号
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.SystemTypeID), m => m.SystemTypeID == parm.SystemTypeID);
            //分组代码
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ClassificationCode), m => m.ClassificationCode.Contains(parm.ClassificationCode));
            //上报代码
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ClassificationReportCode), m => m.ClassificationReportCode.Contains(parm.ClassificationReportCode));
            //内容
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ClassificationName), m => m.ClassificationName.Contains(parm.ClassificationName));
            //行业
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Industry), m => m.Industry.Contains(parm.Industry));

            var response = _bizclassService.GetPages(predicate.ToExpression(), parm);

            return toResponse(response);
        }

        /// <summary>
        /// 查询列表(已启用)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult QueryAll([FromBody] BizClassificationQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Abi_BizClassification>();

            predicate = predicate.And(m => m.Enabled == true);
            //分组代码
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ClassificationCode), m => m.ClassificationCode.Contains(parm.ClassificationCode) || m.ClassificationCode.Contains(parm.ClassificationCode));
            //上报代码
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ClassificationReportCode), m => m.ClassificationReportCode.Contains(parm.ClassificationReportCode) || m.ClassificationReportCode.Contains(parm.ClassificationReportCode));
            //内容
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ClassificationName), m => m.ClassificationName.Contains(parm.ClassificationName) || m.ClassificationName.Contains(parm.ClassificationName));
            //行业
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Industry), m => m.Industry.Contains(parm.Industry) || m.Industry.Contains(parm.Industry));

            var response = _bizclassService.GetPages(predicate.ToExpression(),parm);

            return toResponse(response);
        }

        /// <summary>
        /// 查询列表(已启用)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult QueryBySystemType([FromBody] BizClassificationQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Abi_BizClassification>();

            if(string.IsNullOrEmpty(parm.SystemTypeID))
            {
                return toResponse(StatusCodeType.Error, "体系类别ID不允许为空");
            }

            predicate = predicate.And(m => m.Enabled == true);
            //分组代码
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ClassificationCode), m => m.ClassificationCode.Contains(parm.ClassificationCode) || m.ClassificationCode.Contains(parm.ClassificationCode));
            //上报代码
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ClassificationReportCode), m => m.ClassificationReportCode.Contains(parm.ClassificationReportCode) || m.ClassificationReportCode.Contains(parm.ClassificationReportCode));
            //内容
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ClassificationName), m => m.ClassificationName.Contains(parm.ClassificationName) || m.ClassificationName.Contains(parm.ClassificationName));
            //行业
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Industry), m => m.Industry.Contains(parm.Industry) || m.Industry.Contains(parm.Industry));

            var response = _bizclassService.GetPages(predicate.ToExpression(), parm);

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
            return toResponse(_bizclassService.GetId(id));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_BIZCLASS_CREATE")]
        [Authorization]
        public IActionResult Create([FromBody] BizClassificationCreateDto parm)
        {
            if (_bizclassService.Any(m => m.SystemTypeID == parm.SystemTypeID && m.ClassificationCode == parm.ClassificationCode))
            {
                return toResponse(StatusCodeType.Error, $"当前体系下{parm.SystemTypeID}分组代码{parm.ClassificationCode} 已存在，不能重复！");
            }
            //从 Dto 映射到 实体
            var options = parm.Adapt<Abi_BizClassification>().ToCreate(_tokenManager.GetSessionInfo());

            return toResponse(_bizclassService.Add(options));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_BIZCLASS_UPDATE")]
        //[Authorization]
        public IActionResult Update([FromBody] BizClassificationUpdateDto parm)
        {
            var userSession = _tokenManager.GetSessionInfo();

            if (string.IsNullOrEmpty(parm.ID))
            {
                if (_bizclassService.Any(m => m.SystemTypeID == parm.SystemTypeID && m.ClassificationCode == parm.ClassificationCode))
                {
                    return toResponse(StatusCodeType.Error, $"当前体系下{parm.SystemTypeID}分组代码{parm.ClassificationCode} 已存在，不能重复！");
                }
                //从 Dto 映射到 实体
                var options = parm.Adapt<Abi_BizClassification>().ToCreate(_tokenManager.GetSessionInfo());

                return toResponse(_bizclassService.Add(options));
            }
            else
            {
                return toResponse(_bizclassService.Update(m => m.ID == parm.ID, m => new Abi_BizClassification()
                {
                    SystemTypeID = parm.SystemTypeID,
                    SystemTypeCode = parm.SystemTypeCode,
                    SystemTypeName = parm.SystemTypeName,
                    ClassificationCode = parm.ClassificationCode,
                    SortIndex = parm.SortIndex,
                    ClassificationReportCode = parm.ClassificationReportCode,
                    ClassificationName = parm.ClassificationName,
                    ClassificationNameEN = parm.ClassificationNameEN,
                    Industry = parm.Industry,
                    RiskRegisterID = parm.RiskRegisterID,
                    RiskRegister = parm.RiskRegister,
                    CNAS = parm.CNAS,
                    Remark = parm.Remark,
                    Enabled = parm.Enabled,
                    UpdateID = userSession.UserID,
                    UpdateName = userSession.UserName,
                    UpdateTime = DateTime.Now
                }));
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_BIZCLASS_DELETE")]
        //[Authorization]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "删除 Id 不能为空");
            }

            var response = _bizclassService.Delete(id);

            return toResponse(response);
        }
    }
}
