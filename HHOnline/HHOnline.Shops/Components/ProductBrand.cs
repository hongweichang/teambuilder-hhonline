using System;
using HHOnline.Framework;

namespace HHOnline.Shops
{
    /// <summary>
    /// 产品品牌
    /// </summary>
    public class ProductBrand : ExtendedAttributes
    {
        #region --Private Members--
        private IStorageFile _file = null;
        private int _brandID;
        private string _brandName;
        private string _brandLogo;
        private string _brandTitle;
        private string _brandGroup;
        private string _brandAbstract;
        private string _brandContent;
        private int _displayOrder;
        private ComponentStatus _brandStatus = ComponentStatus.Enabled;
        private DateTime _createTime;
        private int _createUser;
        private DateTime _updateTime;
        private int _updateUser;
        #endregion

        #region --Constructor--
        public ProductBrand()
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
                    if (this.BrandID > 0)
                    {
                        _file = FileStorageProvider.Instance(ProductBrands.FileStoreKey)
                            .GetFile(ProductBrands.MakePath(this.BrandID), this.BrandLogo);
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
        ///品牌编号
        ///</summary>
        public int BrandID
        {
            get { return _brandID; }
            set { _brandID = value; }
        }

        ///<summary>
        ///品牌名称（中英文组合）
        ///</summary>
        public string BrandName
        {
            get { return _brandName; }
            set { _brandName = value; }
        }

        ///<summary>
        ///品牌商标文件路径
        ///</summary>
        public string BrandLogo
        {
            get { return _brandLogo; }
            set { _brandLogo = value; }
        }

        ///<summary>
        ///品牌标题说明（一句话描述）
        ///</summary>
        public string BrandTitle
        {
            get { return _brandTitle; }
            set { _brandTitle = value; }
        }

        ///<summary>
        ///品牌类型或分组
        ///</summary>
        public string BrandGroup
        {
            get { return _brandGroup; }
            set { _brandGroup = value; }
        }

        ///<summary>
        ///品牌介绍摘要
        ///</summary>
        public string BrandAbstract
        {
            get { return _brandAbstract; }
            set { _brandAbstract = value; }
        }

        ///<summary>
        ///品牌介绍内容
        ///</summary>
        public string BrandContent
        {
            get { return _brandContent; }
            set { _brandContent = value; }
        }

        ///<summary>
        ///品牌显示顺序（从1到n排序
        ///</summary>
        public int DisplayOrder
        {
            get { return _displayOrder; }
            set { _displayOrder = value; }
        }

        ///<summary>
        ///品牌状态，1启用、2停用
        ///</summary>
        public ComponentStatus BrandStatus
        {
            get { return _brandStatus; }
            set { _brandStatus = value; }
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
