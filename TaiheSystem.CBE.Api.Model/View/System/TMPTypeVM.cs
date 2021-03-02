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
    public class TMPTypeVM
    {
        [Display(Name = "子部门")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<TMPTypeVM> children;
    }

    public class TMPTypeListVM : Doc_TMPTYPE
    {
        [Display(Name = "子部门")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<TMPTypeListVM> Children { get; set; }
    }
}
