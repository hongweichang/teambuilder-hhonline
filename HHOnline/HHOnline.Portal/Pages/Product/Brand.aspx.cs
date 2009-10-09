using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;
using HHOnline.Shops;

public partial class Pages_Product_Brand : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (brands == null)
        {
            brands = ProductBrands.GetProductBrands();
        }
        if (!IsPostBack)
        {
            BindData();
        }

        base.ExecuteJs("var _nativeUrl = '" + GetUrl() + "'", true);
    }
    List<ProductBrand> brands = null;

    #region -Common-

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
                    url += "&" + k + "=" + Request.QueryString[k];
                }
            }
            return url;
        }
    }
    #endregion

    #region -BindData-
    void BindData()
    {
        string id = Request.QueryString["ID"];
        if (string.IsNullOrEmpty(id))
        {
            hpvlList.Visible = true;
            ucpProducts.Visible = false;
        }
        else
        {
            #region -Bind Data(Private)-
            hpvlList.Visible = false;
            int BrandID = int.Parse(GlobalSettings.Decrypt(id));
            vnProduct.BrandID = BrandID;
            hpblList.BrandID = BrandID;
            #endregion

            #region -BindData-
            ProductQuery query = new ProductQuery();
            query.BrandID = BrandID;
            ucpProducts.Query = query;
            #endregion
        }
    }
    #endregion

    #region -Override-
    public override void OnPageLoaded()
    {
        string id = Request.QueryString["ID"];
        if (!string.IsNullOrEmpty(id))
        {
            int bId = int.Parse(GlobalSettings.Decrypt(id));
            ProductBrand pb = ProductBrands.GetProductBrand(bId);
            this.ShortTitle = pb.BrandName;
        }
        else
        {
            this.ShortTitle = "所有品牌";
        }
        this.SetTitle();
        this.AddJavaScriptInclude("scripts/pages/sortby.aspx.js", false, false);
    }
    #endregion
}
