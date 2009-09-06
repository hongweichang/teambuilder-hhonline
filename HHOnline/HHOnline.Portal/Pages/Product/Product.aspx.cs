using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Shops;

public partial class Pages_Product_Product :HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public override void OnPageLoaded()
    {
        int pid=int.Parse(Request.QueryString["ID"]);
        Product p = Products.GetProduct(pid);
        this.ShortTitle = p.ProductName;
        SetTitle();
    }
}
