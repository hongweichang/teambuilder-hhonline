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
using HHOnline.Framework.Web.Enums;
using HHOnline.Framework;

public partial class ControlPanel_News_CategoryEdit : HHPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack && !IsCallback)
		{
			try
			{
				int id = int.Parse(Request.QueryString["ID"]);
				BindArticleCategory(id);
			}
			catch (Exception ex)
			{
				base.ExecuteJs("msg('" + ex.Message + "');", false);
			}
		}
	}

	private void BindArticleCategory(int id)
	{
		ArticleCategory cat = ArticleManager.GetArticleCategory(id);

		if (cat.ParentID.HasValue)
		{
			int parentID = cat.ParentID.Value;
			ArticleCategory parentCat = ArticleManager.GetArticleCategory(parentID);
			
			if (parentCat != null)
			{
				ltParentCategory.Text = parentCat.Name;
				ltParentCategoryDesc.Text = parentCat.Description;
			}
		}

		txtCategoryName.Text = cat.Name;
		txtCategoryDesc.Text = cat.Description;
		txtDisplayOrder.Text = cat.DisplayOrder.ToString();
	}

	public override void OnPageLoaded()
	{
		this.PageInfoType = InfoType.PopWinInfo;
		this.ShortTitle = "修改资讯分类信息";

		base.OnPageLoaded();
		SetValidator(true, true, 5000);
	}

	protected override void OnPagePermissionChecking()
	{
		this.PagePermission = "NewsCategoryModule-Edit";
		base.OnPagePermissionChecking();
	}

	protected void btnPost_Click(object sender, EventArgs e)
	{
		try
		{
			int id = int.Parse(Request.QueryString["ID"]);
			ArticleCategory cat = ArticleManager.GetArticleCategory(id);

			if (cat != null)
			{
				cat.Name = txtCategoryName.Text;
				cat.Description = txtCategoryDesc.Text;
				cat.DisplayOrder = int.Parse(txtDisplayOrder.Text);
				cat.ID = id;
				cat.UpdateTime = DateTime.Now;
				cat.UpdateUser = Profile.AccountInfo.UserID;

				DataActionStatus status = ArticleManager.UpdateArticleCategory(cat);
				if (status == DataActionStatus.Success)
				{
					base.ExecuteJs("msg('操作成功，已成功修改此资讯分类信息！',true);", false);            
				}
			}
		}
		catch (System.Exception ex)
		{
			
		}

		mbMsg.ShowMsg("修改资讯分类信息失败，请联系管理员！");
	}
}
