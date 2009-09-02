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
            prices = ShopDataProvider.Instance.GetPrices(productID);
            return prices;
        }

        /// <summary>
        /// 获取用户所看到市场价
        /// 根据客户所在区域，获取所有父区域，按照地域最近原则取市场价
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public decimal? GetMarketPrice(int userID, int productID)
        {
            string areaIDList = string.Empty;
            User user = Users.GetUser(userID);
            if (user != null)
            {
                if (user.Company != null)
                {
                    areaIDList = Areas.GetParentAreaIDList(user.Company.CompanyRegion);
                }
            }
            return ShopDataProvider.Instance.GetMarketPrice(areaIDList, productID);
        }
    }
}
