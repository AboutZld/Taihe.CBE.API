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
    [SugarTable("Frm_SelectItemObj")]
    public class Frm_SelectItemObj
    {
          public Frm_SelectItemObj()
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
           /// 描述 :  
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           [SugarColumn(IsIdentity=true)]
           public int SelectItemIndex {get;set;}

           /// <summary>
           /// 描述 : 选项类别编码 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "选项类别编码")]           
           public string SelectItemCode {get;set;}

           /// <summary>
           /// 描述 : 编码 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "编码")]           
           public string SelectItemObjCode {get;set;}

           /// <summary>
           /// 描述 : 名称 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "名称")]           
           public string SelectItemObjName {get;set;}

           /// <summary>
           /// 描述 : 英文名称 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "英文名称")]           
           public string SelectItmeObjNameEn {get;set;}

           /// <summary>
           /// 描述 : 选项说明 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "选项说明")]           
           public string SelectItmeObjDecr {get;set;}

           /// <summary>
           /// 描述 : 序号 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "序号")]           
           public int? SortIndex {get;set;}

           /// <summary>
           /// 描述 : 是否有效 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "是否有效")]           
           public bool? Enabled {get;set;}

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

    }
}