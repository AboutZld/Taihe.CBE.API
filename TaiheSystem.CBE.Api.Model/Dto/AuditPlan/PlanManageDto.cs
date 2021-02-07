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
    /// 查询审核安排信息
    /// </summary>
    public class PlanManageDto : PageParm
    {

        ///// <summary>
        ///// 描述 : 状态 0-待派人 1- 待审批 2-已审批 3-终止审核
        ///// 空值 : False
        ///// 默认 : 
        ///// </summary>
        //[Display(Name = "状态")]
        //public int status { get; set; }

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
}
