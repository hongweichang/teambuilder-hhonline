using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using System.Web.Security;
using HHOnline.Cache;

public partial class ControlPanel_LogOut : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
        HHCache.Instance.Clear();
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "用户注销";
        base.OnPageLoaded();
    }
}
