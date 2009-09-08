using System;
using System.Xml;

namespace HHOnline.Task
{
    /// <summary>
    /// 所有后台任务必须继承此接口
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// 运行时间
        /// </summary>
        /// <param name="node"></param>
        void Execute(XmlNode node);
    }
}
