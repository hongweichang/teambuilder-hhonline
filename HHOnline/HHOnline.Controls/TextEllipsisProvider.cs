using System;
using System.Collections.Generic;
using System.Text;

/**
 * *by Jericho
 * */
namespace HHOnline.Controls
{
    /// <summary>
    /// 自定义模板内容格式化
    /// </summary>
    internal class TextEllipsisProvider : IFormatProvider, ICustomFormatter
    {
        #region -IFormatProvider Members-

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region -ICustomFormatter Members-

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            string text = arg.ToString();
            char str = format[0];
            if ('S' == str)
            {
                string len = format.Substring(1);
                int iLen = 0;
                bool result = Int32.TryParse(len, out iLen);
                if (iLen > 0)
                {
                    Encoding gb2312 = Encoding.GetEncoding("gb2312");
                    byte[] bytes = gb2312.GetBytes(text);
                    int left = iLen * 2;
                    if (bytes.Length > left)
                    {
                        if (bytes[left - 1] > 128 && bytes[left - 2] < 128)
                        {
                            left++;
                        }
                        byte[] subBytes = new byte[left];
                        Array.Copy(bytes, subBytes, left);
                        text = gb2312.GetString(subBytes) + "...";
                    }
                }
            }
            else if ('B' == str)
            {
                try
                {
                    if (bool.Parse(text))
                        text = "是";
                    else
                        text = "否";
                }
                catch
                {
                    text = "否";
                }
            }
            else if ('D' == str)
            {
                try
                {
                    DateTime dt = DateTime.Parse(text);
                    if (dt == DateTime.MinValue)
                        text = "--";
                }
                catch {
                    text = "--";
                }
            }
            return text;
        }

        #endregion
    }
}
