using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Framework.Web;
using HHOnline.SearchBarrel;


public partial class ControlPanel_Site_ManageIndex : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        this.IndexReportRepeater.DataSource = IndexReportManager.GetIndexReports();
        this.IndexReportRepeater.DataBind();
    }

    protected void IndexReportRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.CommandName == "BuildIndex")
            {
                IndexFileReport fileReport = e.Item.DataItem as IndexFileReport;
                if (fileReport == null)
                {
                    string key = "";
                    Label lblIndexKey = e.Item.FindControl("lblIndexKey") as Label;
                    key = lblIndexKey.Text.Trim();
                    if (string.IsNullOrEmpty(key))
                    {
                        Button lkBuild = e.Item.FindControl("lbBuildIndex") as Button;
                        key = lkBuild.CommandArgument;
                    }
                    fileReport = new IndexFileReport(key);
                }
                fileReport.BuildIndex();
                BindData();
            }
        }
    }

    protected void IndexReportRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            IndexFileReport fileReport = e.Item.DataItem as IndexFileReport;
            if (fileReport != null)
            {
                Label lblIndexName = e.Item.FindControl("lblIndexName") as Label;
                Label lblIndexPath = e.Item.FindControl("lblIndexPath") as Label;
                Label lblIndexLength = e.Item.FindControl("lblIndexLength") as Label;
                Label lblIndexModify = e.Item.FindControl("lblIndexModify") as Label;
                Label lblIndexKey = e.Item.FindControl("lblIndexKey") as Label;
                Button lkBuild = e.Item.FindControl("lbBuildIndex") as Button;
                lblIndexKey.Text = fileReport.SearchKey;
                if (lkBuild != null)
                    lkBuild.CommandArgument = fileReport.SearchKey;
                lblIndexName.Text = fileReport.SearchName;
                lblIndexPath.Text = fileReport.PhysicalIndexDirectory;
                lblIndexLength.Text = GlobalSettings.FormatFriendlyFileSize(fileReport.IndexFileSize);
                lblIndexModify.Text = fileReport.LastModified.ToString("yyyy-MM-dd HH:mm");
            }
        }
    }

    protected void lbBuildAllIndex_Click(object sender, EventArgs e)
    {
        IndexReportManager.BuildIndex();
        BindData();
    }

    public override void OnPageLoaded()
    {
        this.ShortTitle = "索引管理";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
    }
}
