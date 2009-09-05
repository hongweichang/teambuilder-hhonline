using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Shops;
using HHOnline.Framework;

public partial class Pages_Product_Category : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void dlProduct_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Product product = e.Item.DataItem as Product;
            Image productPicture = e.Item.FindControl("imgProduct") as Image;
            if (productPicture != null)
            {
                productPicture.ImageUrl = GlobalSettings.RelativeWebRoot+product.GetDefaultImageUrl(100, 100);
            }
            Literal ltPrice = e.Item.FindControl("ltPrice") as Literal;
            if (ltPrice != null)
            {
                decimal? price = ProductPrices.GetPriceDefault( product.ProductID);
                ltPrice.Text = (price == null ? "需询价" : price.ToString());
            }
        }
    }
    #region -BindData-
    void BindData()
    {
        lnkGrid.PostBackUrl = Request.RawUrl;
        lnkList.PostBackUrl = Request.RawUrl;
        lnkGrid.Attributes.Add("rel", "grid");
        lnkList.Attributes.Add("rel", "list");
        string id = Request.QueryString["ID"];
        if (ViewState["ShowBy"] != null)
        {
            switch (ViewState["ShowBy"].ToString())
            {
                case "grid":
                    lnkGrid.CssClass = "showByGrid showBy showByGridActive";
                    lnkList.CssClass = "showByList showBy";
                    break;
                case "list":
                    lnkList.CssClass = "showByList showBy showByListActive";
                    lnkGrid.CssClass = "showByGrid showBy";
                    break;
            }
        }
        else
        {
            lnkGrid.CssClass = "showByGrid showBy showByGridActive";
            lnkList.CssClass = "showByList showBy";
        }
        if (string.IsNullOrEmpty(id))
        {
            clProduct.Visible = true;
            pnlSort.Visible = false;
        }
        else
        {
            clProduct.Visible = false;
            int catId = int.Parse(GlobalSettings.Decrypt(id));
            cnProduct.CategoryID = catId;
            cllProduct.CategoryID = catId;

            ProductQuery query = new ProductQuery();
            query.CategoryID = catId;
            query.ProductOrderBy = ProductOrderBy.DataCreated;
            query.SortOrder = SortOrder.Descending;
            List<Product> prods = Products.GetProducts(query).Records;
            if (prods == null || prods.Count == 0)
            {
                ltProductList.Text = "没有符合条件的产品存在！";
                return;
            }
            ltProductList.Visible = false;
            if (ViewState["ShowBy"] == null || ViewState["ShowBy"].ToString().Equals("grid"))
            {
                dlProduct2.Visible = false;
                dlProduct.Visible = true;
                cpProduct.DataSource = prods;
                cpProduct.BindToControl = dlProduct;
                dlProduct.DataSource = cpProduct.DataSourcePaged;
                dlProduct.DataBind();
            }
            else
            {
                dlProduct.Visible = false;
                dlProduct2.Visible = true;
                cpProduct.DataSource = prods;
                cpProduct.BindToControl = dlProduct2;
                dlProduct2.DataSource = cpProduct.DataSourcePaged;
                dlProduct2.DataBind();

            }
        }
    }
    #endregion

    #region -Event-
    protected void linkButton_Click(object obj, EventArgs e)
    {
        LinkButton lnk = obj as LinkButton;
        ViewState["ShowBy"] = lnk.Attributes["rel"];
        BindData();
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
    }
    #endregion
}
