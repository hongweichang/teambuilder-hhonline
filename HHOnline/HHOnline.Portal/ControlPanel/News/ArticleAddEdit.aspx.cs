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
using HHOnline.News.Services;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Framework;
using System.Text;

public partial class ControlPanel_News_ArticleAddEdit : HHPage
{
	private bool isEdit;

	void BindArticle(int id)
	{
		Article article = ArticleManager.GetArticle(id);
		BindParentCategory(article.Category);
		txtTitle.Text = article.Title;
		txtSubTitle.Text = article.SubTitle;
		txtAbstract.Text = article.Abstract;
		txtContent.Text = article.Content;
		ddlArticleImages.SelectedValue = article.Image.ToString();

		if (article.Date.HasValue)
		{
			txtDate.Text = article.Date.Value.ToString("yyyy年MM月dd日");
		}

		txtCopyFrom.Text = article.CopyFrom;
		txtAuthor.Text = article.Author;
		txtKeywords.Text = article.Keywords;
		txtDisplayOrder.Text = article.DisplayOrder.ToString();
		txtMemo.Text = article.ArticleMemo;
		csArticle.SelectedValue = article.Status;
	}

	void BindParentCategory(int parentID)
	{
		//ArticleCategory category = ArticleManager.GetArticleCategory(parentID);
		//lblParentCategory.Text = category.Name;
		ascCategory.SelectedCategoryID = parentID;
	}

	protected override void OnPagePermissionChecking()
	{
		this.PagePermission = "ArticleModule-Add";
		base.OnPagePermissionChecking();
	}

	private void BindArticleAttachment()
	{
		AttachmentQuery aq = new AttachmentQuery();
		PagingDataSet<ArticleAttachment> items = ArticleAttachments.GetAttachments(aq);

		ddlArticleImages.DataTextField = "Name";
		ddlArticleImages.DataValueField = "ID";
		ddlArticleImages.DataSource = items.Records;
		ddlArticleImages.DataBind();
	}

    void WritePics()
    {
        AttachmentQuery aq = new AttachmentQuery();
        List<ArticleAttachment> items = ArticleAttachments.GetAttachments(aq).Records;
        if (items != null && items.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ArticleAttachment a in items)
            {
                if (a.IsRemote)
                {
                    sb.Append("item" + a.ID.ToString() + ":'" + a.FileName + "',");
                }
                else
                {
                    sb.Append("item" + a.ID.ToString() + ":'" + a.GetDefaultImageUrl(40, 40) + "',");
                }
            }
            ArticleAttachment aa = items[0];
            imgTitleImg.ImageUrl = (aa.IsRemote ? aa.FileName : aa.GetDefaultImageUrl(40, 40));
            base.ExecuteJs("var titlePics = {" + sb.ToString().Remove(sb.ToString().Length - 1) + "};", true);
        }
    }
	protected void Page_Load(object sender, EventArgs e)
	{
		isEdit = Request.QueryString["act"].ToLower() == "edit";
        WritePics();
		if (!IsPostBack && !IsCallback)
		{
			BindArticleAttachment();

			try
			{
				if (isEdit)
				{
					btnPost.Text = " 修 改 ";
					int id = int.Parse(Request.QueryString["ID"]);
					BindArticle(id);
				}
				else
				{
					btnPost.Text = " 增 加 ";

					if (Request.QueryString["catid"] != null)
					{
						int parentID;

						if (int.TryParse(Request.QueryString["catid"], out parentID))
						{
							BindParentCategory(parentID);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new HHException(ExceptionType.ModuleInitFail, ex.Message);
			}
		}
	}

	protected void btnPost_Click(object sender, EventArgs e)
	{
		if (isEdit)
		{
			int id = int.Parse(Request.QueryString["ID"]);
			Article article = ArticleManager.GetArticle(id);

			article.Title = txtTitle.Text.Trim();
			article.SubTitle = txtSubTitle.Text.Trim();
			article.Abstract = txtAbstract.Text;
			article.Content = txtContent.Text;
            article.CopyFrom = txtCopyFrom.Text.Trim();
            article.Author = txtAuthor.Text.Trim();
            article.Keywords = GlobalSettings.FormatKeywords(txtKeywords.Text);
			article.DisplayOrder = int.Parse(txtDisplayOrder.Text);
			article.ArticleMemo = txtMemo.Text;
			article.Status = csArticle.SelectedValue;
            if (!string.IsNullOrEmpty(ddlArticleImages.SelectedValue))
                article.Image = int.Parse(ddlArticleImages.SelectedValue);
            if (string.IsNullOrEmpty(txtDate.Text.Trim()))
                article.Date = DateTime.Now;
            else
                article.Date = DateTime.Parse(txtDate.Text.Trim());
			article.UpdateTime = DateTime.Now;
			article.UpdateUser = Profile.AccountInfo.UserID;

			DataActionStatus status = ArticleManager.UpdateArticle(article);

			if (status == DataActionStatus.DuplicateName)
			{
				//mbMsg.ShowMsg("新增资讯失败，存在同名资讯！");
			}
			else if (status == DataActionStatus.UnknownFailure)
			{
				throw new HHException(ExceptionType.Failed, "更新资讯信息失败，请联系管理员！");
			}
			else if (status == DataActionStatus.Success)
			{
				throw new HHException(ExceptionType.Success, "操作成功，已成功更新资讯！");
			}
		}
		else
		{
			Article article = new Article();
			//int parentID = int.Parse(Request.QueryString["catid"]);
			int parentID = ascCategory.SelectedCategoryID;

			article.Category = parentID;
			article.Title = txtTitle.Text.Trim();
			article.SubTitle = txtSubTitle.Text.Trim();
			article.Abstract = txtAbstract.Text;
			article.Content = txtContent.Text;
            article.CopyFrom = txtCopyFrom.Text.Trim();
            article.Author = txtAuthor.Text.Trim();
            article.Keywords = GlobalSettings.FormatKeywords(txtKeywords.Text);
			article.DisplayOrder = int.Parse(txtDisplayOrder.Text);
			article.ArticleMemo = txtMemo.Text;
            article.Status = csArticle.SelectedValue;
            if (!string.IsNullOrEmpty(ddlArticleImages.SelectedValue))
                article.Image = int.Parse(ddlArticleImages.SelectedValue);
            if (string.IsNullOrEmpty(txtDate.Text.Trim()))
                article.Date = DateTime.Now;
            else
                article.Date = DateTime.Parse(txtDate.Text.Trim());
			article.CreateTime = DateTime.Now;
			article.CreateUser = Profile.AccountInfo.UserID;
			article.UpdateTime = DateTime.Now;
			article.UpdateUser = Profile.AccountInfo.UserID;

			DataActionStatus status;
			ArticleManager.AddArticle(article, out status);

			if (status == DataActionStatus.DuplicateName)
			{
				throw new HHException(ExceptionType.Failed, "新增资讯失败，存在同名资讯！");
			}
			else if (status == DataActionStatus.UnknownFailure)
			{
				throw new HHException(ExceptionType.Failed, "新增资讯失败，请联系管理员！");
			}
			else if (status == DataActionStatus.Success)
			{
				throw new HHException(ExceptionType.Success, "操作成功，已成功增加一个新的资讯！");
			}
		}
	}

	public override void OnPageLoaded()
	{
		this.ShortTitle = "新增文章";

		this.SetTitle();
		this.SetTabName(this.ShortTitle);
		AddJavaScriptInclude("scripts/jquery.datepick.js", false, false);
		AddJavaScriptInclude("scripts/pages/aticleadd.aspx.js", false, false);
	}
}
