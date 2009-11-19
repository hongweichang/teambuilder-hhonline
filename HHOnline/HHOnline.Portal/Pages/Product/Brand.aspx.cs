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
        if (!IsPostBack)
        {
            BindData();
        }

        base.ExecuteJs("var _nativeUrl = '" + GetUrl() + "'", true);
    }


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
                    url += "&" + k + "=" + HttpUtility.UrlEncode(Request.QueryString[k]);
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
            this.ShortTitle = "所有品牌";
        }
        else
        {
            #region -Bind Data(Private)-
            hpvlList.Visible = false;
            int BrandID = int.Parse(GlobalSettings.Decrypt(id));
            vnProduct.BrandID = BrandID;
            hpblList.BrandID = BrandID;

            ProductBrand pb = ProductBrands.GetProductBrand(BrandID);
            if (pb == null)
                this.ShortTitle = pb.BrandName;
            else
                this.ShortTitle = "品牌";
            this.SetTitle();
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
        string brandName = string.Empty, brandGroup = string.Empty;
        string id = Request.QueryString["ID"];
        if (!string.IsNullOrEmpty(id))
        {
            int brandId = int.Parse(GlobalSettings.Decrypt(id));
            ProductBrand pb = ProductBrands.GetProductBrand(brandId);
            brandName = pb.BrandName;
            brandGroup = pb.BrandGroup;
        }
        else
        {
            brandName = "所有品牌";
        }

        if (string.IsNullOrEmpty(brandGroup))
        {
            this.AddKeywords(brandName);
            this.AddDescription("分组显示所有品牌列表，选择品牌导航到对应品牌的产品列表。");
            this.ShortTitle = brandName;
        }
        else
        {
             this.AddKeywords(string.Format("{0},{1}", brandName, brandGroup));
             this.AddDescription(string.Format("显示{0}品牌的产品列表。{1}", brandName, string.Format(" 关键字: {0},{1}", brandName, brandGroup)));
           this.ShortTitle = brandName + " - " + brandGroup;
        }
        this.SetTitle();


        this.AddJavaScriptInclude("scripts/pages/sortby.aspx.js", false, false);
    }
    #endregion
}
