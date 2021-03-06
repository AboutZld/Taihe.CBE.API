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


    [SugarTable("Sys_User_SystemType_Standard")]
    public partial class Sys_User_SystemType_Standard
    {
           public Sys_User_SystemType_Standard(){

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
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string UserSystemTypeID {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string SysStandardID {get;set;}

            /// <summary>
            /// 描述 : 标准简称 
            /// 空值 : True
            /// 默认 : 
            /// </summary>
            [Display(Name = "标准简称")]
            public string SysStandardShortName { get; set; }

            /// <summary>
            /// 描述 : 标准名称 
            /// 空值 : True
            /// 默认 : 
            /// </summary>
            [Display(Name = "标准名称")]           
           public string SysStandardName {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public bool? Enabled {get;set;}

    }
}
