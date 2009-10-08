using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Framework;

public partial class ControlPanel_Users_CompanyCredit : HHPage
{
    static string _postback = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.UrlReferrer != null)
                _postback = Request.UrlReferrer.ToString();
            BindCredit();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int comId = int.Parse(Request.QueryString["ID"]);
        if (cc == null)
        {
            cc = CompanyCredits.CreditSelect(comId);
        }
        if (cc == null)
        {
            cc = new CompanyCredit();
            cc.CreateTime = DateTime.Now;
            cc.CreateUser = Profile.AccountInfo.UserID;
        }
        cc.SupplierID = comId;
        cc.CreditAmount = decimal.Parse(txtAmount.Text);
        cc.CreditDate = DateTime.Parse(txtDate.Text);
        cc.CreditDelta = decimal.Parse(txtDelta.Text);
        cc.CreditDesc = txtDesc.Text.Trim();
        cc.CreditMemo = string.Empty;

        if (CompanyCredits.CreditSave(cc))
            mbMsg.ShowMsg("成功更新保证金信息！", System.Drawing.Color.Navy);
        else
            mbMsg.ShowMsg("无法保存保证金信息，请联系管理员！", System.Drawing.Color.Red);
    }
    CompanyCredit cc = null;
    void BindCredit()
    {
        if (!string.IsNullOrEmpty(_postback))
            btnBack.PostBackUrl = _postback;
        int comId = int.Parse(Request.QueryString["ID"]);
        cc = CompanyCredits.CreditSelect(comId);
        if (cc != null)
        {
            txtAmount.Text = cc.CreditAmount.ToString("f0");
            txtDate.Text = cc.CreditDate.ToString("yyyy年MM月dd日");
            txtDelta.Text = cc.CreditDelta.ToString("f0");
            txtDesc.Text = cc.CreditDesc;
            ltCreateInfo.Text = cc.CreateTime.ToString("yyyy年MM月dd日") + "由【" + Users.GetUser(cc.CreateUser).DisplayName + "】创建。";
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
