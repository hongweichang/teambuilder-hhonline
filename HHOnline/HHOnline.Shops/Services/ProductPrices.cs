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
        #region Create/Update/Delete
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
        #endregion

        #region GetPrice/Prices
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
        #endregion

        #region GetMarketPrice /GetPrice
        /// <summary>
        /// 根据用户级别获取对应商品的会员价
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static decimal? GetPriceMember(int userID, int productID)
        {
            List<UserLevel> levels = new List<UserLevel>() { 
                UserLevel.E,UserLevel.D,UserLevel.C,UserLevel.B,UserLevel.A
            };
            decimal? priceMember = null;
            foreach (UserLevel level in levels)
            {
                priceMember = GetPriceGrade(userID, productID, level);
                if (priceMember.HasValue)
                    return priceMember;
            }
            priceMember = GetPriceDefault(productID);
            return priceMember;
        }

        /// <summary>
        /// 获取用户所看到市场价
        /// 根据客户所在区域，获取所有父区域，按照地域最近原则取市场价
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static decimal? GetPriceMarket(int userID, int productID)
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

        /// <summary>
        /// 根据级别获取会员价<see cref="HHOnline.Framework.UserLevel"/>
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static decimal? GetPriceGrade(int userID, int productID, UserLevel level)
        {
            List<string> filter = GetFilter(userID, level);
            return ShopDataProvider.Instance.GetGradePrice(filter, productID, level);
        }

        public static decimal? GetPriceDefault(int productID)
        {
            return ShopDataProvider.Instance.GetDefaultPrice(productID);
        }

        /// <summary>
        /// 根据用户ID获取用户级别筛选条件
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        private static List<string> GetFilter(int userID, UserLevel level)
        {
            //获取筛选条件
            List<string> filters = new List<string>();
            User user = Users.GetUser(userID);
            if (user != null)
            {

                if (user.UserType == UserType.InnerUser)
                {
                    List<UserGrade> grades = UserGradeManager.GetUserGrades(userID);
                    foreach (UserGrade grade in grades)
                    {
                        if (grade.GradeLevel == level && !GlobalSettings.IsNullOrEmpty(grade.GradeLimit))
                        {
                            filters.Add(grade.GradeLimit);
                        }
                    }
                }
                else if (user.UserType == UserType.CompanyUser)
                {
                    List<CustomerGrade> grades = CustomerGradeManager.GetCustomerGrades(user.CompanyID);
                    foreach (CustomerGrade grade in grades)
                    {
                        if (grade.GradeLevel == level && !GlobalSettings.IsNullOrEmpty(grade.GradeLimit))
                        {
                            filters.Add(grade.GradeLimit);
                        }
                    }
                }
            }
            return filters;
        }
        #endregion
    }
}
