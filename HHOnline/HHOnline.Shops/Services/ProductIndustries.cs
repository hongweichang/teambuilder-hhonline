using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HHOnline.Cache;
using HHOnline.Framework;
using HHOnline.Shops.Providers;

namespace HHOnline.Shops
{
    /// <summary>
    /// 产品行业管理类
    /// </summary>
    public class ProductIndustries
    {
        public static string FileStoreKey = "ProductIndustry";

        #region Create
        /// <summary>
        /// 添加行业信息
        /// </summary>
        /// <param name="industry"></param>
        /// <param name="contentStream"></param>
        public static DataActionStatus Create(ProductIndustry industry, Stream contentStream)
        {
            DataActionStatus status;
            industry = ShopDataProvider.Instance.CreateUpdateIndustry(industry, DataProviderAction.Create, out status);

            if (status == DataActionStatus.Success)
            {
                if (contentStream != null && contentStream.Length > 0)
                {
                    FileStorageProvider fs = new FileStorageProvider(FileStoreKey);
                    fs.AddUpdateFile(MakePath(industry.IndustryID), industry.IndustryLogo, contentStream);
                }

                HHCache.Instance.RemoveByPattern(CacheKeyManager.ProductIndustryXpath);
            }
            return status;
        }

        public static string MakePath(int industryID)
        {
            return GlobalSettings.MakePath(industryID);
        }
        #endregion

        #region Update
        /// <summary>
        /// 更新行业信息
        /// </summary>
        /// <param name="industry"></param>
        /// <param name="contentStream"></param>
        public static DataActionStatus Update(ProductIndustry industry, Stream contentStream)
        {
            DataActionStatus status;
            ShopDataProvider.Instance.CreateUpdateIndustry(industry, DataProviderAction.Update, out status);
            if (status == DataActionStatus.Success)
            {
                if (contentStream != null && contentStream.Length > 0)
                {
                    FileStorageProvider fs = new FileStorageProvider(FileStoreKey);
                    fs.AddUpdateFile(MakePath(industry.IndustryID), industry.IndustryLogo, contentStream);
                }
                HHCache.Instance.RemoveByPattern(CacheKeyManager.ProductIndustryXpath);
            }
            return status;
        }
        #endregion

        #region Delete
        /// <summary>
        ///  删除行业信息
        /// </summary>
        /// <param name="industryID"></param>
        public static DataActionStatus Delete(int industryID)
        {
            DataActionStatus status = ShopDataProvider.Instance.DeleteIndustry(industryID);
            if (status == DataActionStatus.Success)
                HHCache.Instance.RemoveByPattern(CacheKeyManager.ProductIndustryXpath);
            return status;
        }
        #endregion

        #region GetProductBrand
        /// <summary>
        /// 根据IndustryID获取行业信息
        /// </summary>
        /// <param name="industryID"></param>
        /// <returns></returns>
        public static ProductIndustry GetProductIndustry(int industryID)
        {
            ProductIndustry industry = null;
            foreach (ProductIndustry child in GetProductIndustries())
            {
                if (child.IndustryID == industryID)
                {
                    industry = child;
                    break;
                }
            }
            return industry;
        }
        #endregion

        #region GetProductIndustries
        /// <summary>
        /// 获取产品行业信息
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<ProductIndustry> GetIndustriesByProductID(int productID)
        {
            List<ProductIndustry> industries = null;
            string cacheKey = CacheKeyManager.GetIndustryKeyByProductID(productID);
            industries = HHCache.Instance.Get(cacheKey) as List<ProductIndustry>;
            if (industries == null)
            {
                industries = ShopDataProvider.Instance.GetIndustriesByProductID(productID);
                HHCache.Instance.Max(cacheKey, industries);
            }
            return industries;
        }


        /// <summary>
        /// 获取所有行业信息
        /// </summary>
        /// <returns></returns>
        public static List<ProductIndustry> GetProductIndustries()
        {
            List<ProductIndustry> industries = null;
            string cacheKey = CacheKeyManager.ProductIndustryKey;
            industries = HHCache.Instance.Get(cacheKey) as List<ProductIndustry>;
            if (industries == null)
            {
                industries = ShopDataProvider.Instance.GetIndustries();
                HHCache.Instance.Max(cacheKey, industries);
            }
            return industries;
        }

        /// <summary>
        /// 获取其下属子行业(为0则取顶级行业)
        /// </summary>
        /// <param name="industryID"></param>
        /// <returns></returns>
        public static List<ProductIndustry> GetChildIndustries(int industryID)
        {
            List<ProductIndustry> child = new List<ProductIndustry>();
            foreach (ProductIndustry industry in GetProductIndustries())
            {
                if (industry.ParentID == industryID)
                {
                    child.Add(industry);
                }
            }
            return child;
        }

        /// <summary>
        /// 获取层次行业数据
        /// </summary>
        /// <returns></returns>
        public static List<ProductIndustry> GetHierarchyIndustries()
        {
            List<ProductIndustry> industries = null;
            string cacheKey = CacheKeyManager.ProductHierIndustryKey;
            industries = HHCache.Instance.Get(cacheKey) as List<ProductIndustry>;
            if (industries == null)
            {
                industries = new List<ProductIndustry>();
                foreach (ProductIndustry industry in GetChildIndustries(0))
                {
                    industries.Add(industry);
                    GetChildIndustires(industry, 0, ref industries);
                }
                HHCache.Instance.Max(cacheKey, industries);
            }
            return industries;
        }

        /// <summary>
        /// 获取<paramref name="industry"/>下子行业
        /// </summary>
        /// <param name="industry"></param>
        /// <param name="deps"></param>
        /// <param name="industries"></param>
        /// <returns></returns>
        private static void GetChildIndustires(ProductIndustry industry, int deps, ref List<ProductIndustry> industries)
        {
            string block = "┗";
            for (int i = 0; i < deps; i++)
            {
                block = "　" + block;
            }
            ProductIndustry industryCopy = null;
            foreach (ProductIndustry child in GetChildIndustries(industry.IndustryID))
            {
                industryCopy = child.Copy() as ProductIndustry;
                if (industry != null)
                {
                    industryCopy.IndustryName = block + child.IndustryName;
                    industries.Add(industryCopy);
                    GetChildIndustires(child, deps + 1, ref industries);
                }
            }
        }
        #endregion
    }
}
