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


    [SugarTable("uf_hzhb")]
    public partial class uf_hzhb
    {
           public uf_hzhb(){

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
           public int oaid {get;set;}

           /// <summary>
           /// 描述 : 类别 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "类别")]           
           public string lb {get;set;}

           /// <summary>
           /// 描述 : 编码 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "编码")]           
           public string bm {get;set;}

           /// <summary>
           /// 描述 : 机构内部编号 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "机构内部编号")]           
           public string jgnbbh {get;set;}

           /// <summary>
           /// 描述 : 地域 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "地域")]           
           public int? dy {get;set;}

           /// <summary>
           /// 描述 : 名称 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "名称")]           
           public string mc {get;set;}

           /// <summary>
           /// 描述 : 邮政编码 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "邮政编码")]           
           public string yzbm {get;set;}

           /// <summary>
           /// 描述 : 负责人 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "负责人")]           
           public string fzr {get;set;}

           /// <summary>
           /// 描述 : 联系地址 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "联系地址")]           
           public string lxdz {get;set;}

           /// <summary>
           /// 描述 : 联系人 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "联系人")]           
           public string lxr {get;set;}

           /// <summary>
           /// 描述 : 联系手机 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "联系手机")]           
           public string lxsj {get;set;}

           /// <summary>
           /// 描述 : 电话 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "电话")]           
           public string dh {get;set;}

           /// <summary>
           /// 描述 : 传真 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "传真")]           
           public string cz {get;set;}

           /// <summary>
           /// 描述 : 合作状态 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "合作状态")]           
           public string hzzt {get;set;}

           /// <summary>
           /// 描述 : 电子邮箱 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "电子邮箱")]           
           public string dzyx {get;set;}

           /// <summary>
           /// 描述 : 企业网址 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "企业网址")]           
           public string qywz {get;set;}

           /// <summary>
           /// 描述 : 业务范围 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "业务范围")]           
           public string ywfw {get;set;}

           /// <summary>
           /// 描述 : 备注 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "备注")]           
           public string bz {get;set;}

           /// <summary>
           /// 描述 : 创建时间 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "创建时间")]           
           public DateTime? CreateTime {get;set;}

           /// <summary>
           /// 描述 : 更新时间 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "更新时间")]           
           public DateTime? UpdateTime {get;set;}

    }
}
