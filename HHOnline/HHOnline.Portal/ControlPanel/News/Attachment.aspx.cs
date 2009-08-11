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

public partial class ControlPanel_News_Attachment : HHPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack && !IsCallback)
		{
			BindData();
			//BindLinkButton();
		}
	}
	
	private void BindData()
	{
		AttachmentQuery query = AttachmentQuery.GetQueryFromQueryString(Request.QueryString);

		query.PageSize = egvAttachments.PageSize;
		query.PageIndex = egvAttachments.PageIndex;

		PagingDataSet<ArticleAttachment> products = ArticleManager.GetAttachments(query);
		egvAttachments.DataSource = products.Records;
		egvAttachments.DataBind();
	}

	protected void btnSearch_Click(object sender, EventArgs e)
	{

	}
	protected void egvAttachments_RowDataBound(object sender, GridViewRowEventArgs e)
	{

	}

	protected void egvAttachments_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{

	}

	protected void egvAttachments_RowUpdating(object sender, GridViewUpdateEventArgs e)
	{

	}

	public override void OnPageLoaded()
	{
		this.PageInfoType = InfoType.IframeInfo;
		base.OnPageLoaded();

		AddJavaScriptInclude("scripts/jquery.jmodal.js", false, true);
		AddJavaScriptInclude("scripts/pages/attachment.aspx.js", false, false);
	}

	protected override void OnPermissionChecking(PermissionCheckingArgs e)
	{
		this.PagePermission = "ArticleAttachmentModule-View";
		//e.CheckPermissionControls.Add("ArticleModule-Add", btnAddCategory);
		//e.CheckPermissionControls.Add("ArticleModule-Delete", btnDeleteCategory);
		//e.CheckPermissionControls.Add("ArticleAttachmentModule-Add", btnAddArticle);
		//e.CheckPermissionControls.Add("ArticleAttachmentModule-Delete", btnDeleteArticle);

		base.OnPermissionChecking(e);
	}
}
