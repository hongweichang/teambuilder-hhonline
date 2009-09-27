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
        public static Pending PendingGet(int companyID)
        {
            return CommonDataProvider.Instance.PendingGet(companyID);
        }
    }
}
