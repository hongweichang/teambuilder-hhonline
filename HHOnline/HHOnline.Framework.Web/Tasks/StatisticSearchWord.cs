using System;
using System.Xml;
using HHOnline.Task;

namespace HHOnline.Framework.Web.Tasks
{
    public class StatisticSearchWord : ITask
    {

        public void Execute(XmlNode node)
        {
            WordSearchManager.Statistic();
        }

    }
}
