﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
//     author Taihe
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace TaiheSystem.CBE.Api.Model
{
    ///<summary>
    ///用户管理
    ///</summary>
    [SugarTable("Sys_Users")]
    public class Sys_Users
    {
          public Sys_Users()
          {
          }

           /// <summary>
           /// 描述 : 用户账号 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "用户账号")]           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public string UserID {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string LoginName {get;set;}

           /// <summary>
           /// 描述 : 用户名称 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "用户名称")]           
           public string UserName {get;set;}

           /// <summary>
           /// 描述 : 用户昵称 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "用户昵称")]           
           public string NickName {get;set;}

           /// <summary>
           /// 描述 : 邮箱 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "邮箱")]           
           public string Email {get;set;}

           /// <summary>
           /// 描述 : 密码 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "密码")]           
           public string Password {get;set;}

           /// <summary>
           /// 描述 : 性别 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "性别")]           
           public string Sex {get;set;}

           /// <summary>
           /// 描述 : 头像地址 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "头像地址")]           
           public string AvatarUrl {get;set;}

           /// <summary>
           /// 描述 : QQ 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "QQ")]           
           public string QQ {get;set;}

           /// <summary>
           /// 描述 : 手机号码 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "手机号码")]           
           public string Phone {get;set;}

           /// <summary>
           /// 描述 : 用户所在省份编码 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "用户所在省份编码")]           
           public string ProvinceID {get;set;}

           /// <summary>
           /// 描述 : 用户所在省份 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "用户所在省份")]           
           public string Province {get;set;}

           /// <summary>
           /// 描述 : 用户所在城市编码 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "用户所在城市编码")]           
           public string CityID {get;set;}

           /// <summary>
           /// 描述 : 用户所在城市 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "用户所在城市")]           
           public string City {get;set;}

           /// <summary>
           /// 描述 : 用户所在县/区编码 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "用户所在县/区编码")]           
           public string CountyID {get;set;}

           /// <summary>
           /// 描述 : 用户所在县/区 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "用户所在县/区")]           
           public string County {get;set;}

           /// <summary>
           /// 描述 : 地址 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "地址")]           
           public string Address {get;set;}

           /// <summary>
           /// 描述 : 备注 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "备注")]           
           public string Remark {get;set;}

           /// <summary>
           /// 描述 : 身份证 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "身份证")]           
           public string IdentityCard {get;set;}

           /// <summary>
           /// 描述 : 生日 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "生日")]           
           public DateTime? Birthday {get;set;}

           /// <summary>
           /// 描述 : 上次登录时间 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "上次登录时间")]           
           public DateTime? LastLoginTime {get;set;}

           /// <summary>
           /// 描述 : 是否启用 
           /// 空值 : False
           /// 默认 : 1
           /// </summary>
           [Display(Name = "是否启用")]           
           public bool Enabled {get;set;}

           /// <summary>
           /// 描述 : 单用户模式 
           /// 空值 : False
           /// 默认 : 0
           /// </summary>
           [Display(Name = "单用户模式")]           
           public bool OneSession {get;set;}

           /// <summary>
           /// 描述 : 创建时间 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "创建时间")]           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// 描述 : 最后更新时间 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "最后更新时间")]           
           public DateTime UpdateTime {get;set;}

           /// <summary>
           /// 描述 : 创建人编码 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "创建人编码")]           
           public string CreateID {get;set;}

           /// <summary>
           /// 描述 : 创建人 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "创建人")]           
           public string CreateName {get;set;}

           /// <summary>
           /// 描述 : 更新人编码 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "更新人编码")]           
           public string UpdateID {get;set;}

           /// <summary>
           /// 描述 : 更新人 
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "更新人")]           
           public string UpdateName {get;set;}

           /// <summary>
           /// 描述 : 超级管理员 
           /// 空值 : False
           /// 默认 : 0
           /// </summary>
           [Display(Name = "超级管理员")]           
           public bool Administrator {get;set;}

    }
}