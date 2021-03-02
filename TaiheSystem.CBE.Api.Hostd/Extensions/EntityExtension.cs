using TaiheSystem.CBE.Api.Model;
using System;

namespace TaiheSystem.CBE.Api.Hostd.Extensions
{
    public static class EntityExtension
    {
        public static TSource ToCreate<TSource>(this TSource source, UserSessionVM userSession)
        {
            if(userSession == null)
            {
                throw new Exception("登录信息已失效，请重新登录");
            }
            var types = source.GetType();

            if (types.GetProperty("ID") != null)
            {
                if (types.GetProperty("ID").GetValue(source) == null || types.GetProperty("ID").GetValue(source).ToString() == "")
                {
                    types.GetProperty("ID").SetValue(source, Guid.NewGuid().ToString().ToUpper(), null);
                }
            }

            if (types.GetProperty("CreateTime") != null)
            {
                types.GetProperty("CreateTime").SetValue(source, DateTime.Now, null);
            }

            if (types.GetProperty("UpdateTime") != null)
            {
                types.GetProperty("UpdateTime").SetValue(source, DateTime.Now, null);
            }

            if (types.GetProperty("CreateID") != null)
            {
                types.GetProperty("CreateID").SetValue(source, userSession.UserID, null);

                types.GetProperty("CreateName").SetValue(source, userSession.UserName, null);
            }

            if (types.GetProperty("UpdateID") != null)
            {
                types.GetProperty("UpdateID").SetValue(source, userSession.UserID, null);

                types.GetProperty("UpdateName").SetValue(source, userSession.UserName, null);
            }

            if (types.GetProperty("deleted") != null)
            {
                types.GetProperty("deleted").SetValue(source, false, null);
            }

            return source;
        }

        public static TSource ToUpdate<TSource>(this TSource source, UserSessionVM userSession)
        {
            var types = source.GetType();

            if (types.GetProperty("UpdateTime") != null)
            {
                types.GetProperty("UpdateTime").SetValue(source, DateTime.Now, null);
            }

            if (types.GetProperty("UpdateID") != null)
            {
                types.GetProperty("UpdateID").SetValue(source, userSession.UserID, null);
            }

            if (types.GetProperty("UpdateName") != null)
            {
                types.GetProperty("UpdateName").SetValue(source, userSession.UserName, null);
            }

            return source;
        }

    }
}
