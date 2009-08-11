using System;
using HHOnline.Framework;

namespace HHOnline.Shops
{
    /// <summary>
    /// 产品类别表
    /// </summary>
    [Serializable]
    public class ProductCategory
    {
        #region --Private Members--
        private int _categoryID;
        private string _categoryName;
        private string _categoryDesc;
        private int _parentID;
        private int _propertyID;
        private int _displayOrder;
        private ComponentStatus _categoryStatus = ComponentStatus.Enabled;
        private DateTime _createTime;
        private int _createUser;
        private DateTime _updateTime;
        private int _updateUser;
        #endregion

        #region --Constructor--
        public ProductCategory()
        {
        }
        #endregion

        #region --Public Members--
        ///<summary>
        ///类别定义编号
        ///</summary>
        public int CategoryID
        {
            get { return _categoryID; }
            set { _categoryID = value; }
        }

        ///<summary>
        ///类别定义名称
        ///</summary>
        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        ///<summary>
        ///类别定义描述
        ///</summary>
        public string CategoryDesc
        {
            get { return _categoryDesc; }
            set { _categoryDesc = value; }
        }

        ///<summary>
        ///父类别定义编号
        ///</summary>
        public int ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }

        ///<summary>
        ///分类属性编号
        ///</summary>
        public int PropertyID
        {
            get { return _propertyID; }
            set { _propertyID = value; }
        }

        ///<summary>
        ///类型显示顺序（从1到n排序
        ///</summary>
        public int DisplayOrder
        {
            get { return _displayOrder; }
            set { _displayOrder = value; }
        }

        ///<summary>
        ///类别定义状态，1启用、2停用
        ///</summary>
        public ComponentStatus CategoryStatus
        {
            get { return _categoryStatus; }
            set { _categoryStatus = value; }
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
