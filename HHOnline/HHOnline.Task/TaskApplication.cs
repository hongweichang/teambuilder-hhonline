using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HHOnline.Task
{
    /// <summary>
    /// Task Application
    /// </summary>
    public sealed class TaskApplication
    {
        private static object EventPostTaskRun = new object();
        private static object EventPreTaskRun = new object();
        private EventHandlerList Events = new EventHandlerList();
        private static object EventTaskException = new object();
        private static object EventTaskShutdown = new object();
        private static object EventTaskStartup = new object();
        private static TaskApplication instance = new TaskApplication();
        private Dictionary<string, ITaskModule> modules = new Dictionary<string, ITaskModule>();

        /// <summary>
        /// 任务运行结束事件
        /// </summary>
        public event TaskRunEventHandler PostTaskRun
        {
            add
            {
                this.Events.AddHandler(EventPostTaskRun, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventPostTaskRun, value);
            }
        }

        /// <summary>
        /// 任务运行前事件
        /// </summary>
        public event TaskRunEventHandler PreTaskRun
        {
            add
            {
                this.Events.AddHandler(EventPreTaskRun, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventPreTaskRun, value);
            }
        }

        /// <summary>
        /// 任务异常事件
        /// </summary>
        public event TaskExceptionEventHandler TaskException
        {
            add
            {
                this.Events.AddHandler(EventTaskException, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventTaskException, value);
            }
        }

        /// <summary>
        /// 后台任务结束事件
        /// </summary>
        public event TaskEventHandler TaskShutdown
        {
            add
            {
                this.Events.AddHandler(EventTaskShutdown, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventTaskShutdown, value);
            }
        }

        /// <summary>
        /// 后台任务开始事件
        /// </summary>
        public event TaskEventHandler TaskStartup
        {
            add
            {
                this.Events.AddHandler(EventTaskStartup, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventTaskStartup, value);
            }
        }

        private TaskApplication()
        {
        }

        internal void ExecutePostTaskRunEvents(Task task)
        {
            this.ExecuteTaskEvent(task, EventPostTaskRun);
        }

        internal void ExecutePreTaskRunEvents(Task task)
        {
            this.ExecuteTaskEvent(task, EventPreTaskRun);
        }

        private void ExecuteTaskEvent(object EventKey)
        {
            TaskEventHandler handler = this.Events[EventKey] as TaskEventHandler;
            if (handler != null)
            {
                handler();
            }
        }

        internal void ExecuteTaskEvent(Task task, object EventKey)
        {
            TaskRunEventHandler handler = this.Events[EventKey] as TaskRunEventHandler;
            if (handler != null)
            {
                handler(task);
            }
        }

        internal void ExecuteTaskExceptionEvents(Task task, Exception exception)
        {
            TaskExceptionEventHandler handler = this.Events[EventTaskException] as TaskExceptionEventHandler;
            if (handler != null)
            {
                handler(task, exception);
            }
        }

        internal void ExecuteTaskShutdownEvents()
        {
            this.ExecuteTaskEvent(EventTaskShutdown);
        }

        internal void ExecuteTaskStartupEvents()
        {
            this.ExecuteTaskEvent(EventTaskStartup);
        }

        internal static TaskApplication Instance()
        {
            return instance;
        }

        internal Dictionary<string, ITaskModule> Modules
        {
            get
            {
                return this.modules;
            }
            set
            {
                this.modules = value;
            }
        }
    }
}
