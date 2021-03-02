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
    public class TMPQueryDto
    {
        /// <summary>
        /// 描述 : 模板类别ID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "模板类别ID")]
        public string TMPTypeID { get; set; }

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
    public class TMPCreateDto
    {
        /// <summary>
        /// 描述 : 模板类别 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "模板类别")]
        [Required(ErrorMessage = "模板类别不能为空")]
        public string TMPTypeID { get; set; }

        /// <summary>
        /// 描述 : 模板名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "模板名称")]
        [Required(ErrorMessage = "模板名称不能为空")]
        public string TMP { get; set; }

        /// <summary>
        /// 描述 : 模板文件ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "模板文件ID")]
        [Required(ErrorMessage = "模板文件不能为空")]
        public string TMPID { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 描述 : 序号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "序号")]
        public int? SortIndex { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
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
    }

    /// <summary>
    /// 更新选项定义
    /// </summary>
    public class TMPUpdateDto
    {

        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        public string ID { get; set; }

        /// <summary>
        /// 描述 : 模板类别 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "模板类别")]
        [Required(ErrorMessage = "模板类别不能为空")]
        public string TMPTypeID { get; set; }

        /// <summary>
        /// 描述 : 模板名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "模板名称")]
        [Required(ErrorMessage = "模板名称不能为空")]
        public string TMP { get; set; }

        /// <summary>
        /// 描述 : 模板文件ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "模板文件ID")]
        [Required(ErrorMessage = "模板文件不能为空")]
        public string TMPID { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 描述 : 序号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "序号")]
        public int? SortIndex { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
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
    }
}
