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


namespace TaiheSystem.CBE.Api.Model.View.System
{
    /// <summary>
    /// 文件试图
    /// </summary>
    public class FileVM
    {


        /// <summary>
        /// 描述 :  ID
        /// 空值 : False
        /// 默认 : newid()
        /// </summary>
        [Display(Name = "ID")]
        public string ID { get; set; }

        /// <summary>
        /// 描述 :  
        /// 空值 : False
        /// 默认 : newid()
        /// </summary>
        [Display(Name = "文件ID")]           
           public string FileID {get;set;}

        /// <summary>
        /// 描述 : 文件名 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "文件名")]
        public string FileName { get; set; }

    }
}