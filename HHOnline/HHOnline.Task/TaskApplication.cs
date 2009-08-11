using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HHOnline.Task
{
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

        public event TaskEventHandler PostTaskRun
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

        public event TaskEventHandler PreTaskRun
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

        internal void ExecutePostTaskRunEvents()
        {
            this.ExecuteTaskEvent(EventPostTaskRun);
        }

        internal void ExecutePreTaskRunEvents()
        {
            this.ExecuteTaskEvent(EventPreTaskRun);
        }

        private void ExecuteTaskEvent(object EventKey)
        {
            TaskEventHandler handler = this.Events[EventKey] as TaskEventHandler;
            if (handler != null)
            {
                handler();
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
