/*
* ==============================================================================
*
* FileName: QueryableExtension.cs
* Created: 2020/5/19 9:24:54
* Author: Taihe
* Description: 
*
* ==============================================================================
*/
using TaiheSystem.CBE.Api.Model;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;

namespace TaiheSystem.CBE.Api.Interfaces
{
    public static class QueryableExtension
    {
        /// <summary>
        /// 读取列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<PagedInfo<T>> ToPageAsync<T>(this ISugarQueryable<T> source, PageParm parm)
        {
            var page = new PagedInfo<T>();
            var total = await source.CountAsync();
            page.TotalCount = total;
            page.TotalPages = total / parm.PageSize;

            if (total % parm.PageSize > 0)
                page.TotalPages++;

            page.PageSize = parm.PageSize;
            page.PageIndex = parm.PageIndex;

            page.DataSource = await source.OrderByIF(!string.IsNullOrEmpty(parm.OrderBy), $"{parm.OrderBy} {(parm.Sort == "desc" ? "desc" : "asc")}").ToPageListAsync(parm.PageIndex, parm.PageSize);
            return page;
        }

        /// <summary>
        /// 读取列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagedInfo<T> ToPage<T>(this ISugarQueryable<T> source, PageParm parm)
        {
            var page = new PagedInfo<T>();
            var total =  source.Count();
            page.TotalCount = total;
            page.TotalPages = total / parm.PageSize;

            if (total % parm.PageSize > 0)
                page.TotalPages++;

            page.PageSize = parm.PageSize;
            page.PageIndex = parm.PageIndex;

            //排序字段新增中括号（[]）防止命名为数据库内置字段报错
            //新增多字段排序
            if(!string.IsNullOrEmpty(parm.OrderBy))
            {
                string[] orderbys = parm.OrderBy.Split(',');
                string[] sorts = parm.Sort.Split(',');
                for(int i =0;i<orderbys.Length;i++)
                {
                    string sort = sorts.Length == orderbys.Length ? sorts[i] : "asc";
                    source.OrderBy($"[{orderbys[i]}] {(sort == "desc" ? "desc" : "asc")}");
                }
            }
            page.DataSource = source.ToPageList(parm.PageIndex, parm.PageSize);
            return page;
        }


        public static PagedInfo<T> ToPage<T>(this ISugarQueryable<T> source, Expression<Func<T, bool>> where, PageParm parm)
        {
            source = source.Where(where);
            var page = new PagedInfo<T>();
            var total = source.Count();
            page.TotalCount = total;
            page.TotalPages = total / parm.PageSize;

            if (total % parm.PageSize > 0)
                page.TotalPages++;

            page.PageSize = parm.PageSize;
            page.PageIndex = parm.PageIndex;

            //排序字段新增中括号（[]）防止命名为数据库内置字段报错
            //新增多字段排序
            if (!string.IsNullOrEmpty(parm.OrderBy))
            {
                string[] orderbys = parm.OrderBy.Split(',');
                string[] sorts = parm.Sort.Split(',');
                for (int i = 0; i < orderbys.Length; i++)
                {
                    string sort = sorts.Length == orderbys.Length ? sorts[i] : "asc";
                    source.OrderBy($"[{orderbys[i]}] {(sort == "desc" ? "desc" : "asc")}");
                }
            }
            page.DataSource = source.ToPageList(parm.PageIndex, parm.PageSize);
            return page;
        }

    }
}
