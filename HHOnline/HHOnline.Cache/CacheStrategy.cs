using System;
using System.Web.Caching;

namespace HHOnline.Cache
{
    public abstract class CacheStrategy : ICacheStrategy
    {
        protected int Factor = 5;

        public void Insert(string key, object obj)
        {
            Insert(key, obj, 1);
        }
        /// <summary>
        /// 缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="multiple">缓存时间，实际值为 multiple*factor 分钟，factor默认值为5</param>
        public void Insert(string key, object obj, double multiple)
        {
            Insert(key, obj, multiple, CacheItemPriority.Normal);
        }

        public void Insert(string key, object obj, params ICacheItemExpiration[] deps)
        {
            Insert(key, obj, CacheItemPriority.Normal, deps);
        }
        /// <summary>
        /// 缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="multiple">缓存时间，实际值为 multiple*factor 分钟，factor默认值为5</param>
        /// <param name="priority"></param>
        public void Insert(string key, object obj, double multiple, CacheItemPriority priority)
        {
            Insert(key, obj, CacheItemPriority.Normal, new SlidingTime(TimeSpan.FromMinutes(multiple * Factor)));
        }

        public void Max(string key, object obj)
        {
            Insert(key, obj, new NeverExpired());
        }

        public void Max(string key, object obj, params ICacheItemExpiration[] deps)
        {
            System.Collections.Generic.List<ICacheItemExpiration> lstDeps = new System.Collections.Generic.List<ICacheItemExpiration>();
            lstDeps.Add(new NeverExpired());
            lstDeps.AddRange(deps);
            Insert(key, obj, lstDeps.ToArray());
        }

        public abstract void Insert(string key, object obj, CacheItemPriority priority, params ICacheItemExpiration[] deps);

        public abstract object Get(string key);

        public abstract void Remove(string key);

        public abstract void Clear();

        public void ResetFactor(int factor)
        {
            this.Factor = factor;
        }
    }
}
