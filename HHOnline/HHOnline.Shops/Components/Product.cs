using System;
using HHOnline.Framework;

namespace HHOnline.Shops
{
    /// <summary>
    /// 产品信息类
    /// </summary>
    public class Product : ExtendedAttributes
    {

        #region --Private Members--
        private int _productID;
        private string _productCode;
        private string _productName;
        private string _productDesc;
        private string _productAbstract;
        private string _productContent;
        private int _brandID;
        private string _productKeywords;
        private ComponentStatus _productStatus = ComponentStatus.Enabled;
        private int _displayOrder;
        private DateTime _createTime;
        private int _createUser;
        private DateTime _updateTime;
        private int _updateUser;
        private IStorageFile _defaultImageFile;
        #endregion

        #region --Constructor--
        public Product()
        {
        }
        #endregion

        #region --Public Members--
        /// <summary>
        /// 默认图片文件
        /// </summary>
        public IStorageFile DefaultImageFile
        {
            get
            {
                if (_defaultImageFile == null)
                {
                    ProductPicture defaultPicture = ProductPictures.GetDefaultPicture(ProductID);
                    if (defaultPicture != null)
                    {
                        _defaultImageFile = defaultPicture.File;
                    }
                }
                return _defaultImageFile;
            }
        }

        /// <summary>
        /// 默认图片Url
        /// </summary>
        public string GetDefaultImageUrl(int width, int height)
        {
            if (width == 0 || height == 0)
            {
                if (DefaultImageFile != null)
                    return FileStorageProvider.GetGenericDownloadUrl(DefaultImageFile);
                else
                    return SiteUrlManager.GetNoPictureUrl();
            }
            else
            {
                if (DefaultImageFile == null)
                {
                    return SiteUrlManager.GetNoPictureUrl(width, height);
                }
                else
                {
                    return SiteUrlManager.GetResizedImageUrl(DefaultImageFile, width, height);
                }
            }
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
        ///产品编码
        ///</summary>
        public string ProductCode
        {
            get { return _productCode; }
            set { _productCode = value; }
        }

        ///<summary>
        ///产品名称
        ///</summary>
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        ///<summary>
        ///产品说明（名称扩展）
        ///</summary>
        public string ProductDesc
        {
            get { return _productDesc; }
            set { _productDesc = value; }
        }

        ///<summary>
        ///产品描述摘要
        ///</summary>
        public string ProductAbstract
        {
            get { return _productAbstract; }
            set { _productAbstract = value; }
        }

        ///<summary>
        ///产品描述内容
        ///</summary>
        public string ProductContent
        {
            get { return _productContent; }
            set { _productContent = value; }
        }

        ///<summary>
        ///产品品牌
        ///</summary>
        public int BrandID
        {
            get { return _brandID; }
            set { _brandID = value; }
        }

        /// <summary>
        /// 产品品牌名称
        /// </summary>
        public string BrandName
        {
            get
            {
                ProductBrand brand = ProductBrands.GetProductBrand(_brandID);
                if (brand != null)
                    return brand.BrandName;
                else
                    return string.Empty;
            }
        }


        ///<summary>
        ///产品关键字组，逗号分隔
        ///</summary>
        public string ProductKeywords
        {
            get { return _productKeywords; }
            set { _productKeywords = value; }
        }

        ///<summary>
        ///产品状态，1启用、2停用
        ///</summary>
        public ComponentStatus ProductStatus
        {
            get { return _productStatus; }
            set { _productStatus = value; }
        }

        /// <summary>
        /// 排列顺序
        /// </summary>
        public int DisplayOrder
        {
            get { return _displayOrder; }
            set { _displayOrder = value; }
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
