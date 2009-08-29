using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class Pages_Others_UserLicence : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtLicence.Text = ResourceManager.GetLicence();
        }
    }

    public override void OnPageLoaded()
    {
        this.ShortTitle = "用户协议";
        this.SetTitle();
    }
}
