using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Framework;
using HHOnline.Shops;

public partial class ControlPanel_product_ProductFocusAdd : HHPage
{
    int focusID = 0;
    int productID = 0;
    OperateType action;

    protected void Page_Load(object sender, EventArgs e)
    {
        focusID = Convert.ToInt32(Request.QueryString["FocusID"]);
        productID = Convert.ToInt32(Request.QueryString["ProductID"]);
        if (focusID != 0)
        {
            action = OperateType.Edit;
        }
        else
        {
            action = OperateType.Add;
        }
        if (!IsPostBack && !IsCallback)
        {
            BindData();
        }
    }

    void BindData()
    {
        ProductFocus productFocus = ProductFocusManager.Get(focusID);
        if (productFocus == null)
            productFocus = new ProductFocus();
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
        if (productFocus.FocusEnd != DateTime.MinValue)
            this.txtFocusEnd.Text = productFocus.FocusEnd.ToString("yyyy年MM月dd日");
        if (productFocus.FocusFrom != DateTime.MinValue)
            this.txtFocusFrom.Text = productFocus.FocusFrom.ToString("yyyy年MM月dd日");
        ListItem item = this.ddlFocusType.Items.FindByValue(productFocus.FocusType.ToString());
        if (item != null)
            item.Selected = true;
        this.txtRemark.Text = productFocus.FocusMemo;
        this.txtDisplayOrder.Text = productFocus.DisplayOrder.ToString();
        this.csFocus.SelectedValue = productFocus.FocusStatus;
    }

    public void btnPost_Click(object sender, EventArgs e)
    {
        ProductFocus productFocus = null;
        if (action == OperateType.Add)
        {
            productFocus = new ProductFocus();
            productFocus.ProductID = productID;
        }
        else
            productFocus = ProductFocusManager.Get(focusID);

        productFocus.FocusType = ddlFocusType.SelectedValue;
        productFocus.FocusStatus = csFocus.SelectedValue;
        productFocus.FocusMemo = txtRemark.Text.Trim();
        productFocus.FocusFrom = Convert.ToDateTime(txtFocusFrom.Text);
        productFocus.FocusEnd = Convert.ToDateTime(txtFocusEnd.Text);
        productFocus.DisplayOrder = Convert.ToInt32(txtDisplayOrder.Text);
        DataActionStatus status;
        if (action == OperateType.Add)
        {
            status = ProductFocusManager.Create(productFocus);

        }
        else
        {
            status = ProductFocusManager.Update(productFocus);
        }
        switch (status)
        {
            case DataActionStatus.Success:

                mbMessage.ShowMsg("产品分类信息设定成功，可继续设定产品分类信息，若完成请返回！", Color.Navy);
                break;
            case DataActionStatus.UnknownFailure:
            default:
                mbMessage.ShowMsg("产品分类信息设定失败，请联系管理员！", Color.Red);
                break;
        }
        if (status == DataActionStatus.Success)
            BindData();
    }
}
