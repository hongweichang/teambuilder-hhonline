using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using HHOnline.Task;

namespace HHOnline.Framework.Web.Tasks
{
    public class NewsGatherTask : ITask
    {
        public void Execute(XmlNode node)
        {
            NewsGatherManager.GatherIndustryNews();
        }
    }
}
