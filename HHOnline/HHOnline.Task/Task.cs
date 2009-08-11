using System;
using System.Xml;
using System.Xml.Serialization;

namespace HHOnline.Task
{
    [Serializable, XmlRoot("task")]
    public sealed class Task
    {
        private bool _enabled;
        private bool _enableShutDown;
        private bool _isRunning;
        private ITask _itask;
        private Type _jobType;
        private DateTime _lastEnd;
        private DateTime _lastStart;
        private DateTime _lastSucess;
        private string _name;
        private XmlNode _node;

        private Task()
        {
            this._enabled = true;
            this._enableShutDown = false;
            this._node = null;
        }

        internal Task(Type ijob, XmlNode node)
        {
            this._enabled = true;
            this._enableShutDown = false;
            this._node = null;
            this._node = node;
            this._jobType = ijob;
            if (!((node.Attributes["enabled"] == null) || bool.TryParse(node.Attributes["enabled"].Value, out this._enabled)))
            {
                this._enabled = true;
            }
            if (!((node.Attributes["enableShutDown"] == null) || bool.TryParse(node.Attributes["enableShutDown"].Value, out this._enableShutDown)))
            {
                this._enableShutDown = true;
            }
            if (node.Attributes["name"] != null)
            {
                this._name = node.Attributes["name"].Value;
            }
        }

        internal ITask CreateTaskInstance()
        {
            if (this.Enabled && (this._itask == null))
            {
                if (this._jobType != null)
                {
                    this._itask = Activator.CreateInstance(this._jobType) as ITask;
                }
                this._enabled = this._itask != null;
            }
            return this._itask;
        }

        internal void ExecuteTask()
        {
            this._isRunning = true;
            ITask task = this.CreateTaskInstance();
            if (task != null)
            {
                this._lastStart = DateTime.Now;
                try
                {
                    task.Execute(this._node);
                    this._lastEnd = this._lastSucess = DateTime.Now;
                }
                catch (Exception exception)
                {
                    this._enabled = !this.EnableShutDown;
                    this._lastEnd = DateTime.Now;
                    TaskApplication.Instance().ExecuteTaskExceptionEvents(this, exception);
                }
            }
            this._isRunning = false;
        }

        public bool Enabled
        {
            get
            {
                return this._enabled;
            }
        }

        public bool EnableShutDown
        {
            get
            {
                return this._enableShutDown;
            }
        }

        public bool IsRunning
        {
            get
            {
                return this._isRunning;
            }
        }

        public Type JobType
        {
            get
            {
                return this._jobType;
            }
        }

        public DateTime LastEnd
        {
            get
            {
                return this._lastEnd;
            }
        }

        public DateTime LastStarted
        {
            get
            {
                return this._lastStart;
            }
        }

        public DateTime LastSuccess
        {
            get
            {
                return this._lastSucess;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }
    }
}
