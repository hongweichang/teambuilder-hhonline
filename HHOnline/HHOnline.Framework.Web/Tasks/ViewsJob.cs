using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Task;

namespace HHOnline.Framework.Web.Tasks
{
    /// <summary>
    /// 计数更新后台任务
    /// </summary>
    public class ViewsJob : ITask
    {
        public void Execute(System.Xml.XmlNode node)
        {
            ViewsFactory.SaveQueue();
        }
    }
}
