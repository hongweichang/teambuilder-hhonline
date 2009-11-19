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
        string industryName = string.Empty, industryAbstract = string.Empty;
        string id = Request.QueryString["ID"];
        if (!string.IsNullOrEmpty(id))
        {
            int pid = int.Parse(GlobalSettings.Decrypt(id));
            ProductIndustry pi = ProductIndustries.GetProductIndustry(pid);
            if (null != pi)
            {
                industryName = pi.IndustryName;
                industryAbstract = pi.IndustryAbstract;
            }
        }

        if (string.IsNullOrEmpty(industryName))
        {
            industryName = "所有行业";
            this.AddKeywords(industryName);
            this.AddDescription("分组显示所有行业列表，选择行业导航到对应行业的产品列表。");
            this.ShortTitle = industryName;
        }
        else
        {
            this.AddKeywords(industryName);
            this.AddDescription(string.Format("显示{0}行业的产品列表。{1}{2}", industryName, industryAbstract, string.Format(" 关键字: {0}", industryName)));
            this.ShortTitle = industryName;
        }
        this.SetTitle();

        this.AddJavaScriptInclude("scripts/pages/sortby.aspx.js", false, false);
    }
    #endregion
}

