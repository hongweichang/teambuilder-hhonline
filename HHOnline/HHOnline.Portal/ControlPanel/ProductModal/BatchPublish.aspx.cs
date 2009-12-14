using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;

public partial class ControlPanel_ProductModal_BatchDelete : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindJSData();
    }
    void BindJSData() {
        string ids = Request.QueryString["ids"];
        base.ExecuteJs("var __ids = '" + ids + "';var __action='publish';", false);
    }
    public override void OnPageLoaded()
    {
        this.PageInfoType = InfoType.IframeInfo;
        this.PagePermission = "ProductModule-Edit";
        this.AddJavaScriptInclude("scripts/pages/batchopts.aspx.js", false, false);
    }
}
