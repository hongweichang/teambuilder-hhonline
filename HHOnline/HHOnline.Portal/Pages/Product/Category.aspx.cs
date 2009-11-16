using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Shops;
using HHOnline.Framework;
using SD = System.Drawing;
using System.Collections.Specialized;

public partial class Pages_Product_Category : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }

        base.ExecuteJs("var _nativeUrl = '" + GetUrl() + "'", true);
    }

    #region -BindData-
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
    void BindData()
    {
        string id = Request.QueryString["ID"];
        if (string.IsNullOrEmpty(id))
        {
            clProduct.Visible = true;
            ucpProducts.Visible = false;
        }
        else
        {
            #region -Bind Datas(Private)-
            clProduct.Visible = false;
            int catId = int.Parse(GlobalSettings.Decrypt(id));
            cnProduct.CategoryID = catId;
            cllProduct.CategoryID = catId;
            cslProduct.CategoryID = catId;
            #endregion

            #region -BindData-
            ProductQuery query = new ProductQuery();
            query.CategoryID = catId;
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
            int catId = int.Parse(GlobalSettings.Decrypt(id));
            ProductCategory pc = ProductCategories.GetCategory(catId);
            this.ShortTitle = pc.CategoryName;
        }
        else
        {
            this.ShortTitle = "所有分类";
        }
        this.SetTitle();
        this.AddJavaScriptInclude("scripts/pages/sortby.aspx.js", false, false);
    }
    #endregion
}
