using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class Pages_Common_Recruitment : HHPage
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
        if (fi != null && !string.IsNullOrEmpty(fi.Recruitment))
        {
            ltAbout.Text = fi.Recruitment;
        }
        else
        {
            ltAbout.Text = "暂无招聘信息。";
        }
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "人员招聘";
        this.SetTitle();
    }
}

