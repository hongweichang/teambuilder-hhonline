using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class Pages_Profiles_CompanyUsers : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindUsers();
        }
    }

    protected void rpList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = int.Parse(e.CommandArgument.ToString());
        User u = Users.GetUser(id);
        u.AccountStatus = AccountStatus.Deleted;
        u.Password = string.Empty;
        Users.UpdateUser(u);
        BindUsers();
    }
    void BindUsers()
    {
        User u = Profile.AccountInfo;
        if (u.UserType == UserType.InnerUser || u.IsManager == 2)
        {
            lbNewRole.Visible = false;
            mbNC.ShowMsg("内部用户或非领导级别用户无法查看此页面！", System.Drawing.Color.Red);
        }
        else
        {
            UserQuery q = new UserQuery();
            q.CompanyID = u.CompanyID;
            q.UserType = UserType.CompanyUser;
            q.AccountStatus = AccountStatus.Authenticated;
            List<User> users = Users.GetUsers(q).Records;
            rpList.DataSource = users;
            rpList.DataBind();
        }
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "成员管理";
        this.SetTabName(this.ShortTitle);
        this.SetTitle();
        this.AddJavaScriptInclude("scripts/pages/companyuser.aspx.js", false, false);
    }
}
