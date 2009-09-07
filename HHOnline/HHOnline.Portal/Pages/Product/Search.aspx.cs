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

    #region -Events-
    protected void dlProduct_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Product product = e.Item.DataItem as Product;
            Image productPicture = e.Item.FindControl("imgProduct") as Image;
            if (productPicture != null)
            {
                productPicture.ImageUrl = GlobalSettings.RelativeWebRoot + product.GetDefaultImageUrl(100, 100);
            }
            Literal ltPrice = e.Item.FindControl("ltPrice") as Literal;
            if (ltPrice != null)
            {
                decimal? price = null;
                if (!User.Identity.IsAuthenticated)
                {
                    price = ProductPrices.GetPriceDefault(product.ProductID);
                }
                else
                {
                    price = ProductPrices.GetPriceDefault(product.ProductID);
                }
                ltPrice.Text = (price == null ? "需询价" : price.Value.ToString("c"));
            }
        }
    }
    #endregion

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
        if (!string.IsNullOrEmpty(query))
        {
            query = HttpUtility.UrlDecode(query);
            TextBox txt = sMain.FindControl("txtSearch") as TextBox;
            if (txt != null)
            {
                txt.Text = query;
            }
            ProductQuery pq = new ProductQuery();
            pq.ProductNameFilter = query;
            pq.PageSize = Int32.MaxValue;
            SearchResultDataSet<Product> products = ProductSearchManager.Search(pq);
            List<Product> ps = products.Records;
            if (ps == null || ps.Count == 0)
            {
                ltSearchDuration.Text = "搜索用时：" + products.SearchDuration.ToString() + "秒";
                msgBox.ShowMsg("没有搜索到相应的产品信息！", System.Drawing.Color.Gray);
            }
            else
            {
                msgBox.HideMsg();
                ltSearchDuration.Text = "搜索用时：" + products.SearchDuration.ToString() + "ms";
                cpProduct.DataSource = ps;
                cpProduct.BindToControl = dlProduct;
                dlProduct.DataSource = cpProduct.DataSourcePaged;
                dlProduct.DataBind();
            }
        }
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
