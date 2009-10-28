using System;
using System.Collections.Generic;
using System.Web;
using HHOnline.Cache;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    public class FriendLinks
    {

        public static List<FriendLink> FriendLinkGet()
        {
            return CommonDataProvider.Instance.FriendLinkGet();
        }
        public static bool FriendLinkAdd(FriendLink lnk)
        {
            return CommonDataProvider.Instance.FriendLinkAdd(lnk);
        }
        public static bool FriendLinkDelete(int lnkId)
        {
            return CommonDataProvider.Instance.FriendLinkDelete(lnkId);
        }

        public static FriendLink FriendLinkGet(int lnkId)
        {
            return CommonDataProvider.Instance.FriendLinkGet(lnkId);
        }
        public static bool FriendLinkUpdate(FriendLink lnk)
        {
            return CommonDataProvider.Instance.FriendLinkUpdate(lnk);
        }
    }
}
