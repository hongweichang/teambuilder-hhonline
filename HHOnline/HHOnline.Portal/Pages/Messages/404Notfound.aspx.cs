using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;

public partial class Pages_Messages_404Notfound : HHPage
{
    public string _goback = "javascript:{}";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.UrlReferrer != null)
        {
            _goback = Request.UrlReferrer.ToString();
        }
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "页面未找到";
        base.OnPageLoaded();
    }
}
