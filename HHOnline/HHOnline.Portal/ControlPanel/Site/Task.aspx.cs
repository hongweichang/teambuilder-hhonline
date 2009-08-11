using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Task;
using HHOnline.Controls;

public partial class ControlPanel_Site_Task : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            BindData();
        }
    }

    public override void OnPageLoaded()
    {
        this.ShortTitle = "任务查看";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
    }

    void BindData()
    {
        ThreadsRepeater.DataSource = TaskManager.Instance().TaskThreads;
        ThreadsRepeater.DataBind();
    }

    protected void ThreadsRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        ExtensionGridView tasks = e.Item.FindControl("egvTasks") as ExtensionGridView;
        if (tasks != null)
        {
            tasks.DataSource = (e.Item.DataItem as TaskThread).Tasks;
            tasks.DataBind();
        }
    }

    int threadCount = 1;

    protected void ThreadsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        TaskThread taskThread = e.Item.DataItem as TaskThread;
        if (taskThread == null)
            return;

        switch (e.Item.ItemType)
        {
            case ListItemType.AlternatingItem:
            case ListItemType.Item:
                Label ThreadTitle = e.Item.FindControl("ThreadTitle") as Label;
                Label Created = e.Item.FindControl("Created") as Label;
                Label LastStart = e.Item.FindControl("LastStart") as Label;
                Label LastStop = e.Item.FindControl("LastStop") as Label;
                Label IsRunning = e.Item.FindControl("IsRunning") as Label;
                Label Minutes = e.Item.FindControl("Minutes") as Label;
                ThreadTitle.Text = string.Format("任务线程#{0}统计", threadCount++);
                Created.Text = taskThread.Created == DateTime.MinValue ? "--" : taskThread.Created.ToString();
                LastStart.Text = taskThread.Started == DateTime.MinValue ? "--" : taskThread.Started.ToString();
                LastStop.Text = taskThread.Completed == DateTime.MinValue ? "--" : taskThread.Completed.ToString();
                IsRunning.Text = taskThread.IsRunning ? "是" : "否";
                Minutes.Text = (taskThread.Interval / 60000).ToString() + "分钟";
                break;
        }
    }

    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "TaskModule-View";
        base.OnPermissionChecking(e);
    }
}
