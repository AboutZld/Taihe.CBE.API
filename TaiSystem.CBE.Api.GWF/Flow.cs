using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiheSystem.CBE.Api.Model;

namespace TaiheSystem.CBE.Api.GWF
{
    public class Flow
    {
        public static List<Gwf_Flow> OperationList = Core.DbContext.Db.Queryable<Gwf_Flow>().ToList();
    }
}
