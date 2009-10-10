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
using HHOnline.Framework;
using HHOnline.Shops;
using Image = System.Web.UI.WebControls.Image;

public partial class Pages_Profiles_ProductEditSupplier : HHPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			CheckPermission();
			BindData();
		}
	}

	private void CheckPermission()
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
		ShortTitle = "供应信息";
		SetTitle();
		SetTabName(ShortTitle);

		//AddJavaScriptInclude("scripts/jquery.cookie.js", false, false);
		//base.ExecuteJs("$.fn.cookie({ action: 'set', name: 'hhonline_menu', value: 'item_productmanage' });", false);
		AddJavaScriptInclude("scripts/jquery.datepick.js", false, false);
		AddJavaScriptInclude("scripts/pages/producteditsupplier.aspx.js", false, false);
	}

	/// <summary>
	/// 绑定信息
	/// </summary>
	public void BindData()
	{
		int productID = Convert.ToInt32(Request.QueryString["ProductID"]);

	}
}
