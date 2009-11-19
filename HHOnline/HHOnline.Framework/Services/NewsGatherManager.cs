using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Framework.Providers;
using System.Data;
using System.Xml;
using System.Configuration;

namespace HHOnline.Framework
{
    /// <summary>
    /// 新闻资讯收集管理器
    /// </summary>
    public class NewsGatherManager
    {
        public static void GatherIndustryNews()
        {
            //
            //DataTable dtNews = GatherSearchEngineNews();
            //
            //TODO: write dtNews to Database and then View
        }

        private static DataTable GatherSearchEngineNews()
        {
            //加载百度行业新闻
            BaiduRss baidu = new BaiduRss();
            string baiduUrl = baidu.BuildRssUrl();
            DataTable dtBaidu = baidu.LoadRSS(baiduUrl);

            //加载谷歌行业新闻
            GoogleRss google = new GoogleRss();
            string googleUrl = google.BuildRssUrl();
            DataTable dtGoogle = google.LoadRSS(googleUrl);

            DataTable dtNews = dtBaidu.Clone();
            dtNews.Merge(dtBaidu);
            dtNews.Merge(dtGoogle);
            dtNews.AcceptChanges();

            return dtNews;
        }
    }

  //<appSettings>
  //  <add key="BaiduRssUrl" value="http://news.baidu.com/ns?word={0}&amp;tn=newsrss&amp;sr=0&amp;cl=2&amp;rn=10&amp;ct=0"/>
  //  <add key="BaiduRssKeywords" value="华宏在线 华宏仪表 仪器 仪表 工业自动化"/>
  //  <add key="GoogleRssUrl" value="http://news.google.cn/news?um=1&amp;cf=all&amp;ned=ccn&amp;hl=zh-CN&amp;as_scoring=n&amp;as_maxm=11&amp;q={0}&amp;as_qdr=a&amp;as_drrb=b&amp;as_mind=18&amp;as_minm=10&amp;cf=all&amp;as_maxd=17&amp;output=rss"/>
  //  <add key="GoogleRssKeywords" value="华宏在线 华宏仪表 仪器 仪表 工业自动化"/>
  //</appSettings>

    internal class BaiduRss
    {
        public string BuildRssUrl()
        {
            string url = ConfigurationSettings.AppSettings["BaiduRssUrl"];  //配置固定参数，每次读取10条

            string keywords = ConfigurationSettings.AppSettings["BaiduRssKeywords"];
            keywords = keywords.Replace(" ", " | ");
            keywords = "title:(" + keywords + ")";  //标题搜索模式
            keywords = System.Web.HttpUtility.UrlEncode(keywords, Encoding.GetEncoding("GB2312")); //编码格式选择默认的GB2312
            //keywords = keywords.Replace("(", "%28").Replace(")", "%29");    //替换左右括号

            url = string.Format(url, keywords);
            return url;
        }
        public DataTable LoadRSS(string rssUrl)
        {
            DataTable rssTable = new DataTable("BaiduRss");
            rssTable.Columns.Add("title", typeof(string));
            rssTable.Columns.Add("link", typeof(string));
            rssTable.Columns.Add("description", typeof(string));
            rssTable.Columns.Add("pubDate", typeof(DateTime));
            rssTable.Columns.Add("source", typeof(string));
            rssTable.Columns.Add("author", typeof(string));
            rssTable.AcceptChanges();

            try
            {
                XmlDocument rssDoc = new XmlDocument();
                rssDoc.Load(rssUrl);
                XmlNodeList rssItems = rssDoc.SelectNodes("rss/channel/item");
                foreach (XmlNode item in rssItems)
                {
                    string title = "无标题", link = "#", description = "无摘要信息", source = "网络", author = "匿名";
                    DateTime pubDate = DateTime.Now;

                    foreach (XmlNode node in item.ChildNodes)
                    {
                        switch (node.Name)
                        {
                            case "title":
                                title = node.InnerText;
                                break;
                            case "link":
                                link = node.InnerText;
                                break;
                            case "description":
                                description = node.InnerText;
                                break;
                            case "pubDate":
                                if (false == DateTime.TryParse(node.InnerText, out pubDate))
                                    pubDate = DateTime.Now;
                                break;
                            case "source":
                                source = node.InnerText;
                                break;
                            case "author":
                                author = node.InnerText;
                                break;
                        }
                    }
                    rssTable.Rows.Add(title, link, description, pubDate, source, author);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return rssTable;
        }
    }

    internal class GoogleRss
    {
        public string BuildRssUrl()
        {
            string url = ConfigurationSettings.AppSettings["GoogleRssUrl"];  //配置固定参数，每次读取10条

            string keywords = ConfigurationSettings.AppSettings["GoogleRssKeywords"];
            keywords = keywords.Replace(" ", " OR ");
            keywords = "allintitle: " + keywords + "";  //标题搜索模式
            keywords = System.Web.HttpUtility.UrlEncode(keywords, Encoding.UTF8); //编码格式选择默认的UTF8

            url = string.Format(url, keywords);
            return url;
        }
        public DataTable LoadRSS(string rssUrl)
        {
            DataTable rssTable = new DataTable("GoogleRss");
            rssTable.Columns.Add("title", typeof(string));
            rssTable.Columns.Add("link", typeof(string));
            rssTable.Columns.Add("description", typeof(string));
            rssTable.Columns.Add("pubDate", typeof(DateTime));
            rssTable.Columns.Add("source", typeof(string));
            rssTable.Columns.Add("author", typeof(string));
            rssTable.AcceptChanges();

            try
            {
                XmlDocument rssDoc = new XmlDocument();
                rssDoc.Load(rssUrl);
                XmlNodeList rssItems = rssDoc.SelectNodes("rss/channel/item");
                foreach (XmlNode item in rssItems)
                {
                    string title = "无标题", link = "#", description = "无摘要信息", source = "网络", author = "匿名";
                    DateTime pubDate = DateTime.Now;

                    foreach (XmlNode node in item.ChildNodes)
                    {
                        switch (node.Name)
                        {
                            case "title":
                                title = node.InnerText;
                                break;
                            case "link":
                                link = node.InnerText;
                                //google?url=
                                link = System.Web.HttpUtility.ParseQueryString(link)["url"];
                                break;
                            case "description":
                                description = node.InnerText;
                                //table/tr/2,td class='j'/font/3,div class='1h'/.InnerText
                                XmlDocument doc = new XmlDocument();
                                doc.LoadXml(description);
                                XmlNode dNode = doc.SelectNodes("table/tr/td")[1];
                                description = dNode.ChildNodes[0].ChildNodes[2].InnerText;
                                break;
                            case "pubDate":
                                if (false == DateTime.TryParse(node.InnerText, out pubDate))
                                    pubDate = DateTime.Now;
                                break;
                            case "source":
                                source = node.InnerText;
                                break;
                            case "author":
                                author = node.InnerText;
                                break;
                        }
                    }
                    rssTable.Rows.Add(title, link, description, pubDate, source, author);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return rssTable;
        }
    }

}
