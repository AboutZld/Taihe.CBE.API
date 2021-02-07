/*
* ==============================================================================
*
* FileName: MenusDto.cs
* Created: 2020/5/26 20:13:31
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
    /// <summary>
    /// 查询部门
    /// </summary>
    public class DeptsQueryDto
    {
        /// <summary>
        /// 描述 : 部门信息
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "部门信息")]
        public string QueryText { get; set; }
    }

    /// <summary>
    /// 添加部门
    /// </summary>
    public class DeptsCreateDto
    {

        /// <summary>
        /// 描述 : 菜单名称 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "部门名称")]
        [Required(ErrorMessage = "请填写部门名称")]
        public string DeptName { get; set; }

        /// <summary>
        /// 描述 : 部门代号
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "部门代号")]
        [Required(ErrorMessage = "请填写部门代号")]
        public string DeptNo { get; set; }

        /// <summary>
        /// 描述 : 部门简称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "部门简称")]
        public string DeptAbbreviation { get; set; }

        /// <summary>
        /// 描述 : 默认排序 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "默认排序")]
        [Required(ErrorMessage = "请设置默认排序编号")]
        public int SortIndex { get; set; }

        /// <summary>
        /// 描述 : 上级菜单 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "上级菜单")]
        public string ParentUID { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string DeptDescr { get; set; }
    }

   /// <summary>
   /// 更新部门
   /// </summary>
    public class DeptsUpdateDto
    {

        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        [Required(ErrorMessage = "更新菜单编码不能为空")]
        public string ID { get; set; }

        /// <summary>
        /// 描述 : 菜单名称 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "部门名称")]
        [Required(ErrorMessage = "请填写部门名称")]
        public string DeptName { get; set; }

        /// <summary>
        /// 描述 : 部门代号
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "部门代号")]
        [Required(ErrorMessage = "请填写部门代号")]
        public string DeptNo { get; set; }

        /// <summary>
        /// 描述 : 部门简称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "部门简称")]
        public string DeptAbbreviation { get; set; }

        /// <summary>
        /// 描述 : 默认排序 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "默认排序")]
        [Required(ErrorMessage = "请设置默认排序编号")]
        public int SortIndex { get; set; }

        /// <summary>
        /// 描述 : 上级菜单 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "上级菜单")]
        public string ParentUID { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string DeptDescr { get; set; }
    }
}
