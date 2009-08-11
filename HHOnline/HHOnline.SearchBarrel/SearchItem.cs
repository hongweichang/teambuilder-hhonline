using System;

namespace HHOnline.SearchBarrel
{
    public class SearchItem
    {
        /// <summary>
        /// PostID
        /// </summary>
        public int PostID
        {
            get;
            set;
        }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author
        {
            get;
            set;
        }

        /// <summary>
        /// 主题
        /// </summary>
        public string Subject
        {
            get;
            set;
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Body
        {
            get;
            set;
        }

        private DateTime postDate = DateTime.MinValue;
        private DateTime lastUpdatedDate = DateTime.MinValue;

        /// <summary>
        /// 标签
        /// </summary>
        public string[] Tags
        {
            get;
            set;
        }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime PostDate
        {
            get { return postDate; }
            set { postDate = value; }
        }

        /// <summary>
        /// 上次更新日期
        /// </summary>
        public DateTime LastUpdatedDate
        {
            get { return lastUpdatedDate; }
            set { lastUpdatedDate = value; }
        }
    }
}
