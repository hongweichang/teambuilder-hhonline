using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;

public partial class ControlPanel_Site_LinkURL : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "常用链接";
        this.SetTabName(this.ShortTitle);
        this.SetTitle();
    }
}
