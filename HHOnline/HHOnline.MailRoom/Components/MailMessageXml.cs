using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using HHOnline.Common;

namespace HHOnline.MailRoom
{
    [Serializable, XmlRoot("MailMessage")]
    public class MailMessageXml : MailMessage, IXmlSerializable
    {
        #region cntor
        public MailMessageXml() { }

        public MailMessageXml(MailMessage mailMessage)
        {
            TypeHelper.CopyCollection<AlternateView>(mailMessage.AlternateViews, base.AlternateViews);
            TypeHelper.CopyCollection<Attachment>(mailMessage.Attachments, base.Attachments);
            TypeHelper.CopyCollection<MailAddress>(mailMessage.Bcc, base.Bcc);
            TypeHelper.CopyCollection<MailAddress>(mailMessage.CC, base.CC);
            TypeHelper.CopyCollection(mailMessage.Headers, base.Headers);
            TypeHelper.CopyCollection<MailAddress>(mailMessage.To, base.To);
            base.Body = mailMessage.Body;
            base.BodyEncoding = mailMessage.BodyEncoding;
            base.DeliveryNotificationOptions = mailMessage.DeliveryNotificationOptions;
            base.From = mailMessage.From;
            base.IsBodyHtml = mailMessage.IsBodyHtml;
            base.Priority = mailMessage.Priority;
            base.ReplyTo = mailMessage.ReplyTo;
            base.Sender = mailMessage.Sender;
            base.Subject = mailMessage.Subject;
            base.SubjectEncoding = mailMessage.SubjectEncoding;
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }
        #endregion

        #region Read Attachment
        public static bool GetCollectionValue(XmlNode xml, string xpath, out IList<Attachment> objCollection)
        {
            XmlNodeList nodes = XmlLoader.GetNodes(xml, xpath);
            if (nodes != null)
            {
                objCollection = new List<Attachment>();
                foreach (XmlNode node in nodes)
                {
                    try
                    {
                        Attachment attachment;
                        if (GetAttachmentValue(node, out attachment))
                        {
                            objCollection.Add(attachment);
                        }
                        continue;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.ToString());
                        continue;
                    }
                }
                return (objCollection.Count > 0);
            }
            objCollection = null;
            return false;
        }

        public static bool GetAttachmentValue(XmlNode node, out Attachment result)
        {
            if ((node != null) && (node.InnerText != string.Empty))
            {
                try
                {
                    string bytes;
                    string name;
                    string mediaType;
                    if ((XmlLoader.GetStringValue(node, "Bytes", out bytes) &&
                        XmlLoader.GetStringValue(node, "Name", out name)) &&
                        XmlLoader.GetStringValue(node, "MediaType", out mediaType))
                    {
                        int num;
                        MemoryStream contentStream = new MemoryStream(Serializer.ConvertToBytes(bytes));
                        result = new Attachment(contentStream, XmlLoader.XMLDecode(name), XmlLoader.XMLDecode(mediaType));
                        if (XmlLoader.GetIntValue(node, "NameEncoding", out num))
                        {
                            result.NameEncoding = Encoding.GetEncoding(num);
                        }
                        return true;
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                }
            }
            result = null;
            return false;
        }
        #endregion

        #region Write Attachment
        public static string AttachmentNode(Attachment attachment)
        {
            if (((attachment == null) || (attachment.ContentStream == null)) || (attachment.ContentStream.Length == 0L))
            {
                return string.Empty;
            }
            StringBuilder output = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            XmlWriter writer = XmlWriter.Create(output, settings);
            writer.WriteStartElement("Attachment");
            writer.WriteStartElement("Name");
            writer.WriteString(XmlLoader.XMLEncode(attachment.Name));
            writer.WriteEndElement();
            writer.WriteStartElement("MediaType");
            writer.WriteString(XmlLoader.XMLEncode(attachment.ContentType.MediaType));
            writer.WriteEndElement();
            writer.WriteStartElement("NameEncoding");
            writer.WriteString(XmlLoader.XMLEncode(attachment.TransferEncoding.ToString()));
            writer.WriteEndElement();
            writer.WriteStartElement("Bytes");
            byte[] buffer = new byte[attachment.ContentStream.Length];
            attachment.ContentStream.Read(buffer, 0, (int)attachment.ContentStream.Length);
            writer.WriteString(Serializer.ConvertToString(buffer));
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Close();
            return output.ToString();
        }

        public static bool SetNode(ref XmlNode xml, string xpath, AttachmentCollection objCollection)
        {
            XmlNode node = XmlLoader.GetNode(xml, xpath);
            if ((node == null) || (objCollection == null))
            {
                return false;
            }
            StringBuilder builder = new StringBuilder();
            foreach (Attachment attachment in objCollection)
            {
                try
                {
                    builder.Append(AttachmentNode(attachment));
                    continue;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                    continue;
                }
            }
            node.InnerXml = builder.ToString();
            return true;
        }
        #endregion

        public static string ToXml(MailMessage message)
        {
            return Serializer.ConvertToString(new MailMessageXml(message));
        }

        #region IXmlSerializable
        public void ReadXml(XmlReader reader)
        {
            string str;
            object obj;
            int num;
            bool flag;
            NameValueCollection values;
            IList<Attachment> attachment;
            ICollection<MailAddressXml> address;
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            XmlNode xml = doc.SelectSingleNode("MailMessage");
            if (XmlLoader.GetStringValue(xml, "Body", out str))
                base.Body = str;
            if (XmlLoader.GetStringValue(xml, "Subject", out str))
                base.Subject = str;
            if (XmlLoader.GetCollectionValue<MailAddressXml>(xml, "Bcc/*", new MailAddressXml(), out address))
                MailAddressXml.CopyCollection(address, base.Bcc);
            if (XmlLoader.GetCollectionValue<MailAddressXml>(xml, "Cc/MailAddress", new MailAddressXml(), out address))
                MailAddressXml.CopyCollection(address, base.CC);
            if (XmlLoader.GetCollectionValue<MailAddressXml>(xml, "To/MailAddress", new MailAddressXml(), out address))
                MailAddressXml.CopyCollection(address, base.To);
            if (XmlLoader.GetSerializedObject(xml, "From/MailAddress", new MailAddressXml(), out obj))
            {
                base.From = ((MailAddressXml)obj).MailAddress;
            }
            if (XmlLoader.GetSerializedObject(xml, "ReplyTo/MailAddress", new MailAddressXml(), out obj))
            {
                base.ReplyTo = ((MailAddressXml)obj).MailAddress;
            }
            if (XmlLoader.GetSerializedObject(xml, "Sender/MailAddress", new MailAddressXml(), out obj))
            {
                base.Sender = ((MailAddressXml)obj).MailAddress;
            }
            if (XmlLoader.GetEnumValue(xml, "Priority", typeof(MailPriority), out obj))
            {
                base.Priority = (MailPriority)obj;
            }
            if (XmlLoader.GetBoolValue(xml, "IsBodyHtml", out flag))
            {
                base.IsBodyHtml = flag;
            }
            if (XmlLoader.GetCollectionValue(xml, "Headers", out values))
            {
                TypeHelper.CopyCollection(values, base.Headers);
            }
            if (XmlLoader.GetEnumValue(xml, "DeliveryNotificationOptions", typeof(DeliveryNotificationOptions), out obj))
            {
                base.DeliveryNotificationOptions = (DeliveryNotificationOptions)obj;
            }
            if (XmlLoader.GetIntValue(xml, "BodyEncoding", out num))
            {
                base.BodyEncoding = Encoding.GetEncoding(num);
            }
            if (XmlLoader.GetIntValue(xml, "SubjectEncoding", out num))
            {
                base.SubjectEncoding = Encoding.GetEncoding(num);
            }
            if (GetCollectionValue(xml, "Attachments/Attachment", out attachment))
            {
                TypeHelper.CopyCollection<Attachment>(attachment, base.Attachments);
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlDocument xmlTemplate = XmlLoader.GetXmlTemplate(base.GetType(), 0);
            XmlNode xml = xmlTemplate.SelectSingleNode("MailMessage");
            XmlLoader.SetNode(ref xml, "Body", base.Body);
            XmlLoader.SetNode(ref xml, "Subject", base.Subject);
            XmlLoader.SetNode<MailAddressXml>(ref xml, "Bcc", "MailAddress", MailAddressXml.CopyCollection(base.Bcc));
            XmlLoader.SetNode<MailAddressXml>(ref xml, "Cc", "MailAddress", MailAddressXml.CopyCollection(base.CC));
            XmlLoader.SetNode<MailAddressXml>(ref xml, "To", "MailAddress", MailAddressXml.CopyCollection(base.To));
            XmlLoader.SetNode(ref xml, "From/MailAddress", (IXmlSerializable)new MailAddressXml(base.From));
            XmlLoader.SetNode(ref xml, "Sender/MailAddress", (IXmlSerializable)new MailAddressXml(base.Sender));
            XmlLoader.SetNode(ref xml, "ReplyTo/MailAddress", (IXmlSerializable)new MailAddressXml(base.ReplyTo));
            XmlLoader.SetNode(ref xml, "Priority", base.Priority);
            XmlLoader.SetNode(ref xml, "IsBodyHtml", base.IsBodyHtml);
            XmlLoader.SetNode(ref xml, "Headers", base.Headers);
            XmlLoader.SetNode(ref xml, "DeliveryNotificationOptions", base.DeliveryNotificationOptions);
            if (base.BodyEncoding != null)
            {
                XmlLoader.SetNode(ref xml, "BodyEncoding", base.BodyEncoding.CodePage);
            }
            if (base.SubjectEncoding != null)
            {
                XmlLoader.SetNode(ref xml, "SubjectEncoding", base.SubjectEncoding.CodePage);
            }
            SetNode(ref xml, "Attachments", base.Attachments);
            XmlLoader.WriteInnerXml(xmlTemplate, writer);
        }
        #endregion
    }
}
