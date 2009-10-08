using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;

public partial class ControlPanel_Users_CompanyPendingEdit : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCompany();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int pId = int.Parse(Request.QueryString["PendingID"]);
        Pending p = Pendings.PendingGetById(pId);
        if (ynblPending.SelectedValue)
            p.Status = PendingStatus.Inspect;
        else
            p.Status = PendingStatus.Deny;
        p.DenyReason = txtDesc.Text.Trim();
        p.UpdateUser = Profile.AccountInfo.UserID;
        p.UpdateTime = DateTime.Now;
        if (Pendings.PendingUpdate(p))
            base.ExecuteJs("msg('审核成功！',true)", false);
        else
            base.ExecuteJs("msg('审核失败！')", false);
    }

    void BindCompany()
    {
        int comId = int.Parse(Request.QueryString["CompanyID"]);
        int pId=  int.Parse(Request.QueryString["PendingID"]);
        btnUpdateQualify.PostBackUrl = "CompanyQualify.aspx?ID=" + comId;
        btnUpdateDeposit.PostBackUrl = "CompanyDeposit.aspx?ID=" + comId;
        btnUpdateCredit.PostBackUrl = "CompanyCredit.aspx?ID=" + comId;
        Company c = Companys.GetCompany(comId);
        ltCompanyName.Text = c.CompanyName;
        ltAddress.Text = c.Address;
        ltFax.Text = c.Fax;
        ltOrgCode.Text = c.Orgcode;
        ltPhone.Text = c.Phone;
        ltRegCode.Text = c.Regcode;
        try
        {
            ltRegion.Text = Areas.GetArea(c.CompanyRegion).RegionName;
        }
        catch { ltRegion.Text = "--"; }
        ltRemark.Text = c.Remark;
        if (!string.IsNullOrEmpty(c.Website))
            ltWebSite.Text = "<a href='" + c.Website + "' target='_blank'>" + c.Website + "</a>";
        else
            ltWebSite.Text = "--";
        ltZipCode.Text = c.Zipcode;
        ltCompanyType.Text = GetCompanyType(c.CompanyType);
        Pending p = Pendings.PendingGetById(pId);
        ltPendingType.Text = GetCompanyType(p.CompanyType);
        
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
    public override void OnPageLoaded()
    {
        this.PagePermission = "CorpUserModule-Edit";
        this.PageInfoType = InfoType.IframeInfo;
        SetValidator(true, true, 5000);
    }
}
