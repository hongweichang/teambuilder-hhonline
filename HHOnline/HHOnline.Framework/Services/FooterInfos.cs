using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    public class FooterInfos
    {

        public static bool FooterInfoUpdate(FooterUpdateAction action, string value)
        {
            return CommonDataProvider.Instance.FooterInfoUpdate(action,value);
        }
        public static FooterInfo FooterInfoGet()
        {
            return CommonDataProvider.Instance.FooterInfoGet();
        }
    }
}
