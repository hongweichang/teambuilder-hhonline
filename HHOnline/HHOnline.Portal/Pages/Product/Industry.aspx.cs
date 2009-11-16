using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;
using HHOnline.Shops;

public partial class Pages_Product_Industry : HHPage
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
            hpilProduct.Visible = true;
            ucpProducts.Visible = false;
        }
        else
        {
            hpilProduct.Visible = false;
            int pid = int.Parse(GlobalSettings.Decrypt(id));
            inProduct.IndustryID = pid;
            illProduct.IndustryID = pid;
            islProduct.IndustryID = pid;

            #region -BindData-
            ProductQuery query = new ProductQuery();
            query.IndustryID = pid;
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
            int pid = int.Parse(GlobalSettings.Decrypt(id));
            ProductIndustry pi = ProductIndustries.GetProductIndustry(pid);
            this.ShortTitle = pi.IndustryName;
        }
        else
        {
            this.ShortTitle = "所有行业";
        }
        this.SetTitle();
        this.AddJavaScriptInclude("scripts/pages/sortby.aspx.js", false, false);
    }
    #endregion
}

