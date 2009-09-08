using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Xml;

namespace HHOnline.Task
{
    /// <summary>
    /// 任务集
    /// </summary>
    public class TaskThread : IDisposable
    {
        private DateTime completed;
        private DateTime created;
        private bool disposed;
        private int firstRun;
        private bool isRunning;
        private int minutes;
        private int seconds;
        private DateTime started;
        private Dictionary<string, Task> tasks;
        private Timer timer;

        private TaskThread()
        {
            this.timer = null;
            this.disposed = false;
            this.tasks = new Dictionary<string, Task>();
            this.minutes = 15;
            this.seconds = 0;
            this.firstRun = 0;
        }

        internal TaskThread(XmlNode node)
        {
            this.timer = null;
            this.disposed = false;
            this.tasks = new Dictionary<string, Task>();
            this.minutes = 15;
            this.seconds = 0;
            this.firstRun = 0;
            this.created = DateTime.Now;
            this.isRunning = false;
            if (!((node.Attributes["minutes"] == null) || int.TryParse(node.Attributes["minutes"].Value, out this.minutes)))
            {
                this.minutes = 15;
            }
            if (!((node.Attributes["seconds"] == null) || int.TryParse(node.Attributes["seconds"].Value, out this.seconds)))
            {
                this.seconds = 0;
            }
            if (!((node.Attributes["firstRun"] == null) || int.TryParse(node.Attributes["firstRun"].Value, out this.firstRun)))
            {
                this.firstRun = 0;
            }
        }

        internal void AddTask(Task task)
        {
            if (!this.tasks.ContainsKey(task.Name))
            {
                this.tasks.Add(task.Name, task);
            }
        }

        public void Dispose()
        {
            if ((this.timer != null) && !this.disposed)
            {
                lock (this)
                {
                    this.timer.Dispose();
                    this.timer = null;
                    this.disposed = true;
                }
            }
        }

        internal void ExecuteTasks()
        {
            this.started = DateTime.Now;
            this.isRunning = true;

            foreach (Task task in this.tasks.Values)
            {
                if (task.Enabled)
                {
                    TaskApplication.Instance().ExecutePreTaskRunEvents(task);
                    task.ExecuteTask();
                    TaskApplication.Instance().ExecutePostTaskRunEvents(task);
                }
            }

            this.isRunning = false;
            this.completed = DateTime.Now;
        }

        internal void InitializeTimer()
        {
            if (this.timer == null)
            {
                this.timer = new Timer(new TimerCallback(this.timer_Callback), null, this.Interval, this.Interval);
            }
        }

        private void timer_Callback(object state)
        {
            this.timer.Change(-1, -1);
            this.firstRun = -1;
            this.ExecuteTasks();
            this.timer.Change(this.Interval, this.Interval);
        }

        public DateTime Completed
        {
            get
            {
                return this.completed;
            }
        }

        public DateTime Created
        {
            get
            {
                return this.created;
            }
        }

        internal Dictionary<string, Task> CurrentTasks
        {
            get
            {
                return this.tasks;
            }
        }

        public int FirstRun
        {
            get
            {
                return this.firstRun;
            }
        }

        public int Interval
        {
            get
            {
                if (this.firstRun > 0)
                {
                    return (this.firstRun * 1000);
                }
                if (this.seconds > 0)
                {
                    return (this.seconds * 1000);
                }
                return (this.Minutes * 60 * 1000);
            }
        }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
        }

        public int Minutes
        {
            get
            {
                return this.minutes;
            }
        }

        public int Seconds
        {
            get
            {
                return this.seconds;
            }
        }

        public DateTime Started
        {
            get
            {
                return this.started;
            }
        }

        public IList<Task> Tasks
        {
            get
            {
                List<Task> list = new List<Task>();
                foreach (Task task in this.tasks.Values)
                {
                    list.Add(task);
                }
                return new ReadOnlyCollection<Task>(list);
            }
        }
    }
}
