using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace HHOnline.Framework
{
    /// <summary>
    /// 服务器文件Provider
    /// </summary>
    public class FileStorageProvider
    {
        #region --Members--
        private string _fileStoreKey;

        public static readonly string PlaceHolderFileName = "PlaceHolder";

        /// <summary>
        /// 文件存储关键字
        /// </summary>
        public string FileStoreKey
        {
            get
            {
                return this._fileStoreKey;
            }
        }
        #endregion

        #region --Constructor--
        public static FileStorageProvider Instance(string fileStoreKey)
        {
            return new FileStorageProvider(fileStoreKey);
        }

        public FileStorageProvider(string fileStoreKey)
        {
            this._fileStoreKey = fileStoreKey;
        }
        #endregion

        #region --Add File--
        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="contentStream">文件流</param>
        /// <returns>CFS文件</returns>
        public IStorageFile AddUpdateFile(string path, string fileName, Stream contentStream)
        {
            if (GlobalSettings.IsNullOrEmpty(fileName))
                return null;

            string fullPath = GetFullLocalPath(path, fileName);

            EnsurePathExists(fullPath);

            contentStream.Position = 0;
            using (FileStream outStream = File.OpenWrite(fullPath))
            {
                byte[] buffer = new byte[contentStream.Length > 65536 ? 65536 : contentStream.Length];
                int position = 0;
                while (position < contentStream.Length)
                {
                    int read = contentStream.Read(buffer, 0, buffer.Length);
                    outStream.Write(buffer, 0, read);
                    position += read;
                }

                outStream.Flush();
            }

            FileSystemStorageFile file = new FileSystemStorageFile(_fileStoreKey, path, new FileInfo(fullPath));

            return file;
        }

        public IStorageFile AddFile(string path, string fileName, Stream contentStream, bool ensureUniqueFileName)
        {
            if (!ensureUniqueFileName)
                return AddUpdateFile(path, fileName, contentStream);
            else
                return AddUpdateFile(path, GetUniqueFileName(path, fileName), contentStream);
        }

        protected virtual string GetUniqueFileName(string path, string fileName)
        {
            string updatedFileName;
            IStorageFile file = null;
            int prefixLength = 4;
            Random rand = new Random(DateTime.Now.Millisecond);

            do
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < prefixLength; i++)
                {
                    sb.Append(rand.Next(0, 9).ToString());
                }

                sb.Append(".");
                sb.Append(fileName);
                updatedFileName = sb.ToString();

                file = GetFile(path, updatedFileName);
                prefixLength++;
            } while (file != null);

            return updatedFileName;
        }
        #endregion

        #region --Add Directory--
        /// <summary>
        /// 添加文件夹
        /// </summary>
        /// <param name="path">文件夹名</param>
        public void AddPath(string path)
        {
            AddUpdateFile(path, PlaceHolderFileName, new MemoryStream(System.Text.Encoding.UTF8.GetBytes("Path Placeholder")));
        }
        #endregion

        #region --Get File--
        /// <summary>
        /// 通过路径和文件名获取文件
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <param name="fileName">文件名</param>
        /// <returns>文件</returns>
        public IStorageFile GetFile(string path, string fileName)
        {
            string fullPath = GetFullLocalPath(path, fileName);
            if (File.Exists(fullPath))
                return new FileSystemStorageFile(this._fileStoreKey, path, new FileInfo(fullPath));
            else
                return null;
        }

        /// <summary>
        /// 文件系统下的文件
        /// </summary>
        /// <param name="searchOption"><搜索方式/param>
        /// <returns>文件集合</returns>
        public List<IStorageFile> GetFiles(PathSearchOption searchOption)
        {
            List<IStorageFile> files = new List<IStorageFile>();

            foreach (FileInfo file in (new DirectoryInfo(GetBaseFolder())).
                GetFiles("*.*", searchOption == PathSearchOption.AllPaths ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
            {
                if (file.Name != PlaceHolderFileName && (file.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    FileSystemStorageFile fsFile = new FileSystemStorageFile(this.FileStoreKey, GetPath(file.FullName), file);
                    files.Add(fsFile);
                }
            }

            return files;
        }

        /// <summary>
        /// 通过给定文件夹下的文件
        /// </summary>
        /// <param name="path">文件夹路径</param>
        ///  <param name="searchOption">搜索方式</param>
        /// <returns>文件集合</returns>
        public List<IStorageFile> GetFiles(string path, PathSearchOption searchOption)
        {
            List<IStorageFile> files = new List<IStorageFile>();

            string localPath = GetFullLocalPath(path, string.Empty);
            string parentPath = localPath.Substring(0, localPath.LastIndexOf(Path.DirectorySeparatorChar));

            if (Directory.Exists(localPath))
            {
                foreach (FileInfo file in (new DirectoryInfo(localPath)).GetFiles("*.*",
                    searchOption == PathSearchOption.AllPaths ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
                {
                    if (file.Name != PlaceHolderFileName && (file.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {
                        FileSystemStorageFile fsFile = new FileSystemStorageFile(this._fileStoreKey, GetPath(file.FullName), file);
                        files.Add(fsFile);
                    }
                }
            }
            else if (Directory.Exists(parentPath))
            {
                foreach (FileInfo file in (new DirectoryInfo(parentPath)).GetFiles("*.*", SearchOption.AllDirectories))
                {
                    if ((file.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden
                        && GetPath(file.FullName).StartsWith(path))
                    {
                        FileSystemStorageFile fsFile = new FileSystemStorageFile(this._fileStoreKey, GetPath(file.FullName), file);
                        files.Add(fsFile);
                    }
                }
            }

            return files;
        }
        #endregion

        #region --Get Directory--
        /// <summary>
        /// 获取给定路径的文件夹名称
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public List<string> GetPaths(string path)
        {
            List<string> paths = new List<string>();

            foreach (DirectoryInfo dir in (new DirectoryInfo(GetBaseFolder())).GetDirectories())
            {
                if ((dir.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    paths.Add(GetPath(dir.FullName, false));
                }
            }

            return paths;
        }

        /// <summary>
        /// 获取top-level路径
        /// </summary>
        /// <returns></returns>
        public List<string> GetPaths()
        {
            List<string> paths = new List<string>();

            foreach (DirectoryInfo dir in (new DirectoryInfo(GetBaseFolder())).GetDirectories())
            {
                if ((dir.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    paths.Add(GetPath(dir.FullName, false));
                }
            }

            return paths;
        }
        #endregion

        #region --Delete File--
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="fileName">文件名</param>
        public void Delete(string path, string fileName)
        {
            string fullPath = GetFullLocalPath(path, fileName);
            if (File.Exists(fullPath))
                File.Delete(fullPath);

            DeleteEmptyFolders(GetFullLocalPath(path, string.Empty));
        }

        /// <summary>
        /// 删除所有文件
        /// </summary>
        public void Delete()
        {
            Directory.Delete(GetBaseFolder(), true);
        }

        /// <summary>
        /// 删除目录以其所有文件
        /// </summary>
        /// <param name="path"></param>
        public void Delete(string path)
        {

            string localPath = GetFullLocalPath(path, string.Empty);
            string parentPath = localPath.Substring(0, localPath.LastIndexOf(Path.DirectorySeparatorChar));

            if (Directory.Exists(localPath))
            {
                string parentFolder = (new DirectoryInfo(localPath)).Parent.FullName;

                Directory.Delete(localPath, true);

                DeleteEmptyFolders(parentFolder);
            }
            else if (Directory.Exists(parentPath))
            {
                DirectoryInfo parentDirectory = new DirectoryInfo(parentPath);

                List<string> pathsToDelete = new List<string>();
                foreach (FileInfo file in parentDirectory.GetFiles("*.*", SearchOption.AllDirectories))
                {
                    if ((file.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden
                        && GetPath(file.FullName).StartsWith(path))
                        pathsToDelete.Add(file.FullName);
                }

                foreach (string pathToDelete in pathsToDelete)
                {
                    File.Delete(pathToDelete);
                }

                pathsToDelete = new List<string>();
                foreach (DirectoryInfo directory in parentDirectory.GetDirectories())
                {
                    if (GetPath(directory.FullName).StartsWith(path))
                        pathsToDelete.Add(directory.FullName);
                }

                foreach (string pathToDelete in pathsToDelete)
                {
                    if (Directory.Exists(pathToDelete))
                        Directory.Delete(pathToDelete, true);
                }

                DeleteEmptyFolders(parentDirectory.FullName);
            }
        }
        #endregion

        #region --Helper Method--
        /// <summary>
        /// 获取实际路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetFullLocalPath(string path, string fileName)
        {
            string fullPath = GetBaseFolder();

            if (!string.IsNullOrEmpty(path))
                fullPath = Path.Combine(fullPath, path);

            if (!string.IsNullOrEmpty(fileName))
                fullPath = Path.Combine(fullPath, fileName);

            return fullPath;
        }

        /// <summary>
        /// 删除空文件夹
        /// </summary>
        /// <param name="fullPathToFolder"></param>
        private void DeleteEmptyFolders(string fullPathToFolder)
        {
            DirectoryInfo dir = new DirectoryInfo(fullPathToFolder);
            if (dir.GetFiles("*.*", SearchOption.AllDirectories).Length == 0)
            {
                DirectoryInfo baseDir = new DirectoryInfo(this.GetBaseFolder());
                while (dir.Parent != null
                    && dir.Parent.FullName != baseDir.FullName
                    && dir.Parent.GetFiles("*.*", SearchOption.AllDirectories).Length == 0)
                {
                    dir = dir.Parent;
                }

                dir.Delete(true);
            }
        }

        private string GetPath(string fullLocalPath)
        {
            return GetPath(fullLocalPath, true);
        }

        /// <summary>
        /// 获取相对路径
        /// </summary>
        /// <param name="fullLocalPath">文件路径</param>
        /// <param name="pathIncludesFilename">是否包含文件名</param>
        /// <returns>相对路径</returns>
        private string GetPath(string fullLocalPath, bool pathIncludesFilename)
        {
            string path = pathIncludesFilename ? fullLocalPath.Substring(0, fullLocalPath.LastIndexOf(Path.DirectorySeparatorChar)) : fullLocalPath;
            path = path.Replace(GetBaseFolder(), "");

            if (path.IndexOf(Path.DirectorySeparatorChar) == 0)
                path = path.Substring(1);

            if (path.LastIndexOf(Path.DirectorySeparatorChar) == path.Length)
                path = path.Substring(0, path.Length - 1);

            return path;
        }

        private string GetBaseFolder()
        {
            return GlobalSettings.GetFileStorageFolder(this._fileStoreKey);
        }

        /// <summary>
        /// 确保文件路径存在
        /// </summary>
        /// <param name="directoryName"></param>
        private void EnsurePathExists(string fullLocalPath)
        {
            EnsurePathExists(fullLocalPath, true);
        }

        /// <summary>
        /// 确保文件路径存在
        /// </summary>
        /// <param name="fullLocalPath">文件路径</param>
        /// <param name="pathIncludesFilename">包含文件名</param>
        private void EnsurePathExists(string fullLocalPath, bool pathIncludesFilename)
        {
            string path = pathIncludesFilename ? fullLocalPath.Substring(0, fullLocalPath.LastIndexOf(Path.DirectorySeparatorChar)) : fullLocalPath;
            path = path.Substring(GetBaseFolder().Length);

            string currPath = GetBaseFolder();
            foreach (string pathComponent in path.Split(Path.DirectorySeparatorChar))
            {
                if (pathComponent != string.Empty)
                {
                    currPath = Path.Combine(currPath, pathComponent);
                    if (!Directory.Exists(currPath))
                        Directory.CreateDirectory(currPath);
                }
            }
        }

        /// <summary>
        /// 获取目录地址
        /// </summary>
        /// <param name="pathComponents"></param>
        /// <returns></returns>
        public static string MakePath(params string[] pathComponents)
        {
            return string.Join(new string(Path.DirectorySeparatorChar, 1), pathComponents);
        }

        /// <summary>
        /// 获取下载地址
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetGenericDownloadUrl(IStorageFile file)
        {
            return SiteUrlManager.GetGenericDownloadUrl(file);
        }

        /// <summary>
        /// 通过Url获取IStorageFile
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static IStorageFile GetStorageFileByUrl(string url)
        {
            if (url.Contains("?"))
                url = url.Substring(0, url.IndexOf("?"));

            string path = url;

            int index = path.IndexOf("__key/");
            if (index < 0)
                return null;

            path = path.Substring(index + 6);

            index = path.IndexOf('/');
            if (index < 0)
                return null;

            string fileStoreKey = path.Substring(0, index);

            index = path.LastIndexOf('/');
            if (index < 0)
                return null;

            string fileName = path.Substring(index + 1);

            if (path.Length - (fileStoreKey.Length + fileName.Length + 2) <= 0)
                path = string.Empty;
            else
                path = path.Substring(fileStoreKey.Length + 1, path.Length - (fileStoreKey.Length + fileName.Length + 2));

            fileStoreKey = GlobalSettings.UrlDecodePathComponent(fileStoreKey);
            fileName = GlobalSettings.UrlDecodeFileComponent(fileName);
            path = GlobalSettings.UrlDecodePathComponent(path);

            if (GlobalSettings.IsNullOrEmpty(fileStoreKey) || GlobalSettings.IsNullOrEmpty(fileName))
                return null;

            FileStorageProvider fileProvider = new FileStorageProvider(fileStoreKey);
            if (fileProvider != null)
                return fileProvider.GetFile(path, fileName);
            else
                return null;
        }
        #endregion
    }
}
