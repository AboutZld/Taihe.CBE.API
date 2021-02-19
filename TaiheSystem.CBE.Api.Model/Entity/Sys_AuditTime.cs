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


    [SugarTable("Sys_AuditTime")]
    public partial class Sys_AuditTime
    {
           public Sys_AuditTime(){

            this.ID =Guid.NewGuid().ToString();

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
           /// 描述 : 体系认证类型ID 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "体系认证类型ID")]           
           public string SystemTypeID {get;set;}

           /// <summary>
           /// 描述 : 体系认证类型code 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "体系认证类型code")]           
           public string SystemTypeCode {get;set;}

           /// <summary>
           /// 描述 : 体系认证类型name 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "体系认证类型name")]           
           public string SystemTypeName {get;set;}

           /// <summary>
           /// 描述 : 人数下限 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "人数下限")]           
           public int? DownLimt {get;set;}

           /// <summary>
           /// 描述 : 人数上线 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "人数上线")]           
           public int? UpLimit {get;set;}

           /// <summary>
           /// 描述 : 风险等级选项Index 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "风险等级选项Index")]           
           public int? RiskRegisterID {get;set;}

           /// <summary>
           /// 描述 : 风险等级 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "风险等级")]           
           public string RiskRegister {get;set;}

           /// <summary>
           /// 描述 : 审核天数 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "审核天数")]           
           public decimal? AuditDays {get;set;}

    }
}
