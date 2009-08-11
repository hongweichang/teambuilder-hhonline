using System;
using System.IO;
using System.Text;

namespace HHOnline.Framework
{
    public class FileSystemStorageFile : IStorageFile
    {
        private int _contentLength;
        private string _fileName;
        private string _path;
        private string _fileStoreKey;
        private string _fullLocalPath;

        public FileSystemStorageFile(string fileStoreKey, string path, FileInfo fileInfo)
        {
            this._contentLength = (int)fileInfo.Length;
            this._fileStoreKey = fileStoreKey;
            this._path = path;
            this._fileName = fileInfo.Name;
            this._fullLocalPath = fileInfo.FullName;
        }

        public int ContentLength
        {
            get { return _contentLength; }
        }

        public string FileName
        {
            get { return _fileName; }
        }

        public string FilePath
        {
            get { return _path; }
        }

        public string FileStoreKey
        {
            get { return _fileStoreKey; }
        }

        public string FullLocalPath
        {
            get { return _fullLocalPath; }
        }

        public System.IO.Stream OpenReadStream()
        {
            return File.OpenRead(_fullLocalPath);
        }

        public string GetDownloadUrl()
        {
            return FileStorageProvider.GetGenericDownloadUrl(this);
        }
    }
}
