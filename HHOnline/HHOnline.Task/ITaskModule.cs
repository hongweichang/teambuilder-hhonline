using System;
using System.Xml;

namespace HHOnline.Task
{
    public interface ITaskModule
    {
        void Init(TaskApplication taskApplication, XmlNode node);
    }
}
