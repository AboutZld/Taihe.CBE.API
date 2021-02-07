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
    /// 查询审核项目信息
    /// </summary>
    public class PlanQueryDto : PageParm
    {

        /// <summary>
        /// 描述 : 合同编号 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "合同编号")]
        public string ContractNo { get; set; }

        /// <summary>
        /// 描述 : 客户名称 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "客户名称")]
        public string khmc { get; set; }

        /// <summary>
        /// 描述 : 合作伙伴 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "合作伙伴")]
        public string hzhb { get; set; }

        /// <summary>
        /// 描述 : 受理日期开始 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "受理日期开始")]
        public string AcceptDateStart { get; set; }

        /// <summary>
        /// 描述 : 受理日期止
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "受理日期止")]
        public string AcceptDateEnd { get; set; }

    }

    /// <summary>
    /// 生成审核安排信息
    /// </summary>
    public class PlanInitDto
    {
        /// <summary>
        /// 描述 : 选中项目id
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "选中项目ID")]
        [Required(ErrorMessage = "所选项目不能为空")]
        public string Ids { get; set; }

    }
}
