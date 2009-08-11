using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Xml;
using System.Xml.Serialization;
using HHOnline.Common;

namespace HHOnline.MailRoom
{
    [Serializable, XmlRoot("MailAddress")]
    public class MailAddressXml : IXmlSerializable
    {
        private MailAddress mailAddress;

        public MailAddress MailAddress
        {
            get
            {
                return this.mailAddress;
            }
        }

        public MailAddressXml() { }

        public MailAddressXml(MailAddress mailAddress)
        {
            this.mailAddress = mailAddress;
        }

        public MailAddressXml(string address)
        {
            this.mailAddress = new MailAddress(address);
        }

        public MailAddressXml(string address, string displayName)
        {
            this.mailAddress = new MailAddress(address, displayName);
        }

        public static ICollection<MailAddressXml> CopyCollection(ICollection<MailAddress> source)
        {
            ICollection<MailAddressXml> destination = new Collection<MailAddressXml>();
            CopyCollection(source, destination);
            return destination;
        }

        public static void CopyCollection(ICollection<MailAddress> source, ICollection<MailAddressXml> destination)
        {
            destination.Clear();
            foreach (MailAddress address in source)
            {
                destination.Add(new MailAddressXml(address));
            }
        }

        public static void CopyCollection(ICollection<MailAddressXml> source, ICollection<MailAddress> destination)
        {
            destination.Clear();
            foreach (MailAddressXml xml in source)
            {
                destination.Add(xml.MailAddress);
            }
        }

        public static IList<string> GetAddresses(ICollection<MailAddress> source)
        {
            IList<string> list = new List<string>();
            foreach (MailAddress address in source)
            {
                list.Add(address.Address);
            }
            return list;
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            string address;
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            XmlNode xml = doc.SelectSingleNode("MailAddress");
            if (XmlLoader.GetStringValue(xml, "//Address", out address))
            {
                string displayname;
                if (XmlLoader.GetStringValue(xml, "//Name", out displayname))
                {
                    this.mailAddress = new MailAddress(address, displayname);
                }
                else
                {
                    this.mailAddress = new MailAddress(address);
                }
            }

        }

        public void WriteXml(XmlWriter writer)
        {
            if (this.mailAddress != null)
            {
                XmlDocument xmlTemplate = XmlLoader.GetXmlTemplate(base.GetType(), 0);
                XmlNode xml = xmlTemplate.SelectSingleNode("MailAddress");
                XmlLoader.SetNode(ref xml, "Address", this.mailAddress.Address);
                if (!TypeHelper.IsNullOrEmpty(this.mailAddress.DisplayName))
                {
                    XmlLoader.SetNode(ref xml, "Name", this.mailAddress.DisplayName);
                }
                XmlLoader.WriteInnerXml(xmlTemplate, writer);

            }
        }
    }
}
