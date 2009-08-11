using System;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Web;
using HHOnline.Cache;

namespace HHOnline.Framework
{
    /// <summary>
    /// 缓存管理
    /// </summary>
    public class CacheManager
    {
        private CacheManager() { }

        /// <summary>
        /// 获取所有的缓存前缀信息
        /// </summary>
        /// <returns></returns>
        public static List<KeyValue> GetCacheKeyValues()
        {
            List<KeyValue> cacheKeyValues = new List<KeyValue>();
            cacheKeyValues.AddRange(FetchCatchKeys(typeof(CacheKeyManager)));
            //cacheKeyValues.Add(new KeyValue(CacheKeyManager.FrameworkPrefix, "系统设置"));
            //cacheKeyValues.Add(new KeyValue(CacheKeyManager.UserPrefix, "用户信息"));
            //cacheKeyValues.Add(new KeyValue(CacheKeyManager.CompanyPrefix, "公司信息"));
            //cacheKeyValues.Add(new KeyValue(CacheKeyManager.PemissionPrefix, "权限信息"));
            //cacheKeyValues.Add(new KeyValue(CacheKeyManager.ArticlePrefix, "咨询信息"));
            //cacheKeyValues.Add(new KeyValue(CacheKeyManager.ProductPrefix, "产品信息"));
            return cacheKeyValues;
        }
        private static IList<KeyValue> FetchCatchKeys(Type ck)
        {
            IList<KeyValue> kv = new List<KeyValue>();
            Type desAttr = typeof(DescriptionAttribute);
            FieldInfo[] fi = ck.GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (FieldInfo f in fi)
            {
                object[] cusAttrs = f.GetCustomAttributes(desAttr, true);
                if (cusAttrs.Length <= 0) continue;
                DescriptionAttribute da = (DescriptionAttribute)cusAttrs[0];
                kv.Add(new KeyValue(f.GetValue(null).ToString(), da.Description));
            }
            return kv;
        }
        /// <summary>
        /// 获取缓存对象数
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static int GetCacheCount(string cacheKey)
        {
            return HHCache.Instance.GetCount(cacheKey);
        }

        /// <summary>
        /// 清楚指定前缀的缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        public static void Clear(string cacheKey)
        {
            HHCache.Instance.Remove(cacheKey);
        }

        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public static void Clear()
        {
            HHCache.Instance.Clear();
        }

        /// <summary>
        /// 获取缓存CacheKeyDom用于调试
        /// </summary>
        public static void GetCacheKeyDom()
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx != null)
            {
                System.IO.Stream s = HHCache.Instance.GetCacheKeyXml();
                ctx.Response.ContentType = "text/xml";
                ctx.Response.AddHeader("content-disposition", "attachment; filename=cachedom.xml");
                if (s != null)
                {
                    using (s)
                    {
                        byte[] buffer = new byte[64 * 1024];
                        int read;
                        while ((read = s.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            if (!ctx.Response.IsClientConnected)
                                break;

                            ctx.Response.OutputStream.Write(buffer, 0, read);
                            ctx.Response.OutputStream.Flush();
                        }
                    }
                }
                ctx.Response.End();
            }
        }
    }
}
