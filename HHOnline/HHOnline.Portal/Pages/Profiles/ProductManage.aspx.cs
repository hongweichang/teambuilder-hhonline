using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class Pages_Profiles_ProductManage : HHPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			CheckPermission();
			//code goes here
		}
	}
	void CheckPermission()
	{
		User u = Profile.AccountInfo;
		if (u.UserType != UserType.CompanyUser ||
			(u.Company.CompanyType == CompanyType.Ordinary || 
			(u.Company.CompanyType == (CompanyType.Ordinary | CompanyType.Agent)))
			|| u.IsManager != 1)
		{
			throw new HHException(ExceptionType.ModuleInitFail, "没有相应的权限！");
		}
	}

	public override void OnPageLoaded()
	{
		this.ShortTitle = "产品管理";
		this.SetTitle();
		this.SetTabName(this.ShortTitle);
	}
}
