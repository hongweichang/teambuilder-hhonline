using System;
using System.IO;
using System.Collections.Generic;
using HHOnline.Cache;
using HHOnline.Framework;
using HHOnline.Shops.Providers;

namespace HHOnline.Shops
{
    /// <summary>
    /// 产品价格管理
    /// </summary>
    public class ProductPrices
    {
        /// <summary>
        /// 添加产品价格
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static DataActionStatus Create(ProductPrice price)
        {
            DataActionStatus status;
            ShopDataProvider.Instance.CreateUpdatePrice(price, DataProviderAction.Create, out status);
            return status;
        }

        /// <summary>
        /// 编辑产品报价
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static DataActionStatus Update(ProductPrice price)
        {
            DataActionStatus status;
            ShopDataProvider.Instance.CreateUpdatePrice(price, DataProviderAction.Update, out status);
            return status;
        }

        /// <summary>
        /// 删除产品报价
        /// </summary>
        /// <param name="priceID"></param>
        /// <returns></returns>
        public static DataActionStatus Delete(int priceID)
        {
            DataActionStatus status = ShopDataProvider.Instance.DeletePrice(priceID);
            return status;
        }

        /// <summary>
        /// 获取产品报价信息
        /// </summary>
        /// <param name="priceID"></param>
        /// <returns></returns>
        public static ProductPrice GetPrice(int priceID)
        {
            ProductPrice price = null;
            price = ShopDataProvider.Instance.GetPrice(priceID);
            return price;
        }

        /// <summary>
        /// 获取产品报价信息
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<ProductPrice> GetPrices(int productID)
        {
            List<ProductPrice> prices = null;
            return prices;
        }
    }
}
