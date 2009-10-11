using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Shops;
using HHOnline.Framework;

public partial class Pages_Profiles_AddCartSuccess : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindAmount();
    }
    void BindAmount()
    { 
        string userID=string.Empty;
        if (Profile.IsAnonymous)
        {
            userID = Profile.UserName;
        }
        else
        {
            userID = Profile.AccountInfo.UserID.ToString();
        }
        List<Shopping> shops = Shoppings.ShoppingLoad(userID);
        btnSeeShopCart.Text = "查看购物车(" + shops.Count + "件)";
        btnSeeShopCart.Attributes.Add("onclick", "parent.window.location.href='" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?shopcart-cartitems'");
    }
}
