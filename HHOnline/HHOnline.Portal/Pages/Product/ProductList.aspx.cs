using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Shops;
using HHOnline.Framework;

public partial class Pages_Product_ProductList :HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ProductQuery q = new ProductQuery();
            uplProducts.Query = q;
        }

        base.ExecuteJs("var _nativeUrl = '" + GetUrl() + "'", true);
    }

    string GetUrl()
    {
        if (Request.QueryString["sortby"] == null)
        {
            return Request.RawUrl.ToString();
        }
        else
        {
            string url = Request.RawUrl.ToString().Split('&')[0];
            foreach (string k in Request.QueryString.AllKeys)
            {
                if (k != "sortby")
                {
                    url += "&" + k + "=" + HttpUtility.UrlEncode(Request.QueryString[k]);
                }
            }
            return url;
        }
    }
    #region -Override-
    public override void OnPageLoaded()
    {
        this.ShortTitle = "产品列表";
        this.SetTitle();
        this.AddJavaScriptInclude("scripts/pages/sortby.aspx.js", false, false);
    }
    #endregion
}
