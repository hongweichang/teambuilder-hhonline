using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Controls;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using HHOnline.Shops;
using System.Text;

public partial class Pages_Product_GuidByLetter : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }

    }
    void BindData()
    {
        LettersType lt = LettersType.Category;
        NameValueCollection req = Request.QueryString;
        try { lt = (LettersType)(int.Parse(req["t"])); }
        catch { }

        string w = "a";
        if (!string.IsNullOrEmpty(req["w"]))
        {
            w = req["w"];
            if (char.IsLetter(w, 0))
            {
                w = w[0].ToString();
            }
            else
                w = "a";
        }
        w = w.ToUpper();
        llCategory.LetterType = lt;
        llCategory.FirstLetter = w;
        ltLetterType.Text = "按首字母<span class=\"needed\">\"" + w + "\"</span>检索【" + GetDesc(lt) + "】";
    }
    string GetDesc(LettersType lt)
    {
        switch (lt)
        {
            case LettersType.Category:
                return "产品类别";
            case LettersType.Brand:
                return "产品品牌";
            case LettersType.Industry:
                return "产品行业";
        }
        return "——";
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
                    url += "&" + k + "=" + Request.QueryString[k];
                }
            }
            return url;
        }
    }
    #endregion

    public override void OnPageLoaded()
    {
        string letter = this.llCategory.FirstLetter, title = string.Empty;

        StringBuilder sbKeywords = new StringBuilder();
        switch (this.llCategory.LetterType)
        {
            case LettersType.Category:
                List<ProductCategory> listCategory = ProductCategories.GetCategoreisByPY(letter);
                if (null == listCategory || 0 == listCategory.Count)
                { }
                else
                {
                    foreach (ProductCategory item in listCategory)
                    {
                        sbKeywords.AppendFormat("{0},", item.CategoryName);
                    }
                }
                title = "分类";
                break;
            case LettersType.Brand:
                List<ProductBrand> listBrand = ProductBrands.GetBrandsByPY(letter);
                if (null == listBrand || 0 == listBrand.Count)
                { }
                else
                {
                    foreach (ProductBrand item in listBrand)
                    {
                        sbKeywords.AppendFormat("{0},", item.BrandName);
                    }
                }
                title = "品牌";
                break;
            case LettersType.Industry:
                List<ProductIndustry> listIndustry = ProductIndustries.GetIndustriesByPY(letter);
                if (null == listIndustry || 0 == listIndustry.Count)
                { }
                else
                {
                    foreach (ProductIndustry item in listIndustry)
                    {
                        sbKeywords.AppendFormat("{0},", item.IndustryName);
                    }
                }
                title = "行业";
                break;
        }

        string keywords = sbKeywords.ToString().TrimEnd(',');

        if (string.IsNullOrEmpty(keywords))
        {
            //this.AddKeywords(keywords);
            this.AddDescription(string.Format("按照首字母进行{0}检索，首字母是{1}", title, letter.ToUpper()));

            this.ShortTitle = string.Format("首字母{0}检索{1}", title, letter.ToUpper());
        }
        else
        {
            this.AddKeywords(keywords);
            this.AddDescription(string.Format("按照首字母进行{0}检索，首字母是{1}，符合条件的{0}列表如下: {2}", title, letter.ToUpper(), keywords));

            this.ShortTitle = string.Format("首字母{0}检索{1} - {2}", title, letter.ToUpper(), keywords);
        }
        this.SetTitle();
    }
}
