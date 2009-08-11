using System;
using System.Drawing;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Framework.Web;

public partial class ControlPanel_Site_ManagerCache : HHPage
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
        egvCaches.DataSource = CacheManager.GetCacheKeyValues();
        egvCaches.DataBind();
    }
    public void egvCaches_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        egvCaches.PageIndex = e.NewPageIndex;
        BindData();
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "缓存管理";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        CacheManager.Clear();
        BindData();
    }

    protected void egvCaches_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label CacheName = e.Row.FindControl("lblCacheName") as Label;
            Label CacheCount = e.Row.FindControl("lblCacheCount") as Label;
            KeyValue data = e.Row.DataItem as KeyValue;
            CacheName.Text = data.Text;
            CacheCount.Text = CacheManager.GetCacheCount(data.Name).ToString();
        }
    }
    protected void egvCaches_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string key = egvCaches.DataKeys[e.RowIndex].Value.ToString();
        CacheManager.Clear(key);
        BindData();
    }
}
