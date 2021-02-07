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


    [SugarTable("Biz_ContractItem_BizClassification")]
    public partial class Biz_ContractItem_BizClassification
    {
           public Biz_ContractItem_BizClassification(){

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
           /// 描述 : 合同项目ID 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "合同项目ID")]           
           public string ContractItemID {get;set;}

           /// <summary>
           /// 描述 : 专业代码ID 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "专业代码ID")]           
           public string BizClassificationID {get;set;}

    }
}