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
    /// 查询字典定义
    /// </summary>
    public class UserClassificationQueryDto
    {
        /// <summary>
        /// 描述 : 查询字符串 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "查询字符串")]
        public string QueryText { get; set; }
    }

    /// <summary>
    /// 新增字典定义
    /// </summary>
    public class UserClassificationCreateDto
    {
        /// <summary>
        /// 描述 : 注册资格ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "注册资格ID")]
        public string UserSystemTypeID { get; set; }

        /// <summary>
        /// 描述 : 用户ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "用户ID")]
        public string UserID { get; set; }

        /// <summary>
        /// 描述 : 专业代码ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "专业代码ID")]
        public string BizClassificationID { get; set; }

        /// <summary>
        /// 描述 : 分组代码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分组代码")]
        public string ClassificationCode { get; set; }

        /// <summary>
        /// 描述 : 专业代码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "专业代码")]
        public string ClassificationReportCode { get; set; }

        /// <summary>
        /// 描述 : 能力来源选项index 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "能力来源选项index")]
        public int? AbilityTypeIndex { get; set; }

        /// <summary>
        /// 描述 : 能力来源名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "能力来源名称")]
        public string AbilityTypeName { get; set; }

        /// <summary>
        /// 描述 : 评定人员ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "评定人员ID")]
        public string AccessPersonID { get; set; }

        /// <summary>
        /// 描述 : 评定人员名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "评定人员名称")]
        public string AccessPersonName { get; set; }

        /// <summary>
        /// 描述 : 通过日期 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "通过日期")]
        public DateTime? PassDate { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }

    /// <summary>
    /// 更新选项定义
    /// </summary>
    public class UserClassificationUpdateDto
    {

        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        public string ID { get; set; }

        /// <summary>
        /// 描述 : 注册资格ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "注册资格ID")]
        public string UserSystemTypeID { get; set; }

        /// <summary>
        /// 描述 : 用户ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "用户ID")]
        public string UserID { get; set; }

        /// <summary>
        /// 描述 : 专业代码ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "专业代码ID")]
        public string BizClassificationID { get; set; }

        /// <summary>
        /// 描述 : 分组代码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分组代码")]
        public string ClassificationCode { get; set; }

        /// <summary>
        /// 描述 : 专业代码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "专业代码")]
        public string ClassificationReportCode { get; set; }

        /// <summary>
        /// 描述 : 能力来源选项index 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "能力来源选项index")]
        public int? AbilityTypeIndex { get; set; }

        /// <summary>
        /// 描述 : 能力来源名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "能力来源名称")]
        public string AbilityTypeName { get; set; }

        /// <summary>
        /// 描述 : 评定人员ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "评定人员ID")]
        public string AccessPersonID { get; set; }

        /// <summary>
        /// 描述 : 评定人员名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "评定人员名称")]
        public string AccessPersonName { get; set; }

        /// <summary>
        /// 描述 : 通过日期 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "通过日期")]
        public DateTime? PassDate { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}
