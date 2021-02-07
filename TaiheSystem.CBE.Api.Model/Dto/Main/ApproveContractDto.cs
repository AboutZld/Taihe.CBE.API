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
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaiheSystem.CBE.Api.Model.View;
using TaiheSystem.CBE.Api.Model.View.Main;
using TaiheSystem.CBE.Api.Model.View.System;

namespace TaiheSystem.CBE.Api.Model.Dto
{

    /// <summary>
    /// 审批提交操作
    /// </summary>
    public class ApproveContractSubmitDto
    {

        /// <summary>
        /// 描述 : 状态 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "合同项目")]
        [Required(ErrorMessage = "合同项目ID不允许为空")]
        public string ID { get; set; }

        /// <summary>
        /// 描述 : 合同审批日期 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "合同审批日期")]
        [Required(ErrorMessage = "合同审批日期不允许为空")]
        public DateTime ContractApproveTime { get; set; }

        /// <summary>
        /// 描述 : 合同审批日期 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "合同审批日期")]
        [Required(ErrorMessage = "合同审批日期不允许为空")]
        public string ApproveRemark { get; set; }


    }
}
