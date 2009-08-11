using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Shops;
using HHOnline.Framework.Web;

public partial class ControlPanel_Product_Variety : HHPage
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
        this.egvBrands.DataSource = ProductBrands.GetProductBrands();
        this.egvBrands.DataBind();
    }

    void BindLinkButton()
    {
        lbNewBrand.PostBackUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-varietyadd";
    }

    public override void OnPageLoaded()
    {
        this.ShortTitle = "品牌管理";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
    }


    protected void egvBrands_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ProductBrand brand = e.Row.DataItem as ProductBrand;
            Image brandLogo = e.Row.FindControl("BrandLogo") as Image;
            if (brandLogo != null)
            {
                if (brand.File != null)
                    brandLogo.ImageUrl = SiteUrlManager.GetResizedImageUrl(brand.File, (int)brandLogo.Width.Value, (int)brandLogo.Height.Value);
                else
                    brandLogo.ImageUrl = SiteUrlManager.GetNoPictureUrl((int)brandLogo.Width.Value, (int)brandLogo.Height.Value);
            }
            HyperLink hyName = e.Row.FindControl("hlBrandName") as HyperLink;
            if (hyName != null)
            {
                hyName.Text = brand.BrandName;
                hyName.NavigateUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-product&bi=" + brand.BrandID;
            }
        }
    }

    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "VarietyModule-View";
        e.CheckPermissionControls.Add("VarietyModule-Add", lbNewBrand);
        base.OnPermissionChecking(e);
    }

    protected void egvBrands_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int brandID = (int)egvBrands.DataKeys[e.RowIndex].Value;
        DataActionStatus status = ProductBrands.DeleteBrand(brandID);
        switch (status)
        {
            case DataActionStatus.RelationshipExist:
                throw new HHException(ExceptionType.Failed, "此品牌下存在关联商品，无法直接删除(请先删除此品牌下关联商品)！");
            case DataActionStatus.UnknownFailure:
                throw new HHException(ExceptionType.Failed, "删除商品品牌时失败，请确认此商品品牌存在，并状态正常！");
            case DataActionStatus.Success:
                BindData();
                break;
        }
    }

    protected void egvBrands_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Response.Redirect(GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-varietyadd&ID=" + egvBrands.DataKeys[e.RowIndex].Value);
    }

    protected void egvBrands_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        egvBrands.PageIndex = e.NewPageIndex;
        BindData();
    }
}
