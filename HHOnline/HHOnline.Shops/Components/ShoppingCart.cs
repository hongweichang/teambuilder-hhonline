using System;
using HHOnline.Framework;
using System.Collections.Generic;

namespace HHOnline.Shops
{
    /// <summary>
    /// 购物车
    /// </summary>
    [Serializable]
    public class ShoppingCart
    {
        public ShoppingCart() { }
        private List<Shopping> _Shoppings;
        /// <summary>
        /// 购物篮
        /// </summary>
        public List<Shopping> Shoppings
        {
            get { return _Shoppings; }
            set { _Shoppings = value; }
        }
    }
}
