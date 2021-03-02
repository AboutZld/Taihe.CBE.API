/*
* ==============================================================================
*
* FileName: CompanyDto.cs
* Created: 2020/6/08 10:45:49
* Author: Taihe
* Description: 
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaiheSystem.CBE.Api.Model.View.Standard;

namespace TaiheSystem.CBE.Api.Model.Dto
{

    /// <summary>
    /// 认证决定
    /// </summary>
    public class CertificationDecisionDto : PageParm
    {

        /// <summary>
        /// 描述 : 状态 0-未审批 1- 已审批
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "状态")]
        public int status { get; set; }

        /// <summary>
        /// 描述 : 项目编号 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "项目编号")]
        public string ContractItemNo { get; set; }

        /// <summary>
        /// 描述 : 客户名称 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "客户名称")]
        public string khmc { get; set; }

        /// <summary>
        /// 描述 : 审核开始起
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "审核开始起")]
        public string AcceptStartDateStart { get; set; }

        /// <summary>
        /// 描述 : 审核开始止
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "审核开始止")]
        public string AcceptStartDateEnd { get; set; }

        /// <summary>
        /// 描述 : 审核结束起
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "审核结束起")]
        public string AcceptEndDateStart { get; set; }

        /// <summary>
        /// 描述 : 审核结束止
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "审核结束止")]
        public string AcceptEndDateEnd { get; set; }

    }


    /// <summary>
    /// 评定结论保存
    /// </summary>
    public class CertificationDecisionSaveDto
    {

        /// <summary>
        /// 描述 : 任务ID
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "任务ID")]
        [Required(ErrorMessage = "任务ID不允许为空")]
        public string ID { get; set; }

        /// <summary>
        /// 评定问题列表
        /// </summary>
        [Display(Name = "评定问题列表")]
        public List<Biz_ContractItem_Sub> ContractItemSubList { get; set; }

    }

    /// <summary>
    /// 审核员计划提交信息
    /// </summary>
    public class CertificationDecisionSubmitDto
    {
        /// <summary>
        /// 描述 : 计划备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "id")]
        [Required(ErrorMessage = "ID不允许为空")]
        public string id { get; set; }

        /// <summary>
        /// 描述 : 阅卷老师ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "阅卷老师ID")]
        public string ReviewerID { get; set; }

        /// <summary>
        /// 描述 : 阅卷老师名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "阅卷老师名称")]
        public string ReviewerName { get; set; }

        /// <summary>
        /// 描述 : 资料回收日期
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "资料回收日期")]
        public DateTime DataRecoveryDate { get; set; }

        /// <summary>
        /// 描述 : 资料回收备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "资料回收备注")]
        public string DataRecoveryRemark { get; set; }

        /// <summary>
        /// 描述 : 待补充资料 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "待补充资料")]
        public string ReplenishRemark { get; set; }
    }


    /// <summary>
    /// 资料回收详情
    /// </summary>
    public class CertificationDecisionDetail
    {
        /// <summary>
        /// 描述 :  
        /// 空值 : False
        /// 默认 : newid()
        /// </summary>
        [Display(Name = "")]
        public string ID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public int AutoID { get; set; }

        /// <summary>
        /// 描述 : 客户id 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "客户id")]
        public string CustomerID { get; set; }

        /// <summary>
        /// 描述 : 任务编号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "任务编号")]
        public string ContractPlanNo { get; set; }

        /// <summary>
        /// 描述 : 计划起始日期 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "计划起始日期")]
        public DateTime? PlanStartDate { get; set; }

        /// <summary>
        /// 描述 : 计划结束日期 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "计划结束日期")]
        public DateTime? PlanEndDate { get; set; }

        /// <summary>
        /// 描述 : 任务备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "任务备注")]
        public string PlanRemark { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 描述 : 状态 
        /// 空值 : True
        /// 默认 : 30000
        /// </summary>
        [Display(Name = "状态")]
        public int? status { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : DateTime.Now
        /// </summary>
        [Display(Name = "")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string CreateID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string CreateName { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string UpdateID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string UpdateName { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public DateTime? SendSubmitTime { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string SendSubmitID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string SendSubmitName { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public DateTime? ApproveTime { get; set; }

        /// <summary>
        /// 描述 : 审批备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "审批备注")]
        public string ApproveRemark { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string ApproveID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string ApproveName { get; set; }

        /// <summary>
        /// 描述 : 阅卷老师ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "阅卷老师ID")]
        public string ReviewerID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string ReviewerName { get; set; }

        /// <summary>
        /// 描述 : 资料回收日期 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "资料回收日期")]
        public DateTime? DataRecoveryDate { get; set; }

        /// <summary>
        /// 描述 : 资料回收备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "资料回收备注")]
        public string DataRecoveryRemark { get; set; }

        /// <summary>
        /// 描述 : 待补充资料 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "待补充资料")]
        public string ReplenishRemark { get; set; }

        /// <summary>
        /// 描述 : 提交时间 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "提交时间")]
        public DateTime? DataRecoverySubmitDate { get; set; }

        /// <summary>
        /// 描述 : 提交人ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "提交人ID")]
        public string DataRecoverySubmitID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string DataRecoverySubmitName { get; set; }

        /// <summary>
        /// 描述 :  体系名称
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系名称")]
        public string SystemTypeNames { get; set; }

        /// <summary>
        /// 描述 :  审核类型
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "审核类型")]
        public string AuditTypeNames { get; set; }

        /// <summary>
        /// 客户组织信息
        /// </summary>
        public uf_khxx CustomerInfo { get; set; }

        /// <summary>
        /// 分场所信息
        /// </summary>
        public List<uf_fcsxx> CustomerParterList { get; set; }

        /// <summary>
        /// 项目信息
        /// </summary>
        public List<ContractItemSubVM> ContractItemSubList { get; set; }

        /// <summary>
        /// 上传文件列表
        /// </summary>
        public List<Biz_ContractItem_Sub_File> ContractsubitemFileList;


    }
}
