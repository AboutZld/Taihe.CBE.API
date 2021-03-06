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


    [SugarTable("Biz_ContractItem_Sub_File")]
    public partial class Biz_ContractItem_Sub_File
    {
           public Biz_ContractItem_Sub_File(){

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
           /// 描述 : 任务ID 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "任务ID")]           
           public string ContractPlanID {get;set;}

           /// <summary>
           /// 描述 : 任务分配审核员ID 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "任务分配审核员ID")]           
           public string PlanAuditorID {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string FileID {get;set;}

           /// <summary>
           /// 描述 : 0-合同评审文件 1-流转文件 3-一阶段 4-二阶段 5-本次审核文件 6-流转文件 7-评定纠正文件 9-其他 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "0-合同评审文件 1-流转文件 3-一阶段 4-二阶段 5-本次审核文件 6-流转文件 7-评定纠正文件 9-其他")]           
           public int? FileType {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string FileTypeName {get;set;}

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

    }
}
