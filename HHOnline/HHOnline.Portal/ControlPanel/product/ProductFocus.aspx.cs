using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Shops;
using HHOnline.Framework.Web;


public partial class ControlPanel_product_ProductFocus : HHPage
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
        this.egvProductFocus.DataSource = ProductFocusManager.GetList(FocusType.New);
        this.egvProductFocus.DataBind();
    }

    public override void OnPageLoaded()
    {
        this.PagePermission = "FocusModule-View";
        this.ShortTitle = "分类栏目管理";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
    }

    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "FocusModule-View";
        base.OnPermissionChecking(e);
    }
}
