using System;
using System.Xml;
using HHOnline.Task;
using HHOnline.Framework.Web.SiteMap;
using HHOnline.Shops;
using System.Collections.Generic;
using HHOnline.News.Components;
using HHOnline.News.Services;
using System.Threading;
using HHOnline.Framework.Web.HttpHandlers;

namespace HHOnline.Framework.Web.Tasks
{
    public class RefreshSitemap : ITask
    {

        public void Execute(XmlNode node)
        {
            bool isComplete = false;
            while (false == isComplete)
            {
                try
                {
                    bool result = false;
                    SiteMapHandler sitemap = new SiteMapHandler();                    
                    string message = sitemap.RebuildSiteMaps(ref result);

                    isComplete = true;
                }
                catch {
                    isComplete = false;
                    Thread.Sleep(TimeSpan.FromMinutes(5));
                }
            }
        }
    }
}
