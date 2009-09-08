using System;

namespace HHOnline.Task
{
    /// <summary>
    /// 任务处理程序
    /// </summary>
    public delegate void TaskEventHandler();

    /// <summary>
    /// 任务运行处理程序
    /// </summary>
    /// <param name="task">运行的任务</param>
    public delegate void TaskRunEventHandler(Task task);

    /// <summary>
    /// 任务异常处理程序
    /// </summary>
    /// <param name="task">运行的任务</param>
    /// <param name="exception">异常信息</param>
    public delegate void TaskExceptionEventHandler(Task task, Exception exception);
}
