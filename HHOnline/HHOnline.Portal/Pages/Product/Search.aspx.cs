using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Framework.Web;
using HHOnline.SearchBarrel;
using HHOnline.Shops;

public partial class Search : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindSearchs();
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
                    url += "&" + k + "=" + Request.QueryString[k];
                }
            }
            return url;
        }
    }

    #endregion

    void BindSearchs()
    {
        string query = Request.QueryString["w"];
        query = HttpUtility.UrlDecode(query);
        TextBox txt = sMain.FindControl("txtSearch") as TextBox;
        if (txt != null)
        {
            if (!string.IsNullOrEmpty(query))
                txt.Text = query;
        }

        ProductQuery pq = new ProductQuery();
        pq.ProductNameFilter = query;
        pq.PageSize = Int32.MaxValue;
        ucpProducts.IsSearch = true;
        ucpProducts.Query = pq;

    }

    #region -Override-
    public override void OnPageLoaded()
    {
        string query = Request.QueryString["w"];
        this.ShortTitle = "搜索产品";
        if (!string.IsNullOrEmpty(query))
        {
            this.ShortTitle += ": " + HttpUtility.UrlDecode(query);
        }
        this.SetTitle();
        this.AddJavaScriptInclude("scripts/pages/sortby.aspx.js", false, false);
    }
    #endregion
}
