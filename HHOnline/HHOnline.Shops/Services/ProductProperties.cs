using System;
using System.Collections.Generic;
using HHOnline.Cache;
using HHOnline.Framework;
using HHOnline.Shops.Providers;

namespace HHOnline.Shops
{
    /// <summary>
    /// 产品分类属性管理
    /// </summary>
    public class ProductProperties
    {
        #region Create
        /// <summary>
        /// 创建属性
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static DataActionStatus Create(ProductProperty property)
        {
            DataActionStatus status;
            property = ShopDataProvider.Instance.CreateUpdateProperty(property, DataProviderAction.Create, out status);
            HHCache.Instance.RemoveByPattern(CacheKeyManager.ProductPropertyXpath);
            return status;
        }
        #endregion

        #region Update
        /// <summary>
        /// 更新属性
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static DataActionStatus Update(ProductProperty property)
        {
            DataActionStatus status;
            property = ShopDataProvider.Instance.CreateUpdateProperty(property, DataProviderAction.Update, out status);
            HHCache.Instance.RemoveByPattern(CacheKeyManager.ProductPropertyXpath);
            HHCache.Instance.Remove(CacheKeyManager.GetPropertyKeyByID(property.PropertyID));
            return status;
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除分类属性
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static DataActionStatus Delete(int propertyID)
        {
            return Delete(propertyID.ToString());
        }

        /// <summary>
        /// 批量删除分类属性
        /// </summary>
        /// <param name="categoryIDList">ID列表</param>
        /// <returns></returns>
        public static DataActionStatus Delete(string propertyIDList)
        {
            DataActionStatus status = ShopDataProvider.Instance.DeleteProperty(propertyIDList);
            if (status != DataActionStatus.UnknownFailure)
                HHCache.Instance.RemoveByPattern(CacheKeyManager.ProductPropertyXpath);
            return status;
        }
        #endregion

        #region Select
        /// <summary>
        /// 根据ID获取属性信息
        /// </summary>
        /// <param name="propertyID"></param>
        /// <returns></returns>
        public static ProductProperty GetProperty(int propertyID)
        {
            string cacheKey = CacheKeyManager.GetPropertyKeyByID(propertyID);
            ProductProperty property = HHCache.Instance.Get(cacheKey) as ProductProperty;
            if (property == null)
            {
                property = ShopDataProvider.Instance.GetProperty(propertyID);
                HHCache.Instance.Insert(cacheKey, property);
            }
            return property;
        }

        /// <summary>
        /// 获取分类下所有属性（包含继承属性）
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static List<ProductProperty> GetAllPropertyByCategoryID(int categoryID)
        {
            List<ProductProperty> properties = GetPropertiesByCategoryID(categoryID);
            properties.AddRange(GetParentPropertiesByCategoryID(categoryID));
            return properties;
        }

        /// <summary>
        /// 根据分类列表获取所有属性 （包含继承属性）
        /// </summary>
        /// <param name="categoryIDList"></param>
        /// <returns></returns>
        public static List<ProductProperty> GetAllPropertyByCategoryIDList(List<int> categoryIDList)
        {
            Dictionary<int, ProductProperty> properties = new Dictionary<int, ProductProperty>();
            List<ProductProperty> values = new List<ProductProperty>();
            List<ProductProperty> list = null;
            foreach (int categoryID in categoryIDList)
            {
                list = GetAllPropertyByCategoryID(categoryID);
                foreach (ProductProperty property in list)
                {
                    if (!properties.ContainsKey(property.PropertyID))
                        properties.Add(property.PropertyID, property);
                }
            }
            values.AddRange(properties.Values);
            return values;
        }

        /// <summary>
        /// 获取分类下属性信息
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static List<ProductProperty> GetPropertiesByCategoryID(int categoryID)
        {
            string cacheKey = CacheKeyManager.GetPropertyKeyByCategoryID(categoryID);
            List<ProductProperty> properties = HHCache.Instance.Get(cacheKey) as List<ProductProperty>;
            if (properties == null)
            {
                properties = ShopDataProvider.Instance.GetPropertiesByCategoryID(categoryID);
                HHCache.Instance.Insert(cacheKey, properties);
            }
            return properties;
        }

        /// <summary>
        /// 获取产品属性信息
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<ProductProperty> GetPropertiesByProductID(int productID)
        {
            string cacheKey = CacheKeyManager.GetPropertyKeyByProductID(productID);
            List<ProductProperty> properties = HHCache.Instance.Get(cacheKey) as List<ProductProperty>;
            if (properties == null)
            {
                properties = ShopDataProvider.Instance.GetPropertiesByProductID(productID);
                HHCache.Instance.Insert(cacheKey, properties);
            }
            return properties;
        }

        /// <summary>
        /// 获取所有父分类属性信息
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static List<ProductProperty> GetParentPropertiesByCategoryID(int categoryID)
        {
            string cacheKey = CacheKeyManager.GetParentPropertyKeyByCategoryID(categoryID);
            List<ProductProperty> properties = HHCache.Instance.Get(cacheKey) as List<ProductProperty>;
            if (properties == null)
            {
                properties = ShopDataProvider.Instance.GetParentPropertiesByCategoryID(categoryID);
                HHCache.Instance.Insert(cacheKey, properties);
            }
            return properties;
        }
        #endregion
    }
}
