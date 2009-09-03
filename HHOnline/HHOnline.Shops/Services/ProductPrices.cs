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

        /// <summary>
        /// 根据级别获取市场价（UserLevelE,PriceE）<see cref="HHOnline.Framework.UserLevel"/>
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public decimal? GetPriceE(int userID, int productID)
        {
            List<string> filter = GetFilter(userID, UserLevel.E);
            return ShopDataProvider.Instance.GetGradePrice(filter, productID, UserLevel.E);
        }

        /// <summary>
        ///  根据级别获取市场价（UserLevelD,PriceD）<see cref="HHOnline.Framework.UserLevel"/>
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public decimal? GetPriceD(int userID, int productID)
        {
            List<string> filter = GetFilter(userID, UserLevel.D);
            return ShopDataProvider.Instance.GetGradePrice(filter, productID, UserLevel.D);
        }

        /// <summary>
        ///  根据级别获取市场价（UserLevelC,PriceC）<see cref="HHOnline.Framework.UserLevel"/>
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public decimal? GetPriceC(int userID, int productID)
        {
            List<string> filter = GetFilter(userID, UserLevel.C);
            return ShopDataProvider.Instance.GetGradePrice(filter, productID, UserLevel.C);
        }

        /// <summary>
        ///  根据级别获取市场价（UserLevelB,PriceB）<see cref="HHOnline.Framework.UserLevel"/>
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public decimal? GetPriceB(int userID, int productID)
        {
            List<string> filter = GetFilter(userID, UserLevel.B);
            return ShopDataProvider.Instance.GetGradePrice(filter, productID, UserLevel.B);
        }

        /// <summary>
        ///  根据级别获取市场价（UserLevelA,PriceA）<see cref="HHOnline.Framework.UserLevel"/>
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public decimal? GetPriceA(int userID, int productID)
        {
            List<string> filter = GetFilter(userID, UserLevel.A);
            return ShopDataProvider.Instance.GetGradePrice(filter, productID, UserLevel.A);
        }

        /// <summary>
        /// 根据用户ID获取用户级别筛选条件
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        private List<string> GetFilter(int userID, UserLevel level)
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
