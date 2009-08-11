using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using HHOnline.Framework;

namespace HHOnline.Security
{
    public class HHSiteMapProvider:StaticSiteMapProvider
    {
        private static string _siteMamCK = CacheKeyManager.SiteMapPrefix;
        private readonly object _lock = new object();
        //private SiteMapNode rootNode;
        private Dictionary<int, SiteMapNode> nodes = new Dictionary<int, SiteMapNode>(16);
        protected SiteSettings siteSettings;

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection attributes)
        {
            if (attributes == null)
                throw new HHException(ExceptionType.ModuleInitFail, "站点地图无法正常载入！");
            if (string.IsNullOrEmpty(name))
                name = "HHSiteMapProvider";

            if (string.IsNullOrEmpty(attributes["description"]))
            {
                attributes.Remove("description");
                attributes.Add("description", "HHOnline site map provider");
            }
            base.Initialize(name, attributes);
        }
        public override SiteMapNode BuildSiteMap()
        {
            //if (rootNode == null)
            //{
            //    lock (_lock)
            //    {
            //        if (rootNode == null)
            //        {
                        
            //        }
            //    }
            //}
            //return rootNode;
            return null;
        }

        protected override SiteMapNode GetRootNodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
