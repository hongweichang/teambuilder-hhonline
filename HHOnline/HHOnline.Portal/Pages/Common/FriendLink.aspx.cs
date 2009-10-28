using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class Pages_Common_FriendLink : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLinks();
        }
    }
    void BindLinks()
    {
        rpList.DataSource = FriendLinks.FriendLinkGet();
        rpList.DataBind();
    }
    public string GetSize(object size)
    {
        int s = int.Parse(size.ToString());
        return (100 + s * 10).ToString();
    }
    public override void OnPageLoaded()
    {

        this.ShortTitle = "友情链接";
        this.SetTitle();
    }
}
