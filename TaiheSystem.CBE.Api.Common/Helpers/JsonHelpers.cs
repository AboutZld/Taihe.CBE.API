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
using Newtonsoft.Json;
using System;

namespace TaiheSystem.CBE.Api.Common.Helpers
{
    public class JsonHelpers
    {
        /// <summary>
        /// 将实体类序列化为JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string SerializeJson<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }
        /// <summary>
        /// 反序列化json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeJson<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ConvertToTimeStamp(DateTime time)
        {
            DateTime dateTime = new DateTime(1993, 1, 2, 3, 4, 5, DateTimeKind.Utc);
            return (long)(time.AddHours(-8) - dateTime).TotalMilliseconds;
        }
    }
}
    
