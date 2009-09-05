using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using UR=HHOnline.Framework.Web.UrlRewrite;

namespace HHOnline.Framework.Web.HttpModules
{
    public class UrlRewrite : IHttpModule
    {
        #region -IHttpModule Members-
        public void Dispose() { }
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpContext context = (sender as HttpApplication).Context;
            string url = context.Request.RawUrl;
            //string path = context.Request.Path.ToLowerInvariant();
            //if(!string.IsNullOrEmpty(path))
            //{
            //    path = path.Replace(".aspx.cs", "");
            //    context.RewritePath(path);
            //}
            List<UR.UrlRewriteSection> list=UR.UrlRewriteConfig.LoadSections();
            url = url.Replace(".aspx.cs", "");

            if (!url.Contains("404.aspx"))
            {
                string newUrl = UR.RewriteRules.Parse(url);
                if (!string.IsNullOrEmpty(newUrl))
                    context.RewritePath(newUrl);
            }
        }
        #endregion
    }
}
