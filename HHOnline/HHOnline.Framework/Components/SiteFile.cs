using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Framework
{
    /// <summary>
    /// 站点文件
    /// </summary>
    public class SiteFile
    {
        private bool _isFolder = false;
        private string _fileName = string.Empty;
        private string _url = string.Empty;
        private IStorageFile _file = null;

        public SiteFile(IStorageFile file)
        {
            this._isFolder = false;
            this._file = file;
        }

        public SiteFile(string folderName, string url)
        {
            this._isFolder = true;
            this._fileName = folderName;
            this._url = url;
        }

        public bool IsFolder
        {
            get { return _isFolder; }
        }

        public IStorageFile File
        {
            get
            {
                return _file;
            }
        }

        public string FileName
        {
            get
            {
                if (this._file != null)
                    return this._file.FileName;
                else
                    return _fileName;
            }
        }

        public string URL
        {
            get
            {
                if (this._file != null)
                    return FileStorageProvider.GetGenericDownloadUrl(this._file);
                else
                    return _url;
            }
        }

    }
}
