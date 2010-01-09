using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using HHOnline.Framework;
using System.Collections.Generic;
using HHOnline.Shops;
using HHOnline.News.Components;
using HHOnline.News.Services;

public partial class Pages_Common_SiteMap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDataBind();
        }
    }

    private void InitialDataBind()
    {
        SiteSettings ss = HHContext.Current.SiteSettings;
        ltCopyRight.Text = ss.Copyright;

        StringBuilder sbItems = new StringBuilder();
        List<ProductBrand> brands = ProductBrands.GetProductBrands();
        foreach (ProductBrand item in brands)
        {
            sbItems.AppendFormat("<li><a href=\"http://www.ehuaho.com/pages/view.aspx?product-brand&ID={0}\" target=\"_blank\" title=\"{2}\">{1}</a></li>",
                GlobalSettings.Encrypt(item.BrandID.ToString()),
                GlobalSettings.SubString(item.BrandName, 10),
                item.BrandName);
        }
        ltBrand.Text = sbItems.ToString();

        List<ProductIndustry> inds = ProductIndustries.GetProductIndustries();
        sbItems.Remove(0, sbItems.Length);
        foreach (ProductIndustry item in inds)
        {
            sbItems.AppendFormat("<li><a href=\"http://www.ehuaho.com/pages/view.aspx?product-industry&ID={0}\" target=\"_blank\" title=\"{2}\">{1}</a></li>",
                GlobalSettings.Encrypt(item.IndustryID.ToString()),
                GlobalSettings.SubString(item.IndustryName, 10),
                item.IndustryName);
        }
        ltIndustry.Text = sbItems.ToString();

        ProductQuery q = new ProductQuery();
        q.PageIndex = 0;
        q.PageSize = int.MaxValue;
        q.HasPublished = true;
        List<Product> ps = Products.GetProductList(q);
        sbItems.Remove(0, sbItems.Length);
        foreach (Product item in ps)
        {
            sbItems.AppendFormat("<li><a href=\"http://www.ehuaho.com/pages/view.aspx?product-product&ID={0}\" target=\"_blank\" title=\"{2}\">{1}</a></li>",
                GlobalSettings.Encrypt(item.ProductID.ToString()),
                GlobalSettings.SubString(item.ProductName, 10),
                item.ProductName);
        }
        ltProduct.Text = sbItems.ToString();

        List<ProductCategory> cats = ProductCategories.GetCategories();
        sbItems.Remove(0, sbItems.Length);
        foreach (ProductCategory item in cats)
        {
            sbItems.AppendFormat("<li><a href=\"http://www.ehuaho.com/pages/view.aspx?product-category&ID={0}\" target=\"_blank\" title=\"{2}\">{1}</a></li>",
                GlobalSettings.Encrypt(item.CategoryID.ToString()),
                GlobalSettings.SubString(item.CategoryName, 10),
                item.CategoryName);
        }
        ltCategory.Text = sbItems.ToString();


        List<Article> ars = ArticleManager.GetAllArticles();
        sbItems.Remove(0, sbItems.Length);
        foreach (Article item in ars)
        {
            sbItems.AppendFormat("<li><a href=\"http://www.ehuaho.com/pages/view.aspx?news-newsdetail&ID={0}\" target=\"_blank\" title=\"{2}\">{1}</a></li>",
                GlobalSettings.Encrypt(item.ID.ToString()),
                GlobalSettings.SubString(item.Title, 10),
                item.Title);
        }
        ltNews.Text = sbItems.ToString();
        //ltCategory; ltIndustry; ltNews; ltProduct;
    }
}
