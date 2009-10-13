using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;
using HHOnline.Permission.Services;
using System.Collections;

public partial class ControlPanel_Users_Corps : HHPage
{

    private readonly string destUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?users-corps";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLinkButton();
            BindCorps();
        }
    }

   
    #region -Events-
    protected void btnQuickSearch_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        Response.Redirect(btn.PostBackUrl);
    }
    protected void btnSearch_Click(object obj, EventArgs e)
    {
        egvCorps.DataSource = Companys.GetCompanys(GetComStatus(), CompanyType.None, txtCompanyName.Text.Trim());
        egvCorps.DataBind();
    }
    #endregion

    #region -BindData-

    void BindLinkButton()
    {
        btnAll.PostBackUrl = destUrl;
        btnAuth.PostBackUrl = destUrl + "&des=auth";
        btnAuthPre.PostBackUrl = destUrl + "&des=authpre";
        btnDisAuth.PostBackUrl = destUrl + "&des=disauth";
        btnLockon.PostBackUrl = destUrl + "&des=lockon";
        string des = Request.QueryString["des"];
        if (string.IsNullOrEmpty(des))
        {
            btnAll.CssClass = "active";
            btnAuth.CssClass = "";
            btnAuthPre.CssClass = "";
            btnDisAuth.CssClass = "";
            btnLockon.CssClass = "";
        }
        else
        {
            if (des == "auth")
            {
                btnAll.CssClass = "";
                btnAuth.CssClass = "active";
                btnAuthPre.CssClass = "";
                btnDisAuth.CssClass = "";
                btnLockon.CssClass = "";
            }
            if (des == "authpre")
            {
                btnAll.CssClass = "";
                btnAuth.CssClass = "";
                btnAuthPre.CssClass = "active";
                btnDisAuth.CssClass = "";
                btnLockon.CssClass = "";
            }
            if (des == "disauth")
            {
                btnAll.CssClass = "";
                btnAuth.CssClass = "";
                btnAuthPre.CssClass = "";
                btnDisAuth.CssClass = "active";
                btnLockon.CssClass = "";
            }
            if (des == "lockon")
            {
                btnAll.CssClass = "";
                btnAuth.CssClass = "";
                btnAuthPre.CssClass = "";
                btnDisAuth.CssClass = "";
                btnLockon.CssClass = "active";
            }
        }
    }
    CompanyStatus GetComStatus()
    {
        string des = Request.QueryString["des"];
        CompanyStatus cs = CompanyStatus.None;
        if (!string.IsNullOrEmpty(des))
        {
            switch (des)
            {
                case "auth":
                    cs = CompanyStatus.Authenticated;
                    break;
                case "authpre":
                    cs = CompanyStatus.ApprovalPending;
                    break;
                case "disauth":
                    cs = CompanyStatus.Disapproved;
                    break;
                case "lockon":
                    cs = CompanyStatus.Lockon;
                    break;
            }
        }
        return cs;
    }
    void BindCorps()
    {
        string name = Request.QueryString["CorpName"];
        if (!string.IsNullOrEmpty(name))
            txtCompanyName.Text = name;
        egvCorps.DataSource = Companys.GetCompanys(GetComStatus(), CompanyType.None, txtCompanyName.Text.Trim());
        egvCorps.DataBind();
    }
    #endregion

    #region -ExtendgridView-
    public void egvCorps_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = (int)egvCorps.DataKeys[e.RowIndex].Value;
        Company c = Companys.GetCompany(id);
        c.CompanyStatus = CompanyStatus.Lockon;
        Companys.UpdateCompany(c);
        BindCorps();
    }

    public void egvCorps_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        egvCorps.PageIndex = e.NewPageIndex;
        BindCorps();
    }
    #endregion

    #region -Methods-
    public string GetStatus(object status)
    {
        try
        {
            CompanyStatus c = (CompanyStatus)status;
            
            switch (c)
            {
                case CompanyStatus.Deleted:
                    return "<span style=\"color:#800000\">已删除</span>";
                case CompanyStatus.Authenticated:
                    return "<span style=\"color:#20B2AA\">已审核</span>";
                case CompanyStatus.ApprovalPending:
                    return "<span style=\"color:#000080;font-weight:bold\">待审核</span>";
                case CompanyStatus.Disapproved:
                    return "<span style=\"color:#CD5C5C\">审核未通过</span>";
                case CompanyStatus.Lockon:
                    return "<span style=\"color:#708090\">公司停用</span>";
            }
            return "--";
        }
        catch
        {
            return "--";
        }
    }
    string tempUserC = "<ul class=\"companyTypeList\">{0}</ul>";
    string tempUser = "<li><span class=\"{0}\" title=\"{1}\" /></span></li>";
    public string GetCompantType(Object comType)
    {
        string re = string.Empty;
        CompanyType ct = (CompanyType)comType;
        List<ComTypeList> ctls = CompanyTypeKeyValue.Instance[(int)ct] as List<ComTypeList>;
        ComTypeList ctl = null;
        for (int i = 0; i < ctls.Count; i++)
        {
            ctl = ctls[i];
            re += string.Format(tempUser, ctl.CssClass, ctl.Title);
        }
        return string.Format(tempUserC, re);
    }
    Area a = null;
    public string GetRegion(string areaId)
    {
        try
        {
            if (string.IsNullOrEmpty(areaId))
            {
                return "--";
            }
            int id = int.Parse(areaId);
            a = Areas.GetArea(id);
            return a.RegionName;
        }
        catch
        {
            return "--";
        }
    }
    #endregion

    #region -Overload-
    public override void OnPageLoaded()
    {
        this.ShortTitle = "客户管理";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
        this.AddJavaScriptInclude("scripts/pages/corp.aspx.js", false, false);
    }
    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "CorpUserModule-View";
        base.OnPermissionChecking(e);
    }
    #endregion
}
