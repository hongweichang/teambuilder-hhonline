using System;
using HHOnline.Framework;

namespace HHOnline.Shops
{
    /// <summary>
    /// 购物信息
    /// </summary>
    [Serializable]
    public class Shopping
    {
        public Shopping() { }
        private int _ShoppingID;
        /// <summary>
        /// 购物信息编号
        /// </summary>
        public int ShoppingID
        {
            get { return _ShoppingID; }
            set { _ShoppingID = value; }
        }
        private string _UserID;
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        private int _ProductID;
        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        private int _ModelID;
        /// <summary>
        /// 型号编码
        /// </summary>
        public int ModelID
        {
            get { return _ModelID; }
            set { _ModelID = value; }
        }
        private int _Quantity;
        /// <summary>
        /// 数目
        /// </summary>
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        private string _ShoppingMemo;
        /// <summary>
        /// 备注
        /// </summary>
        public string ShoppingMemo
        {
            get { return _ShoppingMemo; }
            set { _ShoppingMemo = value; }
        }
        private DateTime _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }
        private DateTime _UpdateTime;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            get { return _UpdateTime; }
            set { _UpdateTime = value; }
        }

    }
}
