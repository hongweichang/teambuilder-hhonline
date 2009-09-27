using System;
using System.IO;

namespace HHOnline.Framework
{
    /// <summary>
    /// 查询配置类
    /// </summary>
    public abstract class BaseSearchSetting
    {
        /// <summary>
        /// 索引文件相对目录
        /// </summary>
        public virtual string IndexFileDirectory { get; set; }

        private string physicalIndexDirecotry = null;
        /// <summary>
        /// 索引文件目录
        /// </summary>
        public string PhysicalIndexDirectory
        {
            get
            {
                if (this.physicalIndexDirecotry == null)
                {
                    physicalIndexDirecotry =
                     Path.Combine(GlobalSettings.IndexDirectory, IndexFileDirectory);
                }
                return this.physicalIndexDirecotry;
            }
        }

        /// <summary>
        /// 查询设置Key
        /// </summary>
        public virtual string SearchKey { get; set; }

        /// <summary>
        /// 查询名称
        /// </summary>
        public virtual string SearchName { get; set; }

        /// <summary>
        /// 初始化索引
        /// </summary>
        /// <param name="indexPath">索引目录</param>
        public abstract void InitializeIndex(string indexPath);

    }
}
