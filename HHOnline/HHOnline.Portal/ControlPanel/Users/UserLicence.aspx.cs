using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class ControlPanel_Users_UserLicence : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindLicence();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ResourceManager.SaveLicence(txtLicence.Text);
    }
    void BindLicence()
    {
        txtLicence.Text = ResourceManager.GetLicence();
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "注册协议";
        base.SetTitle();
        this.SetTabName(this.ShortTitle);
    }
    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "UserLicenceModule-Edit";
        base.OnPermissionChecking(e);
    }
}
