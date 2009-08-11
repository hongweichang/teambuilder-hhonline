using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class Masters_MsgMasterPage :HHMasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        AddGenericLink("text/css", "Stylesheet", "global", "App_Themes/Default/global.css");
    }
}
