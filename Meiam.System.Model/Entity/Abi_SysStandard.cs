﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
//     author MEIAM
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace Meiam.System.Model
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Abi_SysStandard")]
    public class Abi_SysStandard
    {
          public Abi_SysStandard()
          {
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
           /// 描述 : 体系类别 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "体系类别")]           
           public string SystemTypeID {get;set;}

           /// <summary>
           /// 描述 : 类别代码 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "类别代码")]           
           public string SystemTypeCode {get;set;}

           /// <summary>
           /// 描述 : 类别名称 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "类别名称")]           
           public string SystemTypeName {get;set;}

           /// <summary>
           /// 描述 : 编码 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "编码")]           
           public string SysStandardCode {get;set;}

           /// <summary>
           /// 描述 : 上报编码 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "上报编码")]           
           public string SysStandardReportCode {get;set;}

           /// <summary>
           /// 描述 : 简称 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "简称")]           
           public string SysStandardShortName {get;set;}

           /// <summary>
           /// 描述 : 认证标准 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "认证标准")]           
           public string SysStandardName {get;set;}

           /// <summary>
           /// 描述 : 备注 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "备注")]           
           public string Remark {get;set;}

           /// <summary>
           /// 描述 : 备注英文 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "备注英文")]           
           public string RemarkEN {get;set;}

           /// <summary>
           /// 描述 : 截止时间 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "截止时间")]           
           public DateTime? DeadLine {get;set;}

           /// <summary>
           /// 描述 : 是否启用 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "是否启用")]           
           public bool? Enabled {get;set;}

           /// <summary>
           /// 描述 : 编码 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "编码")]           
           public int? SortIndex {get;set;}

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
           /// 默认 : DateTime.Now
           /// </summary>
           [Display(Name = "")]           
           public DateTime? CreateTime {get;set;}

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
           public DateTime? UpdateTime {get;set;}

    }
}