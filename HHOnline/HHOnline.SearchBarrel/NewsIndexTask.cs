using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using HHOnline.Task;
using HHOnline.Framework;

namespace HHOnline.SearchBarrel
{
    public class NewsIndexTask : ITask
    {
        public void Execute(XmlNode node)
        {
            NewsSearchManager.InitializeIndex();
        }
    }
}
