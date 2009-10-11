using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using HHOnline.Cache;
using HHOnline.Framework;
using HHOnline.Shops.Providers;


namespace HHOnline.Shops
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class Shoppings
    {
        public static bool ShoppingAdd(Shopping shop)
        {
            return ShopDataProvider.Instance.ShoppingAdd(shop);
        }
        public static bool ShoppingTransfer(string oldUserID, string newUserID)
        {
            return ShopDataProvider.Instance.ShoppingTransfer(oldUserID, newUserID);        
        }
        public static bool ShoppingDelete(int shopID)
        {
            return ShopDataProvider.Instance.ShoppingDelete(shopID);
        }
        public static bool ShoppingUpdate(Shopping shop)
        {
            return ShopDataProvider.Instance.ShoppingUpdate(shop);
        }
        public static List<Shopping> ShoppingLoad(string userID)
        {
            return ShopDataProvider.Instance.ShoppingLoad(userID);
        }
    }
}
