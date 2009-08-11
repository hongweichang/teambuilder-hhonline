using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Framework
{
    /// <summary>
    /// 键值对
    /// </summary>
    [Serializable]
    public class KeyValue
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Text
        /// </summary>
        public string Text { get; set; }

        public KeyValue()
        { }

        public KeyValue(string name, string text)
        {
            this.Name = name;
            this.Text = text;
        }
    }
}
