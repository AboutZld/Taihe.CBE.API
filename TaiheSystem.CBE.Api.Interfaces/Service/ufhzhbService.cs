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
    public class ufhzhbService : BaseService<uf_hzhb>, IufhzhbService
    {

        #region CustomInterface 
        /// <summary>
        /// 查询合作伙伴列表（分页）
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<PartnerVM> QueryPartnerPages(PartnerQueryDto parm)
        {
            var source = Db.Queryable<uf_hzhb>()
                .WhereIF(!string.IsNullOrEmpty(parm.mc), (a) => a.mc.Contains(parm.mc))
                .WhereIF(!string.IsNullOrEmpty(parm.bm), (a) => a.bm.Contains(parm.bm))
                .Select(a => new PartnerVM
                {
                    ID = a.ID,
                    bm = a.bm,
                    jgnbbh = a.jgnbbh,
                    dy = a.dy,
                    mc = a.mc,
                    yzbm = a.yzbm,
                    fzr = a.fzr,
                    lxdz = a.lxdz,
                    lxr = a.lxr,
                    lxsj = a.lxsj,
                    dh = a.dh,
                    cz = a.cz,
                    dzyx = a.dzyx,
                    qywz = a.qywz,
                });
            return source.ToPage(new PageParm { PageIndex = parm.PageIndex, PageSize = parm.PageSize, OrderBy = parm.OrderBy, Sort = parm.Sort });
        }
        #endregion

    }
}
