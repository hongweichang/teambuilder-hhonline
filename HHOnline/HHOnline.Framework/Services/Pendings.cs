using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using HHOnline.Cache;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    public class Pendings
    {
        static string prefix = CacheKeyManager.PendingPrefix;
        public static Pending PendingGet(int companyID)
        {
            return CommonDataProvider.Instance.PendingGet(companyID);
        }
        public static bool PendingAdd(Pending pending)
        {
            HHCache.Instance.Remove(prefix);
            OnUpdated();
            return CommonDataProvider.Instance.PendingAdd(pending);
        }
        public static List<Pending> PendingsLoad()
        {
            List<Pending> pendings = HHCache.Instance.Get(prefix) as List<Pending>;
            if (pendings == null || pendings.Count == 0)
            {
                pendings = CommonDataProvider.Instance.PendingsLoad();
                HHCache.Instance.Insert(prefix, pendings, 10);
            }
            return pendings;
        }

        public static Pending PendingGetById(int pendingId)
        {
            return CommonDataProvider.Instance.PendingGetById(pendingId);
        }
        public static bool PendingUpdate(Pending p)
        {
            OnUpdated();
            return CommonDataProvider.Instance.PendingUpdate(p);
        }
        
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
