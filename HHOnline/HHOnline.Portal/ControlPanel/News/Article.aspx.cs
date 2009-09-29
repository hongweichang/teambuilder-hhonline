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
using System.Collections.Generic;
using HHOnline.News.Services;
using HHOnline.Framework;
using HHOnline.Framework.Web.Enums;

public partial class ControlPanel_News_Article : HHPage
{
	private readonly string destUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?news-article";

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack && !IsCallback)
		{
			BindData();
			BindLinkButton();
		}
	}

	void BindLinkButton()
	{
		btnNewArticle.PostBackUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?news-articleaddedit&act=new";
		btnAll.PostBackUrl = destUrl;
		btnSmallHitTimes.PostBackUrl = destUrl + "&hst=0&het=100";
		btnMediumHitTimes.PostBackUrl = destUrl + "&hst=100&het=1000";
		btnLargeHitTimes.PostBackUrl = destUrl + "&hst=1000";
	}

	private void BindData()
	{
		ascCategory.IsShowAllCategory = true;

		ArticleQuery query = ArticleQuery.GetQueryFromQueryString(Request.QueryString);

		btnAll.CssClass = string.Empty;
		lblTip.Text = string.Empty;

		// 判断标题
		if (!string.IsNullOrEmpty(query.Title))
		{
			lblTip.Text = "标题中包含“" + query.Title + "”";
		}

		// 判断分类
		if (query.CategoryID.HasValue)
		{
			if (!string.IsNullOrEmpty(lblTip.Text))
			{
				lblTip.Text += "；";
			}

			// 获取分类
			ArticleCategory ac = ArticleManager.GetArticleCategory(query.CategoryID.Value);
			//ascCategory.SelectedCategoryID = query.CategoryID.Value;
			lblTip.Text += "分类为“" + ac.Name + "”";
		}

		// 判断点击次数
		if (query.HitStartTimes.HasValue)
		{
			if (!string.IsNullOrEmpty(lblTip.Text))
			{
				lblTip.Text += "；";
			}

			lblTip.Text += "访问量大于" + query.HitStartTimes.Value + "次";
		}

		if (query.HitEndTimes.HasValue)
		{
			if (!string.IsNullOrEmpty(lblTip.Text))
			{
				lblTip.Text += "；";
			}

			lblTip.Text += "访问量小于" + query.HitEndTimes.Value + "次";
		}

		// 判断日期
		if (query.CreateStartTime.HasValue)
		{
			if (!string.IsNullOrEmpty(lblTip.Text))
			{
				lblTip.Text += "；";
			}

			lblTip.Text += "时间从“" + query.CreateStartTime.Value.ToShortDateString() + "”开始";
		}

		if (query.CreateEndTime.HasValue)
		{
			if (!string.IsNullOrEmpty(lblTip.Text))
			{
				lblTip.Text += "；";
			}

			lblTip.Text += "时间到“" + query.CreateEndTime.Value.ToShortDateString() + "”截止";
		}

		if (string.IsNullOrEmpty(lblTip.Text))
		{
			lblTip.Text = "全部";
			btnAll.CssClass = "active";
		}
		
		query.PageSize = Int32.MaxValue;

		PagingDataSet<Article> products = ArticleManager.GetArticles(query);
		egvArticles.DataSource = products.Records;
		egvArticles.DataBind();
	}

	protected void btnQuickSearch_Click(object sender, EventArgs e)
	{
		LinkButton btn = sender as LinkButton;
		Response.Redirect(btn.PostBackUrl);
	}

	protected void btnSearch_Click(object sender, EventArgs e)
	{
		string url = destUrl;

		if (!string.IsNullOrEmpty(txtArticleTitle.Text))
		{
			url += "&title=" + txtArticleTitle.Text;
		}

		if (!string.IsNullOrEmpty(txtCreateStartTime.Text))
		{
			DateTime dt = DateTime.Parse(txtCreateStartTime.Text);
			url += "&cst=" + dt.ToShortDateString();
		}

		if (!string.IsNullOrEmpty(txtCreateEndTime.Text))
		{
			DateTime dt = DateTime.Parse(txtCreateEndTime.Text);
			url += "&cet=" + dt.ToShortDateString();
		}

		url += "&cat=" + ascCategory.SelectedCategoryID;

		Response.Redirect(url);
	}

	protected void egvArticles_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			Article article = e.Row.DataItem as Article;

            if (article != null)
            {
                Image image = e.Row.FindControl("imgPicture") as Image;

                if (image != null)
                {
                    //image.ImageUrl = article.GetDefaultImageUrl((int)image.Width.Value, (int)image.Height.Value);
                    // 获取附件
                    ArticleAttachment attachment = ArticleAttachments.GetAttachment(article.Image);
                    if (attachment != null)
                    {
                        string imgPath = "../FileStore/" + ArticleAttachments.FileStoreKey + "/" + attachment.FileName;
                        image.ImageUrl = imgPath;
                        image.Visible = true;
                    }
                    else
                    {
                        image.Visible = false;
                    }
                }

                HyperLink hyName = e.Row.FindControl("hlName") as HyperLink;
                if (hyName != null)
                {
                    hyName.Text = article.Title;
                    hyName.NavigateUrl = "#";
                }
            }
		}
	}

	protected void egvArticles_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		int articleID = (int)egvArticles.DataKeys[e.RowIndex].Value;
		DataActionStatus status = ArticleManager.DeleteArticle(articleID);

		switch (status)
		{
			case DataActionStatus.RelationshipExist:
				throw new HHException(ExceptionType.Failed, "此附件下存在关联数据，无法直接删除！");

			case DataActionStatus.UnknownFailure:
				throw new HHException(ExceptionType.Failed, "删除附件失败，请联系管理人员！");

			default:
			case DataActionStatus.Success:
				BindData();
				break;
		}
	}

	protected void egvArticles_RowUpdating(object sender, GridViewUpdateEventArgs e)
	{
		Response.Redirect(GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?news-articleaddedit&act=edit&ID=" + egvArticles.DataKeys[e.RowIndex].Value);
	}

	public override void OnPageLoaded()
	{
		this.ShortTitle = "附件管理";
		this.SetTitle();
		this.SetTabName(this.ShortTitle);

		AddJavaScriptInclude("scripts/jquery.jmodal.js", false, true);
		AddJavaScriptInclude("scripts/jquery.datepick.js", false, false);
		AddJavaScriptInclude("scripts/pages/article.aspx.js", false, false);
	}

	protected override void OnPermissionChecking(PermissionCheckingArgs e)
	{
		this.PagePermission = "ArticleModule-View";
		//e.CheckPermissionControls.Add("ArticleModule-Add", btnAddCategory);
		//e.CheckPermissionControls.Add("ArticleModule-Delete", btnDeleteCategory);
		//e.CheckPermissionControls.Add("ArticleArticleModule-Add", btnAddArticle);
		//e.CheckPermissionControls.Add("ArticleArticleModule-Delete", btnDeleteArticle);

		base.OnPermissionChecking(e);
	}

	protected void egvArticles_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		egvArticles.PageIndex = e.NewPageIndex;
		BindData();
	}
}
