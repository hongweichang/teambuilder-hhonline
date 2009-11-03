using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class Pages_Common_AbouteHuaho : HHPage
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
        if (fi != null && !string.IsNullOrEmpty(fi.AbouteHuaho))
        {
            ltAbout.Text = fi.AbouteHuaho;
        }
        else
        {
            ltAbout.Text = "暂无介绍。";
        }
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "了解华宏";
        this.SetTitle();
    }
}
