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


    [SugarTable("Biz_ContractFile")]
    public partial class Biz_ContractFile
    {
           public Biz_ContractFile(){

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
           /// 描述 : 主合同ID 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "主合同ID")]           
           public string MainContractID {get;set;}

           /// <summary>
           /// 描述 : 文件ID 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "文件ID")]           
           public string FileID {get;set;}

           /// <summary>
           /// 描述 : 文件名 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "文件名")]           
           public string FileName {get;set;}

           /// <summary>
           /// 描述 : 文件名 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "文件名")]           
           public string Name {get;set;}

           /// <summary>
           /// 描述 : 客户ID 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "客户ID")]           
           public string CustomerID {get;set;}

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

    }
}
