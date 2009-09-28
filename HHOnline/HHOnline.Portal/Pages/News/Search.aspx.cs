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
using HHOnline.Framework.Web;
using HHOnline.News.Components;
using HHOnline.Framework;
using HHOnline.SearchBarrel;

public partial class Pages_News_Search : HHPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			BindSearchs();
		}

		ExecuteJs("var _nativeUrl = '" + GetUrl() + "'", true);
	}

	protected void dlArticle_ItemDataBound(object sender, DataListItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			Article article = e.Item.DataItem as Article;
			Image articlePicture = e.Item.FindControl("imgArticle") as Image;
			if (articlePicture != null)
			{
				articlePicture.ImageUrl = GlobalSettings.RelativeWebRoot + article.GetDefaultImageUrl(100, 100);
			}
			//Literal ltPrice = e.Item.FindControl("ltPrice") as Literal;
			//if (ltPrice != null)
			//{
			//    decimal? price = null;
			//    if (!User.Identity.IsAuthenticated)
			//    {
			//        price = ArticlePrices.GetPriceDefault(article.ID);
			//    }
			//    else
			//    {
			//        price = ArticlePrices.GetPriceDefault(article.ID);
			//    }
			//    ltPrice.Text = (price == null ? "需询价" : price.Value.ToString("c"));
			//}
		}
	}

	private string GetUrl()
	{
		if (Request.QueryString["sortby"] == null)
		{
			return Request.RawUrl.ToString();
		}
		else
		{
			string url = Request.RawUrl.ToString().Split('&')[0];
			foreach (string k in Request.QueryString.AllKeys)
			{
				if (k != "sortby")
				{
					url += "&" + k + "=" + Request.QueryString[k];
				}
			}
			return url;
		}
	}

	private void BindSearchs()
	{
		string query = Request.QueryString["w"];
		if (!string.IsNullOrEmpty(query))
		{
			query = HttpUtility.UrlDecode(query);
			TextBox txt = sArticle.FindControl("txtSearch") as TextBox;
			if (txt != null)
			{
				txt.Text = query;
			}

			ArticleQuery pq = new ArticleQuery();
			pq.Title = query;
			pq.PageSize = Int32.MaxValue;

			SearchResultDataSet<Article> articles = NewsSearchManager.Search(pq);
			List<Article> ps = articles.Records;
			if (ps == null || ps.Count == 0)
			{
				ltSearchDuration.Text = "搜索用时：" + articles.SearchDuration.ToString() + "秒";
				msgBox.ShowMsg("没有搜索到相应的资讯信息！", System.Drawing.Color.Gray);
			}
			else
			{
				msgBox.HideMsg();
				ltSearchDuration.Text = "搜索用时：" + articles.SearchDuration.ToString() + "ms";
				cpArticle.DataSource = ps;
				cpArticle.BindToControl = dlArticle;
				dlArticle.DataSource = cpArticle.DataSourcePaged;
				dlArticle.DataBind();
			}
		}
	}

	public override void OnPageLoaded()
	{
		string query = Request.QueryString["w"];
		this.ShortTitle = "搜索产品";
		if (!string.IsNullOrEmpty(query))
		{
			this.ShortTitle += ": " + HttpUtility.UrlDecode(query);
		}
		this.SetTitle();
		this.AddJavaScriptInclude("scripts/pages/sortby.aspx.js", false, false);
	}
}
