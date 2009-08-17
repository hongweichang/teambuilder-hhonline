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
    }
}
