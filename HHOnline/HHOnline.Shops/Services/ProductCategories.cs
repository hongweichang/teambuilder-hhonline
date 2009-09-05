using System;
using System.Collections.Generic;
using HHOnline.Cache;
using HHOnline.Framework;
using HHOnline.Shops.Providers;

namespace HHOnline.Shops
{
    /// <summary>
    /// 商品分类管理
    /// </summary>
    public class ProductCategories
    {
        #region Create
        /// <summary>
        /// 增加商品分类
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static DataActionStatus Create(ProductCategory category)
        {
            DataActionStatus status;
            category = ShopDataProvider.Instance.CreateUpdateCategory(category, DataProviderAction.Create, out status);
            if (status == DataActionStatus.Success)
                HHCache.Instance.RemoveByPattern(CacheKeyManager.ProductCategoryXpath);
            return status;
        }
        #endregion

        #region Update
        /// <summary>
        /// 更新商品分类
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static DataActionStatus Update(ProductCategory category)
        {
            DataActionStatus status;
            category = ShopDataProvider.Instance.CreateUpdateCategory(category, DataProviderAction.Update, out status);
            if (status == DataActionStatus.Success)
                HHCache.Instance.RemoveByPattern(CacheKeyManager.ProductCategoryXpath);
            return status;
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除商品分类
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static DataActionStatus Delete(int categoryID)
        {
            return Delete(categoryID.ToString());
            //DataActionStatus status = ShopDataProvider.Instance.DeleteCategory(categoryID);
            //if (status == DataActionStatus.Success)
            //    HHCache.Instance.RemoveByPattern(CacheKeyManager.ProductCategoryXpath);
            //return status;
        }

        /// <summary>
        /// 批量删除商品分类
        /// </summary>
        /// <param name="categoryIDList">ID列表</param>
        /// <returns></returns>
        public static DataActionStatus Delete(string categoryIDList)
        {
            DataActionStatus status = ShopDataProvider.Instance.DeleteCategory(categoryIDList);
            if (status != DataActionStatus.UnknownFailure)
                HHCache.Instance.RemoveByPattern(CacheKeyManager.ProductCategoryXpath);
            return status;
        }
        #endregion

        #region Select
        /// <summary>
        /// 根据ID获取分类信息
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static ProductCategory GetCategory(int categoryID)
        {
            foreach (ProductCategory category in GetCategories())
            {
                if (category.CategoryID == categoryID)
                    return category;
            }
            return null;
        }

        /// <summary>
        /// 获取所有分类信息
        /// </summary>
        /// <returns></returns>
        public static List<ProductCategory> GetCategories()
        {
            string cacheKey = CacheKeyManager.ProductCategoryKey;
            List<ProductCategory> categories = HHCache.Instance.Get(cacheKey) as List<ProductCategory>;
            if (categories == null)
            {
                categories = ShopDataProvider.Instance.GetCategories();
                HHCache.Instance.Insert(cacheKey, categories);
            }
            return categories;
        }

        /// <summary>
        /// 根据属性ID获取产品(为零表示无直接属性)
        /// </summary>
        /// <param name="propertyID"></param>
        /// <returns></returns>
        public static List<ProductCategory> GetCategoriesByPropertyID(int propertyID)
        {
            List<ProductCategory> categories = new List<ProductCategory>();
            foreach (ProductCategory category in GetCategories())
            {
                if (category.PropertyID == propertyID)
                    categories.Add(category);
            }
            return categories;
        }

        /// <summary>
        /// 根据首字母缩写获取分类信息
        /// </summary>
        /// <param name="firstLetter"></param>
        /// <returns></returns>
        public static List<ProductCategory> GetCategoreisByPY(string firstLetter)
        {
            string cacheKey = CacheKeyManager.GetCategoryKeyByPY(firstLetter);
            List<ProductCategory> categories = HHCache.Instance.Get(cacheKey) as List<ProductCategory>;
            if (categories == null)
            {
                categories = ShopDataProvider.Instance.GetCategoriesByPY(firstLetter);
                HHCache.Instance.Insert(cacheKey, categories);
            }
            return categories;
        }

        /// <summary>
        /// 根据产品ID获取分类信息
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<ProductCategory> GetCategoreisByProductID(int productID)
        {
            string cacheKey = CacheKeyManager.GetCategoryKeyByProductID(productID);
            List<ProductCategory> categories = HHCache.Instance.Get(cacheKey) as List<ProductCategory>;
            if (categories == null)
            {
                categories = ShopDataProvider.Instance.GetCategoreisByProductID(productID);
                HHCache.Instance.Insert(cacheKey, categories);
            }
            return categories;
        }

        /// <summary>
        /// 根据ID获取子分类信息(为零则返回顶级分类)
        /// </summary>
        /// <returns></returns>
        public static List<ProductCategory> GetChidCategories(int categoryID)
        {
            List<ProductCategory> childs = new List<ProductCategory>();
            foreach (ProductCategory category in GetCategories())
            {
                if (category.ParentID == categoryID)
                    childs.Add(category);
            }
            return childs;
        }

        /// <summary>
        /// 获取产品下拉框值
        /// </summary>
        /// <returns></returns>
        public static List<KeyValue> GetValueRange()
        {
            List<ProductCategory> categoryList = GetChidCategories(0);
            List<KeyValue> valueRange = new List<KeyValue>();
            foreach (ProductCategory category in categoryList)
            {
                valueRange.Add(new KeyValue(category.CategoryID.ToString(), category.CategoryName));
                AddChildCategory(category, 0, ref valueRange);
            }
            return valueRange;
        }

        private static void AddChildCategory(ProductCategory parent, int deps, ref List<KeyValue> valueRange)
        {
            string block = "┗";
            for (int i = 0; i < deps; i++)
            {
                block = "　" + block;
            }
            foreach (ProductCategory chid in ProductCategories.GetChidCategories(parent.CategoryID))
            {
                valueRange.Add(new KeyValue(chid.CategoryID.ToString(), block + chid.CategoryName));
                AddChildCategory(chid, deps + 1, ref valueRange);
            }
        }
        #endregion
    }
}
