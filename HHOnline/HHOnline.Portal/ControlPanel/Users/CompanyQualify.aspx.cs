using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Framework;

public partial class ControlPanel_Users_CompanyQualify : HHPage
{
    static string _postback = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.UrlReferrer != null)
                _postback = Request.UrlReferrer.ToString();
            BindQualify();
        }
    }

    protected void egvQualify_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        egvQualify.PageIndex = e.NewPageIndex;
        BindQualify();
    }

    protected void egvQualify_RowEditing(object sender, GridViewEditEventArgs e)
    {
        btnEdit.Visible = true;
        int id = int.Parse(egvQualify.DataKeys[e.NewEditIndex].Value.ToString());
        hfCurID.Value = id.ToString();
        CompanyQualification cq = CompanyQualifications.GetCompanyQualification(id);
        txtName.Text = cq.QualificationName;
        txtDesc.Text = cq.QualificationDesc;

        ltUpload.Text = "<div>当前附件：<a style='color:#0000ff' href='" + cq.Url + "' target='_blank'>" + cq.File.FileName + "</a></div>";
    }

    protected void egvQualify_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CompanyQualifications.DeleteQualification(int.Parse(egvQualify.DataKeys[e.RowIndex].Value.ToString()));
        BindQualify();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    { 
        int id=0;
        if (!string.IsNullOrEmpty(hfCurID.Value))
            id = int.Parse(hfCurID.Value);
        if (id == 0)
            mbMsg.ShowMsg("没有可编辑的对象！", System.Drawing.Color.Red);
        else
        {
            HttpPostedFile files = fuQualify.PostedFile;
            if (files.ContentLength > 0 && !files.FileName.EndsWith(".rar"))
            {
                mbMsg.ShowMsg("必须上传相关附件且只能是.rar文件!", System.Drawing.Color.Red);
                return;
            }

            CompanyQualification cq = CompanyQualifications.GetCompanyQualification(id);
            if (files.ContentLength == 0)
                cq.QualificationFile = cq.QualificationName;
            cq.QualificationName = txtName.Text.Trim();
            cq.QualificationDesc = txtDesc.Text.Trim();
            CompanyQualifications.EditFile(cq, files.ContentLength == 0 ? null : files.InputStream);

            mbMsg.ShowMsg("成功编辑当前资质文件！", System.Drawing.Color.Navy);
            BindQualify();
            txtName.Text = string.Empty;
            txtDesc.Text = string.Empty;
            ltUpload.Text = string.Empty;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        HttpPostedFile files = fuQualify.PostedFile;
        if (files.ContentLength <= 0 || !files.FileName.EndsWith(".rar"))
        {
            mbMsg.ShowMsg("必须上传相关附件且只能是.rar文件!", System.Drawing.Color.Red);
        }
        else
        {
            mbMsg.HideMsg();
            CompanyQualification q = new CompanyQualification();
            int comId = int.Parse(Request.QueryString["ID"]);
            q.CompanyID = comId;
            q.CreateTime = DateTime.Now;
            q.CreateUser = Profile.AccountInfo.UserID;
            q.QualificationDesc = txtDesc.Text.Trim();
            q.QualificationName = txtName.Text;
            q.QualificationStatus = ComponentStatus.Enabled;
            q.UpdateTime = DateTime.Now;
            q.UpdateUser = Profile.AccountInfo.UserID;
            q.QualificationFile = files.FileName;
            CompanyQualifications.AddFile(q, files.InputStream);
            mbMsg.ShowMsg("成功新增一份资质文件！", System.Drawing.Color.Navy);
            txtName.Text = string.Empty;
            txtDesc.Text = string.Empty;
            BindQualify();
        }
    }
    void BindQualify()
    {
        btnEdit.Visible = false;
        if (!string.IsNullOrEmpty(_postback))
            btnBack.PostBackUrl = _postback;
        else
            btnBack.Visible = false;
        int comId = int.Parse(Request.QueryString["ID"]);

        egvQualify.DataSource = CompanyQualifications.GetCompanyQualifications(comId);
        egvQualify.DataBind();
    }
    public override void OnPageLoaded()
    {
        this.PagePermission = "CorpUserModule-Edit";
        this.PageInfoType = InfoType.IframeInfo;
        SetValidator(true, true, 5000);
    }
}
