using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Framework
{
    /// <summary>
    /// 返回分页数据集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagingDataSet<T>
    {
        public PagingDataSet()
        {
            totalRecords = 0;
            pageSize = 20;
            pageIndex = 1;
            records = new List<T>();
        }

        private int pageSize;
        private int pageIndex;
        private int totalRecords;
        private List<T> records;

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecords
        {
            get
            {
                return totalRecords;
            }
            set
            {
                totalRecords = value;
            }
        }

        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex
        {
            get
            {
                return this.pageIndex;
            }
            set
            {
                this.pageIndex = value;
            }
        }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize
        {
            get
            {
                return this.pageSize;
            }
            set
            {
                this.pageSize = value;
            }
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        public List<T> Records
        {
            get
            {
                if (records == null)
                    records = new List<T>();
                return records;
            }
            set
            {
                records = value;
            }
        }

        /// <summary>
        /// 是否查询到结果
        /// </summary>
        public bool HasResults
        {
            get { return (totalRecords > 0); }
        }

    }
}
