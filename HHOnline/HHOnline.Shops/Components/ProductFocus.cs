using System;
using HHOnline.Framework;

namespace HHOnline.Shops
{
    [Serializable]
    public class ProductFocus : ExtendedAttributes
    {
        #region --Private Members--
        private int _focusID;
        private FocusType _focusType;
        private DateTime _focusFrom;
        private DateTime _focusEnd;
        private int _productID;
        private int _modelID;
        private int _displayOrder;
        private string _focusMemo;
        private ComponentStatus _focusStatus = ComponentStatus.Enabled;
        private DateTime _createTime;
        private int _createUser;
        private DateTime _updateTime;
        private int _updateUser;
        #endregion

        #region --Constructor--
        public ProductFocus()
        {
        }
        #endregion

        #region --Public Members--
        ///<summary>
        ///产品关注编号
        ///</summary>
        public int FocusID
        {
            get { return _focusID; }
            set { _focusID = value; }
        }

        ///<summary>
        ///关注类型，1新品上架、2热卖产品、3最受关注、4促销产品
        ///</summary>
        public FocusType FocusType
        {
            get { return _focusType; }
            set { _focusType = value; }
        }

        ///<summary>
        ///关注起始日期
        ///</summary>
        public DateTime FocusFrom
        {
            get { return _focusFrom; }
            set { _focusFrom = value; }
        }

        ///<summary>
        ///关注截止日期
        ///</summary>
        public DateTime FocusEnd
        {
            get { return _focusEnd; }
            set { _focusEnd = value; }
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
        ///关注展示排序（相同关注类型1到n排序）
        ///</summary>
        public int DisplayOrder
        {
            get { return _displayOrder; }
            set { _displayOrder = value; }
        }

        ///<summary>
        ///关注描述备注
        ///</summary>
        public string FocusMemo
        {
            get { return _focusMemo; }
            set { _focusMemo = value; }
        }

        ///<summary>
        ///产品关注状态，1启用、2停用
        ///</summary>
        public ComponentStatus FocusStatus
        {
            get { return _focusStatus; }
            set { _focusStatus = value; }
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
