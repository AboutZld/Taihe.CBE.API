/*
* ==============================================================================
*
* FileName: RolePowersDto.cs
* Created: 2020/6/3 10:52:45
* Author: Taihe
* Description: 
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaiheSystem.CBE.Api.Model.Dto
{
    public class RoleUsersCreateDto
    {
        /// <summary>
        /// 描述 : 角色id 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "角色id")]
        [Required(ErrorMessage = "roleId 不能为空")]
        public string RoleId { get; set; }

        /// <summary>
        /// 描述 : 用户编码 [1,2,3,4] 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "用户编码 [1,2,3,4]")]
        public List<string> UserIds { get; set; }
    }

    public class RoleUsersDeleteDto
    {
        /// <summary>
        /// 描述 : 角色id 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "角色id")]
        [Required(ErrorMessage = "roleId 不能为空")]
        public string RoleId { get; set; }

        /// <summary>
        /// 描述 : 权限编码 [1,2,3,4] 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "权限编码 [1,2,3,4]")]
        public List<string> UserIds { get; set; }
    }
}
