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


    [SugarTable("Biz_ContractItem_Sub")]
    public partial class Biz_ContractItem_Sub
    {
           public Biz_ContractItem_Sub(){

            this.ID =Guid.NewGuid().ToString();
            this.status =Convert.ToInt32("20000");

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
           public int ContractItemSubID {get;set;}

           /// <summary>
           /// 描述 : 合同ID 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "合同ID")]           
           public string MainContractID {get;set;}

           /// <summary>
           /// 描述 : 主项目ID 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "主项目ID")]           
           public string ContractItemID {get;set;}

           /// <summary>
           /// 描述 : 项目类型ID(0-一阶段；1-二阶段；3-其他) 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "项目类型ID(0-一阶段；1-二阶段；3-其他)")]           
           public int? ContractItemSubType {get;set;}

           /// <summary>
           /// 描述 : 子项目类型 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "子项目类型")]           
           public string ContractItemSubTypeCode {get;set;}

           /// <summary>
           /// 描述 : 审核任务ID 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "审核任务ID")]           
           public string ContractPlanID {get;set;}

           /// <summary>
           /// 描述 : 初审一阶段是否现场 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "初审一阶段是否现场")]           
           public bool? IsScene {get;set;}

           /// <summary>
           /// 描述 : 多场所增加人日 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "多场所增加人日")]           
           public decimal? MultiSiteAddDays {get;set;}

           /// <summary>
           /// 描述 : 审核结合度(%) 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "审核结合度(%)")]           
           public decimal? AuditCombinDegree {get;set;}

           /// <summary>
           /// 描述 : 删减比例(%) 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "删减比例(%)")]           
           public decimal? DeletionScale {get;set;}

           /// <summary>
           /// 描述 : 策划总人日 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "策划总人日")]           
           public decimal? PlanTotalDays {get;set;}

           /// <summary>
           /// 描述 : 非现场人日 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "非现场人日")]           
           public decimal? OffSiteDays {get;set;}

           /// <summary>
           /// 描述 : 一阶段(现场) 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "一阶段(现场)")]           
           public decimal? FirstDays {get;set;}

           /// <summary>
           /// 描述 : 二阶段 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "二阶段")]           
           public decimal? SecondDays {get;set;}

           /// <summary>
           /// 描述 : 多场所增加人日 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "多场所增加人日")]           
           public decimal? MultiSitetTravelDays {get;set;}

           /// <summary>
           /// 描述 : 其他增减人日 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "其他增减人日")]           
           public decimal? OtherAddDays {get;set;}

           /// <summary>
           /// 描述 : 审核实际安排初审一阶段 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "审核实际安排初审一阶段")]           
           public decimal? TrueFirstDays {get;set;}

           /// <summary>
           /// 描述 : 审核实际安排初审二阶段 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "审核实际安排初审二阶段")]           
           public decimal? TrueSecondDays {get;set;}

           /// <summary>
           /// 描述 : 项目状态 
           /// 空值 : True
           /// 默认 : 20000
           /// </summary>
           [Display(Name = "项目状态")]           
           public int? status {get;set;}

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
