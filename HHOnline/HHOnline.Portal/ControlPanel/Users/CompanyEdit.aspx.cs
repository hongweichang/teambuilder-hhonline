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
    void BindData()
    {
        try
        {
            int id = int.Parse(Request.QueryString["ID"]);
            Company c = Companys.GetCompany(id);
            txtCompanyName.Text = c.CompanyName;
            try
            {
                hfRegionCode.Value = c.CompanyRegion.ToString();
                Area a = Areas.GetArea(c.CompanyRegion);
                txtRegion.Text = a.RegionName;
            }
            catch { }

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
            query.AccountStatus=AccountStatus.Authenticated;
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
                    sb.Append("<a href=\"UserEdit.aspx?ID=" + users[i].UserID + "\">" + users[i].UserName + "</a>;&nbsp;");
                }
                ltUsers.Text = sb.ToString();
            }
        }
        catch (Exception ex)
        {
            //base.ExecuteJs("msg('" + ex.Message + "')", false);
        }
    }
    protected void btnEdit_Click(object obj, EventArgs e)
    {
        int id = int.Parse(Request.QueryString["ID"]);
        
        Company com = new Company();
        com.CompanyID = id;
        com.CompanyName = txtCompanyName.Text.Trim();
        com.CompanyRegion = int.Parse(hfRegionCode.Value);
        com.Phone = txtCompanyPhone.Text.Trim();
        com.Fax = txtCompanyFax.Text.Trim();
        com.Address = txtCompanyAddress.Text.Trim();
        com.Zipcode = txtZipCode.Text.Trim();
        com.Website = txtCompanyWebsite.Text.Trim();
        com.Orgcode = txtOrgCode.Text.Trim();
        com.Regcode = txtIcpCode.Text.Trim();
        com.Remark = txtCompanyMemo.Text.Trim();
        com.UpdateTime = DateTime.Now;
        com.UpdateUser = Profile.AccountInfo.UserID;
        

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
