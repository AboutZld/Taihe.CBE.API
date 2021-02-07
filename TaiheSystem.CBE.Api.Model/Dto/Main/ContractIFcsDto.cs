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
    /// 合同分场所
    /// </summary>
    public class ContractIFcsCreateDto
    {

        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        public string ID { get; set; }

        /// <summary>
        /// 描述 : 分场所ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分场所ID")]
        [Required(ErrorMessage = "分场所不能为空")]
        public string fcsID { get; set; }

        /// <summary>
        /// 描述 : 主合同ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "主合同ID")]
        public string MainContractID { get; set; }

        /// <summary>
        /// 描述 : 客户ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "客户ID")]
        public string CustomerID { get; set; }
    }
}
