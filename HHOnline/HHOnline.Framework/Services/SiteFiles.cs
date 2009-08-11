using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HHOnline.Framework
{
    /// <summary>
    /// 站点文件管理类
    /// </summary>
    public class SiteFiles
    {
        public const string FileStoreKey = "SiteFiles";

        #region AddFile
        public static IStorageFile AddFile(byte[] content, string folder, string fileName)
        {
            return AddFile(new MemoryStream(content), folder, fileName);
        }

        public static IStorageFile AddFile(Stream contentStream, string folder, string fileName)
        {
            FileStorageProvider fs = new FileStorageProvider(SiteFiles.FileStoreKey);
            IStorageFile file = fs.AddUpdateFile(folder, fileName, contentStream);
            return file;
        }
        #endregion

        #region GetFile
        public static SiteFile GetFile(string folder, string fileName)
        {
            FileStorageProvider fs = new FileStorageProvider(SiteFiles.FileStoreKey);
            IStorageFile file = fs.GetFile(folder, fileName);
            if (file != null && !IsFileHidden(file.FileName))
                return new SiteFile(file);
            else
                return null;
        }

        private static bool IsFileHidden(string fileName)
        {
            return false;
        }

        public static List<SiteFile> GetFiles(string folder)
        {
            List<SiteFile> files = new List<SiteFile>();
            FileStorageProvider fs = new FileStorageProvider(SiteFiles.FileStoreKey);

            foreach (string subFolderPath in fs.GetPaths(folder))
            {
                files.Add(new SiteFile(subFolderPath, ""));
            }

            foreach (IStorageFile file in fs.GetFiles(folder, PathSearchOption.TopLevelPathOnly))
            {
                if (!IsFileHidden(file.FileName))
                    files.Add(new SiteFile(file));
            }

            return files;
        }
        #endregion

        #region Delete Files
        public static void DeleteFile(string fileName)
        {
            string folder = string.Empty;
            string file = fileName;
            if (fileName.Contains(Path.DirectorySeparatorChar.ToString()))
            {
                folder = fileName.Substring(0, fileName.LastIndexOf(Path.DirectorySeparatorChar));
                file = fileName.Substring(fileName.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            }
            FileStorageProvider fs = new FileStorageProvider(SiteFiles.FileStoreKey);
            fs.Delete(folder, file);
        }

        public static void DeleteFolder(string folderName)
        {
            FileStorageProvider fs = new FileStorageProvider(SiteFiles.FileStoreKey);
            fs.Delete(folderName);
        }
        #endregion
    }
}
