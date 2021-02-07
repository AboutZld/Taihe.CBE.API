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
using System.ComponentModel.DataAnnotations;

namespace TaiheSystem.CBE.Api.Model.Dto
{

    /// <summary>
    /// 添加分场所
    /// </summary>
    public class CustomerSiteCreateDto
    {
        /// <summary>
        /// 描述 : 所属客户ID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "主公司ID")]
        [Required(ErrorMessage = "主公司ID不能为空")]
        public string ParentUID { get; set; }

        /// <summary>
        /// 描述 : 主公司名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "主公司名称")]
        public string zgsmc { get; set; }

        /// <summary>
        /// 描述 : 分场所类型 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分场所类型")]
        public int? fcslx { get; set; }

        /// <summary>
        /// 描述 : 分场所类型名
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分场所类型名称")]
        public string fcslxmc { get; set; }

        /// <summary>
        /// 描述 : 分场所名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分场所名称")]
        [Required(ErrorMessage = "分场所名称不能为空")]
        public string fcsmc { get; set; }

        /// <summary>
        /// 描述 : 分场所名称(英) 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分场所名称(英)")]
        public string fcsmcy { get; set; }

        /// <summary>
        /// 描述 : 地址 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "地址")]
        public string dz { get; set; }

        /// <summary>
        /// 描述 : 地址(英) 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "地址(英)")]
        public string dzy { get; set; }

        /// <summary>
        /// 描述 : 联系电话 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "联系电话")]
        public string lxdh { get; set; }

        /// <summary>
        /// 描述 : 传真 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "传真")]
        public string cz { get; set; }

        /// <summary>
        /// 描述 : 联系人 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "联系人")]
        public string lxr { get; set; }

        /// <summary>
        /// 描述 : 联系人手机 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "联系人手机")]
        public string lxrsj { get; set; }

        /// <summary>
        /// 描述 : 分现场人数 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分现场人数")]
        public string fxcrs { get; set; }

        /// <summary>
        /// 描述 : 距总部距离 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "距总部距离")]
        public string jzbjl { get; set; }

        /// <summary>
        /// 描述 : 职能部门 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "职能部门")]
        public string znbm { get; set; }

        /// <summary>
        /// 描述 : 分场所活动 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分场所活动")]
        public string fcshd { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string bz { get; set; }

    }

    /// <summary>
    /// 更新分场所
    /// </summary>
    public class CustomerSiteUpdateDto
    {
        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        public string ID { get; set; }

        /// <summary>
        /// 描述 : 主公司 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "主公司")]
        [Required(ErrorMessage = "主公司ID不能为空")]
        public string ParentUID { get; set; }

        /// <summary>
        /// 描述 : 主公司名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "主公司名称")]
        public string zgsmc { get; set; }

        /// <summary>
        /// 描述 : 分场所类型 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分场所类型")]
        public int? fcslx { get; set; }

        /// <summary>
        /// 描述 : 分场所类型名
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分场所类型名称")]
        public string fcslxmc { get; set; }

        /// <summary>
        /// 描述 : 分场所名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分场所名称")]
        public string fcsmc { get; set; }

        /// <summary>
        /// 描述 : 分场所名称(英) 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分场所名称(英)")]
        public string fcsmcy { get; set; }

        /// <summary>
        /// 描述 : 地址 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "地址")]
        public string dz { get; set; }

        /// <summary>
        /// 描述 : 地址(英) 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "地址(英)")]
        public string dzy { get; set; }

        /// <summary>
        /// 描述 : 联系电话 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "联系电话")]
        public string lxdh { get; set; }

        /// <summary>
        /// 描述 : 传真 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "传真")]
        public string cz { get; set; }

        /// <summary>
        /// 描述 : 联系人 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "联系人")]
        public string lxr { get; set; }

        /// <summary>
        /// 描述 : 联系人手机 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "联系人手机")]
        public string lxrsj { get; set; }

        /// <summary>
        /// 描述 : 分现场人数 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分现场人数")]
        public string fxcrs { get; set; }

        /// <summary>
        /// 描述 : 距总部距离 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "距总部距离")]
        public string jzbjl { get; set; }

        /// <summary>
        /// 描述 : 职能部门 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "职能部门")]
        public string znbm { get; set; }

        /// <summary>
        /// 描述 : 分场所活动 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分场所活动")]
        public string fcshd { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string bz { get; set; }
    }

    /// <summary>
    /// 更新联系人
    /// </summary>
    public class CustomerSiteDto
    {

        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        [Required(ErrorMessage = "主公司ID不能为空")]
        public string ParentUID { get; set; }

        /// <summary>
        /// 描述 : 查询选项
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "查询字符串")]
        public string QueryText { get; set; }
    }
}
