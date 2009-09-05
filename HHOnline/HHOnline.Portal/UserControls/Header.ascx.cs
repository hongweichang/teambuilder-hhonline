using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;

public partial class UserControls_Header : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTel();            
        }
    }
    void BindTel()
    {
        SiteSettings settings = HHContext.Current.SiteSettings;
        ltPhone.Text = settings.ServiceTel;
        HttpContext context = HttpContext.Current;
        string des = string.Empty;
        if (context.User.Identity.IsAuthenticated)
        {
            des = "欢迎您，" + Profile.AccountInfo.DisplayName + "！&nbsp;&nbsp;" +
                                           "<a href=\"" + GlobalSettings.RelativeWebRoot + "controlpanel/logout.aspx\">[注销]</a>";
            if (Profile.AccountInfo.UserType == UserType.InnerUser)
            {
                des += "&nbsp;|&nbsp;<a href=\"" + GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx\">[管理中心]</a>";
            }
        }
        else
        {
            des = "<a href=\"" + GlobalSettings.RelativeWebRoot + "login.aspx\">[登录]</a>" +
                "&nbsp;|&nbsp;<a href=\"" + GlobalSettings.RelativeWebRoot + "register.aspx\">[注册]</a>";
        }
        ltDescriptions.Text = des;
    }
}
