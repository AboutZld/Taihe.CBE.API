﻿using System;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SqlSugar;

namespace TaiheSystem.CBE.Api.Model
{
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
//     author Taihe
// </auto-generated>
//------------------------------------------------------------------------------


    [SugarTable("Biz_ContractItem_Sub_Certificate")]
    public partial class Biz_ContractItem_Sub_Certificate
    {
           public Biz_ContractItem_Sub_Certificate(){

            this.ID =Guid.NewGuid();
            this.status =Convert.ToInt32("50000");

           }
           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : newid()
           /// </summary>
           [Display(Name = "")]           
           public string ID {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           [SugarColumn(IsIdentity=true)]
           public int AutoID {get;set;}

           /// <summary>
           /// 描述 : 原证书ID 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "原证书ID")]           
           public string OldCertificationID {get;set;}

           /// <summary>
           /// 描述 : 客户ID 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "客户ID")]           
           public string CustomerID {get;set;}

           /// <summary>
           /// 描述 : 主合同ID 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "主合同ID")]           
           public string MainContractID {get;set;}

           /// <summary>
           /// 描述 : 项目ID 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "项目ID")]           
           public string ContractItemID {get;set;}

           /// <summary>
           /// 描述 : 子项目ID 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "子项目ID")]           
           public string ContractSubItemID {get;set;}

           /// <summary>
           /// 描述 : 计划任务ID 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "计划任务ID")]           
           public string ContractPlanID {get;set;}

           /// <summary>
           /// 描述 : 证书编号 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "证书编号")]           
           public string CertificateNo {get;set;}

           /// <summary>
           /// 描述 : 注册日期 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "注册日期")]           
           public DateTime? CertificateRegisterDate {get;set;}

           /// <summary>
           /// 描述 : 注册到期日期 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "注册到期日期")]           
           public DateTime? CertificateExpireDate {get;set;}

           /// <summary>
           /// 描述 : 证书初评日期 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "证书初评日期")]           
           public DateTime? CertificateInitialDate {get;set;}

           /// <summary>
           /// 描述 : 证书范围 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "证书范围")]           
           public string CertificateScope {get;set;}

           /// <summary>
           /// 描述 : 证书范围EN 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "证书范围EN")]           
           public string CertificateScopeEN {get;set;}

           /// <summary>
           /// 描述 : 备注 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "备注")]           
           public string CertificateRemark {get;set;}

           /// <summary>
           /// 描述 : 证书文件ID 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "证书文件ID")]           
           public string CertificateFileID {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 50000
           /// </summary>
           [Display(Name = "")]           
           public int? status {get;set;}

    }
}
