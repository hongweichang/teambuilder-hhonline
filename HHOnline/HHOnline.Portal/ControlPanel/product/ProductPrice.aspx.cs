using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Shops;

public partial class ControlPanel_product_ProductPrice : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override void OnPageLoaded()
    {
        this.PagePermission = "ProductModule-View";
        this.ShortTitle = "产品报价管理";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
    }

    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "ProductModule-View";
        //e.CheckPermissionControls.Add("ProductModule-Add", lbNewProduct);
        base.OnPermissionChecking(e);
    }
}
