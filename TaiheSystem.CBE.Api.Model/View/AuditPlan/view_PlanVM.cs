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


    [SugarTable("view_PlanVM")]
    public partial class view_PlanVM
    {
           public view_PlanVM(){


           }
           /// <summary>
           /// 描述 :  
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string ID {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public int ContractPlanID {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string CustomerID {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string ContractPlanNo {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public DateTime? PlanStartDate {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public DateTime? PlanEndDate {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string PlanRemark {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string Remark {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public int? status {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public DateTime? CreateTime {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string CreateID {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string CreateName {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public DateTime? UpdateTime {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string UpdateID {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string UpdateName {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public DateTime? SendSubmitTime {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string SendSubmitID {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string SendSubmitName {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public DateTime? ApproveTime {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string ApproveID {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string ApproveName {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string zzmc {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string mc {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string StatusName {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string StandardNos {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string ContractItemNos { get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string AuditTypeNames {get;set;}

    }
}
