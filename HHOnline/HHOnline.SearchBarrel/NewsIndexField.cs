using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.SearchBarrel
{
    /// <summary>
    /// 资讯索引字段
    /// </summary>
    public class NewsIndexField
    {
        public static readonly string ArticleID;

        public static readonly string CategoryID;

        public static readonly string Title;

        public static readonly string SubTitle;

        public static readonly string Abstract;

        public static readonly string Content;

        public static readonly string Date;

        public static readonly string CopyFrom;

        public static readonly string Author;

        public static readonly string Keywords;

        static NewsIndexField()
        {
            ArticleID = "ArticleID";

            CategoryID = "CategoryID";

            Title = "Title";

            SubTitle = "SubTitle";

            Abstract = "Abstract";

            Content = "Content";

            Date = "Date";

            CopyFrom = "CopyFrom";

            Author = "Author";

            Keywords = "Keywords";
        }
    }
}
