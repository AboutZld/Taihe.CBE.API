//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
//     author Taihe
// </auto-generated>
//------------------------------------------------------------------------------
using TaiheSystem.CBE.Api.Model;
using TaiheSystem.CBE.Api.Model.Dto;
using TaiheSystem.CBE.Api.Model.View;
using System.Collections.Generic;
using System.Threading.Tasks;
using SqlSugar;
using System.Linq;

namespace TaiheSystem.CBE.Api.Interfaces
{
    public interface ISysUserRelationService : IBaseService<Sys_UserRelation>
    {

        #region CustomInterface 
        /// <summary>
        /// 获取用户拥有公司
        /// </summary>
        /// <param name="userSession"></param>
        /// <param name="useCache"></param>
        /// <param name="cacheSecond"></param>
        /// <returns></returns>
        List<Base_Company> GetUserCompany(UserSessionVM userSession, bool useCache = false, int cacheSecond = 3600);

        /// <summary>
        /// 获取用户拥有工厂
        /// </summary>
        /// <param name="userSession"></param>
        /// <param name="useCache"></param>
        /// <param name="cacheSecond"></param>
        /// <returns></returns>
        List<Base_Factory> GetUserFactory(UserSessionVM userSession, bool useCache = false, int cacheSecond = 3600);

        /// <summary>
        /// 获取用户拥有车间
        /// </summary>
        /// <param name="userSession"></param>
        /// <param name="useCache"></param>
        /// <param name="cacheSecond"></param>
        /// <returns></returns>
        List<Base_WorkShop> GetUserWorkShop(UserSessionVM userSession, bool useCache = false, int cacheSecond = 3600);

        /// <summary>
        /// 获取用户拥有工序
        /// </summary>
        /// <param name="userSession"></param>
        /// <param name="useCache"></param>
        /// <param name="cacheSecond"></param>
        /// <returns></returns>
        List<Base_ProductProcess> GetUserProductProcess(UserSessionVM userSession, bool useCache = false, int cacheSecond = 3600);

        /// <summary>
        /// 获取用户拥有生产线
        /// </summary>
        /// <param name="userSession"></param>
        /// <param name="useCache"></param>
        /// <param name="cacheSecond"></param>
        /// <returns></returns>
        List<Base_ProductLine> GetUserProductLine(UserSessionVM userSession, bool useCache = false, int cacheSecond = 3600);
        #endregion

    }
}
