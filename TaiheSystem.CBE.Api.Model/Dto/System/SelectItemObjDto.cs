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
    /// 查询选项（分页）
    /// </summary>
    public class SelectItemObjQueryDto : PageParm
    {
        /// <summary>
        /// 描述 : 选项类型编码
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "查询字符串")]
        [Required(ErrorMessage = "请填写选项类型编码")]
        public string SelectItemCode { get; set; }

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
    public class SelectItemObjQueryAllDto
    {
        /// <summary>
        /// 描述 : 选项类型编码
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "查询字符串")]
        [Required(ErrorMessage = "请填写选项类型编码")]
        public string SelectItemCode { get; set; }

        /// <summary>
        /// 描述 : 查询选项
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "查询字符串")]
        public string QueryText { get; set; }
    }

    /// <summary>
    /// 查询选项（多个类型）
    /// </summary>
    public class SelectItemObjsQueryDto
    {
        /// <summary>
        /// 描述 : 选项类型编码（多个,分隔）
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "查询字符串")]
        [Required(ErrorMessage = "请填写选项类型编码")]
        public string SelectItemCodes { get; set; }
    }

    /// <summary>
    /// 新增选项
    /// </summary>
    public class SelectItemObjCreateDto
    {
        /// <summary>
        /// 描述 : 选项类别编码 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "选项类别编码")]
        public string SelectItemCode { get; set; }

        /// <summary>
        /// 描述 : 编码 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "编码")]
        public string SelectItemObjCode { get; set; }

        /// <summary>
        /// 描述 : 名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "名称")]
        public string SelectItemObjName { get; set; }

        /// <summary>
        /// 描述 : 英文名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "英文名称")]
        public string SelectItmeObjNameEn { get; set; }

        /// <summary>
        /// 描述 : 选项说明 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "选项说明")]
        public string SelectItmeObjDecr { get; set; }

        /// <summary>
        /// 描述 : 序号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "序号")]
        public int? SortIndex { get; set; }

        /// <summary>
        /// 描述 : 是否有效 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "是否有效")]
        public bool? Enabled { get; set; }

    }

    /// <summary>
    /// 更新选项
    /// </summary>
    public class SelectItemObjUpdateDto
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
        /// 描述 : 选项类别编码 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "选项类别编码")]
        public string SelectItemCode { get; set; }

        /// <summary>
        /// 描述 : 编码 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "编码")]
        public string SelectItemObjCode { get; set; }

        /// <summary>
        /// 描述 : 名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "名称")]
        public string SelectItemObjName { get; set; }

        /// <summary>
        /// 描述 : 英文名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "英文名称")]
        public string SelectItmeObjNameEn { get; set; }

        /// <summary>
        /// 描述 : 选项说明 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "选项说明")]
        public string SelectItmeObjDecr { get; set; }

        /// <summary>
        /// 描述 : 序号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "序号")]
        public int? SortIndex { get; set; }

        /// <summary>
        /// 描述 : 是否有效 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "是否有效")]
        public bool? Enabled { get; set; }
    }
}
