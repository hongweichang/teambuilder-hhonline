using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Framework;
using System.Text;

public partial class ControlPanel_Users_CompanyEdit : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindRegion();
        if (!IsPostBack)
        {
            BindData();
        }
    }
    void BindRegion()
    {
        try
        {
            int regId = int.Parse(hfRegionCode.Value);
            if (regId != 0)
            {
                Area a = Areas.GetArea(regId);
                txtRegion.Text = a.RegionName;
            }
        }
        catch { }
    }
    Company c = null;
    void BindData()
    {
        try
        {
            int id = int.Parse(Request.QueryString["ID"]);
            lbAdd.PostBackUrl = "UserEdit.aspx?ID=" + id + "&Mode=Add";
            btnUpdateQualify.PostBackUrl = "CompanyQualify.aspx?ID=" + id;
            btnUpdateDeposit.PostBackUrl = "CompanyDeposit.aspx?ID=" + id;
            btnUpdateCredit.PostBackUrl = "CompanyCredit.aspx?ID=" + id;
            c = Companys.GetCompany(id);
            txtCompanyName.Text = c.CompanyName;
            try
            {
                hfRegionCode.Value = c.CompanyRegion.ToString();
                Area a = Areas.GetArea(c.CompanyRegion);
                txtRegion.Text = a.RegionName;
            }
            catch { }
            cslMain.SelectedValue = c.CompanyStatus;
            txtCompanyPhone.Text = c.Phone;
            txtCompanyFax.Text = c.Fax;
            txtCompanyAddress.Text = c.Address;
            txtZipCode.Text = c.Zipcode;
            txtCompanyWebsite.Text = c.Website;
            txtOrgCode.Text = c.Orgcode;
            txtIcpCode.Text = c.Regcode;
            txtCompanyMemo.Text = c.Remark;

            UserQuery query = new UserQuery();
            query.CompanyID = c.CompanyID;
            query.UserType = UserType.CompanyUser;
            query.AccountStatus=AccountStatus.All;
            PagingDataSet<User> pds = Users.GetUsers(query, false);
            List<User> users = pds.Records;
            if (users.Count == 0)
            {
                ltUsers.Text = "无";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < users.Count; i++)
                {
                    sb.Append("<a href=\"UserEdit.aspx?ID=");
                    sb.Append(users[i].UserID);
                    sb.Append("\" ");
                    if (users[i].AccountStatus != AccountStatus.Authenticated)
                    {
                        sb.Append("style=\"color:#888\" title=\"被锁定\" ");
                    }
                    sb.Append(">");
                    sb.Append(users[i].UserName);
                    sb.Append("</a>;&nbsp;");
                }
                ltUsers.Text = sb.ToString();
            }
        }
        catch (Exception ex)
        {
            base.ExecuteJs("msg('" + ex.Message + "')", false);
        }
    }
    protected void btnEdit_Click(object obj, EventArgs e)
    {
        if (c == null)
        {
            int id = int.Parse(Request.QueryString["ID"]);
            c = Companys.GetCompany(id);
        }
        c.CompanyName = txtCompanyName.Text.Trim();
        c.CompanyRegion = int.Parse(hfRegionCode.Value);
        c.Phone = txtCompanyPhone.Text.Trim();
        c.Fax = txtCompanyFax.Text.Trim();
        c.Address = txtCompanyAddress.Text.Trim();
        c.Zipcode = txtZipCode.Text.Trim();
        c.Website = txtCompanyWebsite.Text.Trim();
        c.Orgcode = txtOrgCode.Text.Trim();
        c.Regcode = txtIcpCode.Text.Trim();
        c.Remark = txtCompanyMemo.Text.Trim();
        c.CompanyStatus = cslMain.SelectedValue;
        c.UpdateTime = DateTime.Now;
        c.UpdateUser = Profile.AccountInfo.UserID;
        bool result = Companys.UpdateCompany(c);
        if (result)
            base.ExecuteJs("msg('操作成功，已成功修改公司信息！',true);", false);
        else
            base.ExecuteJs("msg('操作失败，无法修改公司信息！',false);", false);

    }
    public override void OnPageLoaded()
    {
        this.PagePermission = "CorpUserModule-Edit";
        this.PageInfoType = InfoType.IframeInfo;
        this.AddJavaScriptInclude("scripts/jquery.password.js", false, false);
        this.AddJavaScriptInclude("scripts/pages/register.aspx.js", false, false);
        SetValidator(true, true, 5000);
    }
}
