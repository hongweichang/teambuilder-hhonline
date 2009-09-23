using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class Pages_Profiles_ChangePwd : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        string uid = User.Identity.Name;
        bool r = Users.ChangePassword(uid, txtOldPassword.Text.Trim(), txtNewPassword.Text.Trim());
        if (r)
            base.ExecuteJs("msg('密码修改成功！')",false);
        else
            base.ExecuteJs("msg('密码修改失败，请确认原密码是否正确！')", false);
    }
    public override void OnPageLoaded()
    {
        SetValidator(true, true, 5000);
    }
}
