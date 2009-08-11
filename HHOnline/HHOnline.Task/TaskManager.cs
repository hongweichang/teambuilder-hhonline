using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace HHOnline.Task
{
    public sealed class TaskManager
    {
        private static readonly TaskManager taskManager = null;

        private List<TaskThread> taskThreads = new List<TaskThread>();

        public IList<TaskThread> TaskThreads { get { return taskThreads; } }

        static TaskManager()
        {
            taskManager = new TaskManager();
        }

        private TaskManager() { }

        public static TaskManager Initialize(string configFile)
        {
            return Initialize(configFile, "Tasks");
        }

        public static TaskManager Initialize(XmlNode node)
        {
            taskManager.LoadConfiguration(node);
            return taskManager;
        }

        public static TaskManager Initialize(string configFile, string nodePath)
        {
            XmlDocument document = new XmlDocument();
            document.Load(configFile);
            return Initialize(document.SelectSingleNode(nodePath));
        }

        public static TaskManager Instance()
        {
            return taskManager;
        }

        private void LoadConfiguration(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                string nodeName = child.Name.ToLower();
                if (nodeName != null)
                {
                    if (!nodeName.Equals("modules", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (nodeName.Equals("threads", StringComparison.CurrentCultureIgnoreCase))
                        {
                            this.LoadThreads(child);
                        }
                    }
                    else
                    {
                        this.LoadModules(child);
                    }
                }
            }
        }

        private void LoadModules(XmlNode modulesNode)
        {
            TaskApplication taskApplication = TaskApplication.Instance();
            XmlAttribute attribute;
            string value;
            foreach (XmlNode node in modulesNode.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Comment)
                {
                    continue;
                }
                string name = node.Name;
                if (name != null)
                {
                    if (name != "clear")
                    {
                        if (name == "remove")
                        {
                            attribute = node.Attributes["name"];
                            value = (attribute == null) ? null : attribute.Value;
                            if (!string.IsNullOrEmpty(value) && taskApplication.Modules.ContainsKey(value))
                            {
                                taskApplication.Modules.Remove(value);
                            }
                        }
                        if (name == "add")
                        {
                            attribute = node.Attributes["enabled"];
                            if ((attribute == null) || (attribute.Value != "false"))
                            {
                                XmlAttribute attName = node.Attributes["name"];
                                XmlAttribute attType = node.Attributes["type"];
                                string strAttName = (attName == null) ? null : attName.Value;
                                string strAttType = (attType == null) ? null : attType.Value;
                                if (!string.IsNullOrEmpty(strAttName) && !string.IsNullOrEmpty(strAttType))
                                {
                                    Type type = Type.GetType(strAttType);
                                    if (type != null)
                                    {
                                        ITaskModule module = Activator.CreateInstance(type) as ITaskModule;
                                        if (module != null)
                                        {
                                            module.Init(taskApplication, node);
                                            taskApplication.Modules[strAttName] = module;
                                        }
                                    }
                                }
                            }
                        }
                        continue;
                    }
                    taskApplication.Modules.Clear();
                }
            }
        }

        private void LoadThreads(XmlNode tasksNode)
        {
            this.taskThreads.Clear();
            foreach (XmlNode node in tasksNode.ChildNodes)
            {
                if (node.Name.ToLower() == "thread")
                {
                    TaskThread item = new TaskThread(node);
                    this.taskThreads.Add(item);
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name.ToLower() == "task")
                        {
                            XmlAttribute attribute = child.Attributes["type"];
                            Type ijob = Type.GetType(attribute.Value);
                            if (ijob != null)
                            {
                                Task task = new Task(ijob, child);
                                item.AddTask(task);
                            }
                        }
                    }
                    continue;
                }
            }

        }

        public void Start()
        {
            TaskApplication.Instance().ExecuteTaskStartupEvents();
            foreach (TaskThread thread in this.taskThreads)
            {
                thread.InitializeTimer();
            }
        }

        public void Stop()
        {
            foreach (TaskThread thread in this.taskThreads)
            {
                thread.Dispose();
            }
            this.taskThreads.Clear();
            TaskApplication.Instance().ExecuteTaskShutdownEvents();
        }
    }
}
