using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiheSystem.CBE.Api.Model;

namespace TaiheSystem.CBE.Api.GWF
{
    public class Node
    {
        public static List<Gwf_Node> NodeList = Core.DbContext.Db.Queryable<Gwf_Node>().ToList();
    }
}
