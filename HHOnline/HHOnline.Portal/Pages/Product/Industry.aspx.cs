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
            hpilProduct.Visible = true;
            pnlSort.Visible = false;
        }
        else
        {
            hpilProduct.Visible = false;
            int pid = int.Parse(GlobalSettings.Decrypt(id));
            inProduct.IndustryID = pid;
            illProduct.IndustryID = pid;
            islProduct.IndustryID = pid;
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
            int pid = int.Parse(GlobalSettings.Decrypt(id));
            ProductIndustry pi = ProductIndustries.GetProductIndustry(pid);
            this.ShortTitle = pi.IndustryName;
        }
        else
        {
            this.ShortTitle = "所有行业";
        }
        this.SetTitle();
    }
    #endregion
}
