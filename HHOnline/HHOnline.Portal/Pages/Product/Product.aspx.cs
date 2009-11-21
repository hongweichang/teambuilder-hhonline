using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Shops;
using HHOnline.Framework;
using System.Text;

public partial class Pages_Product_Product : HHPage
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
        if (p == null)
        {
            p = GetProduct();
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
        pllProduct.ProductID = p.ProductID;
        ltProductName.Text = p.ProductName;
        ltDescription.Text = "最后更新：" + p.UpdateTime.ToShortDateString() + "  关键字：" + p.ProductKeywords;
        ltProductCode.Text = GetString(p.ProductCode);
        ltProductAbstract.Text = GetString(p.ProductAbstract);
        BindCategory(p.ProductID);
        BindIndustry(p.ProductID);
        BindBrand(p);
        BindPrice(p.ProductID);

        ltPAbstract.Text = p.ProductAbstract;
        ltProductDetails.Text = p.ProductContent;

        BindProperty(p.ProductID);
        List<ProductModel> models = ProductModels.GetModelsByProductID(p.ProductID);
        if (models.Count == 0)
        {
            rbModel.Visible = false;
            ltModel.Text = "——";
        }
        else
        {
            ListItem li = null;
            foreach (ProductModel m in models)
            {
                li = new ListItem(m.ModelName, m.ModelID.ToString());
                li.Attributes.Add("title", m.ModelDesc);
                rbModel.Items.Add(li);
            }
            rbModel.SelectedIndex = 0;
        }
    }
    List<ProductCategory> cat = null;
    List<ProductCategory> GetCategories(int pId)
    {
        return ProductCategories.GetCategoreisByProductID(pId);
    }
    void BindCategory(int pId)
    {
        if (cat == null)
            cat = GetCategories(pId);
        if (cat.Count == 0)
        {
            ltCategory.Text = "<ul><li><span>——</span></li></ul>";
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");
            foreach (ProductCategory pc in cat)
            {
                sb.Append( "<li><a target=\"_blank\" href=\"" + GlobalSettings.RelativeWebRoot +
                             "pages/view.aspx?product-category&ID=" + GlobalSettings.Encrypt(pc.CategoryID.ToString()) + "\">" + pc.CategoryName + "</a></li>");
            }
            sb.Append("</ul>");
            ltCategory.Text = sb.ToString();
        }
    }
    void BindIndustry(int pId)
    {
        List<ProductIndustry> pi = ProductIndustries.GetIndustriesByProductID(pId);
        if (pi.Count == 0)
        {
            ltIndustry.Text = "<ul><li><span>——</span></li></ul>";
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");
            foreach (ProductIndustry p in pi)
            {
                sb.Append("<li><a target=\"_blank\" href=\"" + GlobalSettings.RelativeWebRoot +
                           "pages/view.aspx?product-industry&ID=" + GlobalSettings.Encrypt(p.IndustryID.ToString()) + "\">" + p.IndustryName + "</a></li>");
            }
            ltIndustry.Text = sb.ToString();
        }
    }
    void BindBrand(Product p)
    {
        if (p.BrandID == 0 || string.IsNullOrEmpty(p.BrandName))
        {
            ltBrand.Text = "无相关品牌";
        }
        else
        {
            ProductBrand pb = ProductBrands.GetProductBrand(p.BrandID);
            ltBrand.Text = "<b>" + pb.BrandName + "</b>  " + pb.BrandContent;
        }
    }
    void BindPrice(int pId)
    {
        decimal? p = null, p1 = null, p2 = null;
        string priceText = string.Empty;
        if (!Context.User.Identity.IsAuthenticated)
        {
            p1 = ProductPrices.GetPriceDefault(pId);
            p2 = ProductPrices.GetPricePromote(0, pId);
            if (p2 != null)
            {
                lblPrice.Text = "促销价";
                ltPrice1.Text = p2.Value.ToString("c");
            }
            else
            {
                ltPrice1.Text = (p1==null?"需询价":p1.Value.ToString("c"));
            }
        }
        else
        {
            p1 = ProductPrices.GetPriceMarket(Profile.AccountInfo.UserID, pId);
            p2 = ProductPrices.GetPriceMember(Profile.AccountInfo.UserID, pId);
            p = ProductPrices.GetPricePromote(Profile.AccountInfo.UserID, pId);
            if (p != null)
            {
                lblPrice.Text = "促销价";
                ltPrice1.Text = p.Value.ToString("c");
            }
            else
            {
                ltPrice1.Text = (p1==null?"需询价":p1.Value.ToString("c"));
            }
            ltPrice2.Text = (p2 == null ? "需询价" : p2.Value.ToString("c"));
        }
    }
    void BindProperty(int pId)
    {
        List<ProductProperty> props = ProductProperties.GetPropertiesByProductID(pId);
        if (props == null || props.Count == 0)
        {
            msgBox.ShowMsg("--", System.Drawing.Color.Gray);
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

        //构建产品的关键字标签
        StringBuilder sb = new StringBuilder();
        //添加产品关键字
        sb.Append(p.ProductKeywords.Replace(';', ','));
        sb.Append(",");
        //添加产品所属的所有分类的关键字
        cat = GetCategories(p.ProductID);
        if (cat != null && cat.Count > 0)
        {
            foreach (ProductCategory pc in cat)
            {
                sb.Append(pc.CategoryName);
                sb.Append(",");
            }
        }
        //添加品牌关键字
        sb.Append(p.BrandName);

        //设置页面关键字标签
        this.AddKeywords(sb.ToString());
        
        //设置产品页面描述信息为产品简述/名称+关键字列表
        this.AddDescription((string.IsNullOrEmpty(p.ProductAbstract) ? p.ProductName : p.ProductAbstract) + " 关键字: " + sb.ToString());
        
        //设置产品页面标题为名称+" - "+关键字组合标题名
        this.ShortTitle = p.ProductName + " - " + sb.ToString();
        SetTitle();

        AddJavaScriptInclude("scripts/jquery.accordion.js", true, false);
        AddJavaScriptInclude("scripts/jquery.lightbox-0.5.js", true, false);
        AddJavaScriptInclude("scripts/pages/product.aspx.js", true, false);
    }
}
