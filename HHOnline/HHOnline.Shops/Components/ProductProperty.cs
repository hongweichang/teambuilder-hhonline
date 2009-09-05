using System;
using HHOnline.Framework;

namespace HHOnline.Shops
{
    /// <summary>
    /// 产品属性
    /// </summary>
    public class ProductProperty
    {
        #region --Private Members--
        private int _propertyID;
        private string _propertyName;
        private string _propertyValue;
        private string _propertyDesc;
        private int _categoryID;
        private ComponentStatus _dimensionEnabled = ComponentStatus.Enabled;
        private SubCategoryHiddenType _subCategoryHidden = SubCategoryHiddenType.Visible;
        private int _displayOrder;
        private ComponentStatus _propertyStatus = ComponentStatus.Enabled;
        private DateTime _createTime;
        private int _createUser;
        private DateTime _updateTime;
        private int _updateUser;
        #endregion

        #region --Constructor--
        public ProductProperty()
        {
        }
        #endregion

        #region --Public Members--
        /// <summary>
        /// 所属分类
        /// </summary>
        public ProductCategory Category
        {
            get
            {
                return ProductCategories.GetCategory(CategoryID);
            }
        }

        /// <summary>
        ///所属分类名称
        /// </summary>
        public string CategoryName
        {
            get
            {
                if (this.Category != null)
                    return this.Category.CategoryName;
                else
                    return string.Empty;
            }
        }

        ///<summary>
        ///属性定义编号
        ///</summary>
        public int PropertyID
        {
            get { return _propertyID; }
            set { _propertyID = value; }
        }

        ///<summary>
        ///属性定义名称
        ///</summary>
        public string PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }

        /// <summary>
        /// 属性值
        /// </summary>
        public string PropertyValue
        {
            get { return _propertyValue; }
            set { _propertyValue = value; }
        }

        ///<summary>
        ///属性定义描述
        ///</summary>
        public string PropertyDesc
        {
            get { return _propertyDesc; }
            set { _propertyDesc = value; }
        }

        ///<summary>
        ///所属类别编号
        ///</summary>
        public int CategoryID
        {
            get { return _categoryID; }
            set { _categoryID = value; }
        }

        ///<summary>
        ///可否用做分类维度，1可以、2不可以
        ///</summary>
        public ComponentStatus DimensionEnabled
        {
            get { return _dimensionEnabled; }
            set { _dimensionEnabled = value; }
        }

        ///<summary>
        ///属性对子类别是否隐藏，1隐藏、2可见
        ///</summary>
        public SubCategoryHiddenType SubCategoryHidden
        {
            get { return _subCategoryHidden; }
            set { _subCategoryHidden = value; }
        }

        ///<summary>
        ///属性显示顺序（从1到n排序
        ///</summary>
        public int DisplayOrder
        {
            get { return _displayOrder; }
            set { _displayOrder = value; }
        }

        ///<summary>
        ///属性状态，1正常、2停用
        ///</summary>
        public ComponentStatus PropertyStatus
        {
            get { return _propertyStatus; }
            set { _propertyStatus = value; }
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
