using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Shops;
using HHOnline.Framework.Web;


public partial class ControlPanel_product_ProductFocus : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            BindLinkButton();
            BindData();
        }
    }


    void BindData()
    {
        FocusType type = FocusType.Recommend;
        try
        {

            if (!GlobalSettings.IsNullOrEmpty(Request.QueryString["ft"]))
                type = (FocusType)(Convert.ToInt32(Request.QueryString["ft"]));
        }
        catch
        {
            Response.Redirect(this.lnkRecomment.PostBackUrl);
        }
        switch (type)
        {
            case FocusType.Hot:
                lnkHot.CssClass = "active";
                break;
            case FocusType.New:
                lnkNew.CssClass = "active";
                break;
            case FocusType.Promotion:
                lnkPromotion.CssClass = "active";
                break;
            case FocusType.Recommend:
                lnkRecomment.CssClass = "active";
                break;
        }
        this.egvProductFocus.DataSource = ProductFocusManager.GetList(type);
        this.egvProductFocus.DataBind();
    }

    private string destinationURL = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-productfocus";

    void BindLinkButton()
    {
        this.lnkHot.PostBackUrl = destinationURL + "&ft=" + ((int)FocusType.Hot).ToString();
        this.lnkNew.PostBackUrl = destinationURL + "&ft=" + ((int)FocusType.New).ToString();
        this.lnkPromotion.PostBackUrl = destinationURL + "&ft=" + ((int)FocusType.Promotion).ToString();
        this.lnkRecomment.PostBackUrl = destinationURL + "&ft=" + ((int)FocusType.Recommend).ToString();
    }

    protected void lnk_Click(object sender, EventArgs e)
    {
        LinkButton link = sender as LinkButton;
        Response.Redirect(link.PostBackUrl);
    }

    public override void OnPageLoaded()
    {
        this.PagePermission = "FocusModule-View";
        this.ShortTitle = "分类栏目管理";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
    }

    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "FocusModule-View";
        base.OnPermissionChecking(e);
    }
}
