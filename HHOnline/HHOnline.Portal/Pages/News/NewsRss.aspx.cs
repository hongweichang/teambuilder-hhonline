using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using HHOnline.News.Components;
using HHOnline.Framework;
using HHOnline.News.enums;
using HHOnline.News.Services;
using System.Xml;
using System.IO;

public partial class Pages_News_NewsRss : System.Web.UI.Page
{
	private string hostUrl;
	private string httpHead;

	protected void Page_Load(object sender, EventArgs e)
	{
		ArticleQuery query = new ArticleQuery();
		query.SortOrder = SortOrder.Descending;
		query.ArticleOrderBy = ArticleOrderBy.CreateTime;

		List<Article> articles = ArticleManager.GetArticles(query).Records;

		HttpContext context = HttpContext.Current;
		hostUrl = context.Request.Url.ToString();
		hostUrl = hostUrl.Substring(0, hostUrl.IndexOf("/", 8));

		XmlTextWriter writer = new XmlTextWriter(context.Response.OutputStream, System.Text.Encoding.UTF8);
		
		WriteRSSPrologue(writer);
		WriteRSSHeadChennel(writer);

		foreach (Article article in articles)
		{
			AddRSSItem(
				writer,
				article.CreateTime.ToUniversalTime().ToString("r"),
				article.Title,
				"view.aspx?news-newsdetail&id=" + HHOnline.Framework.GlobalSettings.Encrypt(article.ID.ToString()),
				article.Content);
		}
		
		writer.Flush();
		writer.Close();

		context.Response.ContentEncoding = System.Text.Encoding.UTF8;
		context.Response.ContentType = "text/xml";
		context.Response.Cache.SetCacheability(HttpCacheability.Public);
		context.Response.End();
	}

	private XmlTextWriter WriteRSSPrologue(XmlTextWriter writer)
	{
		writer.WriteStartDocument();
		writer.WriteStartElement("rss");
		writer.WriteAttributeString("version", "2.0");
		writer.WriteAttributeString("xmlns:dc", "http://purl.org/dc/elements/1.1/");
		writer.WriteAttributeString("xmlns:trackbac", "http://madskills.com/public/xml/rss/module/trackback/");
		writer.WriteAttributeString("xmlns:wfw", "http://wellformedweb.org/CommentAPI/");
		writer.WriteAttributeString("xmlns:slash", "http://purl.org/rss/1.0/modules/slash/");
		
		return writer;
	}

	private XmlTextWriter WriteRSSHeadChennel(XmlTextWriter writer)
	{
		writer.WriteStartElement("channel");
		writer.WriteElementString("title", "华宏在线 - 最新资讯");
		writer.WriteElementString("link", hostUrl + "/　");
		writer.WriteElementString("description", "华宏在线");
		writer.WriteElementString("copyright", "2009 华宏在线");
		writer.WriteElementString("generator", "华宏在线");
		
		return writer;
	}

	private XmlTextWriter AddRSSItem(XmlTextWriter writer, string pubDate, string title, string link, string desc)
	{
		writer.WriteStartElement("item");
		writer.WriteElementString("title", title);
		writer.WriteElementString("link", link);
		writer.WriteElementString("description", desc);
		writer.WriteElementString("pubDate", pubDate);
		writer.WriteEndElement();
		
		return writer;
	}

	private XmlTextWriter AddRSSItem(XmlTextWriter writer, string title, string link, string desc, bool isDescCDATA)
	{
		writer.WriteStartElement("item");
		writer.WriteElementString("title", title);
		writer.WriteElementString("link", link);
		
		if (isDescCDATA)
		{
			writer.WriteStartElement("description");
			writer.WriteCData(desc);
			writer.WriteEndElement();
		}
		else
		{
			writer.WriteElementString("description", desc);
		}

		writer.WriteElementString("pubDate", DateTime.Now.ToString("r"));
		writer.WriteEndElement();
		
		return writer;
	}

	private XmlTextWriter WriteRSSComplete(XmlTextWriter writer)
	{
		writer.WriteEndElement();
		writer.WriteEndElement();
		writer.WriteEndDocument();

		return writer;
	}
}
