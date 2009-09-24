using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Framework.Web;

public partial class Pages_Profiles_ChangeQA :HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }
    void BindData()
    {
        ltQuestion.Text = Profile.AccountInfo.PasswordQuestion;
    }
    protected void btnChange_Click(object sender, EventArgs e)
    {
        User u = Profile.AccountInfo;
        if (GlobalSettings.EncodePassword(txtOldAnswer.Text.Trim()) != u.PasswordAnswer)
        {
            base.ExecuteJs("msg('原始密码提示答案错误，无法完成修改！')", false);
        }
        else
        {
            u.PasswordAnswer = txtNewAnswer.Text.Trim();
            u.Password = string.Empty;
            if (Users.UpdateUser(u))
                base.ExecuteJs("msg('成功修改密码提示答案！')", false);
            else
                base.ExecuteJs("msg('修改密码提示答案失败！')", false);

        }
    }
    public override void OnPageLoaded()
    {
        SetValidator(true, true, 5000);
    }
}
