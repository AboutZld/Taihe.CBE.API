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
    public class DeptUserVM
    {
        [Display(Name = "子部门")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<DeptUserVM> children;
    }

    public class DeptListVM : Org_Department
    {
        [Display(Name = "子部门")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<DeptListVM> Children { get; set; }
    }
}
