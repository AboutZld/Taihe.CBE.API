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
    /// 认证业务类别
    /// </summary>
    public class BizClassificationQueryDto : PageParm
    {
        /// <summary>
        /// 描述 : 认证体系编号
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "认证体系ID")]
        public string SystemTypeID { get; set; }

        /// <summary>
        /// 描述 : 分组代码
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "分组代码")]
        public string ClassificationCode { get; set; }

        /// <summary>
        /// 描述 : 上报代码
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "上报代码")]
        public string ClassificationReportCode { get; set; }

        /// <summary>
        /// 描述 : 名称
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "名称")]
        public string ClassificationName { get; set; }

        /// <summary>
        /// 描述 : 行业
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "行业")]
        public string Industry { get; set; }
    }

    /// <summary>
    /// 业务类别查询全部
    /// </summary>
    public class BizClassificationQueryAllDto 
    {
        /// <summary>
        /// 描述 : 分组代码
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "分组代码")]
        public string ClassificationCode { get; set; }

        /// <summary>
        /// 描述 : 上报代码
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "上报代码")]
        public string ClassificationReportCode { get; set; }

        /// <summary>
        /// 描述 : 名称
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "名称")]
        public string ClassificationName { get; set; }

        /// <summary>
        /// 描述 : 行业
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "行业")]
        public string Industry { get; set; }
    }


    /// <summary>
    /// 新增类别
    /// </summary>
    public class BizClassificationCreateDto
    {
        /// <summary>
        /// 描述 : 分类代码ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分类代码ID")]
        [Required(ErrorMessage = "分类代码ID不能为空")]
        public string SystemTypeID { get; set; }

        /// <summary>
        /// 描述 : 分类代码编码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分类代码编码")]
        public string SystemTypeCode { get; set; }

        /// <summary>
        /// 描述 : 分类代码名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分类代码名称")]
        public string SystemTypeName { get; set; }

        /// <summary>
        /// 描述 : 分组代码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分组代码")]
        [Required(ErrorMessage = "分组代码不能为空")]
        public string ClassificationCode { get; set; }

        /// <summary>
        /// 描述 : 上报代码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "上报代码")]
        [Required(ErrorMessage = "上报代码不能为空")]
        public string ClassificationReportCode { get; set; }

        /// <summary>
        /// 描述 : 名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "名称不能为空")]
        public string ClassificationName { get; set; }

        /// <summary>
        /// 描述 : 名称(英文) 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "名称(英文)")]
        public string ClassificationNameEN { get; set; }

        /// <summary>
        /// 描述 : 行业名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "行业名称")]
        public string Industry { get; set; }

        /// <summary>
        /// 描述 : 风险登记选项 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "风险登记选项")]
        public int? RiskRegisterID { get; set; }

        /// <summary>
        /// 描述 : 风险登记 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "风险登记")]
        public string RiskRegister { get; set; }

        /// <summary>
        /// 描述 : CNAS标识 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "CNAS标识")]
        public bool? CNAS { get; set; }

        /// <summary>
        /// 描述 : 是否启用 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "是否启用")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// 描述 : 排序 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "排序")]
        public int? SortIndex { get; set; }

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
    public class BizClassificationUpdateDto
    {

        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        [Required(ErrorMessage = "ID不能为空")]
        public string ID { get; set; }

        /// <summary>
        /// 描述 : 分类代码ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分类代码ID")]
        public string SystemTypeID { get; set; }

        /// <summary>
        /// 描述 : 分类代码编码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分类代码编码")]
        public string SystemTypeCode { get; set; }

        /// <summary>
        /// 描述 : 分类代码名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分类代码名称")]
        public string SystemTypeName { get; set; }

        /// <summary>
        /// 描述 : 分组代码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分组代码")]
        [Required(ErrorMessage = "分组代码不能为空")]
        public string ClassificationCode { get; set; }

        /// <summary>
        /// 描述 : 上报代码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "上报代码")]
        [Required(ErrorMessage = "上报代码不能为空")]
        public string ClassificationReportCode { get; set; }

        /// <summary>
        /// 描述 : 名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "名称不能为空")]
        public string ClassificationName { get; set; }

        /// <summary>
        /// 描述 : 名称(英文) 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "名称(英文)")]
        public string ClassificationNameEN { get; set; }

        /// <summary>
        /// 描述 : 行业名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "行业名称")]
        public string Industry { get; set; }

        /// <summary>
        /// 描述 : 风险登记选项 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "风险登记选项")]
        public int? RiskRegisterID { get; set; }

        /// <summary>
        /// 描述 : 风险登记 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "风险登记")]
        public string RiskRegister { get; set; }

        /// <summary>
        /// 描述 : CNAS标识 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "CNAS标识")]
        public bool? CNAS { get; set; }

        /// <summary>
        /// 描述 : 是否启用 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "是否启用")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// 描述 : 排序 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "排序")]
        public int? SortIndex { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}
