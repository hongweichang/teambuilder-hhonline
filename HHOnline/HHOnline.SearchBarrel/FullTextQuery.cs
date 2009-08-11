using System;

namespace HHOnline.SearchBarrel
{
    public class FullTextQuery
    {
        public string Author
        {
            get;
            set;
        }

        public string Keyword
        {
            get;
            set;
        }

        private int pageSize = 20;

        public int PageSize
        {
            get;
            set;
        }

        private int pageIndex = 1;

        public int PageIndex
        {
            get;
            set;
        }

        public string[] TagNames
        {
            get;
            set;
        }
    }
}
