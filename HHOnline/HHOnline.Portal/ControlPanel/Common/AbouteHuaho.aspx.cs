using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;

public partial class ControlPanel_Common_AbouteHuaho : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    { 
        
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "关于我们";
        this.SetTabName(this.ShortTitle);
        this.SetTitle();
    }
}
