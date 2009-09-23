using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class Pages_Profiles_CompanyProfile : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCD();
        }
    }
    void BindCD()
    {
        User u = Profile.AccountInfo;
        if (u.UserType == UserType.InnerUser)
        {
            mbNC.ShowMsg("内部员工没有相关的公司信息，请选择其它信息进行查看！", System.Drawing.Color.Olive);
        }
        else
        {
            mbNC.HideMsg();
            
        }
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "公司信息";
        this.SetTabName(this.ShortTitle);
    }
}
