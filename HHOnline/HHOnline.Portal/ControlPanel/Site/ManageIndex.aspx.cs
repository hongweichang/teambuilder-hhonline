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

    void BindData()
    {
        this.IndexReportRepeater.ItemDataBound += new RepeaterItemEventHandler(IndexReportRepeater_ItemDataBound);
        this.IndexReportRepeater.ItemCommand += new RepeaterCommandEventHandler(IndexReportRepeater_ItemCommand);
        this.IndexReportRepeater.DataSource = IndexReportManager.GetIndexReports();
        this.IndexReportRepeater.DataBind();
    }

    void IndexReportRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "BuildIndex")
        {
            IndexFileReport fileReport = e.Item.DataItem as IndexFileReport;
            if (fileReport != null)
            {
                fileReport.BuildIndex();
                BindData();
            }
        }
    }

    void IndexReportRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        IndexFileReport fileReport = e.Item.DataItem as IndexFileReport;
        if (fileReport != null)
        {
            Label lblIndexName = e.Item.FindControl("lblIndexName") as Label;
            Label lblIndexPath = e.Item.FindControl("lblIndexPath") as Label;
            Label lblIndexLength = e.Item.FindControl("lblIndexLength") as Label;
            Label lblIndexModify = e.Item.FindControl("lblIndexModify") as Label;
            Label lblIndexKey = e.Item.FindControl("lblIndexKey") as Label;
            lblIndexName.Text = fileReport.SearchName;
            lblIndexPath.Text = fileReport.PhysicalIndexDirectory;
            lblIndexLength.Text = GlobalSettings.FormatFriendlyFileSize(fileReport.IndexFileSize);
            lblIndexModify.Text = fileReport.LastModified.ToString("yyyy-MM-dd HH:mm");
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
