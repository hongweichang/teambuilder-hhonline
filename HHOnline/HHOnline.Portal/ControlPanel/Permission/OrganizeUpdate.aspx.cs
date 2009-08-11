using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;
using HHOnline.Framework.Web.Enums;

public partial class ControlPanel_Permission_OrganizeUpdate : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            try
            {
                int id = int.Parse(Request.QueryString["ID"]);
                BindDept(id);
            }
            catch (Exception ex)
            {
                base.ExecuteJs("msg('" + ex.Message + "');", false);
            }
        }
    }
    void BindDept(int id)
    {
        Organization org = Organizations.GetOrganization(id);
        txtDeptName.Text = org.OrganizationName;
        txtDeptDesc.Text = org.OrganizationDesc;
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        try
        {
            int id = int.Parse(Request.QueryString["ID"]);
            Organization org = Organizations.GetOrganization(id);
            org.OrganizationDesc = txtDeptDesc.Text.Trim();
            org.OrganizationName = txtDeptName.Text.Trim();
            org.OrganizationStatus = ComponentStatus.Enabled;
            org.OrganizationID = id;
            org.UpdateTime = DateTime.Now;
            org.UpdateUser = Profile.AccountInfo.UserID;
            Organizations.UpdateOrganization(org);
            base.ExecuteJs("msg('操作成功，已成功修改此部门信息！',true);", false);            
        }
        catch
        {
            mbMsg.ShowMsg("修改部门信息失败，请联系管理员！");
        }
    }
    public override void OnPageLoaded()
    {
        this.PageInfoType = InfoType.PopWinInfo;
        this.ShortTitle = "修改部门信息";
        
        base.OnPageLoaded();
        SetValidator(true, true, 5000);
    }
    protected override void OnPagePermissionChecking()
    {
        this.PagePermission = "OrganizeModule-Edit";
        base.OnPagePermissionChecking();
    }
}