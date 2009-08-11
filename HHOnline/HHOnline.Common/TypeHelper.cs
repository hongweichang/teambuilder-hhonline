using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace HHOnline.Common
{
    public class TypeHelper
    {
        public static void CopyCollection<T>(ICollection<T> source, ICollection<T> destination)
        {
            destination.Clear();
            foreach (T local in source)
            {
                destination.Add(local);
            }
        }

        public static void CopyCollection<T, S>(IDictionary<T, S> source, IDictionary<T, S> destination)
        {
            destination.Clear();
            foreach (T local in source.Keys)
            {
                destination.Add(local, source[local]);
            }
        }

        public static void CopyCollection(NameValueCollection source, NameValueCollection destination)
        {
            destination.Clear();
            foreach (string str in source.Keys)
            {
                destination.Add(str, source[str]);
            }
        }

        public static bool IsDate(string text)
        {
            if (!IsNullOrEmpty(text))
            {
                try
                {
                    DateTime.Parse(text);
                    return true;
                }
                catch (Exception)
                {
                }
            }
            return false;
        }

        public static bool IsNullOrEmpty(string text)
        {
            if (text != null)
            {
                return (text.Trim() == string.Empty);
            }
            return true;
        }

        public static string JoinIntArray(string separator, int[] intArray)
        {
            if ((intArray == null) || (intArray.Length <= 0))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i <= intArray.GetUpperBound(0); i++)
            {
                if (i != 0)
                {
                    builder.Append(separator);
                }
                builder.Append(intArray[i]);
            }
            return builder.ToString();
        }

        public static bool SafeBool(string text, bool defaultValue)
        {
            if (!IsNullOrEmpty(text))
            {
                try
                {
                    return bool.Parse(text);
                }
                catch (Exception)
                {
                }
            }
            return defaultValue;
        }

        public static int SafeInt(string text, int defaultValue)
        {
            if (!IsNullOrEmpty(text))
            {
                try
                {
                    return int.Parse(text);
                }
                catch (Exception)
                {
                }
            }
            return defaultValue;
        }

        public static string CleanSearchString(string searchString)
        {
            if (IsNullOrEmpty(searchString))
                return null;
            searchString = searchString.Replace("*", "%");

            //去掉Html
            searchString = HtmlHelper.RemoveHtml(searchString);

            //替换SQL字符
            searchString = Regex.Replace(searchString, "--|;|'|\"", " ", RegexOptions.Multiline );
            
            //替换多余的空格
            searchString = Regex.Replace(searchString, " {1,}", " ", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            return searchString;
        }

    }
}
