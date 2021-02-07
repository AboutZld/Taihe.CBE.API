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


    [SugarTable("Biz_MainContract")]
    public partial class Biz_MainContract
    {
           public Biz_MainContract(){

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
           public int MainContractID {get;set;}

           /// <summary>
           /// 描述 : OA推送合同关联的订单号 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "OA推送合同关联的订单号")]           
           public string PreContractID {get;set;}

           /// <summary>
           /// 描述 : 关联客户id 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "关联客户id")]           
           public string CustomerID {get;set;}

           /// <summary>
           /// 描述 : 合同编号 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "合同编号")]           
           public string ContractNo {get;set;}

           /// <summary>
           /// 描述 : 关注程度，选项编号-92101 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "关注程度，选项编号-92101")]           
           public string ConcernExtent {get;set;}

           /// <summary>
           /// 描述 : 关注程度名称 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "关注程度名称")]           
           public string ConcernExtentName {get;set;}

           /// <summary>
           /// 描述 : 合同申请日期 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "合同申请日期")]           
           public DateTime? ApplyDate {get;set;}

           /// <summary>
           /// 描述 : 审核预计日期 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "审核预计日期")]           
           public DateTime? AuditPlanDate {get;set;}

           /// <summary>
           /// 描述 : 合同评审要求 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "合同评审要求")]           
           public string JudgeReuqest {get;set;}

           /// <summary>
           /// 描述 : 审核策划要求 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "审核策划要求")]           
           public string AuditPlanRequest {get;set;}

           /// <summary>
           /// 描述 : 审核组要求 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "审核组要求")]           
           public string AuditGroupRequest {get;set;}

           /// <summary>
           /// 描述 : 认证决定要求 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "认证决定要求")]           
           public string DecisionRequest {get;set;}

           /// <summary>
           /// 描述 : 备注 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "备注")]           
           public string Remark {get;set;}

           /// <summary>
           /// 描述 : 合同状态 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "合同状态")]           
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

           /// <summary>
           /// 描述 : 0-否 1-是 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "0-否 1-是")]           
           public int? deleted {get;set;}

           /// <summary>
           /// 描述 : 多场所人日 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "多场所人日")]           
           public decimal? MultiSiteDays {get;set;}

           /// <summary>
           /// 描述 : 提交评审时间 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "提交评审时间")]           
           public DateTime? SubmitAuditTime {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string SubmitAuditID {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string SubmitAuditName {get;set;}

           /// <summary>
           /// 描述 : 评审时间 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "评审时间")]           
           public DateTime? AuditTime {get;set;}

           /// <summary>
           /// 描述 : 评审人 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "评审人")]           
           public string AuditID {get;set;}

           /// <summary>
           /// 描述 : 评审时间 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "评审时间")]           
           public string AuditName {get;set;}

           /// <summary>
           /// 描述 : 申请评审人员ID 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "申请评审人员ID")]           
           public string ApplyAuditID {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string ApplyAuditName {get;set;}

           /// <summary>
           /// 描述 : 技术支持人ID 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "技术支持人ID")]           
           public string SupportUserID {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string SupportUserName {get;set;}

           /// <summary>
           /// 描述 : 市场评审信息 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "市场评审信息")]           
           public string MarketAuditRemark {get;set;}

           /// <summary>
           /// 描述 : 审核策划评审信息 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "审核策划评审信息")]           
           public string AuditPlanRemark {get;set;}

           /// <summary>
           /// 描述 : 审核组评审信息 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "审核组评审信息")]           
           public string AuditTeamRemark {get;set;}

           /// <summary>
           /// 描述 : 评审备注 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "评审备注")]           
           public string AuditRemark {get;set;}

           /// <summary>
           /// 描述 : 批准备注 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "批准备注")]           
           public string ApproveRemark {get;set;}

           /// <summary>
           /// 描述 : 合同审批日期 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "合同审批日期")]           
           public DateTime? ContractApproveTime {get;set;}

           /// <summary>
           /// 描述 : 批准时间 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "批准时间")]           
           public DateTime? ApproveTime {get;set;}

           /// <summary>
           /// 描述 : 批准人 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "批准人")]           
           public string ApproveID {get;set;}

           /// <summary>
           /// 描述 : 批准人 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "批准人")]           
           public string ApproveName {get;set;}

           /// <summary>
           /// 描述 : 体系一体化程度(%) 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "体系一体化程度(%)")]           
           public decimal? IntegrationLevel {get;set;}

    }
}
