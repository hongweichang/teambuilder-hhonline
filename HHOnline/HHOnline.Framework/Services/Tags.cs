using System;
using System.Collections.Generic;
using System.Web;
using HHOnline.Cache;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    /// <summary>
    /// Tags
    /// </summary>
    public class Tags
    {
        /// <summary>
        /// 获取所有Tag
        /// </summary>
        /// <returns></returns>
        public static List<Tag> GetTags()
        {
            return GetTags(true);
        }

        /// <summary>
        /// 获取所有Tag
        /// </summary>
        /// <param name="cacheable"></param>
        /// <returns></returns>
        public static List<Tag> GetTags(bool cacheable)
        {
            List<Tag> tags = null;
            string tagsCacheKey = CacheKeyManager.GetTagKey();
            if (HttpContext.Current != null)
            {
                tags = HttpContext.Current.Items[tagsCacheKey] as List<Tag>;
            }
            if (tags != null)
                return tags;
            if (cacheable)
                tags = HHCache.Instance.Get(tagsCacheKey) as List<Tag>;
            if (tags == null)
            {
                tags = CommonDataProvider.Instance.GetTags();
                //缓存
                if (cacheable)
                {
                    HHCache.Instance.Insert(tagsCacheKey, tags, 1);
                }
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Items[tagsCacheKey] = tags;
                }
            }
            return tags;
        }

        /// <summary>
        /// 根据文章ID获取Tag列表
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        public static List<Tag> GetTagsByArticle(int articleID)
        {
            return GetTagsByArticle(articleID, true);
        }

        /// <summary>
        /// 根据文章ID获取Tag列表
        /// </summary>
        /// <param name="articleTag"></param>
        /// <returns></returns>
        public static List<Tag> GetTagsByArticle(int articleID, bool cacheable)
        {
            List<Tag> tags = null;
            string tagsCacheKey = CacheKeyManager.GetTagArticleKey(articleID);
            if (HttpContext.Current != null)
            {
                tags = HttpContext.Current.Items[tagsCacheKey] as List<Tag>;
            }
            if (tags != null)
                return tags;
            if (cacheable)
                tags = HHCache.Instance.Get(tagsCacheKey) as List<Tag>;
            if (tags == null)
            {
                tags = CommonDataProvider.Instance.GetTagsByArticle(articleID);
                //缓存
                if (cacheable)
                {
                    HHCache.Instance.Insert(tagsCacheKey, tags, 1);
                }
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Items[tagsCacheKey] = tags;
                }
            }
            return tags;
        }

        /// <summary>
        /// 根据产品ID获取Tag列表
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<Tag> GetTagsByProduct(int productID)
        {
            return GetTagsByProduct(productID, true);
        }

        /// <summary>
        /// 根据产品ID获取Tag列表
        /// </summary>
        /// <param name="productTag"></param>
        /// <returns></returns>
        public static List<Tag> GetTagsByProduct(int productID, bool cacheable)
        {
            List<Tag> tags = null;
            string tagsCacheKey = CacheKeyManager.GetTagProductKey(productID);
            if (HttpContext.Current != null)
            {
                tags = HttpContext.Current.Items[tagsCacheKey] as List<Tag>;
            }
            if (tags != null)
                return tags;
            if (cacheable)
                tags = HHCache.Instance.Get(tagsCacheKey) as List<Tag>;
            if (tags == null)
            {
                tags = CommonDataProvider.Instance.GetTagsByProduct(productID);
                //缓存
                if (cacheable)
                {
                    HHCache.Instance.Insert(tagsCacheKey, tags, 1);
                }
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Items[tagsCacheKey] = tags;
                }
            }
            return tags;
        }

        /// <summary>
        /// 更新文章Tag(tagList使用;分隔)
        /// </summary>
        /// <param name="articleID"></param>
        /// <param name="tagList"></param>
        public static void UpdateTagArticle(int articleID, string tagList)
        {
            CommonDataProvider.Instance.UpdateTagArticle(articleID, tagList);
            HHCache.Instance.Remove(CacheKeyManager.GetTagArticleKey(articleID));
            HHCache.Instance.Remove(CacheKeyManager.GetTagKey());
        }

        /// <summary>
        /// 更新产品Tag(tagList使用;分隔)
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="tagList"></param>
        public static void UpdateTagProduct(int productID, string tagList)
        {
            CommonDataProvider.Instance.UpdateTagProduct(productID, tagList);
            HHCache.Instance.Remove(CacheKeyManager.GetTagProductKey(productID));
            HHCache.Instance.Remove(CacheKeyManager.GetTagKey());
        }
    }
}
