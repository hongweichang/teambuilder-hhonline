using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;

namespace HHOnline.Framework.Web.HttpHandlers
{
    public class AdHandler:IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            context.Response.ContentType = "text/xml";
            context.Response.Charset = "UTF-8";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version='1.0' encoding='UTF-8' ?>");
            sb.AppendLine("<ads>");
           
            sb.AppendLine("<ad>");
            sb.AppendLine(CreateData("ad5.jpg", "华宏在线", "华宏在线交易平台为您提供最真诚的服务！"));
            sb.AppendLine("</ad>");
            sb.AppendLine("<ad>");
            sb.AppendLine(CreateData("ad5.jpg", "华宏在线", "华宏在线交易平台为您提供最真诚的服务！"));
            sb.AppendLine("</ad>");
            sb.AppendLine("<ad>");
            sb.AppendLine(CreateData("ad5.jpg", "华宏在线", "华宏在线交易平台为您提供最真诚的服务！"));
            sb.AppendLine("</ad>");
            sb.AppendLine("<ad>");
            sb.AppendLine(CreateData("ad5.jpg", "华宏在线", "华宏在线交易平台为您提供最真诚的服务！"));
            sb.AppendLine("</ad>");
            sb.AppendLine("<ad>");
            sb.AppendLine(CreateData("ad5.jpg", "华宏在线", "华宏在线交易平台为您提供最真诚的服务！"));
            sb.AppendLine("</ad>");

            sb.AppendLine("</ads>");

            context.Response.Write(sb.ToString());
        }
        string CreateData(string picName, string title, string content)
        {
            StringBuilder sb = new StringBuilder();
            SiteFile sf = SiteFiles.GetFile("Ads", picName);
            IStorageFile f = sf.File;
            sb.AppendLine("<picture><![CDATA[" + sf.URL + "]]></picture>");
            sb.AppendLine("<thumbnail><![CDATA[" + SiteUrlManager.GetResizedImageUrl(f, 60, 40) + "]]></thumbnail>");
            sb.AppendLine("<link><![CDATA[http://www.ehuaho.com]]></link>");
            sb.AppendLine("<title><![CDATA[" + title + "]]></title>");
            sb.AppendLine("<content><![CDATA[" + content + "]]></content>");
            return sb.ToString();
        }
    }
}
