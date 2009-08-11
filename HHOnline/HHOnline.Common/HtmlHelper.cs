using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HHOnline.Common
{
    public class HtmlHelper
    {
        private static Regex isWhitespace = new Regex("[^\\w&;#]", RegexOptions.Singleline | RegexOptions.Compiled);

        private static Regex htmlRegex = new Regex("<[^>]+>|\\&nbsp\\;", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static Regex spacer = new Regex(@"\s{2,}", RegexOptions.Compiled);

        /// <summary>
        /// 字符串截取
        /// </summary>
        /// <param name="text">string</param>
        /// <param name="charLimit">最大字符数</param>
        /// <returns>string</returns>
        public static string MaxLength(string text, int charLimit)
        {
            if (TypeHelper.IsNullOrEmpty(text))
                return string.Empty;

            if (charLimit >= text.Length)
                return text;

            Match match = isWhitespace.Match(text, charLimit);
            if (!match.Success)
                return text;
            else
                return text.Substring(0, match.Index);
        }

        /// <summary>
        /// 清除html标记
        /// </summary>
        /// <param name="html">html</param>
        /// <returns>string</returns>
        public static string RemoveHtml(string html)
        {
            return RemoveHtml(html, 0);
        }

        /// <summary>
        /// 清除html标记,并返回指定长度字符串
        /// </summary>
        /// <param name="html">html</param>
        /// <param name="charLimit">最大字符数</param>
        /// <returns>string</returns>
        public static string RemoveHtml(string html, int charLimit)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            string nonhtml = spacer.Replace(htmlRegex.Replace(html, " ").Trim(), " ");
            if (charLimit <= 0 || charLimit >= nonhtml.Length)
                return nonhtml;
            else
                return MaxLength(nonhtml, charLimit);
        }

        /// <summary>
        /// 替换所有Html 标记
        /// </summary>
        /// <param name="html"></param>
        /// <param name="enableMultiLine"></param>
        /// <returns></returns>
        public static string StripAllTags(string html, bool enableMultiLine)
        {
            return null;
        }
    }
}
