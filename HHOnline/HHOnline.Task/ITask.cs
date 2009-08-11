using System;
using System.Xml;

namespace HHOnline.Task
{
    public interface ITask
    {
        void Execute(XmlNode node);
    }
}
