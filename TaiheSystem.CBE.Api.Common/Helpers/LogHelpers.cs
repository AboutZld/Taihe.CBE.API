/*
* ==============================================================================
*
* FileName: LogHelpers.cs
* Created: 2020/6/19 14:20:10
* Author: Taihe
* Description: 
*
* ==============================================================================
*/
using System;

namespace TaiheSystem.CBE.Api.Common.Helpers
{
    public class LogHelpers
    {
        public static string logWrite(string message)
        {
            return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}] => {message}";
        }
    }
}
