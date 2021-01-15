﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
//     author Taihe
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace TaiheSystem.CBE.Api.Model
{
    ///<summary>
    ///数据关系
    ///</summary>
    [SugarTable("Sys_DataRelation")]
    public class Sys_DataRelation
    {
          public Sys_DataRelation()
          {
          }

           /// <summary>
           /// 描述 : UID 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "UID")]           
           [SugarColumn(IsPrimaryKey=true)]
           public string ID {get;set;}

           /// <summary>
           /// 描述 : 来源ID 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "来源ID")]           
           public string Form {get;set;}

           /// <summary>
           /// 描述 : 对应ID 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "对应ID")]           
           public string To {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string Type {get;set;}

    }
}