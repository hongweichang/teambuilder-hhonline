using System;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using HHOnline.Common;

namespace HHOnline.Framework
{
    [Serializable]
    public class ExtendedAttributes : HHCopy
    {
        public ExtendedAttributes()
        {
        }

        NameValueCollection extendedAttributes = new NameValueCollection();

        public string GetExtendedAttribute(string name)
        {
            string returnValue = extendedAttributes[name];

            if (returnValue == null)
                return string.Empty;
            else
                return returnValue;
        }

        public void SetExtendedAttribute(string name, string value)
        {
            if ((value == null) || (value == string.Empty))
                extendedAttributes.Remove(name);
            else
                extendedAttributes[name] = value;
        }

        public int ExtendedAttributesCount
        {
            get { return extendedAttributes.Count; }
        }

        public bool GetBool(string name, bool defaultValue)
        {
            string b = GetExtendedAttribute(name);
            if (b == null || b.Trim().Length == 0)
                return defaultValue;
            try
            {
                return bool.Parse(b);
            }
            catch { }
            return defaultValue;
        }

        public int GetInt(string name, int defaultValue)
        {
            string i = GetExtendedAttribute(name);
            if (i == null || i.Trim().Length == 0)
                return defaultValue;

            return Int32.Parse(i);
        }

        public DateTime GetDateTime(string name, DateTime defaultValue)
        {
            string d = GetExtendedAttribute(name);
            if (d == null || d.Trim().Length == 0)
                return defaultValue;

            return DateTime.Parse(d);
        }

        public DateTime? GetDateTime(string name)
        {
            string d = GetExtendedAttribute(name);
            if (d == null || d.Trim().Length == 0)
                return null;
            return DateTime.Parse(d);
        }

        public string GetString(string name, string defaultValue)
        {
            string v = GetExtendedAttribute(name);
            return (string.IsNullOrEmpty(v)) ? defaultValue : v;
        }

        public override object Copy()
        {
            ExtendedAttributes ea = this.CreateNewInstance() as ExtendedAttributes;
            ea.extendedAttributes = new NameValueCollection(this.extendedAttributes);
            return ea;
        }

        #region Serialization

        public SerializerData GetSerializerData()
        {
            SerializerData data = new SerializerData();

            string keys = null;
            string values = null;

            Serializer.ConvertFromNameValueCollection(this.extendedAttributes, ref keys, ref values);
            data.Keys = keys;
            data.Values = values;

            return data;
        }

        public void SetSerializerData(SerializerData data)
        {
            if (this.extendedAttributes == null || this.extendedAttributes.Count == 0)
            {
                this.extendedAttributes = Serializer.ConvertToNameValueCollection(data.Keys, data.Values);
            }

            if (this.extendedAttributes == null)
                extendedAttributes = new NameValueCollection();
        }
        #endregion
    }
}
