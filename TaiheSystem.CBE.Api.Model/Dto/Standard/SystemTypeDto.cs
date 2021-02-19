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
    public class SystemTypeQueryDto : PageParm
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
    public class SystemTypeQueryAllDto 
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
    public class SystemTypeCreateDto
    {
        /// <summary>
        /// 描述 : 类别编码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "类别编码")]
        [Required(ErrorMessage = "类别编码不能为空")]
        public string TypeCode { get; set; }

        /// <summary>
        /// 描述 : 体系类别名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系类别名称")]
        [Required(ErrorMessage = "类别名称不能为空")]
        public string TypeName { get; set; }


        /// <summary>
        /// 描述 : 英文名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "英文名称")]
        public string TypeNameEN { get; set; }

        /// <summary>
        /// 描述 : 说明 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "说明")]
        public string TypeNameDescr { get; set; }

        /// <summary>
        /// 描述 : 是否启用 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "是否启用")]
        public bool? Enabled { get; set; }


        /// <summary>
        /// 描述 : 序号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "序号")]
        public int? SortIndex { get; set; }

    }

    /// <summary>
    /// 更新类别
    /// </summary>
    public class SystemTypeUpdateDto
    {

        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        public string ID { get; set; }

        /// <summary>
        /// 描述 : 类别编码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "类别编码")]
        public string TypeCode { get; set; }

        /// <summary>
        /// 描述 : 体系类别名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系类别名称")]
        public string TypeName { get; set; }


        /// <summary>
        /// 描述 : 英文名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "英文名称")]
        public string TypeNameEN { get; set; }

        /// <summary>
        /// 描述 : 说明 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "说明")]
        public string TypeNameDescr { get; set; }

        /// <summary>
        /// 描述 : 是否启用 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "是否启用")]
        public bool? Enabled { get; set; }


        /// <summary>
        /// 描述 : 序号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "序号")]
        public int? SortIndex { get; set; }
    }
}
