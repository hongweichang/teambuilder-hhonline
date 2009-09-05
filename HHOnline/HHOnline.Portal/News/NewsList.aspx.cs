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
using HHOnline.News.Services;
using HHOnline.Framework;

public partial class News_NewsList : HHPage
{
	/// <summary>
	/// 一页显示20条
	/// </summary>
	private int pageSize = 20;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack && !IsCallback)
		{
			BindArticles();
		}
	}

	/// <summary>
	/// 绑定文章列表
	/// </summary>
	public void BindArticles()
	{
		// queryString中的参数：
		// cate: 分类id -1为最新分类
		// p: 页码
		// v: 查看方式, 0 详细资料 1 列表

		int categoryID = -1;
		string categoryIDStr = Request.QueryString["cate"];
		if (!string.IsNullOrEmpty(categoryIDStr))
		{
			int.TryParse(categoryIDStr, out categoryID);
		}

		int pageIndex = 0;
		string pageIDStr = Request.QueryString["p"];
		if (!string.IsNullOrEmpty(pageIDStr))
		{
			int.TryParse(pageIDStr, out pageIndex);
		}

		int viewState = 0;
		string viewStateStr = Request.QueryString["v"];
		if (!string.IsNullOrEmpty(viewStateStr))
		{
			int.TryParse(viewStateStr, out viewState);
		}

		// 根据资讯分类绑定资讯
		ArticleQuery query = new ArticleQuery();

		if (categoryID == -1)
		{
			lblCategoryName.Text = "最新资讯";
			query.CategoryID = null;
		}
		else
		{
			ArticleCategory cateInfo = ArticleManager.GetArticleCategory(categoryID);
			lblCategoryName.Text = cateInfo.Name;
			query.CategoryID = categoryID;
		}

		query.PageIndex = pageIndex;
		PagingDataSet<Article> articles = ArticleManager.GetArticles(query);

		// 绑定
		repArticles.DataSource = articles.Records;
		repArticles.Visible = viewState == 0;
		repArticles.DataBind();

		repArticlesList.DataSource = articles.Records;
		repArticlesList.Visible = viewState == 1;
		repArticlesList.DataBind();
	}

	public override void OnPageLoaded()
	{
		this.ShortTitle = "资讯";

		base.OnPageLoaded();
		AddGenericLink("text/css", "Stylesheet", "global", "App_Themes/Default/newsList.css");
		AddJavaScriptInclude("Scripts/Pages/newsList.aspx.js", false, false);
	}

	protected void repArticles_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.Controls.Count > 1)
		{
			Image image = e.Item.Controls[1] as Image;
			if (image != null)
			{
				Article article = e.Item.DataItem as Article;
				if (article != null)
				{
					// 获取附件
					ArticleAttachment attachment = ArticleAttachments.GetAttachment(article.Image);
					if (attachment != null)
					{
						string imgPath = "../FileStore/" + ArticleAttachments.FileStoreKey + "/" + attachment.FileName;
						image.ImageUrl = imgPath;
						image.Visible = true;
					}
				}
				else
				{
					image.Visible = false;
				}
			}
		}
	}

	/// <summary>
	/// 写页面导航
	/// </summary>
	protected void WritePagesNavigator()
	{
		int cateID = -1;
		string cateIDStr = Request.QueryString["cate"];
		if (!string.IsNullOrEmpty(cateIDStr))
		{
			int.TryParse(cateIDStr, out cateID);
		}

		int articlesCount = ArticleManager.GetCategoryArticlesCount(cateID);
		int pagesCount = articlesCount / pageSize;
		if (pagesCount * pageSize < articlesCount)
		{
			++pagesCount;
		}

		string viewStateStr = Request.QueryString["v"];

		// 当前页
		int pageIndex = 0;
		string pageIDStr = Request.QueryString["p"];
		if (!string.IsNullOrEmpty(pageIDStr))
		{
			int.TryParse(pageIDStr, out pageIndex);
		}

		// 如果当前页为第一页则不显示第一页
		if (pageIndex != 0)
		{
			Response.Write("<a href='newslist.aspx?cate=" + cateIDStr + "&amp;v=" + viewStateStr + "&amp;p=" + (pageIndex - 1) + "'>&lt; 上一页</a>");
		}

		// 计算开始显示的页数和未来显示的页数
		int startIndex = Math.Max(pageIndex - 5, 0);
		int endIndex = Math.Min(pageIndex + 5, pagesCount - 1);

		// 是否显示首页
		if (startIndex > 0)
		{
			Response.Write("<a href='newslist.aspx?cate=" + cateIDStr + "&amp;v=" + viewStateStr + "&amp;p=0'>1</a>");
		}

		// 显示中间的页数
		for (int n = startIndex; n <= endIndex; ++n)
		{
			if (n == pageIndex)
			{
				Response.Write("<span class='current'>" + (n + 1) + "</span>");
			}
			else
			{
				Response.Write("<a href='newslist.aspx?cate=" + cateIDStr + "&amp;v=" + viewStateStr + "&amp;p=" + n + "'>" + (n + 1) + "</a>");
			}
		}

		// 是否显示尾页
		if (endIndex < pagesCount - 1)
		{
			Response.Write("<a href='newslist.aspx?cate=" + cateIDStr + "&amp;v=" + viewStateStr + "&amp;p=" + (pagesCount - 1) + "'>" + pagesCount + "</a>");
		}

		// 如果当前页为最后一页则不显示最后一页
		if (pageIndex != pagesCount - 1 && pagesCount > 0)
		{
			Response.Write("<a href='newslist.aspx?cate=" + cateIDStr + "&amp;v=" + viewStateStr + "&amp;p=" + (pageIndex + 1) + "'>下一页 &gt;</a>");
		}
	}
}
