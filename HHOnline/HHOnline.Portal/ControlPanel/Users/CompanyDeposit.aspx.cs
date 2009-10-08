using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Framework;

public partial class ControlPanel_Users_CompanyDeposit : HHPage
{
    static string _postback = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.UrlReferrer != null)
                _postback = Request.UrlReferrer.ToString();
            BindDeposit();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int comId = int.Parse(Request.QueryString["ID"]);
        if (cp == null)
        {
            cp = CompanyDeposits.DepositSelect(comId);
        }
        if (cp == null)
        {
            cp = new CompanyDeposit();
            cp.CreateTime = DateTime.Now;
            cp.CreateUser = Profile.AccountInfo.UserID;
        }
        cp.CompanyID = comId;
        cp.DepositAmount = decimal.Parse(txtAmount.Text);
        cp.DepositDate = DateTime.Parse(txtDate.Text);
        cp.DepositDelta = decimal.Parse(txtDelta.Text);
        cp.DepositDesc = txtDesc.Text.Trim();
        cp.DepositMemo = string.Empty;
        cp.DepositType = (DepositType)(int.Parse(ddlType.SelectedValue));
        if (CompanyDeposits.DepositSave(cp))
            mbMsg.ShowMsg("成功更新保证金信息！", System.Drawing.Color.Navy);
        else
            mbMsg.ShowMsg("无法保存保证金信息，请联系管理员！", System.Drawing.Color.Red);
    }
    CompanyDeposit cp = null;
    void BindDeposit()
    {
        if (!string.IsNullOrEmpty(_postback))
            btnBack.PostBackUrl = _postback;
        int comId = int.Parse(Request.QueryString["ID"]);
        cp = CompanyDeposits.DepositSelect(comId);
        if (cp != null)
        {
            txtAmount.Text = cp.DepositAmount.ToString("f0");
            txtDate.Text = cp.DepositDate.ToString("yyyy年MM月dd日");
            txtDelta.Text = cp.DepositDelta.ToString("f0");
            txtDesc.Text = cp.DepositDesc;
            ddlType.SelectedIndex = (int)cp.DepositType;
            ltCreateInfo.Text = cp.CreateTime.ToString("yyyy年MM月dd日") + "由【" + Users.GetUser(cp.CreateUser).DisplayName + "】创建。";
        }
        else
        {
            txtDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            ltCreateInfo.Text = DateTime.Now.ToString("yyyy年MM月dd日") + "由【" + Profile.AccountInfo.DisplayName + "】创建。";
        }
    }
    public override void OnPageLoaded()
    {
        this.PagePermission = "CorpUserModule-Edit";
        this.PageInfoType = InfoType.IframeInfo;
        SetValidator(true, true, 5000);

        AddJavaScriptInclude("scripts/jquery.datepick.js", false, false);
        AddJavaScriptInclude("scripts/pages/aticleadd.aspx.js", false, false);
    }
}
