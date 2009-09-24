using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;

public partial class Pages_Profiles_CompanyTypeView : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    void BindCompanyType()
    { 
        
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "公司类型";
        this.SetTabName(this.ShortTitle);
        this.SetTitle();
    }
}
