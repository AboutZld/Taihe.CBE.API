/*
* ==============================================================================
*
* FileName: CompanyDto.cs
* Created: 2020/6/08 10:45:49
* Author: Taihe
* Description: 
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaiheSystem.CBE.Api.Model.View.Standard;

namespace TaiheSystem.CBE.Api.Model.Dto
{

    /// <summary>
    /// 证书翻译查询
    /// </summary>
    public class CertificateTranslateDto : PageParm
    {

        /// <summary>
        /// 描述 : 状态 0-未翻译 1- 已翻译
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "状态")]
        public int status { get; set; }

        /// <summary>
        /// 描述 : 项目编号 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "项目编号")]
        public string ContractItemNo { get; set; }

        /// <summary>
        /// 描述 : 客户名称 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "客户名称")]
        public string khmc { get; set; }

        /// <summary>
        /// 描述 : 审核开始起
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "审核开始起")]
        public string AcceptStartDateStart { get; set; }

        /// <summary>
        /// 描述 : 审核开始止
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "审核开始止")]
        public string AcceptStartDateEnd { get; set; }

        /// <summary>
        /// 描述 : 审核结束起
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "审核结束起")]
        public string AcceptEndDateStart { get; set; }

        /// <summary>
        /// 描述 : 审核结束止
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "审核结束止")]
        public string AcceptEndDateEnd { get; set; }

    }

    /// <summary>
    /// 翻译提交信息
    /// </summary>
    public class CertificateTranslateSubmitDto
    {
        /// <summary>
        /// 描述 : 计划备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "id")]
        [Required(ErrorMessage = "ID不允许为空")]
        public string id { get; set; }

        /// <summary>
        /// 描述 : 组织名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "组织名称")]
        public string zzmc { get; set; }

        /// <summary>
        /// 描述 : 组织名称英文 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "组织名称英文")]
        public string ywmc { get; set; }

        /// <summary>
        /// 描述 : 注册地址
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "注册地址")]
        public string zcdz { get; set; }

        /// <summary>
        /// 描述 : 英文注册地址 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "英文注册地址")]
        public string ywdz { get; set; }

        /// <summary>
        /// 描述 : 经营地址 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "经营地址")]
        public string jydz { get; set; }

        /// <summary>
        /// 描述 : 经营地址英文 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "经营地址英文")]
        public string jyyw { get; set; }
    }
}
