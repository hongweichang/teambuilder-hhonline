using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Shops;
using HHOnline.Framework;

public partial class Pages_ShopCart_CartItems : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindCartItems();
    }
    void BindCartItems()
    {
        string userID = Profile.UserName;
        if (!Profile.IsAnonymous) userID = Profile.AccountInfo.UserID.ToString();
        List<Shopping> shops = Shoppings.ShoppingLoad(userID);
        egvShoppings.DataSource = shops;
        egvShoppings.DataBind();

        int quantities = 0;
        
        foreach (Shopping s in shops)
        {
            quantities += s.Quantity;
        }
        ltItemsAmount.Text = "共【" + shops.Count + "】件产品，合计【" + quantities + "】小件。";
    }
    string productFormat = "<div class=\"cartitem_p\" style=\"background:url({3}) no-repeat 2px 2px;\"><a class=\"cart_name\" href=\"" + 
                            GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-product&ID={0}\" title=\"{2}\" target=\"_blank\">{1}</a></div>";
    protected void egvShoppings_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Literal ltPN = e.Row.FindControl("ltProductName") as Literal;
            Literal ltMN = e.Row.FindControl("ltModelName") as Literal;
            Literal ltPC = e.Row.FindControl("ltPrice") as Literal;
            
            Shopping shop = (Shopping)e.Row.DataItem;
            Product p = Products.GetProduct(shop.ProductID);
            ltPN.Text = string.Format(productFormat,
                                        GlobalSettings.Encrypt(p.ProductID.ToString()),
                                        p.ProductName,
                                        p.ProductAbstract,
                                        p.GetDefaultImageUrl(50, 50));
            decimal? price1 = null;
            decimal? price2 = null;
            if (Profile.IsAnonymous)
            {
                price1 = ProductPrices.GetPriceDefault(p.ProductID);
            }
            else
            {
                price1 = ProductPrices.GetPriceMarket(Profile.AccountInfo.UserID, p.ProductID);
                price2 = ProductPrices.GetPriceMember(Profile.AccountInfo.UserID, p.ProductID);
            }
            ltPC.Text = GlobalSettings.GetPrice(price1, price2);
            if (shop.ModelID != 0)
            {
                ProductModel pm = ProductModels.GetModel(shop.ModelID);
                ltMN.Text = pm.ModelName;
            }
            else
            {
                ltMN.Text = "——";
            }
        }
    }
    protected void egvShoppings_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        egvShoppings.EditIndex = -1;
        BindCartItems();
    }

    protected void egvShoppings_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow gvr = egvShoppings.Rows[e.RowIndex];
        int k = int.Parse(egvShoppings.DataKeys[e.RowIndex].Value.ToString());
        try
        {
            int amount = int.Parse((gvr.FindControl("txtAmount") as TextBox).Text.Trim());
            Shopping shop = new Shopping();
            shop.UpdateTime = DateTime.Now;
            shop.ShoppingID = k;
            shop.Quantity = amount;
            Shoppings.ShoppingUpdate(shop);
        }
        catch (Exception ex) { throw new HHException(ExceptionType.Failed, ex.Message); }
        egvShoppings.EditIndex = -1;
        BindCartItems();
    }

    protected void egvShoppings_RowEditing(object sender, GridViewEditEventArgs e)
    {
        egvShoppings.EditIndex = e.NewEditIndex;
        BindCartItems();
        SetValidator(true, true, 5);
    }

    protected void egvShoppings_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Shoppings.ShoppingDelete(int.Parse(egvShoppings.DataKeys[e.RowIndex].Value.ToString()));
        BindCartItems();
    }
    protected void egvShoppings_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        egvShoppings.PageIndex = e.NewPageIndex;
        BindCartItems();
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "我的购物车";
        this.SetTitle();
    }
}
