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


    [SugarTable("gnl_File")]
    public partial class gnl_File
    {
           public gnl_File(){

            this.CreateTime =DateTime.Now;

           }
           /// <summary>
           /// 描述 :  
           /// 空值 : False
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           [SugarColumn(IsPrimaryKey=true)]
           public string FileID {get;set;}

           /// <summary>
           /// 描述 : 文件名 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "文件名")]           
           public string FileName {get;set;}

           /// <summary>
           /// 描述 : 文件后缀 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "文件后缀")]           
           public string FileExt {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string FileType {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string FileGroup {get;set;}

           /// <summary>
           /// 描述 : 存放文件夹路径 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "存放文件夹路径")]           
           public string FileRootPath {get;set;}

           /// <summary>
           /// 描述 : 绝对路径 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "绝对路径")]           
           public string AbsoluteFilePath {get;set;}

           /// <summary>
           /// 描述 : 相对路径 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "相对路径")]           
           public string RelativeFilePath {get;set;}

           /// <summary>
           /// 描述 : 链接文件 
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "链接文件")]           
           public string Url {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public int? FileSize {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public int? DownloadCount {get;set;}

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
           public string CreateUserID {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string CreateUserName {get;set;}

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
           public string UpdateUserID {get;set;}

           /// <summary>
           /// 描述 :  
           /// 空值 : True
           /// 默认 : 
           /// </summary>
           [Display(Name = "")]           
           public string UpdtaeUserName {get;set;}

    }
}
