using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using System.Web.Security;
using HHOnline.Cache;
using HHOnline.Framework;

public partial class ControlPanel_LogOut : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        
        //FormsAuthentication.RedirectToLoginPage();
        if (Request.UrlReferrer != null)
            Response.Redirect(Request.UrlReferrer.ToString());
        else
            Response.Redirect(GlobalSettings.RelativeWebRoot + "Login.aspx");
        HHCache.Instance.Clear();
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "用户注销";
        base.OnPageLoaded();
    }
}
