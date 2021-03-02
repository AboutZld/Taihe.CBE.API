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
    public class UserSystemTypeQueryDto : PageParm
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
    public class UserSystemTypeCreateDto
    {

        /// <summary>
        /// 描述 : 用户ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "用户ID")]
        [Required(ErrorMessage = "用户ID不允许为空")]
        public string UserID { get; set; }

        /// <summary>
        /// 描述 : 体系类别ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系类别ID")]
        [Required(ErrorMessage = "体系类别ID不允许为空")]
        public string SystemTypeID { get; set; }

        /// <summary>
        /// 描述 : 体系类别Code 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系类别Code")]
        public string SystemTypeCode { get; set; }

        /// <summary>
        /// 描述 : 体系类别名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系类别名称")]
        public string SystemTypeName { get; set; }

        /// <summary>
        /// 描述 : 标准日期 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "标准日期")]
        public DateTime? StandardDate { get; set; }

        /// <summary>
        /// 描述 : 注册资格选项index 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "注册资格选项index")]
        public int? QualificationTypeID { get; set; }

        /// <summary>
        /// 描述 : 注册资格选项名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "注册资格选项名称")]
        public string QualificationTypeName { get; set; }

        /// <summary>
        /// 描述 : 资格注册号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "资格注册号")]
        public string RegisterQualificationNo { get; set; }

        /// <summary>
        /// 描述 : 开始时间 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "开始时间")]
        public DateTime? QualificationStartDate { get; set; }

        /// <summary>
        /// 描述 : 结束时间 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "结束时间")]
        public DateTime? QualificationEndedDate { get; set; }

        /// <summary>
        /// 描述 : 组长与否:0-否 1-组长 2-见习组织 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "组长与否:0-否 1-组长 2-见习组织")]
        public int? GroupLeaderType { get; set; }

        /// <summary>
        /// 描述 : 能否做见证 0-无 1-内部见证 2-外部见证 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "能否做见证 0-无 1-内部见证 2-外部见证")]
        public int? WitnessType { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 描述 : 暂停资格 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "暂停资格")]
        public bool? Enabled { get; set; }
    }

    /// <summary>
    /// 更新选项定义
    /// </summary>
    public class UserSystemTypeUpdateDto
    {

        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        public string ID { get; set; }


        /// <summary>
        /// 描述 : 用户ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "用户ID")]
        [Required(ErrorMessage = "用户ID不允许为空")]
        public string UserID { get; set; }

        /// <summary>
        /// 描述 : 体系类别ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系类别ID")]
        [Required(ErrorMessage = "体系类别ID不允许为空")]
        public string SystemTypeID { get; set; }

        /// <summary>
        /// 描述 : 体系类别Code 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系类别Code")]
        public string SystemTypeCode { get; set; }

        /// <summary>
        /// 描述 : 体系类别名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系类别名称")]
        public string SystemTypeName { get; set; }

        /// <summary>
        /// 描述 : 标准名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "标准名称")]
        public string StandardNames { get; set; }

        /// <summary>
        /// 描述 : 标准日期 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "标准日期")]
        public DateTime? StandardDate { get; set; }

        /// <summary>
        /// 描述 : 注册资格选项index 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "注册资格选项index")]
        public int? QualificationTypeID { get; set; }

        /// <summary>
        /// 描述 : 注册资格选项名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "注册资格选项名称")]
        public string QualificationTypeName { get; set; }

        /// <summary>
        /// 描述 : 资格注册号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "资格注册号")]
        public string RegisterQualificationNo { get; set; }

        /// <summary>
        /// 描述 : 开始时间 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "开始时间")]
        public DateTime? QualificationStartDate { get; set; }

        /// <summary>
        /// 描述 : 结束时间 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "结束时间")]
        public DateTime? QualificationEndedDate { get; set; }

        /// <summary>
        /// 描述 : 组长与否:0-否 1-组长 2-见习组织 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "组长与否:0-否 1-组长 2-见习组织")]
        public int? GroupLeaderType { get; set; }

        /// <summary>
        /// 描述 : 能否做见证 0-无 1-内部见证 2-外部见证 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "能否做见证 0-无 1-内部见证 2-外部见证")]
        public int? WitnessType { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 描述 : 暂停资格 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "暂停资格")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// 体系标准类别
        /// </summary>
        [Display(Name = "体系标准类别")]
        public List<Sys_User_SystemType_Standard> UserSystemTypeStandardList;


        /// <summary>
        /// 体系标准类别(更新)
        /// </summary>
        [Display(Name = "体系标准类别(更新)")]
        public List<Sys_User_SystemType_Standard> UserSystemTypeStandardList_update;

        /// <summary>
        /// 体系标准类别(插入)
        /// </summary>
        [Display(Name = "体系标准类别(插入)")]
        public List<Sys_User_SystemType_Standard> UserSystemTypeStandardList_insert;

        /// <summary>
        /// 体系标准类别(删除)
        /// </summary>
        [Display(Name = "体系标准类别(删除)")]
        public List<Sys_User_SystemType_Standard> UserSystemTypeStandardList_delete;
    }
}
