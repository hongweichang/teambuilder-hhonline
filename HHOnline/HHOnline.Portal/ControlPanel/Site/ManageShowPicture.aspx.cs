using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Framework.Web;

public partial class ControlPanel_Site_ManageShowPicture : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            BindData();
            BindLinkButton();
        }
    }

    void BindData()
    {
        this.egvShowPictures.DataSource = ShowPictures.GetShowPictures();
        this.egvShowPictures.DataBind();
    }

    void BindLinkButton()
    {
        this.lblNewShowPicture.PostBackUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?site-showpictureadd";
    }

    protected void egvShowPictures_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        egvShowPictures.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void egvShowPictures_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ShowPicture picture = e.Row.DataItem as ShowPicture;
            Image showPictureThumb = e.Row.FindControl("ShowPictureThumb") as Image;
            if (showPictureThumb != null)
            {
                if (picture.File != null)
                    showPictureThumb.ImageUrl = SiteUrlManager.GetResizedImageUrl(picture.File, (int)showPictureThumb.Width.Value, (int)showPictureThumb.Height.Value);
                else
                    showPictureThumb.ImageUrl = SiteUrlManager.GetNoPictureUrl((int)showPictureThumb.Width.Value, (int)showPictureThumb.Height.Value);
            }
        }
    }

    protected void egvShowPictures_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Guid showPictureID = (Guid)this.egvShowPictures.DataKeys[e.RowIndex].Value;
        DataActionStatus status = ShowPictures.Delete(showPictureID);
        switch (status)
        {
            default:
            case DataActionStatus.Success:
                BindData();
                break;
        }
    }

    protected void egvShowPictures_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Response.Redirect(GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?site-showpictureadd&ID=" + egvShowPictures.DataKeys[e.RowIndex].Value);
    }

    public override void OnPageLoaded()
    {
        //  this.PagePermission = "TradeModule-View";
        this.ShortTitle = "展示图片配置";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
    }

    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        //this.PagePermission = "TradeModule-View";
        // e.CheckPermissionControls.Add("TradeModule-Add", lbNewIndustry);
        base.OnPermissionChecking(e);
    }

}
