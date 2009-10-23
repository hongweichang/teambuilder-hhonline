using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class ControlPanel_Site_ManageSearchWords : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindWords();
    }
    void BindWords()
    {
        egvSW.DataSource = WordSearchManager.GetStatistic(GlobalSettings.MinValue, GlobalSettings.MaxValue);
        egvSW.DataBind();
    }

    protected void egvSW_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        egvSW.EditIndex = -1;
        BindWords();
    }

    protected void egvSW_RowEditing(object sender, GridViewEditEventArgs e)
    {
        egvSW.EditIndex = e.NewEditIndex;
        BindWords();
    }

    protected void egvSW_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        int id = (int)egvSW.DataKeys[e.RowIndex].Value;
        WordSearchManager.DeleteWords(id);
        BindWords();
    }

    protected void egvSW_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow gvr = egvSW.Rows[e.RowIndex];
        int k = (int)egvSW.DataKeys[e.RowIndex].Value;
        string count = (gvr.FindControl("txtCount") as TextBox).Text.Trim();
        decimal c = 0;
        decimal.TryParse(count, out c);
        WordSearchManager.UpdateWordHitCount(k, c);
        egvSW.EditIndex = -1;
        BindWords();
    }

    protected void egvSW_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        egvSW.PageIndex = e.NewPageIndex;
        BindWords();
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "搜索管理";
        this.SetTabName(this.ShortTitle);
        this.SetTitle();
        this.AddJavaScriptInclude("scripts/pages/searchword.aspx.js", false, false);
    }
    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "SearchWordModule-View";
        e.CheckPermissionControls.Add("SearchWordModule-View", lbNewProduct);
        base.OnPermissionChecking(e);
    }
}
