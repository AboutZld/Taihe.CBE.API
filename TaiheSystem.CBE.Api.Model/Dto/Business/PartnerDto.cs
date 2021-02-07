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
    /// 查询合作伙伴
    /// </summary>
    public class PartnerQueryDto : PageParm
    {

        /// <summary>
        /// 描述 : 合作伙伴名称
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "合作伙伴名称")]
        public string mc { get; set; }

        /// <summary>
        /// 描述 : 合作伙伴编码
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "合作伙伴编码")]
        public string bm { get; set; }

    }

    /// <summary>
    /// 添加合作伙伴
    /// </summary>
    public class PartnerCreateDto
    {
        /// <summary>
        /// 描述 :  
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public int oaid { get; set; }

        /// <summary>
        /// 描述 : 类别 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "类别")]
        public string lb { get; set; }

        /// <summary>
        /// 描述 : 编码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "编码")]
        public string bm { get; set; }

        /// <summary>
        /// 描述 : 机构内部编号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "机构内部编号")]
        public string jgnbbh { get; set; }

        /// <summary>
        /// 描述 : 地域 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "地域")]
        public int? dy { get; set; }

        /// <summary>
        /// 描述 : 名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "名称")]
        public string mc { get; set; }

        /// <summary>
        /// 描述 : 邮政编码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "邮政编码")]
        public string yzbm { get; set; }

        /// <summary>
        /// 描述 : 负责人 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "负责人")]
        public string fzr { get; set; }

        /// <summary>
        /// 描述 : 联系地址 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "联系地址")]
        public string lxdz { get; set; }

        /// <summary>
        /// 描述 : 联系人 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "联系人")]
        public string lxr { get; set; }

        /// <summary>
        /// 描述 : 联系手机 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "联系手机")]
        public string lxsj { get; set; }

        /// <summary>
        /// 描述 : 电话 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "电话")]
        public string dh { get; set; }

        /// <summary>
        /// 描述 : 传真 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "传真")]
        public string cz { get; set; }

        /// <summary>
        /// 描述 : 合作状态 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "合作状态")]
        public string hzzt { get; set; }

        /// <summary>
        /// 描述 : 电子邮箱 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "电子邮箱")]
        public string dzyx { get; set; }

        /// <summary>
        /// 描述 : 企业网址 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "企业网址")]
        public string qywz { get; set; }

        /// <summary>
        /// 描述 : 业务范围 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "业务范围")]
        public string ywfw { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string bz { get; set; }
    }

    /// <summary>
    /// 更新合作伙伴
    /// </summary>
    public class PartnerUpdateDto
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
        /// 描述 :  
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public int oaid { get; set; }

        /// <summary>
        /// 描述 : 类别 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "类别")]
        public string lb { get; set; }

        /// <summary>
        /// 描述 : 编码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "编码")]
        public string bm { get; set; }

        /// <summary>
        /// 描述 : 机构内部编号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "机构内部编号")]
        public string jgnbbh { get; set; }

        /// <summary>
        /// 描述 : 地域 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "地域")]
        public int? dy { get; set; }

        /// <summary>
        /// 描述 : 名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "名称")]
        public string mc { get; set; }

        /// <summary>
        /// 描述 : 邮政编码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "邮政编码")]
        public string yzbm { get; set; }

        /// <summary>
        /// 描述 : 负责人 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "负责人")]
        public string fzr { get; set; }

        /// <summary>
        /// 描述 : 联系地址 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "联系地址")]
        public string lxdz { get; set; }

        /// <summary>
        /// 描述 : 联系人 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "联系人")]
        public string lxr { get; set; }

        /// <summary>
        /// 描述 : 联系手机 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "联系手机")]
        public string lxsj { get; set; }

        /// <summary>
        /// 描述 : 电话 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "电话")]
        public string dh { get; set; }

        /// <summary>
        /// 描述 : 传真 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "传真")]
        public string cz { get; set; }

        /// <summary>
        /// 描述 : 合作状态 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "合作状态")]
        public string hzzt { get; set; }

        /// <summary>
        /// 描述 : 电子邮箱 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "电子邮箱")]
        public string dzyx { get; set; }

        /// <summary>
        /// 描述 : 企业网址 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "企业网址")]
        public string qywz { get; set; }

        /// <summary>
        /// 描述 : 业务范围 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "业务范围")]
        public string ywfw { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string bz { get; set; }
    }


}
