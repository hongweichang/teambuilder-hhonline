using System;
using HHOnline.Task;
using log4net;

namespace HHOnline.Framework.Web.TaskModules
{

    public class TaskLogModule : ITaskModule
    {
        private static readonly ILog log = LogManager.GetLogger("TaskLogger");


        public void Init(TaskApplication taskApplication, System.Xml.XmlNode node)
        {
            taskApplication.TaskStartup += new TaskEventHandler(taskApplication_TaskStartup);
            taskApplication.TaskShutdown += new TaskEventHandler(taskApplication_TaskShutdown);
            taskApplication.PreTaskRun += new TaskRunEventHandler(taskApplication_PreTaskRun);
            taskApplication.PostTaskRun += new TaskRunEventHandler(taskApplication_PostTaskRun);
            taskApplication.TaskException += new TaskExceptionEventHandler(taskApplication_TaskException);
        }

        void taskApplication_PostTaskRun(HHOnline.Task.Task task)
        {
            log.Info("完成运行后台任务：" + task.JobType.ToString());
        }

        void taskApplication_PreTaskRun(HHOnline.Task.Task task)
        {
            log.Info("准备运行后台任务：" + task.JobType.ToString());
        }

        void taskApplication_TaskShutdown()
        {
            log.Info("后台任务停止运行");
        }

        void taskApplication_TaskStartup()
        {
            log.Info("后台任务开始运行");
        }

        void taskApplication_TaskException(HHOnline.Task.Task task, Exception exception)
        {
            log.Error(task, exception);
        }

    }
}
