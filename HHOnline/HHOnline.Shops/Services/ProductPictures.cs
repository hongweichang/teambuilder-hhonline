using System;
using System.IO;
using System.Collections.Generic;
using HHOnline.Cache;
using HHOnline.Framework;
using HHOnline.Shops.Providers;

namespace HHOnline.Shops
{
    /// <summary>
    /// 产品图片管理类
    /// </summary>
    public class ProductPictures
    {
        public static string FileStoreKey = "ProductPicture";

        #region Get
        public static ProductPicture GetDefaultPicture(int productID)
        {
            return ShopDataProvider.Instance.GetDefaultPicture(productID);
        }

        /// <summary>
        /// 根据产品ID获取图片信息
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<ProductPicture> GetPictures(int productID)
        {
            string cacheKey = CacheKeyManager.GetPictureKeyByProductID(productID);
            List<ProductPicture> pictures = HHCache.Instance.Get(cacheKey) as List<ProductPicture>;
            if (pictures == null)
            {
                pictures = ShopDataProvider.Instance.GetPicturesByProductID(productID);
                HHCache.Instance.Insert(cacheKey, pictures);
            }
            return pictures;
        }

        /// <summary>
        /// 根据ID获取图片信息
        /// </summary>
        /// <param name="pictureID"></param>
        /// <returns></returns>
        public static ProductPicture GetPicture(int pictureID)
        {
            string cacheKey = CacheKeyManager.GetPictureKey(pictureID);
            ProductPicture picture = HHCache.Instance.Get(cacheKey) as ProductPicture;
            if (picture == null)
            {
                picture = ShopDataProvider.Instance.GetPicture(pictureID);
                HHCache.Instance.Insert(cacheKey, picture);
            }
            return picture;
        }
        #endregion

        #region Create
        /// <summary>
        /// 添加产品图片
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static DataActionStatus Create(ProductPicture picture, Stream stream)
        {
            DataActionStatus status;
            picture = ShopDataProvider.Instance.CreateUpdatePicture(picture, DataProviderAction.Create, out status);
            if (status == DataActionStatus.Success)
            {
                FileStorageProvider fs = new FileStorageProvider(FileStoreKey);
                fs.AddUpdateFile(MakePath(picture.ProductID), picture.PictureFile, stream);

                HHCache.Instance.Remove(CacheKeyManager.GetPictureKey(picture.PictureID));
                HHCache.Instance.Remove(CacheKeyManager.GetPictureKeyByProductID(picture.ProductID));
            }
            return status;
        }

        public static string MakePath(int productID)
        {
            return GlobalSettings.MakePath(productID);
        }
        #endregion

        #region Update
        /// <summary>
        /// 修改产品图片信息
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        public static DataActionStatus Update(ProductPicture picture)
        {
            DataActionStatus status;
            ShopDataProvider.Instance.CreateUpdatePicture(picture, DataProviderAction.Update, out status);
            if (status == DataActionStatus.Success)
            {
                HHCache.Instance.Remove(CacheKeyManager.GetPictureKey(picture.PictureID));
                HHCache.Instance.Remove(CacheKeyManager.GetPictureKeyByProductID(picture.ProductID));
            }
            return status;
        }
        #endregion

        #region Delete
        /// <summary>
        /// 根据ID删除产品图片信息
        /// </summary>
        /// <param name="pictureID"></param>
        /// <returns></returns>
        public static DataActionStatus Delete(int pictureID)
        {
            DataActionStatus status = ShopDataProvider.Instance.DeletePicture(pictureID);
            if (status == DataActionStatus.Success)
            {
                ProductPicture picture = GetPicture(pictureID);
                if (picture != null)
                {
                    HHCache.Instance.Remove(CacheKeyManager.GetPictureKey(picture.PictureID));
                    HHCache.Instance.Remove(CacheKeyManager.GetPictureKeyByProductID(picture.ProductID));
                }
            }
            return status;
        }
        #endregion
    }
}
