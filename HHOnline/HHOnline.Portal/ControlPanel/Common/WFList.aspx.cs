﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class ControlPanel_Common_WFList : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) BindInfo();
    }
    void BindInfo()
    {
        FooterInfo fi = FooterInfos.FooterInfoGet();
        if (fi != null && !string.IsNullOrEmpty(fi.WFList))
        {
            txtAbout.Text = fi.WFList;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        FooterInfos.FooterInfoUpdate(FooterUpdateAction.WFList, txtAbout.Text.Trim());
        BindInfo();
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "业务流程";
        this.SetTabName(this.ShortTitle);
        this.SetTitle();
    }
}
