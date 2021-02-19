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
        public int AutoID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string ContractPlanID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string UserID { get; set; }

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

        /// <summary>
        /// 项目列表
        /// </summary>
        public List<Biz_Contract_PlanAuditor_Item> ContractsubitemList;

        /// <summary>
        /// 上传文件列表
        /// </summary>
        public List<Biz_ContractItem_Sub_File> ContractsubitemFileList;


    }

    public class SubItemFileCreateDto
    {

        /// <summary>
        /// 描述 : 子项目id 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "子项目id")]
        public string ContractItemSubID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "文件ID")]
        [Required(ErrorMessage = "文件ID不允许为空")]
        public string FileID { get; set; }

        /// <summary>
        /// 描述 : 0-合同评审文件 1-流转文件 3-一阶段 4-二阶段 5-本次审核文件 6-流转文件 7-评定纠正文件 9-其他 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "0-合同评审文件 1-流转文件 3-一阶段 4-二阶段 5-本次审核文件 6-流转文件 7-评定纠正文件 9-其他")]
        public int? FileType { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string FileTypeName { get; set; }
    }
}
