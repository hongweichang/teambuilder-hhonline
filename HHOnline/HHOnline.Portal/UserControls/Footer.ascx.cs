using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;

public partial class UserControls_Footer : System.Web.UI.UserControl
{
    public static string _url = GlobalSettings.RelativeWebRoot;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCR();
        }
    }
    void BindCR()
    {
        SiteSettings ss = HHContext.Current.SiteSettings;
        ltCopyRight.Text = ss.Copyright;
        ltIcp.Text = "<a style=\"font-size:10px\" href=\"http://www.miibeian.gov.cn/\" target=\"_blank\">" + ss.CompanyICP + "</a>";
    }
}
