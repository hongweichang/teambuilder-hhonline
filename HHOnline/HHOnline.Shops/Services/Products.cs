using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using HHOnline.Cache;
using HHOnline.Framework;
using HHOnline.Shops.Providers;

namespace HHOnline.Shops
{
    /// <summary>
    /// 产品管理类
    /// </summary>
    public class Products
    {
        #region Create
        /// <summary>
        /// 添加产品信息
        /// </summary>
        /// <param name="product">产品信息</param>
        /// <param name="categoryIDList">分类ID列表，用逗号分隔</param>
        /// <param name="industryIDList">行业ID列表，用逗号分隔</param>
        /// <param name="properties">产品属性列表</param>
        /// <returns>产品</returns>
        public static DataActionStatus Create(Product product, string categoryIDList, string industryIDList, List<ProductProperty> properties)
        {
            DataActionStatus status;
            product = ShopDataProvider.Instance.CreateUpdateProduct(product, categoryIDList, industryIDList,
                properties, DataProviderAction.Create, out status);
            if (status == DataActionStatus.Success)
            {
                //处理临时附件信息  
                SavePicturesAndFilterBody(product);
                //处理缓存信息
                HHCache.Instance.Remove(CacheKeyManager.ProductListKey);
            }
            return status;
        }

        /// <summary>
        /// 保存临时附件到产品图片表
        /// </summary>
        /// <param name="product"></param>
        private static void SavePicturesAndFilterBody(Product product)
        {
            List<TemporaryAttachment> temporaryAttachments =
                TemporaryAttachments.GetTemporaryAttachments(GlobalSettings.GetCurrentUser().UserID, AttachmentType.ProductPhoto);
            Dictionary<TemporaryAttachment, ProductPicture> dicPictures = new Dictionary<TemporaryAttachment, ProductPicture>();
            if (temporaryAttachments != null && temporaryAttachments.Count > 0)
            {
                foreach (TemporaryAttachment attachment in temporaryAttachments)
                {
                    //插入到产品图片表
                    ProductPicture picture = new ProductPicture();
                    picture.PictureName = attachment.FriendlyFileName;
                    picture.ProductID = product.ProductID;
                    picture.DisplayOrder = attachment.DisplayOrder;
                    picture.PictureFile = attachment.FileName;
                    using (Stream stream = attachment.File.OpenReadStream())
                    {
                        ProductPictures.Create(picture, stream);
                    }
                    if (!dicPictures.ContainsKey(attachment))
                        dicPictures[attachment] = picture;
                    //删除附件
                    TemporaryAttachments.Delete(attachment.AttachmentID);
                    //更新产品图片链接地址

                }
            }
        }

        /// <summary>
        /// 更新产品图片链接地址
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        private static string FilterAttachmentInBody(Dictionary<TemporaryAttachment, ProductPicture> dicPictures, string body)
        {
            return body;
            //string pattern = "<img (.*?)src=(\"|')(.*?)/image.axd/(.*)/__key/{0}/(.*)/{1}(\"|').*?\\/>";
            //Match match;
            //Regex regex;
            //foreach (KeyValuePair<TemporaryAttachment, ProductPicture> pair in attachmentDictionary)
            //{
            //    regex = new Regex(string.Format(pattern, TemporaryAttachments.FileStoreKey, pair.Key.FileName));
            //    match = regex.Match(body);
            //    if (match.Success)
            //    { 
            //        body = regex.Replace(body,pair.Value.
            //    }
            //}
        }
        #endregion

        #region Update
        /// <summary>
        /// 修改产品信息
        /// </summary>
        /// <param name="product">产品信息</param>
        /// <param name="categoryIDList">分类ID列表，用逗号分隔</param>
        /// <param name="industryIDList">行业ID列表，用逗号分隔</param>
        /// <param name="properties">产品属性列表</param>
        /// <returns>产品</returns>
        public static DataActionStatus Update(Product product, string categoryIDList, string industryIDList, List<ProductProperty> properties)
        {
            DataActionStatus status;
            product = ShopDataProvider.Instance.CreateUpdateProduct(product, categoryIDList, industryIDList,
                properties, DataProviderAction.Update, out status);
            if (status == DataActionStatus.Success)
            {
                HHCache.Instance.Remove(CacheKeyManager.GetProductKey(product.ProductID));
                HHCache.Instance.Remove(CacheKeyManager.ProductListKey);
            }
            return status;
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static DataActionStatus Delete(int productID)
        {
            DataActionStatus status = ShopDataProvider.Instance.DeleteProduct(productID);
            if (status == DataActionStatus.Success)
            {
                HHCache.Instance.Remove(CacheKeyManager.GetProductKey(productID));
                HHCache.Instance.Remove(CacheKeyManager.ProductListKey);
            }
            return status;
        }
        #endregion

        #region Get
        /// <summary>
        /// 根据商品编号获取商品信息
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static Product GetProduct(int productID)
        {
            string cacheKey = CacheKeyManager.GetProductKey(productID);
            Product product = HHCache.Instance.Get(cacheKey) as Product;
            if (product == null)
            {
                product = ShopDataProvider.Instance.GetProduct(productID);
                HHCache.Instance.Insert(cacheKey, product);
            }
            return product;
        }

        /// <summary>
        /// 根据查询字符串获取商品（不分页）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static List<Product> GetProductList(ProductQuery query)
        {
            query.PageIndex = 0;
            query.PageSize = Int32.MaxValue;
            string cacheKey = query.GetQueryKey();
            List<Product> productList = HHCache.Instance.Get(cacheKey) as List<Product>;
            if (productList == null)
            {
                int totalRecords;
                productList = ShopDataProvider.Instance.GetProducts(query, out totalRecords);
                HHCache.Instance.Insert(cacheKey, productList);
            }
            return productList;
        }

        /// <summary>
        /// 根据查询字符串获取商品（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static PagingDataSet<Product> GetProducts(ProductQuery query)
        {
            int totalRecords;
            PagingDataSet<Product> products = null;
            List<Product> productList = ShopDataProvider.Instance.GetProducts(query, out totalRecords);
            products = new PagingDataSet<Product>();
            products.Records = productList;
            products.TotalRecords = totalRecords;
            return products;
        }
        #endregion
    }
}
