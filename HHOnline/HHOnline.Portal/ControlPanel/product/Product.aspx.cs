using System;
using System.Drawing;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Shops;
using HHOnline.Framework.Web;
using Image = System.Web.UI.WebControls.Image;

public partial class ControlPanel_Product_Product : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            BindData();
            BindLinkButton();
        }
    }

    #region Bind
    void BindData()
    {
        ProductQuery query = ProductQuery.GetQueryFromQueryString(Request.QueryString);
        lnkAll.CssClass = "active";
        lblTip.Text = "“全部”";
        if (query.Filter !=null)
        { 
                lnkAll.CssClass = "";
                switch (query.Filter)
                {
                    case ProviderFilter.All:
                        lnProvider.CssClass = "active";
                        lblTip.Text = "查找“供应商提供产品”";
                        break;
                    case ProviderFilter.Deny:
                        lnProviderDeny.CssClass = "active";
                        lblTip.Text = "查找“供应商已发布产品”";
                        break;
                    case ProviderFilter.Inspect:
                        lnProviderInspect.CssClass = "active";
                        lblTip.Text = "查找“供应商未发布产品”";
                        break;
                }
        }
        if (query.HasPictures.HasValue)
        {
            if (query.HasPictures.Value)
            {
                lnkAll.CssClass = "";
                lnkPicture.CssClass = "active";
                lblTip.Text = "“有图”";
            }
            else
            {
                lnkAll.CssClass = "";
                lnkNoPicture.CssClass = "active";
                lblTip.Text = "“无图”";
            }
        }

        if (query.HasPrice.HasValue)
        {
            if (query.HasPrice.Value)
            {
                lnkAll.CssClass = "";
                lnkPriced.CssClass = "active";
                lblTip.Text = "“已报价”";
            }
            else
            {
                lnkAll.CssClass = "";
                lnkNoPriced.CssClass = "active";
                lblTip.Text = "“未报价”";
            }
        }

        if (query.HasPublished.HasValue)
        {
            if (query.HasPublished.Value)
            {
                lnkAll.CssClass = "";
                lnkPublished.CssClass = "active";
                lblTip.Text = "“已发布”";
            }
            else
            {
                lnkAll.CssClass = "";
                lnkUnPublishied.CssClass = "active";
                lblTip.Text = "“未发布”";
            }
        }
        bool flag = false;
        if (!GlobalSettings.IsNullOrEmpty(query.ProductNameFilter))
        {
            txtProductName.Text = query.ProductNameFilter;
            lblTip.Text = "名称中包含“" + query.ProductNameFilter + "”";
            flag = true;
        }
        //BrandID
        ddlBrands.DataSource = ProductBrands.GetProductBrands();
        ddlBrands.DataTextField = "BrandName";
        ddlBrands.DataValueField = "BrandID";
        ddlBrands.DataBind();
        ddlBrands.Items.Insert(0, new ListItem("=所有品牌=", "0"));

        if (query.BrandID.HasValue && query.BrandID.Value != 0)
        {
            ListItem item = ddlBrands.Items.FindByValue(query.BrandID.Value.ToString());
            if (item != null)
            {
                item.Selected = true;
                if (flag)
                {
                    lblTip.Text = lblTip.Text + ",品牌为“" + ProductBrands.GetProductBrand(query.BrandID.Value).BrandName + "”";
                }
                else
                {
                    lblTip.Text = "品牌为“" + ProductBrands.GetProductBrand(query.BrandID.Value).BrandName + "”";
                    flag = true;
                }
            }
        }

        //CategoryID
        ddlCategory.DataSource = ProductCategories.GetValueRange();
        ddlCategory.DataTextField = "Text";
        ddlCategory.DataValueField = "Name";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new ListItem("=所有分类=", "0"));

        if (query.CategoryID.HasValue && query.CategoryID.Value != 0)
        {
            ListItem item = ddlCategory.Items.FindByValue(query.CategoryID.Value.ToString());
            if (item != null)
            {
                item.Selected = true;
                if (flag)
                {
                    lblTip.Text = lblTip.Text + ",分类为“" + ProductCategories.GetCategory(query.CategoryID.Value).CategoryName + "”";
                }
                else
                {
                    lblTip.Text = "分类为“" + ProductCategories.GetCategory(query.CategoryID.Value).CategoryName + "”";
                    flag = true;
                }
            }
        }
        //IndustryID
        ddlIndustry.DataSource = ProductIndustries.GetHierarchyIndustries();
        ddlIndustry.DataTextField = "IndustryName";
        ddlIndustry.DataValueField = "IndustryID";
        ddlIndustry.DataBind();
        ddlIndustry.Items.Insert(0, new ListItem("=所有行业=", "0"));
        if (query.IndustryID.HasValue && query.IndustryID.Value != 0)
        {
            ListItem item = ddlIndustry.Items.FindByValue(query.IndustryID.Value.ToString());
            if (item != null)
            {
                item.Selected = true;
                if (flag)
                {
                    lblTip.Text = lblTip.Text + ",行业为“" + ProductIndustries.GetProductIndustry(query.IndustryID.Value).IndustryName + "”";
                }
                else
                {
                    lblTip.Text = "行业为“" + ProductIndustries.GetProductIndustry(query.IndustryID.Value).IndustryName + "”";
                    flag = true;
                }
            }
        }

        query.PageSize = this.egvProducts.PageSize;
        query.PageIndex = this.egvProducts.PageIndex;
        query.ProductOrderBy = ProductOrderBy.DataCreated;
        query.SortOrder = SortOrder.Descending;
        List<Product> products = Products.GetProductList(query);
        this.egvProducts.DataSource = products;
        this.egvProducts.DataBind();
    }

    private string destinationURL = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-product";

    void BindLinkButton()
    {
        this.lbNewProduct.PostBackUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-productadd";
        this.lbQuickNew.PostBackUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-productquickadd";
        this.lnkAll.PostBackUrl = destinationURL;
        this.lnkNoPicture.PostBackUrl = destinationURL + "&hp=0";
        this.lnkNoPriced.PostBackUrl = destinationURL + "&pr=0";
        this.lnkPicture.PostBackUrl = destinationURL + "&hp=1";
        this.lnkPriced.PostBackUrl = destinationURL + "&pr=1";
        this.lnkPublished.PostBackUrl = destinationURL + "&pb=1";
        this.lnkUnPublishied.PostBackUrl = destinationURL + "&pb=0";

        this.lnProvider.PostBackUrl = destinationURL + "&p=1";
        this.lnProviderInspect.PostBackUrl = destinationURL + "&p=2";
        this.lnProviderDeny.PostBackUrl = destinationURL + "&p=3";

    }

    protected void lnk_Click(object sender, EventArgs e)
    {
        LinkButton link = sender as LinkButton;
        Response.Redirect(link.PostBackUrl);
    }

    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        string url = destinationURL;
        if (!GlobalSettings.IsNullOrEmpty(this.txtProductName.Text))
            url += "&pn=" + this.txtProductName.Text;
        if (ddlBrands.SelectedValue != "0")
            url += "&bi=" + this.ddlBrands.SelectedValue;
        if (ddlCategory.SelectedValue != "0")
            url += "&ci=" + this.ddlCategory.SelectedValue;
        if (ddlIndustry.SelectedValue != "0")
            url += "&ii=" + this.ddlIndustry.SelectedValue;
        Response.Redirect(url);
    }
    #endregion

    #region Override
    public override void OnPageLoaded()
    {
        AddJavaScriptInclude("scripts/jquery.cookie.js", false, false);
        this.PagePermission = "ProductModule-View";
        this.ShortTitle = "产品管理";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
        base.ExecuteJs("$.fn.cookie({ action: 'set', name: 'hhonline_menu', value: 'item_productmanage' });", false);
        this.AddJavaScriptInclude("scripts/pages/cpproduct.aspx.js", false, false);
    }

    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "ProductModule-View";
        e.CheckPermissionControls.Add("ProductModule-Add", lbNewProduct);
        base.OnPermissionChecking(e);
    }
    #endregion

    #region Event
    protected void egvProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int productID = (int)egvProducts.DataKeys[e.RowIndex].Value;
        DataActionStatus status = Products.Delete(productID);
        switch (status)
        {
            case DataActionStatus.RelationshipExist:
                throw new HHException(ExceptionType.Failed, "此产品信息下存在关联数据，无法直接删除！");
            case DataActionStatus.UnknownFailure:
                throw new HHException(ExceptionType.Failed, "删除产品信息失败，请联系管理人员！");
            default:
            case DataActionStatus.Success:
                BindData();
                break;
        }
    }

    protected void egvProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        egvProducts.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void egvProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //Response.Redirect(GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-productadd&ID=" + egvProducts.DataKeys[e.RowIndex].Value);
        Response.Redirect(GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-productquickadd&ID=" + egvProducts.DataKeys[e.RowIndex].Value);
    }

    protected void egvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Product product = e.Row.DataItem as Product;
            Image productPicture = e.Row.FindControl("ProductPicture") as Image;
            Literal ltComming = e.Row.FindControl("ltComming") as Literal;

            if (productPicture != null)
            {
                productPicture.ImageUrl = product.GetDefaultImageUrl((int)productPicture.Width.Value, (int)productPicture.Height.Value);
            }
            HyperLink hyName = e.Row.FindControl("hlProductName") as HyperLink;
            Company c = Users.GetUser(product.CreateUser).Company;
            if (c != null && c.CompanyID != 0)
            {
                ProductSupply ps = ProductSupplyManager.GetProductSupply(product.ProductID, c.CompanyID);
                ltComming.Text = "[<a class=\"navCompany\" href='javascript:{}' onclick=\"showSupply(" + ps.SupplyID + ")\">"+c.CompanyName+"</a>]&nbsp;&nbsp;";
            }
            else
            {
                ltComming.Text = "华宏";
            }

            if (hyName != null)
            {
                hyName.Text = product.ProductName;
                hyName.NavigateUrl = GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-product&ID=" + GlobalSettings.Encrypt(product.ProductID.ToString());
            }
        }
    }

    protected void egvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = ((LinkButton)e.CommandSource).Parent.Parent.Parent.Parent as GridViewRow;
        if (row != null)
        {
            int index = row.RowIndex;
            object productID = egvProducts.DataKeys[index].Value;
            if (e.CommandName == "ViewPrice")
            {
                Response.Redirect(GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-productprice&ProductID=" + productID);
            }
            else if (e.CommandName == "SetFocus")
            {
                Response.Redirect(GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-productfocusadd&ProductID=" + productID);
            }
        }
    }
    #endregion
}
