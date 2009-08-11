using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;
using HHOnline.Framework.Web.Enums;

public partial class ControlPanel_Permission_OrganizeAdd : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int id = int.Parse(Request.QueryString["ID"]);
            BindParentDept(id);
        }
        catch (Exception ex)
        {
            base.ExecuteJs("msg('" + ex.Message + "');", false);
        }
    }
    void BindParentDept(int id)
    {
        Organization org = Organizations.GetOrganization(id);
        ltParDept.Text = org.OrganizationName;
        ltParDeptDesc.Text = org.OrganizationDesc;
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        try
        {
            int id = int.Parse(Request.QueryString["ID"]);
            Organization org = new Organization();
            org.OrganizationDesc = txtDeptDesc.Text.Trim();
            org.CreateTime = DateTime.Now;
            org.CreateUser = Profile.AccountInfo.UserID;
            org.DisplayOrder = 1;
            org.OrganizationMemo = string.Empty;
            org.OrganizationName = txtDeptName.Text.Trim();
            org.OrganizationStatus = ComponentStatus.Enabled;
            org.ParentID = id;
            org.UpdateTime = DateTime.Now;
            org.UpdateUser = Profile.AccountInfo.UserID;
            Organization orgs = Organizations.CreateOrganization(org);
            if (orgs == null || orgs.OrganizationID == 0)
            {
                mbMsg.ShowMsg("新增组织机构失败，请联系管理员！");
            }
            else
            {
                if (orgs.OrganizationID == -1)
                {
                    mbMsg.ShowMsg("新增组织机构失败，存在同名组织结构！");
                }
                else
                {
                    base.ExecuteJs("msg('操作成功，已成功增加一个新的部门！',true);", false);
                }
            }
        }
        catch (Exception ex)
        {
            base.ExecuteJs("msg('" + ex.Message + "');", false);
        }
    }
    public override void OnPageLoaded()
    {
        this.PageInfoType = InfoType.PopWinInfo;
        this.ShortTitle = "新增部门";
        
        base.OnPageLoaded();
        SetValidator(true,true,5000);
    }
    protected override void OnPagePermissionChecking()
    {
        this.PagePermission = "OrganizeModule-Add";
        base.OnPagePermissionChecking();
    }
}
