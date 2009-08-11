using System;
using HHOnline.Framework;

namespace HHOnline.Shops
{
    /// <summary>
    /// 产品图片表
    /// </summary>
    [Serializable]
    public class ProductPicture
    {
        #region --Private Members--
        private IStorageFile _file = null;
        private int _pictureID;
        private int _productID;
        private int _modelID;
        private int _displayOrder = 1;
        private string _pictureName;
        private string _pictureFile;
        private ComponentStatus _pictureStatus = ComponentStatus.Enabled;
        private DateTime _createTime;
        private int _createUser;
        private DateTime _updateTime;
        private int _updateUser;
        private int thumbnailWidth = 40;
        private int thumbnailHeight = 40;
        #endregion

        #region --Constructor--
        public ProductPicture()
        {
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
                    if (this.ProductID > 0)
                    {
                        _file = FileStorageProvider.Instance(ProductPictures.FileStoreKey)
                            .GetFile(ProductPictures.MakePath(this.ProductID), this.PictureFile);
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
        ///图片编号
        ///</summary>
        public int PictureID
        {
            get { return _pictureID; }
            set { _pictureID = value; }
        }

        ///<summary>
        ///产品编号
        ///</summary>
        public int ProductID
        {
            get { return _productID; }
            set { _productID = value; }
        }

        ///<summary>
        ///产品型号编号
        ///</summary>
        public int ModelID
        {
            get { return _modelID; }
            set { _modelID = value; }
        }

        ///<summary>
        ///显示顺序，1为默认图片
        ///</summary>
        public int DisplayOrder
        {
            get { return _displayOrder; }
            set { _displayOrder = value; }
        }

        ///<summary>
        ///图片名称（用于ALT属性）
        ///</summary>
        public string PictureName
        {
            get { return _pictureName; }
            set { _pictureName = value; }
        }

        ///<summary>
        ///图片文件路径
        ///</summary>
        public string PictureFile
        {
            get { return _pictureFile; }
            set { _pictureFile = value; }
        }

        ///<summary>
        ///产品图片状态，1启用、2停用
        ///</summary>
        public ComponentStatus PictureStatus
        {
            get { return _pictureStatus; }
            set { _pictureStatus = value; }
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

        /// <summary>
        /// 缩略图地址
        /// </summary>
        public string ThumbnailUrl
        {
            get
            {
                if (this.File != null)
                    return SiteUrlManager.GetResizedImageUrl(this.File, thumbnailWidth, thumbnailHeight);
                else
                    return SiteUrlManager.GetNoPictureUrl(thumbnailWidth, thumbnailHeight);
            }
        }
        #endregion
    }
}
