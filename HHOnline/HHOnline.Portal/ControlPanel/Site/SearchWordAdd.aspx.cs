using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Framework;

public partial class ControlPanel_Site_SearchWordAdd : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        WordStatistic ws = new WordStatistic();
        decimal i = 0;
        decimal.TryParse(txtCount.Text, out i);
        ws.HitCount = i;
        ws.SearchWord = txtSearchWord.Text.Trim();
        if (WordSearchManager.SaveStatistic(ws))
            base.ExecuteJs("msg('成功添加一条新的搜索关键词记录！',true)", false);
        else
            base.ExecuteJs("msg('增加搜索关键词记录时失败！')", false);
    }
    public override void OnPageLoaded()
    {
        this.PageInfoType = InfoType.IframeInfo;
        SetValidator(true, true, 5);
    }
    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "SearchWordModule-View";
        base.OnPermissionChecking(e);
    }
}
