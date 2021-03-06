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
    public interface IBaseProductLineService : IBaseService<Base_ProductLine>
    {

        #region CustomInterface 
        /// <summary>
        /// 查询产线（分页）
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        PagedInfo<ProductLineVM> QueryLinePages(ProductLineQueryDto parm);

        /// <summary>
        /// 根据ID查询产线
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProductLineVM GetLine(string id);

        /// <summary>
        /// 查询所有产线
        /// </summary>
        /// <returns></returns>
        List<ProductLineVM> GetAllLine(bool? enable = null);

        /// <summary>
        /// 查询同工序下是否存在相同生产线编码
        /// </summary>
        /// <param name="lineNo"></param>
        /// <param name="processId"></param>
        /// <returns></returns>
        bool Any(string Id, string lineNo, string processId);

        #endregion

    }
}
