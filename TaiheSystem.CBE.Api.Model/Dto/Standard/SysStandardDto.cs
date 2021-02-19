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
    /// 认证体系类别
    /// </summary>
    public class SysStandardQueryDto : PageParm
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
    public class SysStandardQueryAllDto 
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
    /// 按照业务体系类别查询
    /// </summary>
    public class SystemType2QueryDto : PageParm
    {
        /// <summary>
        /// 描述 : 查询选项
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "认证体系类别ID")]
        [Required(ErrorMessage = "认证体系类别ID不能为空")]
        public string SystemTypeID { get; set; }

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
    public class SysStandardCreateDto
    {
        /// <summary>
        /// 描述 : 体系类别 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系类别")]
        public string SystemTypeID { get; set; }

        /// <summary>
        /// 描述 : 类别代码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "类别代码")]
        public string SystemTypeCode { get; set; }

        /// <summary>
        /// 描述 : 类别名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "类别名称")]
        public string SystemTypeName { get; set; }

        /// <summary>
        /// 描述 : 编码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "编码")]
        [Required(ErrorMessage = "编码不能为空")]
        public string SysStandardCode { get; set; }

        /// <summary>
        /// 描述 : 上报编码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "上报编码")]
        [Required(ErrorMessage = "上报编码不能为空")]
        public string SysStandardReportCode { get; set; }

        /// <summary>
        /// 描述 : 简称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "简称")]
        public string SysStandardShortName { get; set; }

        /// <summary>
        /// 描述 : 认证标准 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "认证标准")]
        public string SysStandardName { get; set; }

        /// <summary>
        /// 描述 : 代号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "代号")]
        public string SysStandardNo { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 描述 : 备注英文 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注英文")]
        public string RemarkEN { get; set; }

        /// <summary>
        /// 描述 : 截止时间 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "截止时间")]
        public DateTime? DeadLine { get; set; }

        /// <summary>
        /// 描述 : 是否启用 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "是否启用")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// 描述 : 编码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "编码")]
        public int? SortIndex { get; set; }

    }

    /// <summary>
    /// 更新类别
    /// </summary>
    public class SysStandardUpdateDto
    {

        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        public string ID { get; set; }

        /// <summary>
        /// 描述 : 体系类别 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系类别")]
        public string SystemTypeID { get; set; }

        /// <summary>
        /// 描述 : 类别代码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "类别代码")]
        public string SystemTypeCode { get; set; }

        /// <summary>
        /// 描述 : 类别名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "类别名称")]
        public string SystemTypeName { get; set; }

        /// <summary>
        /// 描述 : 编码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "编码")]
        [Required(ErrorMessage = "编码不允许为空")]
        public string SysStandardCode { get; set; }

        /// <summary>
        /// 描述 : 上报编码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "上报编码")]
        [Required(ErrorMessage = "上报编码不能为空")]
        public string SysStandardReportCode { get; set; }

        /// <summary>
        /// 描述 : 简称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "简称")]
        public string SysStandardShortName { get; set; }

        /// <summary>
        /// 描述 : 认证标准 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "认证标准")]
        public string SysStandardName { get; set; }

        /// <summary>
        /// 描述 : 代号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "代号")]
        public string SysStandardNo { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 描述 : 备注英文 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注英文")]
        public string RemarkEN { get; set; }

        /// <summary>
        /// 描述 : 截止时间 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "截止时间")]
        public DateTime? DeadLine { get; set; }

        /// <summary>
        /// 描述 : 是否启用 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "是否启用")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// 描述 : 编码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "编码")]
        public int? SortIndex { get; set; }
    }
}
