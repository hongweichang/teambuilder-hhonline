using System;
using System.Xml;

namespace HHOnline.Task
{
    /// <summary>
    /// 所有任务事件处理程序必须执行此接口
    /// </summary>
    public interface ITaskModule
    {
        void Init(TaskApplication taskApplication, XmlNode node);
    }
}
