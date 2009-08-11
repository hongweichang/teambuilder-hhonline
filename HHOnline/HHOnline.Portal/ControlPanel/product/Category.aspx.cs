using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;

public partial class ControlPanel_Product_Category : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override void OnPageLoaded()
    {
        this.PagePermission = "ProductCategoryModule-View";
        this.ShortTitle = "分类管理";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
    }
}
