using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class Pages_Profiles_CompanyProfile : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCD();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        User u = Profile.AccountInfo;
        if (c == null)
        {
            c = Companys.GetCompanyByUser(u.UserID);
        }
        c.CompanyRegion = int.Parse(hfRegionCode.Value);
        c.Phone = txtCompanyPhone.Text.Trim();
        c.Fax = txtCompanyFax.Text.Trim();
        c.Address = txtCompanyAddress.Text.Trim();
        c.Zipcode = txtZipCode.Text.Trim();
        c.Website = txtCompanyWebsite.Text.Trim();
        c.Orgcode = txtOrgCode.Text.Trim();
        c.Regcode = txtIcpCode.Text.Trim();
        c.Remark = txtCompanyMemo.Text.Trim();
        c.UpdateTime = DateTime.Now;
        c.UpdateUser = Profile.AccountInfo.UserID;
        Companys.UpdateCompany(c);
        BindCD();
    }
    Company c = null;
    void BindCD()
    {
        User u = Profile.AccountInfo;
        if (u.UserType == UserType.InnerUser)
        {
            mbNC.ShowMsg("内部员工没有相关的公司信息，请选择其它信息进行查看！", System.Drawing.Color.Olive);
            pnlNormal.Visible = false; 
            pnlManager.Visible = false;
        }
        else
        {
            if (u.IsManager == 1)
            {
                pnlManager.Visible = true;
                pnlNormal.Visible = false;
            }
            else
            {
                pnlManager.Visible = false;
                pnlNormal.Visible = true;
            }
            mbNC.HideMsg();
            c = Companys.GetCompanyByUser(u.UserID);
            ltCompanyName.Text = c.CompanyName;
            ltName.Text = c.CompanyName;
            hfRegionCode.Value = c.CompanyRegion.ToString();
            try
            {
                Area a = Areas.GetArea(c.CompanyRegion);
                txtRegion.Text = a.RegionName;
                ltArea.Text = a.RegionName;
            }
            catch { }
            txtCompanyPhone.Text = c.Phone;
            ltPhone.Text = c.Phone;
            txtCompanyFax.Text = c.Fax;
            ltFax.Text = c.Fax;
            txtCompanyAddress.Text = c.Address;
            ltAddress.Text = c.Address;
            txtZipCode.Text = c.Zipcode;
            ltZipcode.Text = c.Zipcode;
            txtCompanyWebsite.Text = c.Website;
            ltWebsite.Text = "<a href='" + c.Website + "' target='_blank' style='color:#0000ff' >" + c.Website + "</a>";
            txtOrgCode.Text = c.Orgcode;
            ltOrgCode.Text = c.Orgcode;
            txtIcpCode.Text = c.Regcode;
            ltRegCode.Text = c.Regcode;
            txtCompanyMemo.Text = c.Remark;
            ltDescription.Text = c.Remark;
        }
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "公司信息";
        this.SetTabName(this.ShortTitle);
        this.AddJavaScriptInclude("scripts/pages/company.aspx.js", false, false); 
        this.SetTitle();
    }
}
