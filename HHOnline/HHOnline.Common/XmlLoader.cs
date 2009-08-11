using System;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace HHOnline.Common
{
    public class XmlLoader
    {
        private XmlLoader() { }

        #region Members
        private static string filePattern = "{0}.Xml.{1}.xml";
        private static string fileVersionPattern = "{0}.{1}";

        public static string FilePattern
        {
            get
            {
                return filePattern;
            }
        }

        public static string FileVersionPattern
        {
            get
            {
                return fileVersionPattern;
            }
        }
        #endregion

        #region GetXmlTemplate
        public static XmlDocument GetXmlTemplate(Type type)
        {
            return GetXmlTemplate(type, 0);
        }

        public static XmlDocument GetXmlTemplate(Type type, int version)
        {
            Assembly assembly = Assembly.GetAssembly(type);
            XmlDocument doc = new XmlDocument();
            Stream inStream = null;
            try
            {
                while ((inStream == null) && (version > -1))
                {
                    inStream = assembly.GetManifestResourceStream(GetResourceName(type, version--));
                    if (inStream != null)
                    {
                        doc.Load(inStream);
                    }

                }
            }
            finally
            {
                if (inStream != null)
                {
                    inStream.Close();
                }
            }
            return doc;
        }

        private static string GetResourceName(Type type, int version)
        {
            string name = type.Name;
            if (version > 0)
            {
                name = string.Format(FileVersionPattern, name, version);
            }
            return string.Format(FilePattern, type.Namespace, name);
        }
        #endregion

        #region GetNode
        public static XmlNodeList GetNodes(XmlNode xml, string xpath)
        {
            return xml.SelectNodes(xpath);
        }

        public static XmlNode GetNode(XmlNode xml, string xpath)
        {
            return xml.SelectSingleNode(xpath);
        }
        #endregion

        #region SetInnerXml
        public static bool SetInnerXml<T>(ref XmlNode xml, string localElementName, ICollection<T> objList)
        {
            if ((xml == null) || (objList == null))
            {
                return false;
            }
            StringBuilder builder = new StringBuilder();
            foreach (IXmlSerializable serializable in objList)
            {
                StringBuilder output = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.ConformanceLevel = ConformanceLevel.Fragment;
                XmlWriter writer = XmlWriter.Create(output, settings);
                writer.WriteStartElement(localElementName);
                serializable.WriteXml(writer);
                writer.WriteEndElement();
                writer.Close();
                builder.Append(output.ToString());
            }
            xml.InnerXml = builder.ToString();
            return (objList.Count > 0);
        }

        public static bool SetInnerXml(ref XmlNode xml, string localElementName, IDictionary<string, string> objDictionary)
        {
            if ((xml == null) || (objDictionary == null))
            {
                return false;
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str in objDictionary.Keys)
            {
                StringBuilder output = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.ConformanceLevel = ConformanceLevel.Fragment;
                XmlWriter writer = XmlWriter.Create(output, settings);
                writer.WriteStartElement(localElementName);
                writer.WriteStartElement("Key");
                writer.WriteString(XMLEncode(str));
                writer.WriteEndElement();
                writer.WriteStartElement("Value");
                writer.WriteString(XMLEncode(objDictionary[str]));
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Close();
                builder.Append(output.ToString());
            }
            xml.InnerXml = builder.ToString();
            return (objDictionary.Count > 0);
        }

        public static bool SetInnerXml(ref XmlNode xml, string localElementName, IList<byte[]> objList)
        {
            if ((xml == null) || (objList == null))
            {
                return false;
            }
            StringBuilder builder = new StringBuilder();
            foreach (byte[] buffer in objList)
            {
                StringBuilder output = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.ConformanceLevel = ConformanceLevel.Fragment;
                XmlWriter writer = XmlWriter.Create(output, settings);
                writer.WriteStartElement(localElementName);
                writer.WriteString(Serializer.ConvertToString(buffer));
                writer.WriteEndElement();
                writer.Close();
                builder.Append(output.ToString());
            }
            xml.InnerXml = builder.ToString();
            return (objList.Count > 0);
        }

        public static bool SetInnerXml<T>(ref XmlNode xml, string localElementName, IList<T> objList)
        {
            if ((xml == null) || (objList == null))
            {
                return false;
            }
            StringBuilder builder = new StringBuilder();
            foreach (IXmlSerializable serializable in objList)
            {
                StringBuilder output = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.ConformanceLevel = ConformanceLevel.Fragment;
                XmlWriter writer = XmlWriter.Create(output, settings);
                writer.WriteStartElement(localElementName);
                serializable.WriteXml(writer);
                writer.WriteEndElement();
                writer.Close();
                builder.Append(output.ToString());
            }
            xml.InnerXml = builder.ToString();
            return (objList.Count > 0);
        }

        public static bool SetInnerXml(ref XmlNode xml, string localElementName, IList<string> objList)
        {
            if ((xml == null) || (objList == null))
            {
                return false;
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str in objList)
            {
                StringBuilder output = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.ConformanceLevel = ConformanceLevel.Fragment;
                XmlWriter writer = XmlWriter.Create(output, settings);
                writer.WriteStartElement(localElementName);
                writer.WriteString(XMLEncode(str));
                writer.WriteEndElement();
                writer.Close();
                builder.Append(output.ToString());
            }
            xml.InnerXml = builder.ToString();
            return (objList.Count > 0);
        }

        public static bool SetInnerXml(ref XmlNode xml, string localElementName, IXmlSerializable obj)
        {
            if ((xml == null) || (obj == null))
            {
                return false;
            }
            StringBuilder output = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            XmlWriter writer = XmlWriter.Create(output, settings);
            if (!TypeHelper.IsNullOrEmpty(localElementName))
            {
                writer.WriteStartElement(localElementName);
            }
            obj.WriteXml(writer);
            if (!TypeHelper.IsNullOrEmpty(localElementName))
            {
                writer.WriteEndElement();
            }
            writer.Close();
            xml.InnerXml = output.ToString();
            return true;
        }
        #endregion

        #region SetNode
        public static bool SetNode(ref XmlNode xml, string xpath, NameValueCollection objCollection)
        {
            XmlNode node = GetNode(xml, xpath);
            if ((node == null) || (objCollection == null))
            {
                return false;
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str in objCollection.Keys)
            {
                StringBuilder output = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.ConformanceLevel = ConformanceLevel.Fragment;
                XmlWriter writer = XmlWriter.Create(output, settings);
                writer.WriteStartElement("NameValueCollection");
                writer.WriteStartElement("Key");
                writer.WriteString(XMLEncode(str));
                writer.WriteEndElement();
                writer.WriteStartElement("Value");
                writer.WriteString(XMLEncode(objCollection[str]));
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Close();
                builder.Append(output.ToString());
            }
            node.InnerXml = builder.ToString();
            return (objCollection.Count > 0);
        }

        public static bool SetNode(ref XmlNode xml, string xpath, DateTime value)
        {
            return SetNode(ref xml, xpath, value.ToUniversalTime().ToString("o"));
        }

        public static bool SetNode(ref XmlNode xml, string xpath, object value)
        {
            return ((value != null) && SetNode(ref xml, xpath, value.ToString()));
        }

        public static bool SetNode(ref XmlNode xml, string xpath, string value)
        {
            XmlNode node = xml.SelectSingleNode(xpath);
            if ((node != null) && (value != null))
            {
                node.InnerText = XMLEncode(value);
            }
            return ((node != null) && (value != null));
        }

        public static bool SetNode(ref XmlNode xml, string xpath, IXmlSerializable value)
        {
            XmlNode node = xml.SelectSingleNode(xpath);
            if (((value != null) && (node != null)) && (value != null))
            {
                return SetInnerXml(ref node, value.GetType().ToString(), value);
            }
            return ((node != null) && (value != null));
        }

        public static bool SetNode<T>(ref XmlNode xml, string xpath, string localElementName, ICollection<T> objList)
        {
            XmlNode node = xml.SelectSingleNode(xpath);
            return (((xml != null) && (objList != null)) && SetInnerXml<T>(ref node, localElementName, objList));
        }

        public static bool SetNode(ref XmlNode xml, string xpath, string localElementName, IDictionary<string, string> objDictionary)
        {
            XmlNode node = xml.SelectSingleNode(xpath);
            return (((xml != null) && (objDictionary != null)) && SetInnerXml(ref node, localElementName, objDictionary));
        }

        public static bool SetNode(ref XmlNode xml, string xpath, string localElementName, IList<string> objList)
        {
            XmlNode node = xml.SelectSingleNode(xpath);
            return (((xml != null) && (objList != null)) && SetInnerXml(ref node, localElementName, objList));
        }

        public static bool SetNode(ref XmlNode xml, string xpath, string localElementName, IList<byte[]> objList)
        {
            XmlNode node = xml.SelectSingleNode(xpath);
            return (((xml != null) && (objList != null)) && SetInnerXml(ref node, localElementName, objList));
        }

        public static bool SetNode<T>(ref XmlNode xml, string xpath, string localElementName, IList<T> objList)
        {
            XmlNode node = xml.SelectSingleNode(xpath);
            return (((xml != null) && (objList != null)) && SetInnerXml<T>(ref node, localElementName, objList));
        }
        #endregion

        #region WriteXml
        public static void WriteInnerXml(XmlDocument document, XmlWriter writer)
        {
            document.SelectSingleNode("/*").WriteContentTo(writer);
        }

        public static void WriteInnerXml(XmlNode xml, XmlWriter writer)
        {
            xml.WriteContentTo(writer);
        }
        #endregion

        #region GetValue
        public static bool GetCollectionValue(XmlNode xml, string xpath, out IDictionary<string, string> objDictionary)
        {
            XmlNodeList nodes = GetNodes(xml, xpath);
            if (nodes != null)
            {
                objDictionary = new Dictionary<string, string>();
                foreach (XmlNode node in nodes)
                {
                    try
                    {
                        XmlNode node2 = GetNode(node, "//Key");
                        XmlNode node3 = GetNode(node, "//Value");
                        objDictionary.Add(XMLDecode(node2.InnerText), XMLDecode(node3.InnerText));
                        continue;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                return (objDictionary.Count > 0);
            }
            objDictionary = null;
            return false;
        }

        public static bool GetCollectionValue(XmlNode xml, string xpath, out IList<string> objList)
        {
            XmlNodeList nodes = GetNodes(xml, xpath);
            if (nodes != null)
            {
                objList = new List<string>();
                foreach (XmlNode node in nodes)
                {
                    try
                    {
                        objList.Add(XMLDecode(node.InnerText));
                        continue;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                return (objList.Count > 0);
            }
            objList = null;
            return false;
        }
        public static bool GetCollectionValue(XmlNode xml, string xpath, out IList<byte[]> objList)
        {
            XmlNodeList nodes = GetNodes(xml, xpath);
            if (nodes != null)
            {
                objList = new List<byte[]>();
                foreach (XmlNode node in nodes)
                {
                    try
                    {
                        objList.Add(Serializer.ConvertToBytes(node.InnerText));
                        continue;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                return (objList.Count > 0);
            }
            objList = null;
            return false;
        }




        public static bool GetCollectionValue(XmlNode xml, string xpath, out NameValueCollection objCollection)
        {
            XmlNodeList nodes = GetNodes(xml, xpath);
            if (nodes != null)
            {
                objCollection = new NameValueCollection();
                foreach (XmlNode node in nodes)
                {
                    try
                    {
                        XmlNode node2 = GetNode(node, "//Key");
                        XmlNode node3 = GetNode(node, "//Value");
                        objCollection.Add(XMLDecode(node2.InnerText), XMLDecode(node3.InnerText));
                        continue;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                return (objCollection.Count > 0);
            }
            objCollection = null;
            return false;
        }

        public static bool GetCollectionValue<T>(XmlNode xml, string xpath, IXmlSerializable obj, out ICollection<T> objList)
        {
            XmlNodeList nodes = GetNodes(xml, xpath);
            if (nodes != null)
            {
                objList = new Collection<T>();
                foreach (XmlNode node in nodes)
                {
                    try
                    {
                        object obj2;
                        if (GetSerializedObject(node, obj, out obj2))
                        {
                            objList.Add((T)obj2);
                        }
                        continue;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                return (objList.Count > 0);
            }
            objList = null;
            return false;
        }

        public static bool GetCollectionValue<T>(XmlNode xml, string xpath, IXmlSerializable obj, out IList<T> objList)
        {
            XmlNodeList nodes = GetNodes(xml, xpath);
            if (nodes != null)
            {
                objList = new List<T>();
                foreach (XmlNode node in nodes)
                {
                    try
                    {
                        object obj2;
                        if (GetSerializedObject(node, obj, out obj2))
                        {
                            objList.Add((T)obj2);
                        }
                        continue;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                return (objList.Count > 0);
            }
            objList = null;
            return false;
        }

        public static bool GetSerializedObject(XmlNode xml, string xpath, IXmlSerializable obj, out object result)
        {
            XmlNode node = GetNode(xml, xpath);
            if ((node != null) && (node.InnerText != string.Empty))
            {
                return GetSerializedObject(node, obj, out result);
            }
            result = null;
            return false;
        }

        public static bool GetSerializedObject(XmlNode node, IXmlSerializable obj, out object result)
        {
            if ((node != null) && (node.OuterXml != string.Empty))
            {
                if (GetSerializedObject(node.InnerXml, obj, out result))
                {
                    return true;
                }
                if (GetSerializedObject(node.OuterXml, obj, out result))
                {
                    return true;
                }
            }
            result = null;
            return false;
        }

        public static bool GetSerializedObject(string xml, IXmlSerializable obj, out object result)
        {
            if ((xml != null) && (xml != string.Empty))
            {
                bool flag;
                try
                {
                    if (!xml.TrimStart().StartsWith("<?xml"))
                    {
                        xml = "<?xml version=\"1.0\" encoding=\"utf-16\" ?>" + xml;
                    }
                    result = Serializer.ConvertToObject(xml, obj.GetType());
                    if (result == null)
                    {
                        return false;
                    }
                    flag = true;
                }
                catch
                {
                    result = null;
                    flag = false;
                }
                return flag;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public static bool GetBoolValue(XmlNode xml, string xpath, out bool result)
        {
            XmlNode node = GetNode(xml, xpath);
            if ((node != null) && (node.InnerText != string.Empty))
            {
                return bool.TryParse(XMLDecode(node.InnerText), out result);
            }
            result = false;
            return false;
        }

        public static bool GetStringValue(XmlNode xml, string xpath, out string result)
        {
            XmlNode node = GetNode(xml, xpath);
            if (node != null)
            {
                result = XMLDecode(node.InnerText);
            }
            else
            {
                result = null;
            }
            return (node != null);
        }

        public static bool GetIntValue(XmlNode xml, string xpath, out int result)
        {
            XmlNode node = GetNode(xml, xpath);
            if ((node != null) && (node.InnerText != string.Empty))
            {
                return int.TryParse(XMLDecode(node.InnerText), out result);
            }
            result = -1;
            return false;
        }

        public static bool GetEnumValue(XmlNode xml, string xpath, Type type, out object result)
        {
            XmlNode node = GetNode(xml, xpath);
            if ((node != null) && (node.InnerText != string.Empty))
            {
                try
                {
                    result = Enum.Parse(type, XMLDecode(node.InnerText));
                    return true;
                }
                catch (Exception)
                {
                }
            }
            result = null;
            return false;
        }

        public static bool GetLongValue(XmlNode xml, string xpath, out long result)
        {
            XmlNode node = GetNode(xml, xpath);
            if ((node != null) && (node.InnerText != string.Empty))
            {
                return long.TryParse(XMLDecode(node.InnerText), out result);
            }
            result = -1L;
            return false;
        }

        public static bool GetDateTimeValue(XmlNode xml, string xpath, out DateTime result)
        {
            XmlNode node = GetNode(xml, xpath);
            if ((node != null) && (node.InnerText != string.Empty))
            {
                return DateTime.TryParse(XMLDecode(node.InnerText), out result);
            }
            result = DateTime.MinValue;
            return false;
        }
        #endregion

        #region XML De/Encode
        public static string XMLDecode(string xml)
        {
            int[] numArray = new int[] { 0x22, 0x26, 0x27, 0x3c, 0x3d, 0x3e };
            for (int i = 0; i < numArray.Length; i++)
            {
                xml = xml.Replace("&#" + numArray[i].ToString() + ";", ((char)numArray[i]).ToString());
            }
            return xml;
        }

        public static string XMLEncode(string xml)
        {
            //替换 " & ' < = >  
            int[] numArray = new int[] { 0x22, 0x26, 0x27, 0x3c, 0x3d, 0x3e };
            for (int i = 0; i < numArray.Length; i++)
            {
                xml = xml.Replace(((char)numArray[i]).ToString(), "&#" + numArray[i].ToString() + ";");
            }
            return xml;
        }
        #endregion
    }
}
