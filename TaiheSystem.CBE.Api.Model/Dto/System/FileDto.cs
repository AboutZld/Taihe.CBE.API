/*
* ==============================================================================
*
* FileName: RolesDto.cs
* Created: 2020/5/29 10:45:49
* Author: Taihe
* Description: 
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaiheSystem.CBE.Api.Model.Dto
{

    /// <summary>
    /// 文件管理
    /// </summary>
    public class FileDto : PageParm
    {

        /// <summary>
        /// 描述 : 查询字符串 
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "查询字符串")]
        public string QueryText { get; set; }

    }

    /// <summary>
    /// 添加文件
    /// </summary>
    public class FileCreateDto
    {
        /// <summary>
        /// 描述 :  
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public Guid ID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public Guid FileID { get; set; }

        /// <summary>
        /// 描述 : 文件名 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "文件名")]
        public string FileName { get; set; }

        /// <summary>
        /// 描述 : 文件后缀 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "文件后缀")]
        public string FileExt { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string FileType { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public Guid? FileGroup { get; set; }

        /// <summary>
        /// 描述 : 存放文件夹路径 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "存放文件夹路径")]
        public string FileRootPath { get; set; }

        /// <summary>
        /// 描述 : 绝对路径 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "绝对路径")]
        public string AbsoluteFilePath { get; set; }

        /// <summary>
        /// 描述 : 相对路径 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "相对路径")]
        public string RelativeFilePath { get; set; }

        /// <summary>
        /// 描述 : 链接文件 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "链接文件")]
        public string Url { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public int? FileSize { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public int? DownloadCount { get; set; }

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
        public string CreateUserID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string CreateUserName { get; set; }

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
        public int? UpdateUserID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string UpdtaeUserName { get; set; }
    }

    /// <summary>
    /// 更新用户
    /// </summary>
    public class FileUpdateDto
    {
        /// <summary>
        /// 描述 :  
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public Guid ID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public Guid FileID { get; set; }

        /// <summary>
        /// 描述 : 文件名 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "文件名")]
        public string FileName { get; set; }

        /// <summary>
        /// 描述 : 文件后缀 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "文件后缀")]
        public string FileExt { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string FileType { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public Guid? FileGroup { get; set; }

        /// <summary>
        /// 描述 : 存放文件夹路径 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "存放文件夹路径")]
        public string FileRootPath { get; set; }

        /// <summary>
        /// 描述 : 绝对路径 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "绝对路径")]
        public string AbsoluteFilePath { get; set; }

        /// <summary>
        /// 描述 : 相对路径 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "相对路径")]
        public string RelativeFilePath { get; set; }

        /// <summary>
        /// 描述 : 链接文件 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "链接文件")]
        public string Url { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public int? FileSize { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public int? DownloadCount { get; set; }

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
        public string CreateUserID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string CreateUserName { get; set; }

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
        public int? UpdateUserID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "")]
        public string UpdtaeUserName { get; set; }
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    public class FileDeleteDto 
    {

        /// <summary>
        /// 描述 : 删除文件
        /// 空值 : False
        /// 默认 : 
        /// </summary>
        [Display(Name = "删除文件")]
        public List<string> IDs { get; set; }
    }


}
