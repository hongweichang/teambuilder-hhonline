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
        string catName = string.Empty, catRelated = string.Empty;
        StringBuilder sbRelated = new StringBuilder();

        string id = Request.QueryString["ID"];
        if (!string.IsNullOrEmpty(id))
        {
            int catId = int.Parse(GlobalSettings.Decrypt(id));
            ProductCategory pc = ProductCategories.GetCategory(catId);
<<<<<<< .mine            if (pc != null)
                this.ShortTitle = pc.CategoryName;
            else
                this.ShortTitle = "分类信息";
=======            catName = pc.CategoryName;

            //获取子分类
            List<ProductCategory> childCategories = ProductCategories.GetChidCategories(pc.CategoryID);
            if (null == childCategories || 0 == childCategories.Count)
            { }
            else
            {
                foreach (ProductCategory pcChild in childCategories)
                {
                    sbRelated.AppendFormat("{0},", pcChild.CategoryName);
                }
            }
            //获取相关分类
            List<ProductCategory> relateCategories = ProductCategories.GetChidCategories(pc.ParentID);
            if (null == relateCategories || 0 == relateCategories.Count)
            { }
            else
            {
                foreach (ProductCategory pcRelate in relateCategories)
                {
                    sbRelated.AppendFormat("{0},", pcRelate.CategoryName);
                }
            }
            catRelated = sbRelated.ToString().Replace(catName + ",", string.Empty).TrimEnd(',');
>>>>>>> .theirs        }
        else
        {
            List<ProductCategory> relateCategories = ProductCategories.GetCategories();
            if (null == relateCategories || 0 == relateCategories.Count)
            { }
            else
            {
                foreach (ProductCategory pcRelate in relateCategories)
                {
                    if (pcRelate.ParentID > 0) continue;
                    sbRelated.AppendFormat("{0},", pcRelate.CategoryName);
                }
            }
            catRelated = sbRelated.ToString().TrimEnd(',');
        }

        if (string.IsNullOrEmpty(catName))
        {
            catName = "所有分类";
            this.AddKeywords(catName + "," + catRelated);
            this.AddDescription("显示所有一级和二级产品分类，选择产品分类导航到对应分类的产品列表。" + string.Format(" 关键字: {0},{1}", catName, catRelated));
            this.ShortTitle = catName;
        }
        else
        {
            this.AddKeywords(string.Format("{0},{1}", catName, catRelated));
            this.AddDescription(string.Format("显示{0}分类的产品列表。{1}", catName, string.Format(" 关键字: {0},{1}", catName, catRelated)));
            this.ShortTitle = catName + " - " + catRelated;
        }
        this.SetTitle();

        this.AddJavaScriptInclude("scripts/pages/sortby.aspx.js", false, false);
    }
    #endregion
}
