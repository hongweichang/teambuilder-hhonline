using System;
using System.Collections.Generic;
using System.IO;
using HHOnline.Framework;

namespace HHOnline.SearchBarrel
{
    /// <summary>
    /// 索引文件管理
    /// </summary>
    public class IndexFileReport
    {
        private BaseSearchSetting searchSetting = null;

        public IndexFileReport(string searchKey)
        {
            searchSetting = SearchConfiguration.GetConfig().SearchSettings[searchKey];
            if (searchSetting == null)
                throw new HHException(ExceptionType.TypeInitFail, "找不到SearchKey(" + searchKey + "),请检查HHOnline.config文件");

            physicalIndexDirectory = searchSetting.PhysicalIndexDirectory;
            indexFileDirectory = searchSetting.IndexFileDirectory;
            searchName = searchSetting.SearchName;
            if (Directory.Exists(physicalIndexDirectory))
            {
                FileInfo[] _indexFiles = (new DirectoryInfo(physicalIndexDirectory)).GetFiles("*.*");
                if (_indexFiles != null && _indexFiles.Length > 0)
                {
                    foreach (FileInfo file in _indexFiles)
                    {
                        this.indexFileSize += file.Length;
                        if (file.LastWriteTime > this.lastModified)
                            this.lastModified = file.LastWriteTime;
                    }
                }
            }
        }

        public void BuildIndex()
        {
            string indexPath = physicalIndexDirectory;
            string indexTempPath = Path.Combine(physicalIndexDirectory, "temp");
            try
            {
                if (System.IO.Directory.Exists(indexTempPath))
                    System.IO.Directory.Delete(indexTempPath, true);
            }
            catch { }
            searchSetting.InitializeIndex(indexTempPath);
            try
            {
                string[] oldIndexFiles = System.IO.Directory.GetFiles(indexPath, "*.*");
                foreach (var oldIndexFile in oldIndexFiles)
                {
                    System.IO.File.Delete(oldIndexFile);
                }

                string[] newIndexFiles = System.IO.Directory.GetFiles(indexTempPath, "*.*");
                foreach (var newIndexFile in newIndexFiles)
                {
                    FileInfo newFile = new FileInfo(newIndexFile);

                    System.IO.File.Copy(newIndexFile, Path.Combine(indexPath, newFile.Name));
                }

                System.IO.Directory.Delete(indexTempPath, true);
            }
            catch { }
        }

        private string indexFileDirectory = string.Empty;
        /// <summary>
        /// 索引文件所在目录
        /// </summary>
        public string IndexFileDirectory
        {
            get { return indexFileDirectory; }
        }

        private string physicalIndexDirectory = string.Empty;
        /// <summary>
        /// 索引文件物理路径
        /// </summary>
        public string PhysicalIndexDirectory
        {
            get { return physicalIndexDirectory; }
        }

        private long indexFileSize = 0L;
        /// <summary>
        /// 索引文件大小
        /// </summary>
        public long IndexFileSize
        {
            get { return indexFileSize; }
        }

        private DateTime lastModified = DateTime.MinValue;
        /// <summary>
        /// 上次更新
        /// </summary>
        public DateTime LastModified
        {
            get { return lastModified; }
        }

        private string searchName = null;

        /// <summary>
        /// 查询名称
        /// </summary>
        public string SearchName
        {
            get { return searchName; }
        }
    }
}
