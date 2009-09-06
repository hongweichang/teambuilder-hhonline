using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Framework
{
    /// <summary>
    /// 查询返回结果集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SearchResultDataSet<T> : PagingDataSet<T>
    {
        /// <summary>
        /// 查询耗时
        /// </summary>
        public double SearchDuration { get; set; }
    }
}
