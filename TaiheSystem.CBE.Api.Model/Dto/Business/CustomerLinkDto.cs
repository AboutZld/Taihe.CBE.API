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
    /// 添加联系人
    /// </summary>
    public class CustomerLinkCreateDto
    {
        /// <summary>
        /// 描述 : 所属客户ID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "所属客户ID")]
        [Required(ErrorMessage = "所属客户ID不能为空")]
        public string ParentUID { get; set; }

        /// <summary>
        /// 描述 : 联系人 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "联系人")]
        [Required(ErrorMessage = "联系人不能为空")]
        public string lxr { get; set; }

        /// <summary>
        /// 描述 : 部门 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "部门")]
        public string bm { get; set; }

        /// <summary>
        /// 描述 : 职务 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "职务")]
        public string zw { get; set; }

        /// <summary>
        /// 描述 : 座机 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "座机")]
        public string zj { get; set; }

        /// <summary>
        /// 描述 : 手机 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "手机")]
        public string sj { get; set; }

        /// <summary>
        /// 描述 : 邮箱 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "邮箱")]
        public string yx { get; set; }

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
    public class CustomerLinkUpdateDto
    {
        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        public string ID { get; set; }

        /// <summary>
        /// 描述 : 所属客户ID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "所属客户ID")]
        [Required(ErrorMessage = "所属客户ID不能为空")]
        public string ParentUID { get; set; }

        /// <summary>
        /// 描述 : 联系人 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "联系人")]
        [Required(ErrorMessage = "联系人不能为空")]
        public string lxr { get; set; }

        /// <summary>
        /// 描述 : 部门 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "部门")]
        public string bm { get; set; }

        /// <summary>
        /// 描述 : 职务 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "职务")]
        public string zw { get; set; }

        /// <summary>
        /// 描述 : 座机 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "座机")]
        public string zj { get; set; }

        /// <summary>
        /// 描述 : 手机 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "手机")]
        public string sj { get; set; }

        /// <summary>
        /// 描述 : 邮箱 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "邮箱")]
        public string yx { get; set; }

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
    public class CustomerLinkDto
    {
        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        public string ID { get; set; }

        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        [Required(ErrorMessage = "公司ID不能为空")]
        public string ParentUID { get; set; }

        /// <summary>
        /// 描述 : 联系人 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "联系人")]
        [Required(ErrorMessage = "联系人不能为空")]
        public string lxr { get; set; }

        /// <summary>
        /// 描述 : 部门 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "部门")]
        public string bm { get; set; }

        /// <summary>
        /// 描述 : 职务 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "职务")]
        public string zw { get; set; }

        /// <summary>
        /// 描述 : 座机 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "座机")]
        public string zj { get; set; }

        /// <summary>
        /// 描述 : 手机 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "手机")]
        public string sj { get; set; }

        /// <summary>
        /// 描述 : 邮箱 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "邮箱")]
        public string yx { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string bz { get; set; }
    }
}
