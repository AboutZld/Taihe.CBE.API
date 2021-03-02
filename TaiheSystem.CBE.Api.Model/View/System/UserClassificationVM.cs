/*
* ==============================================================================
*
* FileName: MenusVM.cs
* Created: 2020/5/25 13:41:09
* Author: Taihe
* Description: 
*
* ==============================================================================
*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaiheSystem.CBE.Api.Model.View
{

    public class UserClassificationVM : Sys_User_Classification
    {
        /// <summary>
        /// 描述 : 业务类别名称 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "业务类别名称")]
        public string SystemTypeName { get; set; }

        /// <summary>
        /// 描述 : 资格类型 
        /// 空值 : True
        /// 默认 : 
        /// </summary>
        [Display(Name = "资格类型")]
        public string QualificationTypeName { get; set; }
    }
}
