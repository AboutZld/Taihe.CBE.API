using Mapster;
using TaiheSystem.CBE.Api.Hostd.Authorization;
using TaiheSystem.CBE.Api.Hostd.Extensions;
using TaiheSystem.CBE.Api.Interfaces;
using TaiheSystem.CBE.Api.Model;
using TaiheSystem.CBE.Api.Model.Dto;
using TaiheSystem.CBE.Api.Model.View;
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
    /// 部门接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeptController : BaseController
    {
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<DeptController> _logger;

        /// <summary>
        /// 菜单接口
        /// </summary>
        private readonly IOrgDepartmentService _menuService;

        public DeptController(TokenManager tokenManager, IOrgDepartmentService menuService, ILogger<DeptController> logger)
        {
            _tokenManager = tokenManager;
            _menuService = menuService;
            _logger = logger;
        }


        /// <summary>
        /// 查询部门列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Query([FromBody] DeptsQueryDto parm)
        {
            if (string.IsNullOrEmpty(parm.QueryText) )
            {
                //获取系统所有菜单
                var allMenus = _menuService.GetAll().OrderBy(m=> m.SortIndex == null ? 0 : (int)m.SortIndex).ToList();

                return toResponse(ResolveDeptTree(allMenus));
            }

            //开始拼装查询条件
            var predicate = Expressionable.Create<Org_Department>();

            predicate = predicate.And(m => m.DeptName.Contains(parm.QueryText) && m.DeptNo.Contains(parm.QueryText));

            //获取系统所有菜单
            var serachMenus = (_menuService.GetWhere(predicate.ToExpression(), m => m.SortIndex)).Select(m => new DeptListVM
            {
                ID = m.ID,
                DeptNo = m.DeptNo,
                DeptName = m.DeptName,
                DeptAbbreviation = m.DeptAbbreviation,
                DeptDescr = m.DeptDescr,
                SortIndex = m.SortIndex,
                ParentUID = m.ParentUID,
                Children = null,
                CreateTime = m.CreateTime,
                UpdateTime = m.UpdateTime,
                CreateID = m.CreateID,
                CreateName = m.CreateName,
                UpdateID = m.UpdateID,
                UpdateName = m.UpdateName
            });

            return toResponse(serachMenus);
        }


        /// <summary>
        /// 返回部门信息
        /// </summary>
        /// <param name="id">部门id</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult Get(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "部门 Id 不能为空");
            }
            var response = _menuService.GetId(id);

            return toResponse(response);
        }

        /// <summary>
        /// 获取部门用户
        /// </summary>
        /// <param name="id">部门id</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult GetDeptUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return toResponse(StatusCodeType.Error, "部门 Id 不能为空");
            }
            List<Sys_Users> userslist = Core.DbContext.Current.Ado.SqlQuery<Sys_Users>("select * from Sys_Users where ID in (select UserID from Org_User_Department where DeptID=@ID)", new { ID = id });

            return toResponse(userslist);
        }
    }
}
