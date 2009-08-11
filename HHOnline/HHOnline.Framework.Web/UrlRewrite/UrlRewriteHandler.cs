using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;

namespace HHOnline.Framework.Web.UrlRewrite
{
    public class UrlRewriteHandler:IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            List<UrlRewriteSection> list = new List<UrlRewriteSection>();
            UrlRewriteSection l = null;
            foreach (XmlNode urlRewrite in section.SelectNodes("//rewrite"))
            {
                l = new UrlRewriteSection(urlRewrite);
                list.Add(l);
            }
            return list;
        }
    }
}
