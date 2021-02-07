using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaiheSystem.CBE.Api.Interfaces;
using TaiheSystem.CBE.Api.Message.Number;
using TaiheSystem.CBE.Api.Model;

namespace TaiheSystem.CBE.Api.Hostd.Common
{
    public sealed class SerialNoHelper
    {
        private static volatile SerialNoHelper helper;
        private static readonly Object syncRoot = new Object();
        

        public SerialNoHelper()
        {
            Core.DbContext db = new Core.DbContext();
            if (NumberSeed.number == null || NumberSeed.number.Count == 0)
            {
                NumberSeed.number = db.SysNumberDb.GetList();
            }
            if (NumberSeed.numprefix == null || NumberSeed.numprefix.Count == 0)
            {
                NumberSeed.numprefix = db.SysNumberPrefixDb.GetList();
            }
            if (NumberSeed.number_numprefix == null || NumberSeed.number_numprefix.Count == 0)
            {
                NumberSeed.number_numprefix = db.SysNumberNumPrefixDb.GetList();
            }
        }

        public static SerialNoHelper Helper
        {
            get
            {
                if (helper == null)
                {
                    lock (syncRoot)
                    {
                        if (helper == null)
                            helper = new SerialNoHelper();
                    }
                }
                return helper;
            }
        }

        /// <summary>
        /// 生成流水号
        /// </summary>
        /// <param name="NumCode">流水号代码</param>
        /// <returns></returns>
        public String Generate(String NumCode)
        {
            
            string numPrefixFormat = NumberSeed.ReadNumberSeed(NumCode);
            lock (syncRoot)
            {
                if (numPrefixFormat.Contains("{0"))
                {
                    Core.DbContext db = new Core.DbContext();
                    var Seed = db.SysNumberSeedDb.GetSingle(x => x.NumCode == NumCode && x.PrefixFormat == numPrefixFormat);

                    if (Seed == null)
                    {
                        db.SysNumberSeedDb.Insert(new Model.Sys_NumberSeed { PrefixFormat = numPrefixFormat, NumCode = NumCode, Seed = 1 });
                        return string.Format(numPrefixFormat, 1);
                    }

                    Seed.Seed = Convert.ToInt32(Seed.Seed) + 1;
                    db.SysNumberSeedDb.Update(Seed);
                    return string.Format(numPrefixFormat, Seed.Seed);
                }
                return numPrefixFormat;

            }
        }
    }
}
