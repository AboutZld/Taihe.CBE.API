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
    /// 查询审核安排信息
    /// </summary>
    public class AuditorPlanDto : PageParm
    {

        /// <summary>
        /// 描述 : 状态 0-未完成 1- 已编制 2-评定通过 3-评定未通过 4-评审有问题
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
        /// 描述 : 合作伙伴 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "合作伙伴")]
        public string hzhb { get; set; }

        /// <summary>
        /// 描述 : 受理日期开始 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "受理日期开始")]
        public string AcceptDateStart { get; set; }

        /// <summary>
        /// 描述 : 受理日期止
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "受理日期止")]
        public string AcceptDateEnd { get; set; }
    }

    /// <summary>
    /// 计划日志编制
    /// </summary>
    public class AuditorDrawUpdateDto
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
        public int PlanAuditorItemID { get; set; }

        /// <summary>
        /// 描述 : 任务id 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "任务id")]
        public string ContractPlanID { get; set; }

        /// <summary>
        /// 描述 : 项目id 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "项目id")]
        public string ContractItemSubID { get; set; }

        /// <summary>
        /// 描述 : 任务审核员id 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "任务审核员id")]
        public string PlanAuditorID { get; set; }

        /// <summary>
        /// 描述 : 组内身份ID（选项） 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "组内身份ID（选项）")]
        public int? GroupIdentityID { get; set; }

        /// <summary>
        /// 描述 : 组内身份名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "组内身份名称")]
        public string GroupIdentityName { get; set; }

        /// <summary>
        /// 描述 : 见证类型ID（选项） 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "见证类型ID（选项）")]
        public int? WitnessTypeID { get; set; }

        /// <summary>
        /// 描述 : 见证类型 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "见证类型")]
        public string WitnessTypeName { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string WitnessTypeUserName { get; set; }

        /// <summary>
        /// 描述 : 分组代码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "分组代码")]
        public string GroupCode { get; set; }

        /// <summary>
        /// 描述 : 专业代码 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "专业代码")]
        public string ProfessionCode { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
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
        /// 默认 : 40000
        /// </summary>
        [Display(Name = "")]
        public int? status { get; set; }

        /// <summary>
        /// 编制计划列表
        /// </summary>
        public List<Biz_Contract_PlanAuditor_Draw> PlanAuditorDrawList;

        /// <summary>
        /// 编制计划列表(更新)
        /// </summary>
        public List<Biz_Contract_PlanAuditor_Draw> PlanAuditorDrawList_update;

        /// <summary>
        /// 编制计划列表(插入)
        /// </summary>
        public List<Biz_Contract_PlanAuditor_Draw> PlanAuditorDrawList_insert;

        /// <summary>
        /// 编制计划列表(删除)
        /// </summary>
        public List<Biz_Contract_PlanAuditor_Draw> PlanAuditorDrawList_delete;


    }
}
