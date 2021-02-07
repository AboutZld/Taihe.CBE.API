using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiheSystem.CBE.Api.Model;

namespace TaiheSystem.CBE.Api.Message.Number
{
    public static class NumberSeed
    {
        public static List<Sys_Number> number; //流水号类别
        public static List<Sys_NumberPrefix> numprefix;//流水号前缀信息
        public static List<Sys_Number_NumPrefix> number_numprefix; //流水号类型信息

        public static string ReadNumberSeed(string NumCode)
        {
            if(number == null || number.Count == 0)
            {
                return "";
            }
            string lastPrefix = GenPrefix(NumCode);

            return lastPrefix;
        }

        static string GenPrefix(string NumCode)
        {
            var NumPrefixList = number_numprefix.Where(x => x.NumCode == NumCode).OrderBy(x => x.PrefixPosition).ToList();
            string Prefix = "";
            DateTime Now = DateTime.Now;
            foreach(var NumPrefix in NumPrefixList)
            {
                switch(NumPrefix.NumPrefixCode)
                {
                    case "P99":
                        Prefix += NumPrefix.PrefixText;
                        break;
                    default:
                        string FormatPrefix = numprefix.First(x => x.NumPrefixCode == NumPrefix.NumPrefixCode).NumPrefix;
                        Prefix += Now.ToString(FormatPrefix);
                        break;
                }
            }

            var Number = NumberSeed.number.First(x => x.NumCode == NumCode);
            if (Number == null)
            {
                return "0000000";
            }

            int num = (int)Number.SNLength;
            if (num > 0)
            {
                Prefix += "{0:";
                for (int i = 0; i < num; i++)
                {
                    Prefix += "0";
                }
                Prefix += "}";
            }
            return Prefix;
        }
    }
}
