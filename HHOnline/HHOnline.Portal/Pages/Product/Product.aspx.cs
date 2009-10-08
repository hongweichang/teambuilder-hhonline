using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Shops;
using HHOnline.Framework;

public partial class Pages_Product_Product :HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindPicture();
        if (!IsPostBack)
        {
            BindProduct();
        }
    }
    Product p = null;
    void BindPicture()
    {
        if(p==null)
        {
            p=GetProduct();
        }
        List<ProductPicture> pics = ProductPictures.GetPictures(p.ProductID);
        string picStr = "''";
        if (pics != null && pics.Count > 0)
        {
            picStr = Newtonsoft.Json.JavaScriptConvert.SerializeObject(pics);
        }
        base.ExecuteJs("var pictures=" + picStr + ";var _infos={l:" + User.Identity.IsAuthenticated.ToString().ToLower() + ",d:" + p.ProductID + "}", true);
    }
    void BindProduct()
    {
        if (p == null)
        {
            p = GetProduct();
        }
        ltProductName.Text = p.ProductName;
        ltDescription.Text = "最后更新：" + p.UpdateTime.ToShortDateString() + "  关键字：" + p.ProductKeywords;
        ltProductCode.Text = GetString(p.ProductCode);
        ltProductAbstract.Text = GetString(p.ProductAbstract);
        BindCategory(p.ProductID);
        BindIndustry(p.ProductID);
        BindBrand(p);
        BindPrice(p.ProductID);
        ltDescription1.Text = p.ProductContent;
        BindProperty(p.ProductID);
    }
    void BindCategory(int pId)
    {
       List<ProductCategory> cat = ProductCategories.GetCategoreisByProductID(pId);
       if (cat.Count == 0)
       {
           ltCategory.Text = "——";
       }
       else
       {
           string cats = string.Empty;
           foreach (ProductCategory pc in cat)
           {
               cats += "<a target=\"_blank\" href=\"" + GlobalSettings.RelativeWebRoot +
                            "pages/view.aspx?product-category&ID=" + GlobalSettings.Encrypt(pc.CategoryID.ToString()) + "\">" + pc.CategoryName + "</a>";
           }
           ltCategory.Text = cats;
       }
    }
    void BindIndustry(int pId)
    {
        List<ProductIndustry> pi = ProductIndustries.GetIndustriesByProductID(pId);
        if (pi.Count == 0)
        {
            ltIndustry.Text = "——";
        }
        else
        {
            string pis = string.Empty;
            foreach (ProductIndustry p in pi)
            {
                pis += "<a target=\"_blank\" href=\"" + GlobalSettings.RelativeWebRoot +
                            "pages/view.aspx?product-industry&ID=" + GlobalSettings.Encrypt(p.IndustryID.ToString()) + "\">" + p.IndustryName + "</a>";
            }
            ltIndustry.Text = pis;
        }
    }
    void BindBrand(Product p)
    {
        if (p.BrandID==0||string.IsNullOrEmpty(p.BrandName))
        {
            ltBrand.Text = "——";
        }
        else
        {
            ltBrand.Text = "<a target=\"_blank\"  href=\"" + GlobalSettings.RelativeWebRoot +
                                    "pages/view.aspx?product-brand&ID=" + GlobalSettings.Encrypt(p.BrandID.ToString()) + "\">" + p.BrandName + "</a>"; ;
        }
    }
    void BindPrice(int pId)
    {
        decimal? price = null;
        string priceText = string.Empty;
        if (!User.Identity.IsAuthenticated)
        {
            price = ProductPrices.GetPriceDefault(pId);
            priceText = (price == null ? "需询价" : price.Value.ToString("c"));
        }
        else
        {
            price = ProductPrices.GetPriceMarket(Profile.AccountInfo.UserID, pId);
            decimal? price1 = ProductPrices.GetPriceMember(Profile.AccountInfo.UserID, pId);
            if (price == null)
            {
                priceText = (price1 == null ? "需询价" : price1.Value.ToString("c"));
            }
            else
            {
                if (price1 == null)
                {
                    priceText = (price == null ? "需询价" : price.Value.ToString("c"));
                }
                else
                {
                    if (price == price1)
                    {
                        priceText = price.Value.ToString("c");
                    }
                    else
                    {
                        priceText = "<s>" + price.Value.ToString("c") + "</s><br />" + price1.Value.ToString("c");
                    }
                }
            }
        }
        ltPrice.Text = priceText;
    }
    void BindProperty(int pId)
    {
        List<ProductProperty> props = ProductProperties.GetPropertiesByProductID(pId);
        if (props == null || props.Count == 0)
        {
            msgBox.ShowMsg("此产品没有设置任何属性！", System.Drawing.Color.Gray);
        }
        else
        {
            msgBox.HideMsg();
            rpProperties.DataSource = props;
            rpProperties.DataBind();
        }
    }
    string GetString(string str)
    {
        if (string.IsNullOrEmpty(str)) return "——";
        else return str;
    }
    Product GetProduct()
    {
        int pid = int.Parse(GlobalSettings.Decrypt(Request.QueryString["ID"]));
        return Products.GetProduct(pid);
    }
    public override void OnPageLoaded()
    {
        p = GetProduct();
        this.ShortTitle = p.ProductName;
        SetTitle();
        AddJavaScriptInclude("scripts/jquery.accordion.js", true, false);
        AddJavaScriptInclude("scripts/pages/product.aspx.js", true, false);
    }
}
