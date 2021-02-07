/*
* ==============================================================================
*
* FileName: OptionsDto.cs
* Created: 2020/6/2 13:15:22
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
    /// 审核类型
    /// </summary>
    public class AuditTypeQueryDto : PageParm
    {
        /// <summary>
        /// 描述 : 查询选项
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "查询字符串")]
        public string QueryText { get; set; }
    }

    /// <summary>
    /// 查询选项定义(不分页)
    /// </summary>
    public class AuditTypeQueryAllDto
    {
        /// <summary>
        /// 描述 : 查询选项
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "查询字符串")]
        public string QueryText { get; set; }
    }

    /// <summary>
    /// 新增类别
    /// </summary>
    public class AuditTypeCreateDto
    {
        /// <summary>
        /// 描述 : 编码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "编码")]
        public string AuditTypeCode { get; set; }

        /// <summary>
        /// 描述 : 名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "名称")]
        public string AuditTypeName { get; set; }

        /// <summary>
        /// 描述 : 名称(英文) 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "名称(英文)")]
        public string AuditTypeNameEN { get; set; }

        /// <summary>
        /// 描述 : 排序 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "排序")]
        public int? SortIndex { get; set; }

        /// <summary>
        /// 描述 : 是否启用 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "是否启用")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

    }

    /// <summary>
    /// 更新类别
    /// </summary>
    public class AuditTypeUpdateDto
    {

        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]

        public string ID { get; set; }

        /// <summary>
        /// 描述 : 编码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "编码")]
        [Required(ErrorMessage = "编码不能为空")]
        public string AuditTypeCode { get; set; }

        /// <summary>
        /// 描述 : 名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "名称")]
        public string AuditTypeName { get; set; }

        /// <summary>
        /// 描述 : 名称(英文) 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "名称(英文)")]
        public string AuditTypeNameEN { get; set; }

        /// <summary>
        /// 描述 : 排序 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "排序")]
        public int? SortIndex { get; set; }

        /// <summary>
        /// 描述 : 是否启用 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "是否启用")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}
