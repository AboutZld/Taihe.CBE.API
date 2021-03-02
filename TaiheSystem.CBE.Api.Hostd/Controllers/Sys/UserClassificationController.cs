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
using System.Collections.Generic;
using TaiheSystem.CBE.Api.Model.View;

namespace TaiheSystem.CBE.Api.Hostd.Controllers.Sys
{
    /// <summary>
    /// 业务代码管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserClassificationController : BaseController
    {
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<UserClassificationController> _logger;

        /// <summary>
        /// 注册资格管理
        /// </summary>
        private readonly ISysUserSystemTypeService _usersystemtypeService;

        private readonly ISysUserSystemTypeStandardService _usersystemtypestandardService;

        /// <summary>
        /// 业务代码管理
        /// </summary>
        private readonly ISysUserClassificationService _userclassificationService;

        public UserClassificationController(TokenManager tokenManager, ISysUserSystemTypeService usersystemtypeService, ILogger<UserClassificationController> logger, ISysUserSystemTypeStandardService usersystemtypestandardService, ISysUserClassificationService userclassificationService)
        {
            _tokenManager = tokenManager;
            _usersystemtypeService = usersystemtypeService;
            _usersystemtypestandardService = usersystemtypestandardService;
            _userclassificationService = userclassificationService;
            _logger = logger;
        }


        /// <summary>
        /// 查询当前人的业务类别代码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult GetListbyUserID(string UserID)
        {
            if(string.IsNullOrEmpty(UserID))
            {
                return toResponse(StatusCodeType.Error, "UserID不能为空，请核对！");
            }
            var response = _userclassificationService.GetWhere(m=>m.UserID == UserID);

            return toResponse(response);
        }

        /// <summary>
        /// 查询当前人注册资格下的业务类别代码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult GetListbySystemTyID(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "id不允许为空，请核对！");
            }
            //var response = _userclassificationService.GetWhere(m => m.UserSystemTypeID == id);
            var response = Core.DbContext.CurrentDB.Ado.SqlQuery<UserClassificationVM>(@"
select uc.*,us.SystemTypeName,us.QualificationTypeName from Sys_User_Classification uc 
inner join Sys_User_SystemType us on uc.UserSystemTypeID = us.ID
inner join Abi_BizClassification bc on bc.ID = uc.BizClassificationID
where uc.UserSystemTypeID = @id
",new { id = id}).ToList();

            return toResponse(response);
        }


        /// <summary>
        /// 获取业务代码详情
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "id为空，请核对！");
            }
            var ret = _userclassificationService.GetId(id);

            return toResponse(ret);
        }

        /// <summary>
        /// 添加业务代码
        /// Power = PRIV_USERCLASSIFICATION_CREATE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_USERCLASSIFICATION_CREATE")]
        //[Authorization]
        public IActionResult Create([FromBody] UserClassificationCreateDto parm)
        {
            if (_userclassificationService.Any(m => m.BizClassificationID == parm.BizClassificationID && m.UserSystemTypeID == parm.UserSystemTypeID))
            {
                return toResponse(StatusCodeType.Error, $"当前用户下当前业务代码 {parm.ClassificationCode} 已存在，不能重复！");
            }

            //从 Dto 映射到 实体
            var options = parm.Adapt<Sys_User_Classification>().ToCreate(_tokenManager.GetSessionInfo());

            return toResponse(_userclassificationService.Add(options));
        }

        /// <summary>
        /// 更新/添加业务代码
        /// Power = PRIV_USERCLASSIFICATION_UPDATE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_USERCLASSIFICATION_UPDATE")]
        //[Authorization]
        public IActionResult Update([FromBody] UserClassificationUpdateDto parm)
        {
            var userSession = _tokenManager.GetSessionInfo();

            //判断是更新还是新增
            if (string.IsNullOrEmpty(parm.ID))
            {
                if (_userclassificationService.Any(m => m.BizClassificationID == parm.BizClassificationID && m.UserSystemTypeID == parm.UserSystemTypeID))
                {
                    return toResponse(StatusCodeType.Error, $"当前用户下当前业务代码 {parm.ClassificationCode} 已存在，不能重复！");
                }

                //从 Dto 映射到 实体
                var options = parm.Adapt<Sys_User_Classification>().ToCreate(_tokenManager.GetSessionInfo());

                return toResponse(_userclassificationService.Add(options));
            }
            else
            {
                _userclassificationService.Update(m => m.ID == parm.ID, m => new Sys_User_Classification()
                {
                    BizClassificationID = parm.BizClassificationID,
                    ClassificationCode = parm.ClassificationCode,
                    AbilityTypeIndex = parm.AbilityTypeIndex,
                    AbilityTypeName = parm.AbilityTypeName,
                    AccessPersonID = parm.AccessPersonID,
                    AccessPersonName = parm.AccessPersonName,
                    PassDate = parm.PassDate,
                    Remark = parm.Remark,
                    UpdateID = userSession.ID,
                    UpdateName = userSession.UserName,
                    UpdateTime = DateTime.Now
                }) ;

                return toResponse(parm.ID);
            }
        }

        /// <summary>
        /// 删除业务代码
        /// Power = PRIV_USERCLASSIFICATION_DELETE
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorization(Power = "PRIV_USERCLASSIFICATION_DELETE")]
        //[Authorization]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "删除 Id 不能为空");
            }

            var response = _userclassificationService.Delete(id);

            return toResponse(response);
        }
    }
}
