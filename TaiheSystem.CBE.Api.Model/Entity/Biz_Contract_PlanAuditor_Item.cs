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


    [SugarTable("Biz_Contract_PlanAuditor_Item")]
    public partial class Biz_Contract_PlanAuditor_Item
    {
           public Biz_Contract_PlanAuditor_Item(){

            this.ID =Guid.NewGuid().ToString();
            this.status =Convert.ToInt32("40000");

           }
           /// <summary>
           /// 描述 :  
           /// 空值 : False
           /// 默认 : newid()
           /// </summary>
           [Display(Name = "")]           
           [SugarColumn(IsPrimaryKey=true)]
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
           /// 描述 : 任务id 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "任务id")]           
           public string ContractPlanID {get;set;}

           /// <summary>
           /// 描述 : 项目id 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "项目id")]           
           public string ContractItemSubID {get;set;}

           /// <summary>
           /// 描述 : 任务审核员id 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "任务审核员id")]           
           public string PlanAuditorID {get;set;}

           /// <summary>
           /// 描述 : 组内身份ID（选项） 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "组内身份ID（选项）")]           
           public int? GroupIdentityID {get;set;}

           /// <summary>
           /// 描述 : 组内身份名称 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "组内身份名称")]           
           public string GroupIdentityName {get;set;}

           /// <summary>
           /// 描述 : 见证类型ID（选项） 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "见证类型ID（选项）")]           
           public int? WitnessTypeID {get;set;}

           /// <summary>
           /// 描述 : 见证类型 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "见证类型")]           
           public string WitnessTypeName {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string WitnessTypeUserName {get;set;}

           /// <summary>
           /// 描述 : 分组代码 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "分组代码")]           
           public string GroupCode {get;set;}

           /// <summary>
           /// 描述 : 专业代码 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "专业代码")]           
           public string ProfessionCode {get;set;}

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
           /// 默认 : 40000
           /// </summary>
           [Display(Name = "")]           
           public int? status {get;set;}

    }
}
