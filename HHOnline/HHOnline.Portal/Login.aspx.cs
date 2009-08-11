using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using System.Drawing;
using HHOnline.Framework;

public partial class Login : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (User.Identity.IsAuthenticated)
                throw new HHException(ExceptionType.Failed, "您已经登录，请勿重复操作！");
            HttpCookie c = HHCookie.GetCookie("HHOnline-UserInfo");
            if (c != null)
            {
                string uid = GlobalSettings.Decrypt(c.Values["UserName"]);
                string pwd = GlobalSettings.Decrypt(c.Values["Password"]);
                base.ExecuteJs("window.$userinfo={uid:'" + uid + "',pwd:'" + pwd + "'}", false);
            }
        }

    }

    public override void OnPageLoaded()
    {
        this.ShortTitle = "用户登录";
        base.OnPageLoaded();

        this.AddJavaScriptInclude("scripts/pages/login.aspx.js", false, false);
        SetValidator(false, true, 3);
    }
}
