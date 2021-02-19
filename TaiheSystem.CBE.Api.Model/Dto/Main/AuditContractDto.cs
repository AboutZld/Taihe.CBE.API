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
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaiheSystem.CBE.Api.Model.View;
using TaiheSystem.CBE.Api.Model.View.Main;
using TaiheSystem.CBE.Api.Model.View.System;

namespace TaiheSystem.CBE.Api.Model.Dto
{

    /// <summary>
    /// 查询基础人日信息
    /// </summary>
    public class AuditContractBaseDaysDto : PageParm
    {

        /// <summary>
        /// 描述 : 状态 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "合同项目")]
        [Required(ErrorMessage = "合同项目ID不允许为空")]
        public string ContractItemID { get; set; }

        /// <summary>
        /// 描述 : 合同编号 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "风险登记ID")]
        [Required(ErrorMessage = "风险登记不允许为空")]
        public int RiskRegisterID { get; set; }


    }

    /// <summary>
    /// 返回基础人日
    /// </summary>
    public class BaseMandaysDto
    {
        /// <summary>
        /// 描述 : 初审基础人日 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "初审基础人日")]
        public decimal FirstTrialBaseDays { get; set; }

        /// <summary>
        /// 描述 : 监督基础人日 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "监督基础人日")]
        public decimal SupervisionBaseDays { get; set; }

        /// <summary>
        /// 描述 : 再认证基础人日 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "再认证基础人日")]
        public decimal RecertificationBaseDays { get; set; }
    }

    /// <summary>
    /// 合同更新模块
    /// </summary>
    public class AuditContractUpdateDto
    {
        /// <summary>
        /// 描述 : UUID 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "UUID")]
        [Required(ErrorMessage = "ID不能为空")]

        public string ID { get; set; }

        /// <summary>
        /// 描述 : OA推送合同关联的订单号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "OA推送合同关联的订单号")]
        public string PreContractID { get; set; }

        /// <summary>
        /// 描述 : 关联客户id 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "关联客户id")]
        public string CustomerID { get; set; }

        /// <summary>
        /// 描述 : 项目编号 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "项目编号")]
        public string ContractNo { get; set; }

        /// <summary>
        /// 描述 : 关注程度，选项编号-92101 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "关注程度，选项编号-92101")]
        public string ConcernExtent { get; set; }

        /// <summary>
        /// 描述 : 关注程度名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "关注程度名称")]
        public string ConcernExtentName { get; set; }

        /// <summary>
        /// 描述 : 合同申请日期 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "合同申请日期")]
        public DateTime? ApplyDate { get; set; }

        /// <summary>
        /// 描述 : 审核预计日期 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "审核预计日期")]
        public DateTime? AuditPlanDate { get; set; }

        /// <summary>
        /// 描述 : 合同评审要求 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "合同评审要求")]
        public string JudgeReuqest { get; set; }

        /// <summary>
        /// 描述 : 审核策划要求 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "审核策划要求")]
        public string AuditPlanRequest { get; set; }

        /// <summary>
        /// 描述 : 审核组要求 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "审核组要求")]
        public string AuditGroupRequest { get; set; }

        /// <summary>
        /// 描述 : 认证决定要求 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "认证决定要求")]
        public string DecisionRequest { get; set; }

        /// <summary>
        /// 描述 : 备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 描述 : 多场所人日 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "多场所人日")]
        public decimal? MultiSiteDays { get; set; }

        /// <summary>
        /// 描述 : 提交评审时间 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "提交评审时间")]
        public DateTime? SubmitAuditTime { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string SubmitAuditID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string SubmitAuditName { get; set; }

        /// <summary>
        /// 描述 : 评审时间 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "评审时间")]
        public DateTime? AuditTime { get; set; }

        /// <summary>
        /// 描述 : 评审人 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "评审人")]
        public string AuditID { get; set; }

        /// <summary>
        /// 描述 : 评审时间 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "评审时间")]
        public string AuditName { get; set; }

        /// <summary>
        /// 描述 : 申请评审人员ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "申请评审人员ID")]
        public string ApplyAuditID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string ApplyAuditName { get; set; }

        /// <summary>
        /// 描述 : 技术支持人ID 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "技术支持人ID")]
        public string SupportUserID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string SupportUserName { get; set; }

        /// <summary>
        /// 描述 : 市场评审信息 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "市场评审信息")]
        public string MarketAuditRemark { get; set; }

        /// <summary>
        /// 描述 : 审核策划评审信息 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "审核策划评审信息")]
        public string AuditPlanRemark { get; set; }

        /// <summary>
        /// 描述 : 审核组评审信息 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "审核组评审信息")]
        public string AuditTeamRemark { get; set; }

        /// <summary>
        /// 描述 : 评审备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "评审备注")]
        public string AuditRemark { get; set; }

        /// <summary>
        /// 描述 : 批准备注 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "批准备注")]
        public string ApproveRemark { get; set; }

        /// <summary>
        /// 描述 : 批准时间 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "批准时间")]
        public DateTime? ApproveTime { get; set; }

        /// <summary>
        /// 描述 : 批准人 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "批准人")]
        public string ApproveID { get; set; }

        /// <summary>
        /// 描述 : 批准时间 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "批准时间")]
        public string ApproveName { get; set; }

        /// <summary>
        /// 描述 : 体系一体化程度(%) 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "体系一体化程度(%)")]
        public decimal? IntegrationLevel { get; set; }

        /// <summary>
        /// 支持方式选项
        /// </summary>
        public List<Frm_SelectItemObj> SupportWayList { get; set; }

        /// <summary>
        /// 描述 : 关联客户信息 
        /// 空值 : false
        /// 默认 : 
        /// </summary>
        [Display(Name = "关联客户信息")]
        public uf_khxx ContractCustomerInfo;


        /// <summary>
        /// 描述 : 体系项目列表
        /// 空值 : false
        /// 默认 : 
        /// </summary>
        [Display(Name = "合同体系项目列表")]
        public List<ContractItemVM> ContractItemList;

        /// <summary>
        /// 描述 : 体系项目列表
        /// 空值 : false
        /// 默认 : 
        /// </summary>
        [Display(Name = "合同体系项目列表")]
        public List<ContractItemVM> ContractItemList_update;

        /// <summary>
        /// 描述 : 合同分场所信息(插入)
        /// 空值 : false
        /// 默认 : 
        /// </summary>
        [Display(Name = "合同分场所信息")]
        public List<FcsVM> ContractfcsList;


        /// <summary>
        /// 描述 : 合同上传文件列表
        /// 空值 : false
        /// 默认 : 
        /// </summary>
        [Display(Name = "合同上传文件列表")]
        public List<FileVM> ContractFileList;


    }
}
