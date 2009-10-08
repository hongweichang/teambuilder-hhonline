using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class ControlPanel_product_PendingList : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPendings();
        }
    }

    protected void egvPendings_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Literal lt1 = e.Row.Cells[0].FindControl("ltCompanyName") as Literal;
            Literal lt2 = e.Row.Cells[1].FindControl("ltCompanyType") as Literal;
            Pending p = e.Row.DataItem as Pending;
            Company c = Companys.GetCompany(p.CompanyID);
            lt1.Text = c.CompanyName;
            lt2.Text = GetCompanyType(c.CompanyType);
        }
    }
    string tempUserC = "<ul class=\"companyTypeList\">{0}</ul>";
    string tempUser = "<li><span class=\"{0}\" title=\"{1}\" /></span></li>";
    public string GetCompanyType(CompanyType ct)
    {
        string re = string.Empty;
        List<ComTypeList> ctls = CompanyTypeKeyValue.Instance[(int)ct] as List<ComTypeList>;
        ComTypeList ctl = null;
        for (int i = 0; i < ctls.Count; i++)
        {
            ctl = ctls[i];
            re += string.Format(tempUser, ctl.CssClass, ctl.Title);
        }
        return string.Format(tempUserC, re);
    }
    public string GetCompantType(Object comType)
    {
        CompanyType ct = (CompanyType)comType;
        return GetCompanyType(ct);
    }
    void BindPendings()
    {
        egvPendings.DataSource = Pendings.PendingsLoad();
        egvPendings.DataBind();
    }

    protected void egvPendings_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        egvPendings.PageIndex = e.NewPageIndex;
        BindPendings();
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "客户变更";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
        this.AddJavaScriptInclude("scripts/pages/corppend.aspx.js", false, false);
    }
}
