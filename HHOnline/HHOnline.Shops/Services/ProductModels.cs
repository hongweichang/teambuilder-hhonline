using System;
using System.Web;
using System.Collections.Generic;
using HHOnline.Cache;
using HHOnline.Framework;
using HHOnline.Shops.Providers;

namespace HHOnline.Shops
{
    /// <summary>
    /// 产品型号管理
    /// </summary>
    public class ProductModels
    {
        #region Create
        /// <summary>
        /// 添加产品型号
        /// </summary>
        /// <param name="model"></param>
        public static void Create(ProductModel model)
        {
            model = ShopDataProvider.Instance.CreateUpdateModel(model, DataProviderAction.Create);
            HHCache.Instance.Remove(CacheKeyManager.GetModelKeyByID(model.ModelID));
            HHCache.Instance.Remove(CacheKeyManager.GetModelKeyByProductID(model.ProductID));
        }
        #endregion

        #region Update
        /// <summary>
        /// 更新产品型号
        /// </summary>
        /// <param name="model"></param>
        public static void Update(ProductModel model)
        {
            ShopDataProvider.Instance.CreateUpdateModel(model, DataProviderAction.Update);
            HHCache.Instance.Remove(CacheKeyManager.GetModelKeyByID(model.ModelID));
            HHCache.Instance.Remove(CacheKeyManager.GetModelKeyByProductID(model.ProductID));
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除产品型号
        /// </summary>
        /// <param name="modelID"></param>
        public static void Delete(int modelID)
        {
            ShopDataProvider.Instance.DeleteModel(modelID);
            HHCache.Instance.Remove(CacheKeyManager.GetModelKeyByID(modelID));
            HHCache.Instance.Remove(CacheKeyManager.GetModelKeyByProductID(GetModel(modelID).ProductID));
        }
        #endregion

        #region Get
        /// <summary>
        /// 获取产品型号
        /// </summary>
        /// <param name="modelID"></param>
        /// <returns></returns>
        public static ProductModel GetModel(int modelID)
        {
            string cacheKey = CacheKeyManager.GetModelKeyByID(modelID);
            ProductModel model = HHCache.Instance.Get(cacheKey) as ProductModel;
            if (model == null)
            {
                model = ShopDataProvider.Instance.GetModel(modelID);
                HHCache.Instance.Insert(cacheKey, model);
            }
            return model;
        }

        /// <summary>
        /// 根据产品编号获取产品型号
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<ProductModel> GetModelsByProductID(int productID)
        {
            string cacheKey = CacheKeyManager.GetModelKeyByProductID(productID);
            List<ProductModel> models = HHCache.Instance.Get(cacheKey) as List<ProductModel>;
            if (models == null)
            {
                models = ShopDataProvider.Instance.GetModelsByProductID(productID);
            }
            return models;
        }
        #endregion
    }
}