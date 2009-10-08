using System;

namespace HHOnline.Framework
{
    [Serializable]
    public class CompanyQualification
    {
        #region --Private Members--
        private int _qualificationID;
        private int _companyID;
        private string _qualificationFile;
        private string _qualificationName;
        private string _qualificationDesc = string.Empty;
        private string _qualificationMemo = string.Empty;
        private IStorageFile _file = null;
        private ComponentStatus _qualificationStatus = ComponentStatus.Enabled;
        private DateTime _createTime;
        private int _createUser;
        private DateTime _updateTime;
        private int _updateUser;
        #endregion

        #region --Constructor--
        public CompanyQualification()
        {
        }

        public CompanyQualification(string fileName)
        {
            this._qualificationName = fileName;
        }

        public CompanyQualification(IStorageFile file)
        {
            this._file = file;
        }

        #endregion

        #region --Public Members--
        /// <summary>
        /// 存储文件信息
        /// </summary>
        public IStorageFile File
        {
            get
            {
                if (_file == null)
                {
                    if (this.CompanyID > 0)
                    {
                        _file = FileStorageProvider.Instance(CompanyQualifications.FileStoreKey)
                            .GetFile(CompanyQualifications.MakePath(this._companyID), GlobalSettings.EnsureHtmlEncoded(this._qualificationName) + ".rar");
                    }
                }
                return _file;
            }
            set
            {
                _file = value;
            }
        }

        /// <summary>
        /// 下载地址
        /// </summary>
        public string Url
        {
            get
            {
                if (this.File != null)
                {
                    return FileStorageProvider.GetGenericDownloadUrl(File);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        ///<summary>
        ///资质证明文件编号
        ///</summary>
        public int QualificationID
        {
            get { return _qualificationID; }
            set { _qualificationID = value; }
        }

        ///<summary>
        ///公司编号
        ///</summary>
        public int CompanyID
        {
            get { return _companyID; }
            set { _companyID = value; }
        }

        ///<summary>
        ///资质证明文件路径
        ///</summary>
        public string QualificationFile
        {
            get { return _qualificationFile; }
            set { _qualificationFile = value; }
        }

        ///<summary>
        ///资质证明文件名称
        ///</summary>
        public string QualificationName
        {
            get { return _qualificationName; }
            set { _qualificationName = value; }
        }

        ///<summary>
        ///资质证明文件描述
        ///</summary>
        public string QualificationDesc
        {
            get { return _qualificationDesc; }
            set { _qualificationDesc = value; }
        }

        ///<summary>
        ///资质证明文件备注
        ///</summary>
        public string QualificationMemo
        {
            get { return _qualificationMemo; }
            set { _qualificationMemo = value; }
        }

        ///<summary>
        ///资质证明状态，1正常、2停用
        ///</summary>
        public ComponentStatus QualificationStatus
        {
            get { return _qualificationStatus; }
            set { _qualificationStatus = value; }
        }

        ///<summary>
        ///创建时间
        ///</summary>
        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        ///<summary>
        ///创建操作人
        ///</summary>
        public int CreateUser
        {
            get { return _createUser; }
            set { _createUser = value; }
        }

        ///<summary>
        ///最后更新时间
        ///</summary>
        public DateTime UpdateTime
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }

        ///<summary>
        ///最后更新操作人
        ///</summary>
        public int UpdateUser
        {
            get { return _updateUser; }
            set { _updateUser = value; }
        }
        #endregion
    }
}

