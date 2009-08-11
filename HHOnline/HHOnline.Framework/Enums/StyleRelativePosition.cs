using System;

namespace HHOnline.Framework
{

    public enum StyleRelativePosition
    {
        /// <summary>
        /// 在unspecified and last items前输出
        /// </summary>
        First = 1,

        /// <summary>
        /// 在first and unspecified items后输出
        /// </summary>
        Last = 2,

        /// <summary>
        /// 默认输出选项，在first and last items之间输出
        /// </summary>
        Unspecified = 3


    }

    public class StyleQueueItem
    {
        public StyleRelativePosition Position;
        public string StyleTag;

        public StyleQueueItem(string styleTag, StyleRelativePosition position)
        {
            StyleTag = styleTag;
            Position = position;
        }
    }
}
