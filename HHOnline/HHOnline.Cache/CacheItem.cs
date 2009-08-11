using System;
using MC = Microsoft.Practices.EnterpriseLibrary.Caching;
using MCE = Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace HHOnline.Cache
{

    public interface ICacheItemExpiration : MC.ICacheItemExpiration
    { }

    /// <summary>
    /// 对过期时间策略进行封装
    /// </summary>
    [Serializable]
    public class AbsoluteTime : MCE.AbsoluteTime, ICacheItemExpiration
    {
        public AbsoluteTime(DateTime absoluteTime)
            : base(absoluteTime)
        {
        }

        public AbsoluteTime(TimeSpan timeFromNow)
            : base(timeFromNow)
        { }
    }

    /// <summary>
    /// 对过期时间策略进行封装
    /// </summary>
    [Serializable]
    public class ExtendedFormatTime : MCE.ExtendedFormatTime, ICacheItemExpiration
    {
        public ExtendedFormatTime(string timeFormat)
            : base(timeFormat)
        {
        }
    }

    /// <summary>
    /// 对过期时间策略进行封装
    /// </summary>
    [Serializable]
    public class FileDependency : MCE.FileDependency, ICacheItemExpiration
    {
        public FileDependency(string fullFileName)
            : base(fullFileName)
        {
        }
    }

    /// <summary>
    /// 对过期时间策略进行封装
    /// </summary>
    [Serializable]
    public class NeverExpired : MCE.NeverExpired, ICacheItemExpiration
    {
        public NeverExpired()
            : base()
        { }
    }

    [Serializable]
    public class SlidingTime : MCE.SlidingTime, ICacheItemExpiration
    {
        public SlidingTime(TimeSpan slidingExpiration)
            : base(slidingExpiration)
        { }

        public SlidingTime(TimeSpan slidingExpiration, DateTime originalTimeStamp)
            : base(slidingExpiration, originalTimeStamp)
        {
        }
    }



    public enum CacheItemPriority
    {
        None = 0,
        Low = 1,
        Normal = 2,
        High = 3,
        NotRemovable = 4,
    }
}
