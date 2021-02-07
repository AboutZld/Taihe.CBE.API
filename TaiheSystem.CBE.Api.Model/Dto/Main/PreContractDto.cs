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
    /// 查询公司定义
    /// </summary>
    public class PreContractQueryDto : PageParm
    {

        /// <summary>
        /// 描述 : 合同编号 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "合同编号")]
        [SwaggerSchema("合同编号")]
        public string ContractNo { get; set; }

        /// <summary>
        /// 描述 : 服务项目 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "服务项目")]
        public string ServiceItem { get; set; }

        /// <summary>
        /// 描述 : 客户名称 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "客户名称")]
        public string khmc { get; set; }


        /// <summary>
        /// 描述 : 联系人 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "联系人")]
        public string lxr { get; set; }

        /// <summary>
        /// 描述 : 登记状态 0-未登记 1- 已登记
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "登记状态")]
        [Required(ErrorMessage = "合同状态不能为空")]
        public string status { get; set; }
    }
}
