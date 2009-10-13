using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Shops;
using HHOnline.Framework;
using HHOnline.Shops.Enums;

public partial class ControlPanel_product_SupplyDetails : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindSupply();
        }
    }
    void BindSupply()
    {
        int id = int.Parse(Request.QueryString["ID"]);
        ProductSupply ps = ProductSupplyManager.GetProductSupply(id);
        Company c = Companys.GetCompany(ps.SupplierID);
        Area a =Areas.GetArea(c.CompanyRegion);
        string AN="--";
        if(a!=null) AN=a.RegionName;
        ltCompanyName.Text = "<a class=\"navCompany\" href=\"javascript:{}\">["+c.CompanyName+
                                "]<div class=\"companyDetailsRemark_s\">"+
                                "<div>区域：" + AN + "</div>" +
                                "<div>组织机构代码：" + c.Orgcode + "</div>" +
                                "<div>工商注册号码：" + c.Regcode + "</div>" +
                                "<div>联系电话：" + c.Phone + "</div>" +
                                "<div>公司传真：" + c.Fax + "</div>" +
                                "<div>联系地址：" + c.Address + "</div>" +
                                "<div>邮政编码：" + c.Zipcode + "</div>" +
                                "<div>公司主页：" + c.Website + "</div>" +
                                "</div></a>";
        ltFaxRate.Text = ps.ApplyTaxRate.ToString("P2");
        dsDeliverySpan.DateSpanValue = ps.DeliverySpan;
        ltCreateTime.Text = ps.CreateTime.ToShortDateString();
        ltCreateUser.Text = Users.GetUser(ps.CreateUser).UserName;
        ltHasfeight.Text = (ps.IncludeFreight == FreightIncludeType.Include ? "是" : "<span class='needed'>否</span>");
        ltIncludeTax.Text = (ps.IncludeTax == TaxIncludeType.Include ? "是" : "<span class='needed'>否</span>");
        
        if (ps.SupplyRegion == null) { ltRegion.Text = "全国"; }
        else {
            AN=string.Empty;
            a = Areas.GetArea(ps.SupplyRegion.Value);
            if(a!=null) AN=a.RegionName;
            ltRegion.Text = a.RegionName;
        }

        ltStartTime.Text = ps.QuoteFrom.ToShortDateString();
        ltEndTime.Text = ps.QuoteEnd.ToShortDateString();
        ltAmount.Text = (ps.QuoteMOQ == null ? "无要求" : ps.QuoteMOQ.Value.ToString());
        ltPrice.Text = GlobalSettings.GetPrice(ps.QuotePrice);
        ltQuoteRenewal.Text = (ps.QuoteRenewal == 0 ? "无" : ps.QuoteRenewal + "月");
        dsWS.DateSpanValue = ps.WarrantySpan;
        ltSupply.Text = (ps.SupplyStatus == ComponentStatus.Enabled ? "是" : "<span class='needed'>否</span>");
    }
    public override void OnPageLoaded()
    {
        this.PagePermission = "ProductModule-View";
        this.PageInfoType = InfoType.IframeInfo;
    }
}
