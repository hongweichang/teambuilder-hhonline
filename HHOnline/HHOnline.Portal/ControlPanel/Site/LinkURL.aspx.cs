using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class ControlPanel_Site_LinkURL : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLinkUrl();
        }
    }

    protected void rpList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = int.Parse(e.CommandArgument.ToString());
        LinkUrls.LinkUrlDelete(id);
        BindLinkUrl();
    }
    void BindLinkUrl()
    {
        rpList.DataSource = LinkUrls.LinkUrlGet();
        rpList.DataBind();
    }
    public override void OnPageLoaded()
    {
        this.PagePermission = "HHOnlineUser-View";
        this.ShortTitle = "常用链接";
        this.SetTabName(this.ShortTitle);
        this.SetTitle();
        this.AddJavaScriptInclude("scripts/pages/linkurl.aspx.js", false, false);
    }
}
