using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;

public partial class Pages_ShopCart_CartItems : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "我的购物车";
        this.SetTitle();
    }
}
