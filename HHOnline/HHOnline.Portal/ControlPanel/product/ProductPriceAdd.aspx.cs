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

public partial class ControlPanel_product_ProductPriceAdd : HHPage
{
    OperateType action;
    int priceID = 0;
    int productID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        priceID = Convert.ToInt32(Request.QueryString["ID"]);
        productID = Convert.ToInt32(Request.QueryString["ProductID"]);
        if (priceID != 0)
            action = OperateType.Edit;
        else
            action = OperateType.Add;

        if (!IsPostBack && !IsCallback)
        {
            BindSupplyRegion();
            BindData();
            if (Request.UrlReferrer != null)
            {
                btnPostBack.PostBackUrl = Request.UrlReferrer.ToString();
                btnPostBack.Visible = true;
            }
            btnPost.Visible = true;
        }
    }

    void BindSupplyRegion()
    {
        List<KeyValue> values = Areas.GetValueRange();
        ddlSupplyRegion.DataSource = values;
        ddlSupplyRegion.DataTextField = "Text";
        ddlSupplyRegion.DataValueField = "Name";
        ddlSupplyRegion.DataBind();
        ddlSupplyRegion.Items.Insert(0, new ListItem("全国", "0"));
    }

    void BindData()
    {
        Product product = Products.GetProduct(productID);
        if (product != null)
        {
            this.hyProductName.Text = product.ProductName;
            this.hyProductName.NavigateUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-product&&di=" + productID;
        }
        else
        {
            throw new HHException(ExceptionType.ProductNotFound, "未找到商品信息！");
        }
        ProductPrice price = ProductPrices.GetPrice(priceID);
        if (price == null)
            price = new ProductPrice();
        txtApplyTaxRate.Text = price.ApplyTaxRate.ToString();
        txtPriceFloor.Text = price.PriceFloor.HasValue ? price.PriceFloor.Value.ToString() : string.Empty;
        txtPriceGradeA.Text = price.PriceGradeA.HasValue ? price.PriceGradeA.Value.ToString() : string.Empty;
        txtPriceGradeB.Text = price.PriceGradeB.HasValue ? price.PriceGradeA.Value.ToString() : string.Empty;
        txtPriceGradeC.Text = price.PriceGradeC.HasValue ? price.PriceGradeA.Value.ToString() : string.Empty;
        txtPriceGradeD.Text = price.PriceGradeD.HasValue ? price.PriceGradeA.Value.ToString() : string.Empty;
        txtPriceGradeE.Text = price.PriceGradeE.HasValue ? price.PriceGradeA.Value.ToString() : string.Empty;
        txtPriceMarket.Text = price.PriceMarket.HasValue ? price.PriceMarket.Value.ToString() : string.Empty;
        txtPricePromotion.Text = price.PricePromotion.HasValue ? price.PricePromotion.Value.ToString() : string.Empty;
        txtPricePurchase.Text = price.PricePurchase.HasValue ? price.PricePurchase.Value.ToString() : string.Empty;
        txtQuoteEnd.Text = price.QuoteEnd.ToString("yyyy年MM月dd日");
        txtQuoteFrom.Text = price.QuoteFrom.ToString("yyyy年MM月dd日");
        txtQuoteMOQ.Text = price.QuoteMOQ.HasValue ? price.QuoteMOQ.Value.ToString() : string.Empty;
        txtQuoteRenewal.Text = price.QuoteRenewal.ToString();
        ddlSupplyRegion.SelectedValue = price.SupplyRegion.ToString();
        piDeliverySpan.DateSpanValue = price.DeliverySpan;
        piFreight.SelectedValue = price.IncludeFreight;
        piTax.SelectedValue = price.IncludeTax;
        piWarrantySpan.DateSpanValue = price.WarrantySpan;
        csPrice.SelectedValue = price.SupplyStatus;
    }

    public void btnPost_Click(object sender, EventArgs e)
    {
        ProductPrice price = null;
        if (action == OperateType.Add)
        {
            price = new ProductPrice();
            price.ProductID = productID;
        }
        else
        {
            price = ProductPrices.GetPrice(priceID);
        }
        price.ApplyTaxRate = Convert.ToDecimal(txtApplyTaxRate.Text);
        if (!string.IsNullOrEmpty(txtPriceFloor.Text))
            price.PriceFloor = Convert.ToDecimal(txtPriceFloor.Text);
        else
            price.PriceFloor = null;

        if (!string.IsNullOrEmpty(txtPriceFloor.Text))
            price.PriceFloor = Convert.ToDecimal(txtPriceFloor.Text);
        else
            price.PriceFloor = null;

        if (!string.IsNullOrEmpty(txtPriceGradeA.Text))
            price.PriceGradeA = Convert.ToDecimal(txtPriceGradeA.Text);
        else
            price.PriceGradeA = null;

        if (!string.IsNullOrEmpty(txtPriceGradeB.Text))
            price.PriceGradeB = Convert.ToDecimal(txtPriceGradeB.Text);
        else
            price.PriceGradeB = null;

        if (!string.IsNullOrEmpty(txtPriceGradeC.Text))
            price.PriceGradeC = Convert.ToDecimal(txtPriceGradeC.Text);
        else
            price.PriceGradeC = null;

        if (!string.IsNullOrEmpty(txtPriceGradeD.Text))
            price.PriceGradeD = Convert.ToDecimal(txtPriceGradeD.Text);
        else
            price.PriceGradeD = null;

        if (!string.IsNullOrEmpty(txtPriceGradeE.Text))
            price.PriceGradeE = Convert.ToDecimal(txtPriceGradeE.Text);
        else
            price.PriceGradeE = null;

        if (!string.IsNullOrEmpty(txtPriceMarket.Text))
            price.PriceMarket = Convert.ToDecimal(txtPriceMarket.Text);
        else
            price.PriceMarket = null;

        if (!string.IsNullOrEmpty(txtPricePromotion.Text))
            price.PricePromotion = Convert.ToDecimal(txtPricePromotion.Text);
        else
            price.PricePromotion = null;

        if (!string.IsNullOrEmpty(txtPricePurchase.Text))
            price.PricePurchase = Convert.ToDecimal(txtPricePurchase.Text);
        else
            price.PricePurchase = null;

        price.QuoteEnd = Convert.ToDateTime(txtQuoteEnd.Text);
        price.QuoteFrom = Convert.ToDateTime(txtQuoteFrom.Text);

        if (!string.IsNullOrEmpty(txtQuoteMOQ.Text))
            price.QuoteMOQ = Convert.ToInt32(txtQuoteMOQ.Text);
        else
            price.QuoteMOQ = null;

        price.QuoteRenewal = Convert.ToInt32(txtQuoteRenewal.Text);
        price.DeliverySpan = piDeliverySpan.DateSpanValue;
        price.IncludeFreight = piFreight.SelectedValue;
        price.IncludeTax = piTax.SelectedValue;
        price.WarrantySpan = piWarrantySpan.DateSpanValue;
        price.SupplyStatus = csPrice.SelectedValue;
        price.SupplyRegion = Convert.ToInt32(ddlSupplyRegion.SelectedValue);
        DataActionStatus status;
        if (action == OperateType.Add)
        {
            status = ProductPrices.Create(price);
            switch (status)
            {
                case DataActionStatus.DuplicateName:
                    mbMessage.ShowMsg("新增行业信息失败，存在同名行业信息！", Color.Red);
                    break;
                case DataActionStatus.UnknownFailure:
                    mbMessage.ShowMsg("新增行业信息失败，请联系管理员！", Color.Red);
                    break;
                case DataActionStatus.Success:
                default:
                    mbMessage.ShowMsg("新增行业信息成功，可继续填写新行业信息，若完成请返回！", Color.Navy);
                    break;
            }
        }
        else
        {
            status = ProductPrices.Update(price);
            switch (status)
            {
                case DataActionStatus.DuplicateName:
                    mbMessage.ShowMsg("修改行业信息失败，存在同名行业信息！", Color.Red);
                    break;
                case DataActionStatus.UnknownFailure:
                    mbMessage.ShowMsg("修改行业信息失败，请联系管理员！", Color.Red);
                    break;
                case DataActionStatus.Success:
                default:
                    mbMessage.ShowMsg("修改行业信息成功，可继续修改行业信息，若完成请返回！", Color.Navy);
                    break;
            }
        }
        if (status == DataActionStatus.Success)
            BindData();
    }

    public override void OnPageLoaded()
    {
        if (action == OperateType.Add)
            this.ShortTitle = "产品报价";
        else
            this.ShortTitle = "编辑价格";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
        this.PageInfoType = InfoType.PopWinInfo;
        AddJavaScriptInclude("scripts/jquery.datepick.js", false, false);
    }

    protected override void OnPagePermissionChecking()
    {
        if (action == OperateType.Add)
            this.PagePermission = "ProductModule-Add";
        else
            this.PagePermission = "ProductModule-Edit";
        base.OnPagePermissionChecking();
    }
}
