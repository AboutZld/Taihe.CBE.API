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
    public class SelectItemsQueryDto 
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
    public class SelectItemCreateDto
    {
        /// <summary>
        /// 描述 : 选项代号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "选项代号")]
        [Required(ErrorMessage = "请填写选项代号")]
        public string SelectItemCode { get; set; }

        /// <summary>
        /// 描述 : 选项名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "选项名称")]
        [Required(ErrorMessage = "请填写选项称")]
        public string SelectItemName { get; set; }

        /// <summary>
        /// 描述 : 说明 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "说明")]
        public string SelectItemDecr { get; set; }

        /// <summary>
        /// 描述 : 序号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "序号")]
        public int? SortIndex { get; set; }
    }

    /// <summary>
    /// 更新选项定义
    /// </summary>
    public class SelectItemUpdateDto
    {

        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        [Required(ErrorMessage = "更新ID不能为空")]
        public string ID { get; set; }

        /// <summary>
        /// 描述 : 选项代号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "选项代号")]
        [Required(ErrorMessage = "请填写选项代号")]
        public string SelectItemCode { get; set; }

        /// <summary>
        /// 描述 : 选项名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "选项名称")]
        [Required(ErrorMessage = "请填写选项称")]
        public string SelectItemName { get; set; }

        /// <summary>
        /// 描述 : 说明 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "说明")]
        public string SelectItemDecr { get; set; }

        /// <summary>
        /// 描述 : 序号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "序号")]
        public int? SortIndex { get; set; }
    }
}
