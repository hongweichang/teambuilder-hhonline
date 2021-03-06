﻿using System;
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
            Pending p = new Pending();
            p.CompanyID = c.CompanyID;
            switch ((sender as Button).PostBackUrl)
            {
                case "#Agent":
                    p.CompanyType = CompanyType.Agent | c.CompanyType;
                    break;
                case "#Provider":
                    p.CompanyType = CompanyType.Provider | c.CompanyType;
                    break;
            }
            p.CreateTime = DateTime.Now;
            p.CreateUser = u.UserID;
            p.DenyReason = string.Empty;
            p.Status = PendingStatus.Pending;
            p.UpdateTime = DateTime.Now;
            p.UpdateUser = u.UserID;
            bool r = Pendings.PendingAdd(p);
            if (r)
            {
                BindCompanyType();
            }
            else
            {
                throw new HHException(ExceptionType.OperationError, "操作失败，申请过程中发生了错误，请联系管理员！"); 
            }
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
            if (pend == null)
            {
                ltPendingCom.Text = "--";
            }
            else
            {
                ltPendingCom.Text = GetCompantType(pend.CompanyType);
                ltStatus.Text = GetStatus(pend.Status);
                if (pend.Status == PendingStatus.Deny && !string.IsNullOrEmpty(pend.DenyReason))
                {
                    ltDenyUser.Text = "<span style='color:#ff0000'>" +
                                    Users.GetUser(pend.UpdateUser).DisplayName +
                                    ": </span>" +
                                    pend.DenyReason;
                }
                if (pend.Status == PendingStatus.Pending)
                {
                    btnAgent.Visible = false;
                    btnProvider.Visible = false;
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
                r = "<span style='color:#0000ff'>审核未通过！</span>";
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