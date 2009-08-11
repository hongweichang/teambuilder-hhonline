using System.Xml;

namespace HHOnline.Framework
{
    public interface IGlobalModule
    {
        void Init(GlobalApplication context, XmlNode node);
    }
}
