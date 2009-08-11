using System;
using HHOnline.Framework;


namespace HHOnline.Shops
{
    public class ProductIndustry : ExtendedAttributes
    {
        #region --Private Members--
        private IStorageFile _file = null;
        private int _industryID;
        private string _industryName;
        private string _industryLogo;
        private string _industryTitle;
        private string _industryAbstract;
        private string _industryContent;
        private int _parentID;
        private int _displayOrder;
        private ComponentStatus _industryStatus = ComponentStatus.Enabled;
        private DateTime _createTime;
        private int _createUser;
        private DateTime _updateTime;
        private int _updateUser;
        #endregion

        public override object Copy()
        {
            ProductIndustry industry = this.CreateNewInstance() as ProductIndustry;
            industry.IndustryAbstract = this.IndustryAbstract;
            industry.IndustryContent = this.IndustryContent;
            industry.IndustryID = this.IndustryID;
            industry.IndustryLogo = this.IndustryLogo;
            industry.IndustryName = this.IndustryName;
            industry.IndustryStatus = this.IndustryStatus;
            industry.IndustryTitle = this.IndustryTitle;
            industry.ParentID = this.ParentID;
            industry.DisplayOrder = this.DisplayOrder;
            industry.UpdateTime = this.UpdateTime;
            industry.UpdateUser = this.UpdateUser;
            industry.CreateTime = this.CreateTime;
            industry.CreateUser = this.CreateUser;
            return industry;
        }

        #region --Constructor--
        public ProductIndustry()
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
                    if (this.IndustryID > 0)
                    {
                        _file = FileStorageProvider.Instance(ProductIndustries.FileStoreKey)
                            .GetFile(ProductIndustries.MakePath(this.IndustryID), this.IndustryLogo);
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
        ///行业编号
        ///</summary>
        public int IndustryID
        {
            get { return _industryID; }
            set { _industryID = value; }
        }

        ///<summary>
        ///行业名称
        ///</summary>
        public string IndustryName
        {
            get { return _industryName; }
            set { _industryName = value; }
        }

        ///<summary>
        ///行业标志图片路径
        ///</summary>
        public string IndustryLogo
        {
            get { return _industryLogo; }
            set { _industryLogo = value; }
        }

        ///<summary>
        ///行业标题说明（一句话描述）
        ///</summary>
        public string IndustryTitle
        {
            get { return _industryTitle; }
            set { _industryTitle = value; }
        }

        ///<summary>
        ///行业介绍摘要
        ///</summary>
        public string IndustryAbstract
        {
            get { return _industryAbstract; }
            set { _industryAbstract = value; }
        }

        ///<summary>
        ///行业介绍内容
        ///</summary>
        public string IndustryContent
        {
            get { return _industryContent; }
            set { _industryContent = value; }
        }

        ///<summary>
        ///父行业编号（用于行业细分）
        ///</summary>
        public int ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }

        ///<summary>
        ///行业显示顺序（从1到n排序
        ///</summary>
        public int DisplayOrder
        {
            get { return _displayOrder; }
            set { _displayOrder = value; }
        }

        ///<summary>
        ///行业状态，1启用、2停用
        ///</summary>
        public ComponentStatus IndustryStatus
        {
            get { return _industryStatus; }
            set { _industryStatus = value; }
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
