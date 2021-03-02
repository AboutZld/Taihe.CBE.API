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

namespace TaiheSystem.CBE.Api.Hostd.Controllers.Sys
{
    /// <summary>
    /// 注册资格管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserSystemtypeController : BaseController
    {
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<UserSystemtypeController> _logger;

        /// <summary>
        /// 注册资格管理
        /// </summary>
        private readonly ISysUserSystemTypeService _usersystemtypeService;

        private readonly ISysUserSystemTypeStandardService _usersystemtypestandardService;

        /// <summary>
        /// 业务代码管理
        /// </summary>
        private readonly ISysUserClassificationService _userclassificationService;

        public UserSystemtypeController(TokenManager tokenManager, ISysUserSystemTypeService usersystemtypeService, ILogger<UserSystemtypeController> logger, ISysUserSystemTypeStandardService usersystemtypestandardService, ISysUserClassificationService userclassificationService)
        {
            _tokenManager = tokenManager;
            _usersystemtypeService = usersystemtypeService;
            _usersystemtypestandardService = usersystemtypestandardService;
            _userclassificationService = userclassificationService;
            _logger = logger;
        }

        /// <summary>
        /// 查询注册资格列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Query([FromBody]UserSystemTypeQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Sys_User_SystemType>();

            var response = _usersystemtypeService.GetPages(predicate.ToExpression(), parm);

            return toResponse(response);
        }

        /// <summary>
        /// 查询当前人的注册资格
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
            var response = _usersystemtypeService.GetWhere(m=>m.UserID == UserID);

            return toResponse(response);
        }


        /// <summary>
        /// 获取人员注册资格详情
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
            var ret = _usersystemtypeService.GetId(id);
            if(ret == null)
            {
                return toResponse(StatusCodeType.Error, "当前数据不存在，请核对！");
            }
            UserSystemTypeUpdateDto datainfo = Api.Common.Helpers.ComHelper.Mapper<UserSystemTypeUpdateDto, Sys_User_SystemType>(ret);
            datainfo.UserSystemTypeStandardList = _usersystemtypestandardService.GetWhere(m=>m.UserSystemTypeID == ret.ID);

            return toResponse(datainfo);
        }

        /// <summary>
        /// 添加注册资格
        /// Power = PRIV_USERSYSTEMTYPE_CREATE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_USERSYSTEMTYPE_CREATE")]
        //[Authorization]
        public IActionResult Create([FromBody] UserSystemTypeCreateDto parm)
        {
            if (_usersystemtypeService.Any(m => m.SystemTypeID == parm.SystemTypeID && m.UserID == parm.UserID))
            {
                return toResponse(StatusCodeType.Error, $"当前用户下当前资质证书 {parm.SystemTypeName} 已存在，不能重复！");
            }

            //从 Dto 映射到 实体
            var options = parm.Adapt<Sys_User_SystemType>().ToCreate(_tokenManager.GetSessionInfo());

            return toResponse(_usersystemtypeService.Add(options));
        }

        /// <summary>
        /// 更新注册资格
        /// Power = PRIV_USERSYSTEMTYPE_UPDATE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_USERSYSTEMTYPE_UPDATE")]
        //[Authorization]
        public IActionResult Update([FromBody] UserSystemTypeUpdateDto parm)
        {
            var userSession = _tokenManager.GetSessionInfo();

            //判断是更新还是新增
            if (string.IsNullOrEmpty(parm.ID))
            {
                if (_usersystemtypeService.Any(m => m.SystemTypeID == parm.SystemTypeID && m.UserID == parm.UserID))
                {
                    return toResponse(StatusCodeType.Error, $"当前用户下当前资质证书 {parm.SystemTypeName} 已存在，不能重复！");
                }

                //从 Dto 映射到 实体
                var options = parm.Adapt<Sys_User_SystemType>().ToCreate(_tokenManager.GetSessionInfo());
                List<Sys_User_SystemType_Standard> standardlist_insert = parm.UserSystemTypeStandardList;//插入

                //复制标准名称聚合
                options.StandardNames = string.Join(",",standardlist_insert.Select(m => m.SysStandardName));

                _usersystemtypeService.Add(options);
                //插入类别关联标准
                foreach (var standard in standardlist_insert)
                {
                    var insert = standard.Adapt<Sys_User_SystemType_Standard>().ToCreate(_tokenManager.GetSessionInfo());
                    insert.UserSystemTypeID = options.ID;
                    _usersystemtypestandardService.Add(insert);
                }


                return toResponse(options.ID);
            }
            else
            {
                List<Sys_User_SystemType_Standard> standardlist_delete = parm.UserSystemTypeStandardList.Where(m=> !string.IsNullOrEmpty(m.ID)).ToList();//删除
                List<Sys_User_SystemType_Standard> standardlist_update = parm.UserSystemTypeStandardList.Where(m => !string.IsNullOrEmpty(m.ID)).ToList();//更新
                List<Sys_User_SystemType_Standard> standardlist_insert = parm.UserSystemTypeStandardList.Where(m => string.IsNullOrEmpty(m.ID)).ToList();//插入

                //删除标准信息
                string[] ids = standardlist_delete.Where(x => !string.IsNullOrEmpty(x.ID)).Select(x => x.ID).ToArray();
                _usersystemtypestandardService.Delete(m => m.UserSystemTypeID == parm.ID && !ids.Contains(m.ID));

                //更新标准
                foreach (var standard in standardlist_update)
                {
                    _usersystemtypestandardService.Update(m => m.ID == standard.ID, m => new Sys_User_SystemType_Standard()
                    {
                        SysStandardID = standard.SysStandardID,
                        SysStandardName = standard.SysStandardName,
                        Enabled = standard.Enabled
                    });
                }

                //插入标准
                foreach (var standard in standardlist_insert)
                {
                    var insert = standard.Adapt<Sys_User_SystemType_Standard>().ToCreate(_tokenManager.GetSessionInfo());
                    insert.UserSystemTypeID = parm.ID;
                    _usersystemtypestandardService.Add(insert);
                }
                standardlist_update.AddRange(standardlist_insert);
                string standardnames = string.Join(",", standardlist_update.Select(m => m.SysStandardName));

                _usersystemtypeService.Update(m => m.ID == parm.ID, m => new Sys_User_SystemType()
                {
                    SystemTypeID = parm.SystemTypeID,
                    SystemTypeCode = parm.SystemTypeCode,
                    SystemTypeName = parm.SystemTypeName,
                    StandardNames = standardnames,
                    StandardDate = parm.StandardDate,
                    QualificationTypeID = parm.QualificationTypeID,
                    QualificationTypeName = parm.QualificationTypeName,
                    RegisterQualificationNo = parm.RegisterQualificationNo,
                    QualificationStartDate = parm.QualificationStartDate,
                    QualificationEndedDate = parm.QualificationEndedDate,
                    GroupLeaderType = parm.GroupLeaderType,
                    WitnessType = parm.WitnessType,
                    Remark = parm.Remark,
                    Enabled = parm.Enabled,
                    UpdateID = userSession.ID,
                    UpdateName = userSession.UserName,
                    UpdateTime = DateTime.Now
                }) ;

                return toResponse(parm.ID);
            }
        }

        /// <summary>
        /// 删除注册资格
        /// Power = PRIV_USERSYSTEMTYPE_DELETE
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorization(Power = "PRIV_USERSYSTEMTYPE_DELETE")]
        //[Authorization]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "删除 Id 不能为空");
            }

            var response = _usersystemtypeService.Delete(id);
            _usersystemtypestandardService.Delete(m=>m.UserSystemTypeID == id);
            _userclassificationService.Delete(m => m.UserSystemTypeID == id);
            return toResponse(response);
        }
    }
}
