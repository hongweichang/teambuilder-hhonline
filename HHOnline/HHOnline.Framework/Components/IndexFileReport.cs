using System;


namespace HHOnline.Framework
{
    /// <summary>
    /// 索引文件报告
    /// </summary>
    public class IndexFileReport
    {
        private long indexFileSize = 0L;
        private DateTime lastModified = DateTime.MinValue;
        private string searchName = string.Empty;
        private string indexFileDirectory = string.Empty;
        private string physicalIndexDirectory = string.Empty;

        public IndexFileReport(string indexFileDirectory)
        { }

        /// <summary>
        /// 索引文件所在目录
        /// </summary>
        public string IndexFileDirectory
        {
            get { return indexFileDirectory; }
        }

        /// <summary>
        /// 索引文件物理路径
        /// </summary>
        public string PhysicalIndexDirectory
        {
            get { return physicalIndexDirectory; }
        }

        /// <summary>
        /// 索引文件大小
        /// </summary>
        public long IndexFileSize
        {
            get { return indexFileSize; }
        }

        /// <summary>
        /// 上次更新
        /// </summary>
        public DateTime LastModified
        {
            get { return lastModified; }
        }

        /// <summary>
        /// 索引类型
        /// </summary>
        public string SearchName
        {
            get { return searchName; }
        }
    }
}
