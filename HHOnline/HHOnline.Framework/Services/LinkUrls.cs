using System;
using System.Collections.Generic;
using System.Web;
using HHOnline.Cache;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    /// <summary>
    /// 收藏管理
    /// </summary>
    public class LinkUrls
    {

        public static List<LinkUrl> LinkUrlGet()
        {
            return CommonDataProvider.Instance.LinkUrlGet();
        }
        public static bool LinkUrlAdd(LinkUrl lnk)
        {
            return CommonDataProvider.Instance.LinkUrlAdd(lnk);
        }
        public static bool LinkUrlDelete(int lnkId)
        {
            return CommonDataProvider.Instance.LinkUrlDelete(lnkId);
        }

    }
}
