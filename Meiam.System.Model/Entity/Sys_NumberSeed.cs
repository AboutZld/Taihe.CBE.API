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
    [SugarTable("Sys_NumberSeed")]
    public class Sys_NumberSeed
    {
          public Sys_NumberSeed()
          {
          }

           /// <summary>
           /// 描述 :  
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           [SugarColumn(IsPrimaryKey=true)]
           public string PrefixFormat {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           [SugarColumn(IsPrimaryKey=true)]
           public string NumCode {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public int? Seed {get;set;}

    }
}