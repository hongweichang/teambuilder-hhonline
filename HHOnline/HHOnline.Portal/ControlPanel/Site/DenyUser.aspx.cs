using System;
using System.Drawing;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Framework.Web;

public partial class ControlPanel_Site_DenyUser : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            BindData();
        }
    }

    void BindData()
    {
        foreach (string name in DisallowedNames.GetNames())
        {
            Usernames.Text += name.Trim('*') + "\n";
        }
        Usernames.Text = Usernames.Text.TrimEnd('\n');
    }

    public override void OnPageLoaded()
    {
        this.ShortTitle = "用户名过滤";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
    }

    public void btnPost_Click(object sender, EventArgs e)
    {
        ArrayList currentNames = DisallowedNames.GetNames();

        foreach (string nameToDelete in currentNames)
        {
            DisallowedNames.DeleteName(nameToDelete);
        }

        String[] newNames = Usernames.Text.Split('\n');

        foreach (string nameToAdd in newNames)
        {
            if (nameToAdd.Length > 0)
                DisallowedNames.CreateName("*" + nameToAdd.Trim() + "*");
        }

        mbMessage.ShowMsg("修改用户名过滤信息成功！", Color.Navy);
    }

    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "EmailModule-View";
        e.CheckPermissionControls.Add("EmailModule-Edit", btnPost);
        base.OnPermissionChecking(e);
    }
}
