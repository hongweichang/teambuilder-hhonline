using System;
using HHOnline.Framework;

namespace HHOnline.Shops
{
    /// <summary>
    /// 产品型号
    /// </summary>
    [Serializable]
    public class ProductModel : ExtendedAttributes
    {
        #region --Private Members--
        private int _modelID;
        private int _productID;
        private string _modelCode;
        private string _modelName;
        private string _modelDesc;
        private ComponentStatus _modelStatus = ComponentStatus.Enabled;
        private DateTime _createTime;
        private int _createUser;
        private DateTime _updateTime;
        private int _updateUser;
        #endregion

        #region --Constructor--
        public ProductModel()
        {
        }
        #endregion

        #region --Public Members--
        ///<summary>
        ///产品型号编号
        ///</summary>
        public int ModelID
        {
            get { return _modelID; }
            set { _modelID = value; }
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
        ///产品型号编码
        ///</summary>
        public string ModelCode
        {
            get { return _modelCode; }
            set { _modelCode = value; }
        }

        ///<summary>
        ///产品型号名称
        ///</summary>
        public string ModelName
        {
            get { return _modelName; }
            set { _modelName = value; }
        }

        ///<summary>
        ///产品型号描述
        ///</summary>
        public string ModelDesc
        {
            get { return _modelDesc; }
            set { _modelDesc = value; }
        }

        ///<summary>
        ///产品型号状态，1启用、2停用
        ///</summary>
        public ComponentStatus ModelStatus
        {
            get { return _modelStatus; }
            set { _modelStatus = value; }
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
