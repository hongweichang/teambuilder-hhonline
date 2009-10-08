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
        this.PagePermission = "ProductModule-View";
        this.ShortTitle = "分类栏目";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
    }

    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "ProductModule-View";
        base.OnPermissionChecking(e);
    }

    protected void egvProductFocus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        egvProductFocus.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void egvProductFocus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ProductFocus focus = e.Row.DataItem as ProductFocus;
            HyperLink hyName = e.Row.FindControl("hlProductName") as HyperLink;
            Product product = Products.GetProduct(focus.ProductID);
            if (hyName != null && product != null)
            {
                hyName.Text = product.ProductName;
                hyName.NavigateUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-product&di=" + focus.ProductID;
            }
        }
    }

    protected void egvProductFocus_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Response.Redirect(GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-productfocusadd&focusID=" + egvProductFocus.DataKeys[e.RowIndex].Value);

    }

    protected void egvProductFocus_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int focusID = (int)egvProductFocus.DataKeys[e.RowIndex].Value;
        DataActionStatus status = ProductFocusManager.Delete(focusID);
        switch (status)
        {
            case DataActionStatus.Success:
                BindData();
                break;
            default:
            case DataActionStatus.UnknownFailure:
                throw new HHException(ExceptionType.Failed, "删除失败，请确认此条记录存在，并状态正常！");
        }
    }
}
