using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Shops;

public partial class Pages_Product_ProductFocus : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProductQuery q = new ProductQuery();
            FocusType ft = FocusType.Promotion;
            try { ft = (FocusType)int.Parse(Request.QueryString["t"]); }
            catch { }
            q.FocusType = ft;
            switch (ft)
            {
                case FocusType.New:
                    this.ShortTitle = "新品上架";
                    break;
                case FocusType.Hot:
                    this.ShortTitle = "热卖上架";
                    break;
                case FocusType.Recommend:
                    this.ShortTitle = "推荐产品";
                    break;
                case FocusType.Promotion:
                    this.ShortTitle = "促销产品";
                    break;
            }
            this.SetTitle();
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
                    url += "&" + k + "=" + Request.QueryString[k];
                }
            }
            return url;
        }
    }
    #region -Override-
    public override void OnPageLoaded()
    {
        this.AddJavaScriptInclude("scripts/pages/sortby.aspx.js", false, false);
    }
    #endregion
}

