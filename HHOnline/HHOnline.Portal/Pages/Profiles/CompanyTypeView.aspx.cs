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
        if (Profile.AccountInfo.UserType == UserType.InnerUser)
        {
            mbNC.ShowMsg("内部用户无法查看客户状态！", System.Drawing.Color.Red);
            pnlManager.Visible = false;
        }
        else
        {
            mbNC.HideMsg();
            User u = Profile.AccountInfo;
            if (u.Company.CompanyType == CompanyType.Agent ||
                u.Company.CompanyType == (CompanyType.Agent | CompanyType.Ordinary) ||
                u.Company.CompanyType == (CompanyType.Agent | CompanyType.Ordinary | CompanyType.Provider))
            {
                btnAgent.Visible = false;
            }
            if (u.Company.CompanyType == CompanyType.Provider ||
               u.Company.CompanyType == (CompanyType.Provider | CompanyType.Ordinary) ||
               u.Company.CompanyType == (CompanyType.Agent | CompanyType.Ordinary | CompanyType.Provider))
            {
                btnProvider.Visible = false;
            }
            ltComType.Text = GetCompantType(u.Company.CompanyType);

            Pending pend = Pendings.PendingGet(u.CompanyID);
            if (pend == null)
            {
                ltPendingCom.Text = "--";
            }
            else
            {
                CompanyQualification q;
                
            }
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
    public override void OnPageLoaded()
    {
        this.ShortTitle = "公司类型";
        this.SetTabName(this.ShortTitle);
        this.SetTitle();
    }
}