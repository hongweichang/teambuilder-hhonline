using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class Pages_Profiles_CompanyTypeView : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindCompanyType();
    }
    void BindCompanyType()
    {
        User u = Profile.AccountInfo;
        ltComType.Text = GetCompantType(u.Company.CompanyType);
    }
    object[] _arr = null;
   
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
    public override void OnPageLoaded()
    {
        this.ShortTitle = "公司类型";
        this.SetTabName(this.ShortTitle);
        this.SetTitle();
    }
}