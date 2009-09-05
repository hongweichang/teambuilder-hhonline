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
	private readonly string destUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?news-attachment";

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
		btnNewAttachment.PostBackUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?news-attachmentaddedit";
		btnAll.PostBackUrl = destUrl;
		btnSmallSizeFile.PostBackUrl = destUrl + "&css=0&ces=1048576";
		btnMediumSizeFile.PostBackUrl = destUrl + "&css=1048576&ces=10485760";
		btnLargeSizeFile.PostBackUrl = destUrl + "&css=10485760";
	}

	private void BindData()
	{
		AttachmentQuery query = AttachmentQuery.GetQueryFromQueryString(Request.QueryString);

		btnAll.CssClass = string.Empty;
		lblTip.Text = string.Empty;

		// 判断名称
		if (!string.IsNullOrEmpty(query.Name))
		{
			lblTip.Text = "名称中包含“" + query.Name + "”";
		}

		// 判断大小
		if (query.ContentStartSize.HasValue)
		{
			if (!string.IsNullOrEmpty(lblTip.Text))
			{
				lblTip.Text += "；";
			}

			lblTip.Text += "大小大于" + query.ContentStartSize.Value + "字节";
		}

		if (query.ContentEndSize.HasValue)
		{
			if (!string.IsNullOrEmpty(lblTip.Text))
			{
				lblTip.Text += "；";
			}

			lblTip.Text += "大小小于" + query.ContentEndSize.Value + "字节";
		}

		// 判断日期
		if (query.CreateStartTime.HasValue)
		{
			if (!string.IsNullOrEmpty(lblTip.Text))
			{
				lblTip.Text += "；";
			}

			lblTip.Text += "上传日期从“" + query.CreateStartTime.Value.ToShortDateString() + "”开始";
		}

		if (query.CreateEndTime.HasValue)
		{
			if (!string.IsNullOrEmpty(lblTip.Text))
			{
				lblTip.Text += "；";
			}

			lblTip.Text += "上传日期到“" + query.CreateEndTime.Value.ToShortDateString() + "”截止";
		}

		if (string.IsNullOrEmpty(lblTip.Text))
		{
			lblTip.Text = "全部";
			btnAll.CssClass = "active";
		}

		query.PageSize = egvAttachments.PageSize;
		query.PageIndex = egvAttachments.PageIndex;

		PagingDataSet<ArticleAttachment> products = ArticleAttachments.GetAttachments(query);
		egvAttachments.DataSource = products.Records;
		egvAttachments.DataBind();
	}

	protected void btnQuickSearch_Click(object sender, EventArgs e)
	{
		LinkButton btn = sender as LinkButton;
		Response.Redirect(btn.PostBackUrl);
	}

	protected void btnSearch_Click(object sender, EventArgs e)
	{
		string url = destUrl;

		if (!string.IsNullOrEmpty(txtAttachmentName.Text))
		{
			url += "&name=" + txtAttachmentName.Text;
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

		Response.Redirect(url);
	}

	protected void egvAttachments_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			ArticleAttachment attachment = e.Row.DataItem as ArticleAttachment;
			Image picture = e.Row.FindControl("imgPicture") as Image;

			if (picture != null)
			{
				picture.ImageUrl = attachment.GetDefaultImageUrl((int)picture.Width.Value, (int)picture.Height.Value);
			}

			HyperLink hyName = e.Row.FindControl("hlName") as HyperLink;
			if (hyName != null)
			{
				hyName.Text = attachment.Name;
				hyName.NavigateUrl = "#";
			}
		}
	}

	protected void egvAttachments_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		int attachmentID = (int)egvAttachments.DataKeys[e.RowIndex].Value;
		DataActionStatus status = ArticleAttachments.DeleteAttachment(
			Server.MapPath("~") + "/FileStore/" + ArticleAttachments.FileStoreKey,
			attachmentID);

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

	protected void egvAttachments_RowUpdating(object sender, GridViewUpdateEventArgs e)
	{
		Response.Redirect(GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?news-attachmentaddedit&ID=" + egvAttachments.DataKeys[e.RowIndex].Value);
	}

	public override void OnPageLoaded()
	{
		this.PageInfoType = InfoType.IframeInfo;
		base.OnPageLoaded();
		this.ShortTitle = "附件管理";
		this.SetTitle();
		this.SetTabName(this.ShortTitle);

		AddJavaScriptInclude("scripts/jquery.jmodal.js", false, true);
		AddJavaScriptInclude("scripts/jquery.datepick.js", false, false);
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
