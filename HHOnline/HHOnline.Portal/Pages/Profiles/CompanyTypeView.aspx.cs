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

    protected void btnAgentProvider_Click(object sender, EventArgs e)
    {
        User u = Profile.AccountInfo;
        Company c = u.Company;
        if (string.IsNullOrEmpty(c.Regcode) || string.IsNullOrEmpty(c.Orgcode) || string.IsNullOrEmpty(c.Phone))
        {
            throw new HHException(ExceptionType.OperationError, "操作失败，进行申请前请进入【公司信息】页完善公司相关信息，包括【联系电话】，【组织结构代码】，【工商注册号】等。");
        }
        else
        { 
            
        }
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
            AdaptButton(u.Company.CompanyType);

            ltComType.Text = GetCompantType(u.Company.CompanyType);

            Pending pend = Pendings.PendingGet(u.CompanyID);
            if (pend == null || pend.Status == PendingStatus.Deny)
            {
                ltPendingCom.Text = "--";
            }
            else
            {
                btnProvider.Visible = false;
                btnAgent.Visible = false;
                ltCS.Text = "<span style='color:#888'>等待管理员进行申请审批。。。</span>";
                ltPendingCom.Text = GetCompantType(pend.CompanyType);
                ltStatus.Text = GetStatus(pend.Status);
                if (!string.IsNullOrEmpty(pend.DenyReason))
                {
                    ltDenyUser.Text = "<span style='color:#ff0000'>" +
                                    Users.GetUser(pend.UpdateUser).DisplayName +
                                    ": </span>" +
                                    pend.DenyReason;
                }

            }
        }
    }
    string GetStatus(PendingStatus status)
    {
        string r = string.Empty;
        switch (status)
        {
            case PendingStatus.Pending:
                r = "审核中。。。";
                break;
            case PendingStatus.Deny:
                r = "<span style='color:#ff0000'>审核未通过！</span>";
                break;
            case PendingStatus.Inspect:
                r = "审核通过！";
                break;
        }
        return r;
    }
    void AdaptButton(CompanyType type)
    {
        if (type == CompanyType.Agent ||
                type == (CompanyType.Agent | CompanyType.Ordinary) ||
                type == (CompanyType.Agent | CompanyType.Ordinary | CompanyType.Provider))
        {
            btnAgent.Visible = false;
        }
        if (type == CompanyType.Provider ||
           type == (CompanyType.Provider | CompanyType.Ordinary) ||
           type == (CompanyType.Agent | CompanyType.Ordinary | CompanyType.Provider))
        {
            btnProvider.Visible = false;
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