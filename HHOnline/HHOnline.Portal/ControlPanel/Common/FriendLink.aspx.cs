using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class ControlPanel_Common_FriendLink : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLinks();
        }
    }

    protected void rpList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item||e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkEdit = e.Item.FindControl("lnkEdit") as LinkButton;
            LinkButton lnkDelete = e.Item.FindControl("lnkDelete") as LinkButton;
            if (!User.IsInRole("FriendLinkModule-Edit"))
            {
                lnkEdit.Visible = false;  
            }
            if (!User.IsInRole("FriendLinkModule-Delete"))
            {
                lnkDelete.Visible = false;
            }
        }
    }

    protected void rpList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = int.Parse(e.CommandArgument.ToString());
        if (e.CommandName == "Delete")
        {
            FriendLinks.FriendLinkDelete(id);
        }
        BindLinks();
    }
    void BindLinks()
    {
        rpList.DataSource = FriendLinks.FriendLinkGet();
        rpList.DataBind();
    }
    public override void OnPageLoaded()
    {
        this.PagePermission = "FriendLinkModule-View";
        this.ShortTitle = "友情链接";
        this.SetTabName(this.ShortTitle);
        this.SetTitle();
        this.AddJavaScriptInclude("scripts/pages/linkurl.aspx.js", false, false);
    }
    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        e.CheckPermissionControls.Add("FriendLinkModule-Add", lnkAdd);
        base.OnPermissionChecking(e);
    }
}
