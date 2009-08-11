using System;

namespace HHOnline.Framework
{
    /// <summary>
    /// 标签类
    /// </summary>
    [Serializable]
    public class Tag : ITag, IComparable
    {
        public Tag()
        { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="itemCount">命中次数</param>
        public Tag(string name, int itemCount)
        {
            _name = name;
            _itemCount = itemCount;
        }

        private string _name;
        private int _itemCount;

        /// <summary>
        /// 命中次数
        /// </summary>
        public int ItemCount
        {
            get { return _itemCount; }
            set { _itemCount = value; }
        }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// IComparable
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (!(obj is Tag))
                throw new ArgumentException("Specified object is not of type Tag");
            Tag tag = (Tag)obj;
            return _itemCount.CompareTo(tag.ItemCount);
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModified { get; set; }

    }
}
