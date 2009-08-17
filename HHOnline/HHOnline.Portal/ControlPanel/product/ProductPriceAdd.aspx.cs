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
            BindData();
            if (Request.UrlReferrer != null)
            {
                btnPostBack.PostBackUrl = Request.UrlReferrer.ToString();
                btnPostBack.Visible = true;
            }
            btnPost.Visible = true;
        }
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
    }

    public void btnPost_Click(object sender, EventArgs e)
    {

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
