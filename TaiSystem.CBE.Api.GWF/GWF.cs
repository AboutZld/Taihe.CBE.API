using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TaiheSystem.CBE.Api.GWF
{
    public class GWF
    {
        //后置操作字典
        public static Dictionary<string, MethodInfo> DicAfterDone { get; set; }
    }
}
