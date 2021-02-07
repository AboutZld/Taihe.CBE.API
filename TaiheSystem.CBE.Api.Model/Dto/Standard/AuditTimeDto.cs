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
    public class AuditTimeQueryDto : PageParm
    {
        /// <summary>
        /// 描述 : 认证体系编号
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "认证体系ID")]
        public string SystemTypeID { get; set; }
    }


    /// <summary>
    /// 新增类别
    /// </summary>
    public class AuditTimeCreateDto
    {
        /// <summary>
        /// 描述 : 体系认证类型ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系认证类型ID")]
        public string SystemTypeID { get; set; }

        /// <summary>
        /// 描述 : 体系认证类型code 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系认证类型code")]
        public string SystemTypeCode { get; set; }

        /// <summary>
        /// 描述 : 体系认证类型name 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系认证类型name")]
        public string SystemTypeName { get; set; }

        /// <summary>
        /// 描述 : 人数下限 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "人数下限")]
        public int? DownLimt { get; set; }

        /// <summary>
        /// 描述 : 人数上线 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "人数上线")]
        public int? UpLimit { get; set; }

        /// <summary>
        /// 描述 : 风险等级选项Index 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "风险等级选项Index")]
        public int? RiskRegisterID { get; set; }

        /// <summary>
        /// 描述 : 风险等级 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "风险等级")]
        public string RiskRegister { get; set; }

        /// <summary>
        /// 描述 : 审核时间 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "审核时间")]
        public decimal? AuditDays { get; set; }

    }

    /// <summary>
    /// 更新类别
    /// </summary>
    public class AuditTimeUpdateDto
    {

        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        public string ID { get; set; }

        /// <summary>
        /// 描述 : 体系认证类型ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系认证类型ID")]
        public string SystemTypeID { get; set; }

        /// <summary>
        /// 描述 : 体系认证类型code 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系认证类型code")]
        public string SystemTypeCode { get; set; }

        /// <summary>
        /// 描述 : 体系认证类型name 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系认证类型name")]
        public string SystemTypeName { get; set; }

        /// <summary>
        /// 描述 : 人数下限 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "人数下限")]
        public int? DownLimt { get; set; }

        /// <summary>
        /// 描述 : 人数上线 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "人数上线")]
        public int? UpLimit { get; set; }

        /// <summary>
        /// 描述 : 风险等级选项Index 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "风险等级选项Index")]
        public int? RiskRegisterID { get; set; }

        /// <summary>
        /// 描述 : 风险等级 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "风险等级")]
        public string RiskRegister { get; set; }

        /// <summary>
        /// 描述 : 审核时间 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "审核时间")]
        public decimal? AuditDays { get; set; }
    }
}
