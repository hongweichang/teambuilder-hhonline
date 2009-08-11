using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Framework
{
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

        public bool HasResults
        {
            get { return (totalRecords > 0); }
        }

    }
}
