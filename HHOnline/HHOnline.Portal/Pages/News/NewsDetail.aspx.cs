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
using HHOnline.Framework.Algorithm.FastRelaceAlgorithm;
using HHOnline.Shops;
using HHOnline.Framework;
using HHOnline.News;

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
			if (int.TryParse(HHOnline.Framework.GlobalSettings.Decrypt(articleIDStr), out articleID))
			{
				// 增加点击率
				//ArticleManager.IncreaseHitTimes(articleID);
				BaseViews views = ViewsFactory.GetViews(typeof(ArticleViews));
				views.AddViewCount(articleID);

				Article article = ArticleManager.GetArticle(articleID);
				if (article != null)
				{
					lblAbstract.InnerHtml = article.Abstract;
					lblAuthor.Text = string.IsNullOrEmpty(article.Author) ? "匿名" : article.Author;
					lblDate.Text = article.Date.HasValue ? article.Date.Value.ToString() : DateTime.Now.ToString();
					lblHitTimes.Text = article.HitTimes.ToString();
					lblTitle.Text = article.Title;
					lblSubTitle.Text = article.SubTitle;
					lblKeywords.Text = string.IsNullOrEmpty(article.Keywords) ? "无" : article.Keywords;

					// 获取所有产品
					ProductQuery pq = new ProductQuery();
					List<Product> products = Products.GetProductList(pq);
					List<ReplaceKeyValue> rkvs = new List<ReplaceKeyValue>();

					foreach (Product item in products)
					{
						ReplaceKeyValue rkv = new ReplaceKeyValue();
						rkv.Key = item.ProductName;
                        rkv.Value = "<a style='color: blue; text-decoration:underline;' href=\"view.aspx?product-product&ID=" + HHOnline.Framework.GlobalSettings.Encrypt(item.ProductID.ToString()) + "\">" + item.ProductName + "</a>";
						rkvs.Add(rkv);
					}

					FastReplace fr = new FastReplace(rkvs.ToArray());
					lblContent.InnerHtml = fr.ReplaceAll(article.Content);

					// 查找分类
					ArticleCategory ac = ArticleManager.GetArticleCategory(article.Category);
					if (ac != null)
					{
						btnCategory.Text = ac.Name;
                        btnCategory.OnClientClick = "window.location.href='view.aspx?news-newslist&cate=" + HHOnline.Framework.GlobalSettings.Encrypt(ac.ID.ToString()) + "';return false;";
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
            this.SetTitle();
		}
	}

	public override void OnPageLoaded()
	{
		AddJavaScriptInclude("Scripts/Pages/newsDetail.aspx.js", false, false);
	}
}
