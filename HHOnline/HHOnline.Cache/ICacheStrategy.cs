using System;

namespace HHOnline.Cache
{
    public interface ICacheStrategy
    {
        void Insert(string key, object obj);

        void Insert(string key, object obj, double multiple);

        void Insert(string key, object obj, params ICacheItemExpiration[] deps);

        void Insert(string key, object obj, double multiple, CacheItemPriority priority);

        void Insert(string key, object obj, CacheItemPriority priority, params ICacheItemExpiration[] deps);

        /// <summary>
        /// 缓存最大容许时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        void Max(string key, object obj);

        void Max(string key, object obj, params ICacheItemExpiration[] deps);

        object Get(string key);

        void Remove(string key);

        void Clear();

        void ResetFactor(int factor);
    }
}
