using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TaiheSystem.CBE.Api.Common.Helpers
{
    public static class ComHelper
    {
        public static D Mapper<D, S>(S s)
        {
            D d = Activator.CreateInstance<D>();
            try
            {
                var sType = s.GetType();
                var dType = typeof(D);
                foreach (PropertyInfo sP in sType.GetProperties())
                {
                    foreach (PropertyInfo dP in dType.GetProperties())
                    {
                        if (dP.Name == sP.Name)
                        {
                            dP.SetValue(d, sP.GetValue(s)); break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return d;
        }

        public static D MapperMatch<D, S>(this D d ,S s,params string[] exclude)
        {
            try
            {
                var sType = s.GetType();
                var dType = typeof(D);
                foreach (PropertyInfo sP in sType.GetProperties())
                {
                    if (exclude.Contains(sP.Name))
                        continue;
                    foreach (PropertyInfo dP in dType.GetProperties())
                    {
                        if (dP.Name == sP.Name)
                        {
                            dP.SetValue(d, sP.GetValue(s)); break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return d;
        }
    }
}
