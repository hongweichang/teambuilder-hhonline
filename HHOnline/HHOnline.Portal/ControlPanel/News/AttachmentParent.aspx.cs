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

public partial class ControlPanel_News_AttachmentParent : HHPage
{
	protected void Page_Load(object sender, EventArgs e)
	{

	}

	public override void OnPageLoaded()
	{
		this.ShortTitle = "附件管理";
		this.SetTitle();
		SetTabName(this.ShortTitle);
	}

	protected override void OnPermissionChecking(PermissionCheckingArgs e)
	{
		this.PagePermission = "ArticleAttachmentModule-View";
		base.OnPermissionChecking(e);
	}
}
