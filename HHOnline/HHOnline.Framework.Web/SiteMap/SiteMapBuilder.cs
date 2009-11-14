using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;
/*
 * sitemap 0.9
 * <?xml version="1.0" encoding="UTF-8"?>
    <urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
        <url>
            <loc>http://www.example.com/</loc>
            <lastmod>2005-01-01</lastmod>
            <changefreq>monthly</changefreq>
            <priority>0.8</priority>
        </url>
    </urlset>
 * */
namespace HHOnline.Framework.Web.SiteMap
{
    public class SiteMapBuilder
    {
        private XmlDocument _siteMap = null;
        private XmlElement _smEle = null;
        private static readonly string _freq = HHConfiguration.GetConfig()["siteMapFreq"].ToString();
        private static readonly string _prior = HHConfiguration.GetConfig()["siteMapPrior"].ToString();
        private static readonly string _nativeUrl = HHConfiguration.GetConfig()["nativeUrl"].ToString();
        public SiteMapBuilder()
        {
            _siteMap = new XmlDocument();
            XmlDeclaration _xmlDec = _siteMap.CreateXmlDeclaration("1.0", "UTF-8", null);
            _siteMap.AppendChild(_xmlDec);
            _smEle = _siteMap.CreateElement("urlset");
            _smEle.SetAttribute("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
        }
        public void AddLocalUrl(string loc, DateTime lastmod)
        {
            XmlNode _url = _siteMap.CreateElement("url");
            XmlElement _xe = _siteMap.CreateElement("loc");
            _xe.InnerText = _nativeUrl + ESCTransact(loc);
            _url.AppendChild(_xe);            

            _xe = _siteMap.CreateElement("lastmod");
            _xe.InnerText = lastmod.ToString("yyyy-MM-dd");
            _url.AppendChild(_xe);

            _xe = _siteMap.CreateElement("changefreq");
            _xe.InnerText = _freq;
            _url.AppendChild(_xe);

            _xe = _siteMap.CreateElement("priority");
            _xe.InnerText = _prior;
            _url.AppendChild(_xe);
            _smEle.AppendChild(_url);
        }
        public void Save(string filePath)
        {
            _siteMap.AppendChild(_smEle);
            _siteMap.Save(filePath);
        }

        private string ESCTransact(string str)
        {
            /*
            string newStr = str.Replace("&", "&amp;");
            newStr = newStr.Replace("'", "&apos;");
            newStr = newStr.Replace("\"", "&quot;");
            newStr = newStr.Replace(">", "&gt;");
            newStr = newStr.Replace("<", "&lt;");
            return newStr;
            */
            return HttpUtility.UrlEncode(str);
        }
    }
}
