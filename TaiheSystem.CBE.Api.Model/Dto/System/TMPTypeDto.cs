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
    public class TMPTypeQueryDto
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
    public class TMPTypeCreateDto
    {
        /// <summary>
        /// 描述 : 类别代号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "类别代号")]
        [Required(ErrorMessage = "类别代号不能为空")]
        public string TMPTypeCode { get; set; }

        /// <summary>
        /// 描述 : 类别名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "类别名称")]
        [Required(ErrorMessage = "类别名称不能为空")]
        public string TMPTypeName { get; set; }

        /// <summary>
        /// 描述 : 类别说明 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "类别说明")]
        public string Remark { get; set; }

        /// <summary>
        /// 描述 : 父级类别ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "父级类别ID")]
        public string ParentUID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : DateTime.Now
        /// </summary>
        [Display(Name = "")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string CreateID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string CreateName { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string UpdateID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string UpdateName { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 0
        /// </summary>
        [Display(Name = "")]
        public bool? deleted { get; set; }

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
    public class TMPTypeUpdateDto
    {

        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        public string ID { get; set; }

        /// <summary>
        /// 描述 : 类别代号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "类别代号")]
        [Required(ErrorMessage = "类别代号不能为空")]
        public string TMPTypeCode { get; set; }

        /// <summary>
        /// 描述 : 类别名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "类别名称")]
        [Required(ErrorMessage = "类别名称不能为空")]
        public string TMPTypeName { get; set; }

        /// <summary>
        /// 描述 : 类别说明 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "类别说明")]
        public string Remark { get; set; }

        /// <summary>
        /// 描述 : 父级类别ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "父级类别ID")]
        public string ParentUID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : DateTime.Now
        /// </summary>
        [Display(Name = "")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string CreateID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string CreateName { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string UpdateID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string UpdateName { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 0
        /// </summary>
        [Display(Name = "")]
        public bool? deleted { get; set; }

        /// <summary>
        /// 描述 : 序号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "序号")]
        public int? SortIndex { get; set; }
    }
}
