using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    public class FooterInfos
    {

        public static FooterInfo FooterInfoGet(FooterUpdateAction action, string value)
        {
            return CommonDataProvider.Instance.FooterInfoGet(action,value);
        }
    }
}
