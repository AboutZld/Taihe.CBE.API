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
using System.ComponentModel.DataAnnotations;

namespace TaiheSystem.CBE.Api.Model.Dto
{

    /// <summary>
    /// 合同体系项目
    /// </summary>
    public class ContractItemCreateDto 
    {

        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        public string ID { get; set; }

        /// <summary>
        /// 描述 : 合同ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "合同ID")]
        public string MainContractID { get; set; }

        /// <summary>
        /// 描述 : 项目标准ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "项目标准ID")]
        [Required(ErrorMessage = "标准ID不能为空")]
        public string ItemStandardID { get; set; }

        /// <summary>
        /// 描述 : 项目编号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "项目编号")]
        public string ItemStandardCode { get; set; }

        /// <summary>
        /// 描述 : 项目名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "项目名称")]
        public string ItemName { get; set; }

        /// <summary>
        /// 描述 : 审核类型ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "审核类型ID")]
        [Required(ErrorMessage = "审核类型不能为空")]
        public string AuditTypeID { get; set; }

        /// <summary>
        /// 描述 : 审核类型名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "审核类型名称")]
        public string AuditTypeIName { get; set; }

        /// <summary>
        /// 描述 : 体系人数 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系人数")]
        public int? PropleNum { get; set; }

        /// <summary>
        /// 描述 : 申请范围 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "申请范围")]
        public string ApplyRange { get; set; }

        /// <summary>
        /// 描述 : 申请范围(英文) 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "申请范围(英文)")]
        public string ApplyRangeEN { get; set; }

        /// <summary>
        /// 描述 : 是否机构转入 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "是否机构转入")]
        public bool? OrganizationIn { get; set; }
    }
}
