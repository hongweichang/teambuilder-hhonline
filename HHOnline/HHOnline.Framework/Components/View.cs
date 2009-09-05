using System;

namespace HHOnline.Framework
{
    /// <summary>
    /// 计数类
    /// </summary>
    public class ViewCounter
    {
        public ViewCounter()
        { }

        public ViewCounter(int relatedID)
        {
            this.RelatedID = relatedID;
        }

        /// <summary>
        /// 相关ID
        /// </summary>
        public int RelatedID { get; set; }

        /// <summary>
        /// 点击次数
        /// </summary>
        public int Count { get; set; }
    }
}
