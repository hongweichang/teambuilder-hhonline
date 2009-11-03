using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Framework.Web;

public partial class Pages_Common_RightNotice : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindInfos();
        }
    }
    void BindInfos()
    {
        FooterInfo fi = FooterInfos.FooterInfoGet();
        if (fi != null && !string.IsNullOrEmpty(fi.RightNotice))
        {
            ltAbout.Text = fi.RightNotice;
        }
        else
        {
            ltAbout.Text = "暂无介绍。";
        }
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "版权声明";
        this.SetTitle();
    }
}
