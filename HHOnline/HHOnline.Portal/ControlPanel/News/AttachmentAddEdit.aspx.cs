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
using HHOnline.News.Components;
using HHOnline.News.Services;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Framework;
using System.IO;

public partial class ControlPanel_News_AttachmentAddEdit : HHPage
{
	/// <summary>
	/// 添加
	/// </summary>
	private OperateType action = OperateType.Add;
	/// <summary>
	/// 附件ID
	/// </summary>
	private int attachmentID;

	private string attachmentLocalPath;

	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			attachmentID = Convert.ToInt32(Request.QueryString["ID"].Replace("#", ""));
		}
		catch
		{
			attachmentID = 0;
		}

		if (attachmentID != 0)
		{
			action = OperateType.Edit;
		}

		if (!IsPostBack && !IsCallback)
		{
			BindDetail();
		}

		attachmentLocalPath = Server.MapPath("~") + "/FileStore/" + ArticleAttachments.FileStoreKey;
	}

	private void BindDetail()
	{
		ArticleAttachment attachment = ArticleAttachments.GetAttachment(attachmentID);

		if (attachment != null)
		{
			txtTitle.Text = attachment.Name;
			txtMIMEType.Text = attachment.ContentType;
			txtDesc.Text = attachment.Desc;
			cboAttachmentType.SelectedIndex = attachment.IsRemote ? 1 : 0;
			csAttachment.SelectedValue = attachment.Status;
			txtMemo.Text = attachment.Memo;

			ClientScript.RegisterClientScriptBlock(typeof(string), "CheckAttachmentType", "window.onload = function() {CheckAttachmentType();}", true);
		}
	}

	public override void OnPageLoaded()
	{
		if (action == OperateType.Add)
		{
			this.ShortTitle = "新增附件";
		}
		else
		{
			this.ShortTitle = "编辑编辑";
		}

		SetTitle();
		SetTabName(this.ShortTitle);
		PageInfoType = InfoType.PopWinInfo;
		AddJavaScriptInclude("scripts/pages/attachmentadd.aspx.js", false, false);
	}

	protected override void OnPagePermissionChecking()
	{
		if (action == OperateType.Add)
		{
			this.PagePermission = "ArticleAttachmentModule-Add";
		}
		else
		{
			this.PagePermission = "ArticleAttachmentModule-Edit";
		}

		base.OnPagePermissionChecking();
	}

	protected void btnPost_Click(object sender, EventArgs e)
	{
		if (action == OperateType.Edit)
		{
			int id = int.Parse(Request.QueryString["ID"]);
			ArticleAttachment attachment = ArticleAttachments.GetAttachment(id);

			attachment.Name = txtTitle.Text;
			attachment.ContentType = txtMIMEType.Text;
			attachment.Desc = txtDesc.Text;
			attachment.IsRemote = cboAttachmentType.SelectedIndex == 1;
			attachment.Memo = txtMemo.Text;
			attachment.Status = csAttachment.SelectedValue;

			// 判断是远程则直接更新URL，否则先删除本地文件
			if (cboAttachmentType.SelectedIndex == 1)
			{
				// 远程
				attachment.IsRemote = true;

				// TODO: 删除文件
				string filePath = attachmentLocalPath + attachment.FileName;
				File.Delete(filePath);

				// 更新字段
				attachment.FileName = txtUrl.Text;
			}
			else
			{
				// 本地上传
				attachment.IsRemote = false;

				// TODO: 本地上传
				// 获取扩展名
				string ext = Path.GetExtension(fuLocal.FileName);

				attachment.FileName = Guid.NewGuid().ToString() + ext;
				string filePath = attachmentLocalPath + attachment.FileName;

				fuLocal.SaveAs(filePath);
			}

			attachment.UpdateTime = DateTime.Now;
			attachment.UpdateUser = Profile.AccountInfo.UserID;

			DataActionStatus status = ArticleAttachments.UpdateArticleAttachment(attachment);

			if (status == DataActionStatus.DuplicateName)
			{
				mbMsg.ShowMsg("修改附件失败，存在同名附件！");
			}
			else if (status == DataActionStatus.UnknownFailure)
			{
				throw new HHException(ExceptionType.Failed, "更新附件信息失败，请联系管理员！");
			}
			else if (status == DataActionStatus.Success)
			{
				throw new HHException(ExceptionType.Success, "操作成功，已成功更新附件！");
			}
		}
		else
		{
			ArticleAttachment attachment = new ArticleAttachment();

			attachment.Name = txtTitle.Text;
			attachment.ContentType = txtMIMEType.Text;
			attachment.Desc = txtDesc.Text;
			attachment.IsRemote = cboAttachmentType.SelectedIndex == 1;
			attachment.Memo = txtMemo.Text;
			attachment.Status = csAttachment.SelectedValue;

			// 判断是远程则直接更新URL
			if (cboAttachmentType.SelectedIndex == 1)
			{
				// 远程
				attachment.IsRemote = true;

				// url字段
				attachment.FileName = txtUrl.Text;
			}
			else
			{
				// 本地上传
				attachment.IsRemote = false;

				// TODO: 本地上传
				string ext = Path.GetExtension(fuLocal.FileName);

				attachment.FileName = Guid.NewGuid().ToString() + ext;
				string filePath = attachmentLocalPath + attachment.FileName;

				fuLocal.SaveAs(filePath);
			}

			attachment.CreateTime = DateTime.Now;
			attachment.CreateUser = Profile.AccountInfo.UserID;
			attachment.UpdateTime = DateTime.Now;
			attachment.UpdateUser = Profile.AccountInfo.UserID;

			DataActionStatus status;
			ArticleAttachments.AddArticleAttachment(attachment, out status);

			if (status == DataActionStatus.DuplicateName)
			{
				throw new HHException(ExceptionType.Failed, "新增附件失败，存在同名附件！");
			}
			else if (status == DataActionStatus.UnknownFailure)
			{
				throw new HHException(ExceptionType.Failed, "新增附件失败，请联系管理员！");
			}
			else if (status == DataActionStatus.Success)
			{
				throw new HHException(ExceptionType.Success, "操作成功，已成功增加一个新的附件！");
			}
		}
	}
}
