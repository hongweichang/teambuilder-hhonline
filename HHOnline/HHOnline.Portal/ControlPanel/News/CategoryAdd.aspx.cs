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
using HHOnline.Framework.Web.Enums;
using HHOnline.Framework.Web;
using HHOnline.News.Components;
using HHOnline.News.Services;
using HHOnline.Framework;

public partial class ControlPanel_News_CategoryAdd : HHPage
{
	public override void OnPageLoaded()
	{
		this.PageInfoType = InfoType.PopWinInfo;
		this.ShortTitle = "新增资讯分类";
		
		SetValidator(true, true, 5000);
	}

	protected override void OnPagePermissionChecking()
	{
		this.PagePermission = "NewsCategoryModule-Add";
		base.OnPagePermissionChecking();
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			int id = int.Parse(Request.QueryString["ID"]);
			BindParentCategory(id);
		}
		catch (Exception ex)
		{
			base.ExecuteJs("msg('" + ex.Message + "');", false);
		}
	}

	void BindParentCategory(int id)
	{
		ArticleCategory category = ArticleManager.GetArticleCategory(id);
		ltParentCategory.Text = category.Name;
		ltParentCategoryDesc.Text = category.Description;
	}

	protected void btnPost_Click(object sender, EventArgs e)
	{
		try
		{
			int id = int.Parse(Request.QueryString["ID"]);
			ArticleCategory category = new ArticleCategory();

			category.Description = txtCategoryDesc.Text.Trim();
			category.CreateTime = DateTime.Now;
			category.CreateUser = Profile.AccountInfo.UserID;
			category.DisplayOrder = int.Parse(txtDisplayOrder.Text);
			category.Memo = string.Empty;
			category.Name = txtCategoryName.Text.Trim();
			category.Status = ComponentStatus.Enabled;
			category.ParentID = id;
			category.UpdateTime = DateTime.Now;
			category.UpdateUser = Profile.AccountInfo.UserID;

			DataActionStatus status;
			ArticleCategory orgs = ArticleManager.AddArticleCategory(category, out status);

			if (status == DataActionStatus.DuplicateName)
			{
				mbMsg.ShowMsg("新增资讯分类失败，存在同名资讯分类！");
			}
			else if (status == DataActionStatus.UnknownFailure)
			{
				mbMsg.ShowMsg("新增资讯分类失败，请联系管理员！");
			}
			else if (status == DataActionStatus.Success)
			{
				base.ExecuteJs("msg('操作成功，已成功增加一个新的资讯分类！',true);", false);
			}
		}
		catch (Exception ex)
		{
			base.ExecuteJs("msg('" + ex.Message + "');", false);
		}
	}
}
