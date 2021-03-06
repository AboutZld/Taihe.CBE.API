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


    [SugarTable("Abi_BizClassification")]
    public partial class Abi_BizClassification
    {
           public Abi_BizClassification(){

            this.ID =Guid.NewGuid().ToString();
            this.CreateTime =DateTime.Now;

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
           /// 描述 : 分类代码ID 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "分类代码ID")]           
           public string SystemTypeID {get;set;}

           /// <summary>
           /// 描述 : 分类代码编码 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "分类代码编码")]           
           public string SystemTypeCode {get;set;}

           /// <summary>
           /// 描述 : 分类代码名称 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "分类代码名称")]           
           public string SystemTypeName {get;set;}

           /// <summary>
           /// 描述 : 分组代码 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "分组代码")]           
           public string ClassificationCode {get;set;}

           /// <summary>
           /// 描述 : 上报代码 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "上报代码")]           
           public string ClassificationReportCode {get;set;}

           /// <summary>
           /// 描述 : 内容 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "内容")]           
           public string ClassificationName {get;set;}

           /// <summary>
           /// 描述 : 名称(英文) 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "名称(英文)")]           
           public string ClassificationNameEN {get;set;}

           /// <summary>
           /// 描述 : 行业名称 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "行业名称")]           
           public string Industry {get;set;}

           /// <summary>
           /// 描述 : 风险登记选项 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "风险登记选项")]           
           public int? RiskRegisterID {get;set;}

           /// <summary>
           /// 描述 : 风险登记 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "风险登记")]           
           public string RiskRegister {get;set;}

           /// <summary>
           /// 描述 : CNAS标识 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "CNAS标识")]           
           public bool? CNAS {get;set;}

           /// <summary>
           /// 描述 : 是否启用 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "是否启用")]           
           public bool? Enabled {get;set;}

           /// <summary>
           /// 描述 : 排序 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "排序")]           
           public int? SortIndex {get;set;}

           /// <summary>
           /// 描述 : 备注 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "备注")]           
           public string Remark {get;set;}

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
