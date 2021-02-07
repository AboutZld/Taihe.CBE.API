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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaiheSystem.CBE.Api.Model.Dto
{

    /// <summary>
    /// 查询客户信息
    /// </summary>
    public class CustomerQueryDto : PageParm
    {
        /// <summary>
        /// 描述 : 统一信用代码
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "统一信用代码")]
        public string tyxydm { get; set; }

        /// <summary>
        /// 描述 : 客户名称
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "客户名称")]
        public string zzmc { get; set; }

    }

    /// <summary>
    /// 添加客户信息
    /// </summary>
    public class CustomerCreateDto
    {
        /// <summary>
        /// 描述 :  
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public int oaid { get; set; }

        /// <summary>
        /// 描述 : 组织名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "组织名称")]
        public string zzmc { get; set; }
    }

    /// <summary>
    /// 更新客户信息
    /// </summary>
    public class CustomerUpdateDto
    {
        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        [Required(ErrorMessage = "更新公司定义不能为空")]
        public string ID { get; set; }

        /// <summary>
        /// 描述 : 组织名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "组织名称")]
        public string zzmc { get; set; }

        /// <summary>
        /// 联系人列表
        /// </summary>
        [Display(Name = "组织名称")]
        public List<CustomerLinkDto> CustomerLinkList { get; set; }
    }


}
