using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Shops;

public partial class ControlPanel_product_TradeSelect : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<ProductIndustry> industries = ProductIndustries.GetProductIndustries();

            rpTrade.DataSource = industries;
            rpTrade.DataBind();
        }
    }
    public override void OnPageLoaded()
    {
        this.PageInfoType = InfoType.PopWinInfo;
        User u = Profile.AccountInfo;
        if (u.UserType != UserType.CompanyUser ||
            (u.Company.CompanyType == CompanyType.Ordinary ||
            (u.Company.CompanyType == (CompanyType.Ordinary | CompanyType.Agent)))
            || u.IsManager != 1)
        {
            this.PagePermission = "ProductModule-Add";
        }
    }
}
