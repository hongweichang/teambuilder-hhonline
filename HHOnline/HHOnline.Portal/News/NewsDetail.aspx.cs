using System;
using System.Collections;
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

public partial class News_NewsDetail : HHPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack && !IsCallback)
		{
			BindData();
		}
	}

	private void BindData()
	{
		string articleIDStr = Request.QueryString["id"];
		if (!string.IsNullOrEmpty(articleIDStr))
		{
			int articleID;
			if (int.TryParse(articleIDStr, out articleID))
			{
				Article article = ArticleManager.GetArticle(articleID);
				if (article != null)
				{
					lblAbstract.Text = article.Abstract;
					lblAuthor.Text = string.IsNullOrEmpty(article.Author) ? "匿名" : article.Author;
					lblContent.Text = article.Content;
					lblDate.Text = article.Date.HasValue ? article.Date.Value.ToString() : DateTime.Now.ToString();
					lblHitTimes.Text = article.HitTimes.ToString();
					lblTitle.Text = article.Title;
					lblSubTitle.Text = article.SubTitle;
					lblKeywords.Text = string.IsNullOrEmpty(article.Keywords) ? "无" : article.Keywords;

					// 查找分类
					ArticleCategory ac = ArticleManager.GetArticleCategory(article.Category);
					if (ac != null)
					{
						btnCategory.Text = ac.Name;
						btnCategory.OnClientClick = "window.location.href='newslist.aspx?cate=" + ac.ID + "';return false;";
					}

					if (!string.IsNullOrEmpty(article.CopyFrom))
					{
						lblCopyForm.Text = "文章来源: " + article.CopyFrom;
						lblCopyForm.Visible = true;
					}
					else
					{
						lblCopyForm.Visible = false;
					}

					// 获取附件
					ArticleAttachment attachment = ArticleAttachments.GetAttachment(article.Image);
					if (attachment != null)
					{
						string imgPath = "../FileStore/" + ArticleAttachments.FileStoreKey + "/" + attachment.FileName;
						imgAttachment.ImageUrl = imgPath;
						imgAttachment.Visible = true;
					}
					else
					{
						imgAttachment.Visible = false;
					}

					this.ShortTitle = article.Title;
				}
				else
				{
					imgAttachment.Visible = false;
				}
			}
		}
	}

	public override void OnPageLoaded()
	{
		base.OnPageLoaded();
		AddGenericLink("text/css", "Stylesheet", "global", "App_Themes/Default/newsList.css");
		AddGenericLink("text/css", "Stylesheet", "global", "App_Themes/Default/newsDetail.css");
		AddJavaScriptInclude("Scripts/Pages/newsDetail.aspx.js", false, false);
	}
}
