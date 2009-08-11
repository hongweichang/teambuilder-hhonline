using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;

public partial class ControlPanel_News_CategoryParent : HHPage
{
	protected void Page_Load(object sender, EventArgs e)
	{

	}

	public override void OnPageLoaded()
	{
		this.ShortTitle = "资讯分类";
		this.SetTitle();
		SetTabName(this.ShortTitle);
	}
	protected override void OnPermissionChecking(PermissionCheckingArgs e)
	{
		this.PagePermission = "NewsCategoryModule-View";
		base.OnPermissionChecking(e); 
	}
}
