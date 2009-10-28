using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;
using HHOnline.Framework.Web.Enums;

public partial class ControlPanel_Common_FriendLinkEdit : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLinks();
        }
    }
    static FriendLink lnk = null;
    void BindLinks()
    {
        int id = 0;
        try
        {
            id = int.Parse(Request.QueryString["id"]);
            lnk = FriendLinks.FriendLinkGet(id);
            txtTitle.Text = lnk.Title;
            txtLink.Text = lnk.Url;
            ddlRank.SelectedIndex = lnk.Rank - 1;
        }
        catch (Exception ex)
        {
            throw new HHException(ExceptionType.NoMasterError, ex.Message);
        }
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        if (lnk == null)
        {
            throw new HHException(ExceptionType.NoMasterError, "无法完成信息修改！");
        }
        lnk.Title = txtTitle.Text.Trim();
        lnk.Url = txtLink.Text.Trim();
        lnk.Rank = int.Parse(ddlRank.SelectedValue);
        bool r = FriendLinks.FriendLinkUpdate(lnk);
        if (r)
            base.ExecuteJs("msg('成功修改此友情链接信息！',true)", false);
        else
            base.ExecuteJs("msg('修改友情链接信息失败，请联系管理员！')", false);
    }
    public override void OnPageLoaded()
    {
        this.PageInfoType = InfoType.PopWinInfo;
        this.PagePermission = "FriendLinkModule-Edit";
        SetValidator(true, true, 5);
    }
}