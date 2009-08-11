using System;
using System.IO;
using System.Collections.Generic;
using HHOnline.Cache;
using HHOnline.Framework;
using HHOnline.Shops.Providers;

namespace HHOnline.Shops
{
    public class ProductPrices
    {
        /// <summary>
        /// 添加产品价格
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public DataActionStatus Create(ProductPrice price)
        {
            DataActionStatus status;
            ShopDataProvider.Instance.CreateUpdatePrice(price, DataProviderAction.Create, out status);
            return status;
        }

        public DataActionStatus Update(ProductPrice price)
        {
            DataActionStatus status;
            ShopDataProvider.Instance.CreateUpdatePrice(price, DataProviderAction.Update, out status);
            return status;
        }

        public DataActionStatus Delete(int priceID)
        {
            DataActionStatus status = ShopDataProvider.Instance.DeletePrice(priceID);
            return status;
        }

        public ProductPrice GetPrice(int priceID)
        {
            ProductPrice price = null;
            price = ShopDataProvider.Instance.GetPrice(priceID);
            return price;
        }

        public List<ProductPrice> GetPrices(int productID)
        {
            List<ProductPrice> prices = null;
            return prices;
        }
    }
}
