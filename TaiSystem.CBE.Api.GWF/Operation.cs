using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiheSystem.CBE.Api.Model;

namespace TaiheSystem.CBE.Api.GWF
{
    public class Operation
    {
        public static List<Gwf_Operation> OperationList = Core.DbContext.Db.Queryable<Gwf_Operation>().ToList();
    }
}
