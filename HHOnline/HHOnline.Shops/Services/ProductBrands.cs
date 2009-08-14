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
    /// 产品品牌管理类
    /// </summary>
    public class ProductBrands
    {
        public static string FileStoreKey = "ProductBrand";

        #region Create
        /// <summary>
        /// 添加品牌信息
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="contentStream"></param>
        public static DataActionStatus Create(ProductBrand brand, Stream contentStream)
        {
            DataActionStatus status;
            brand = ShopDataProvider.Instance.CreateUpdateBrand(brand, DataProviderAction.Create, out status);
            if (status == DataActionStatus.Success)
            {
                if (contentStream != null && contentStream.Length > 0)
                {
                    FileStorageProvider fs = new FileStorageProvider(FileStoreKey);
                    fs.AddUpdateFile(MakePath(brand.BrandID), brand.BrandLogo, contentStream);
                }
                HHCache.Instance.RemoveByPattern(CacheKeyManager.ProductBrandXpath);
                OnUpdated();
                //HHCache.Instance.Remove(CacheKeyManager.ProductBrandKey);
            }
            return status;
        }

        public static string MakePath(int brandID)
        {
            return GlobalSettings.MakePath(brandID);
        }
        #endregion

        #region Update
        /// <summary>
        /// 更新品牌信息
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="contentStream"></param>
        public static DataActionStatus Update(ProductBrand brand, Stream contentStream)
        {
            DataActionStatus status;
            ShopDataProvider.Instance.CreateUpdateBrand(brand, DataProviderAction.Update, out status);
            if (status == DataActionStatus.Success)
            {
                if (contentStream != null && contentStream.Length > 0)
                {
                    FileStorageProvider fs = new FileStorageProvider(FileStoreKey);
                    fs.AddUpdateFile(MakePath(brand.BrandID), brand.BrandLogo, contentStream);
                }
                HHCache.Instance.Remove(CacheKeyManager.GetProductBrandKey(brand.BrandID));
                HHCache.Instance.RemoveByPattern(CacheKeyManager.ProductBrandXpath);
                OnUpdated();
                //HHCache.Instance.Remove(CacheKeyManager.ProductBrandKey);
            }
            return status;
        }
        #endregion

        #region Delete
        public static DataActionStatus DeleteBrand(int brandID)
        {
            DataActionStatus status = ShopDataProvider.Instance.DeleteBrand(brandID);
            HHCache.Instance.Remove(CacheKeyManager.GetProductBrandKey(brandID));
            HHCache.Instance.RemoveByPattern(CacheKeyManager.ProductBrandXpath);
            OnUpdated();
            return status;
        }
        #endregion

        #region GetProductBrand
        /// <summary>
        /// 根据BrandID获取品牌
        /// </summary>
        /// <param name="brandID"></param>
        /// <returns></returns>
        public static ProductBrand GetProductBrand(int brandID)
        {
            ProductBrand brand = null;
            string cacheKey = CacheKeyManager.GetProductBrandKey(brandID);
            brand = HHCache.Instance.Get(cacheKey) as ProductBrand;
            if (brand == null)
            {
                foreach (ProductBrand child in GetProductBrands())
                {
                    if (child.BrandID == brandID)
                    {
                        brand = child;
                        break;
                    }
                }
                if (brand == null)
                    brand = ShopDataProvider.Instance.GetBrand(brandID);
                HHCache.Instance.Insert(cacheKey, brand);
            }
            return brand;
        }
        #endregion

        #region GetProductBrands
        /// <summary>
        /// 获取所有品牌
        /// </summary>
        /// <returns></returns>
        public static List<ProductBrand> GetProductBrands()
        {
            List<ProductBrand> brands = null;
            string cacheKey = CacheKeyManager.ProductBrandKey;
            brands = HHCache.Instance.Get(cacheKey) as List<ProductBrand>;
            if (brands == null)
            {
                brands = ShopDataProvider.Instance.GetBrands();
                HHCache.Instance.Max(cacheKey, brands);
            }
            return brands;
        }

        /// <summary>
        /// 获取所有品牌分组
        /// </summary>
        /// <returns></returns>
        public static List<string> GetBrandGroup()
        {
            List<string> brandGroups = new List<string>();
            foreach (ProductBrand brand in GetProductBrands())
            {
                if (!GlobalSettings.IsNullOrEmpty(brand.BrandGroup) && !brandGroups.Contains(brand.BrandGroup))
                    brandGroups.Add(brand.BrandGroup);
            }
            return brandGroups;
        }
        #endregion

        #region -EventHandler-
        public static EventHandler<EventArgs> Updated;
        protected static void OnUpdated()
        {
            if (Updated != null)
            {
                Updated(null, EventArgs.Empty);
            }
        }
        #endregion
    }
}
