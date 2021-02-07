using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace TaiheSystem.CBE.Api.GWF
{
    public class AfterDone
    {
        public AfterDone(SqlSugarClient db, object jContext)
        {
            _cmd = db;
            _jContext = jContext;
        }
        private SqlSugarClient _cmd;
        private object _jContext;
    }
}
