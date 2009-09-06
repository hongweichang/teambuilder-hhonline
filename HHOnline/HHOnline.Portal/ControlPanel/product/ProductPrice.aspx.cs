using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Shops;

public partial class ControlPanel_product_ProductPrice : HHPage
{
    int productID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        productID = Convert.ToInt32(Request.QueryString["ProductID"]);
        if (!IsPostBack && !IsCallback)
        {
            BindData();
            BindLinkButton();
        }
    }

    #region Bind
    void BindData()
    {
        this.egvProductPrices.DataSource = ProductPrices.GetPrices(productID);
        this.egvProductPrices.DataBind();
    }

    void BindLinkButton()
    {
        hyAllProduct.NavigateUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-Product";
        Product product = Products.GetProduct(productID);
        if (product != null)
        {
            hyProductPrice.Text = product.ProductName;
            hyProductPrice.NavigateUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-productprice&ProductID=" + productID;
        }
        this.lbNewPrice.PostBackUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-productpriceadd&ProductID=" + productID;
    }
    #endregion

    #region GridView
    protected void egvProductPrices_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }

    protected void egvProductPrices_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int priceID = (int)egvProductPrices.DataKeys[e.RowIndex].Value;
        DataActionStatus status = ProductPrices.Delete(priceID);
        switch (status)
        {
            case DataActionStatus.Success:
                BindData();
                break;
            default:
            case DataActionStatus.UnknownFailure:
                throw new HHException(ExceptionType.Failed, "删除报价信息失败，请确认此报价信息存在，并状态正常！");
        }
    }

    protected void egvProductPrices_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Response.Redirect(GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-productpriceadd&ID=" + egvProductPrices.DataKeys[e.RowIndex].Value);
    }

    protected void egvProductPrices_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        egvProductPrices.PageIndex = e.NewPageIndex;
        BindData();
    }
    #endregion

    #region Override
    public override void OnPageLoaded()
    {
        this.PagePermission = "ProductModule-View";
        this.ShortTitle = "报价管理";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
    }

    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "ProductModule-View";
        e.CheckPermissionControls.Add("ProductModule-Add", lbNewPrice);
        base.OnPermissionChecking(e);
    }
    #endregion
}
