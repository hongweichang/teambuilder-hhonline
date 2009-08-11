using System;
using System.Web;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace HHOnline.Common
{
    public class WebHelper
    {
        #region Members
        public static Regex _escapePeriod = new Regex(@"(?:\.config|\.ascx|\.asax|\.cs|\.vb)$", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static Regex _fileComponentTextToEscape = new Regex(@"([^A-Za-z0-9 ]+|\.| )", RegexOptions.Singleline | RegexOptions.Compiled);
        public static Regex _fileComponentTextToUnescape = new Regex(@"((?:_(?:[0-9a-f][0-9a-f][0-9a-f][0-9a-f])+_)|_|\-)", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static Regex _pathComponentTextToEscape = new Regex(@"([^A-Za-z0-9\- ]+|\.| )", RegexOptions.Singleline | RegexOptions.Compiled);
        public static Regex _pathComponentTextToUnescape = new Regex(@"((?:_(?:[0-9a-f][0-9a-f][0-9a-f][0-9a-f])+_)|\+)", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static Regex _strayAmpRegex = new Regex("&(?!(?:#[0-9]{2,4};|[a-z0-9]+;))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        #endregion

        #region AppendQuerystring
        public static string AppendQuerystring(string url, string querystring)
        {
            return AppendQuerystring(url, querystring, false);
        }

        public static string AppendQuerystring(string url, string querystring, bool urlEncoded)
        {
            string str = "?";
            if (url.IndexOf('?') > -1)
            {
                if (!urlEncoded)
                {
                    str = "&";
                }
                else
                {
                    str = "&amp;";
                }
            }
            return (url + str + querystring);
        }
        #endregion

        #region EnsureHtmlEncoded
        public static string EnsureHtmlEncoded(string text)
        {
            text = _strayAmpRegex.Replace(text, "&amp;");
            text = text.Replace("\"", "&quot;");
            text = text.Replace("'", "&#39;");
            text = text.Replace("<", "&lt;");
            text = text.Replace(">", "&gt;");
            return text;
        }
        #endregion

        #region FullPath
        public static string FullPath(string local)
        {
            if (TypeHelper.IsNullOrEmpty(local))
            {
                return local;
            }
            if (local.ToLower().StartsWith("http://") || local.ToLower().StartsWith("https://"))
            {
                return local;
            }
            if (HttpContext.Current == null)
            {
                return local;
            }
            return FullPath(HostPath(HttpContext.Current.Request.Url), local);
        }

        public static string FullPath(string hostPath, string local)
        {
            return (hostPath + local);
        }

        public static string HostPath(Uri uri)
        {
            string str = (uri.Port == 80) ? string.Empty : (":" + uri.Port.ToString());
            return string.Format("{0}://{1}{2}", uri.Scheme, uri.Host, str);
        }
        #endregion

        #region Html De/Encode
        public static string HtmlDecode(string textToFormat)
        {
            if (TypeHelper.IsNullOrEmpty(textToFormat))
            {
                return textToFormat;
            }
            return HttpUtility.HtmlDecode(textToFormat);
        }

        public static string HtmlEncode(string textToFormat)
        {
            if (TypeHelper.IsNullOrEmpty(textToFormat))
            {
                return textToFormat;
            }
            return HttpUtility.HtmlEncode(textToFormat);
        }
        #endregion

        #region Return404
        public static void Return404(HttpContext Context)
        {
            Context.Response.StatusCode = 0x194;
            Context.Response.SuppressContent = true;
            Context.Response.End();
        }
        #endregion

        #region UrlDecode
        public static string UrlDecode(string urlToDecode)
        {
            return UrlDecode(urlToDecode, Encoding.UTF8);
        }

        public static string UrlDecode(string urlToDecode, Encoding e)
        {
            if (TypeHelper.IsNullOrEmpty(urlToDecode))
            {
                return urlToDecode;
            }
            return HttpUtility.UrlDecode(urlToDecode, e);
        }

        private static string UrlDecode(string text, Regex pattern)
        {
            if (TypeHelper.IsNullOrEmpty(text))
            {
                return text;
            }
            Match match = pattern.Match(text);
            StringBuilder builder = new StringBuilder();
            int startIndex = 0;
            while (match.Value != string.Empty)
            {
                if (startIndex != match.Index)
                {
                    builder.Append(text.Substring(startIndex, match.Index - startIndex));
                }
                if (match.Value.Length == 1)
                {
                    builder.Append(" ");
                }
                else
                {
                    byte[] bytes = new byte[(match.Value.Length - 2) / 2];
                    for (int i = 1; i < (match.Value.Length - 1); i += 2)
                    {
                        bytes[(i - 1) / 2] = byte.Parse(match.Value.Substring(i, 2), NumberStyles.AllowHexSpecifier);
                    }
                    builder.Append(Encoding.Unicode.GetString(bytes));
                }
                startIndex = match.Index + match.Length;
                match = pattern.Match(text, startIndex);
            }
            if (startIndex < text.Length)
            {
                builder.Append(text.Substring(startIndex));
            }
            return builder.ToString();
        }

        public static string UrlDecodeFileComponent(string text)
        {
            return UrlDecode(text, _fileComponentTextToUnescape);
        }

        public static string UrlDecodePathComponent(string text)
        {
            return UrlDecode(text, _pathComponentTextToUnescape);
        }
        #endregion

        #region UrlEncode
        public static string UrlEncode(string urlToEncode)
        {
            return UrlEncode(urlToEncode, Encoding.UTF8);
        }

        public static string UrlEncode(string urlToEncode, Encoding e)
        {
            if (TypeHelper.IsNullOrEmpty(urlToEncode))
            {
                return urlToEncode;
            }
            return HttpUtility.UrlEncode(urlToEncode, e).Replace("'", "%27");
        }

        private static string UrlEncode(string text, Regex pattern, char spaceReplacement, char escapePrefix)
        {
            if (TypeHelper.IsNullOrEmpty(text))
            {
                return text;
            }
            Match match = pattern.Match(text);
            StringBuilder builder = new StringBuilder();
            int startIndex = 0;
            bool flag = true;
            //bool flag = _escapePeriod.IsMatch(text);
            while (match.Value != string.Empty)
            {
                if (startIndex != match.Index)
                {
                    builder.Append(text.Substring(startIndex, match.Index - startIndex));
                }
                if (match.Value == " ")
                {
                    builder.Append(spaceReplacement);
                }
                else if (((match.Value == ".") && (match.Index != (text.Length - 1))) && !flag)
                {
                    builder.Append(".");
                }
                else
                {
                    builder.Append(escapePrefix);
                    byte[] bytes = Encoding.Unicode.GetBytes(match.Value);
                    if (bytes != null)
                    {
                        foreach (byte num2 in bytes)
                        {
                            string str = num2.ToString("X");
                            if (str.Length == 1)
                            {
                                builder.Append("0");
                            }
                            builder.Append(str);
                        }
                    }
                    builder.Append(escapePrefix);
                }
                startIndex = match.Index + match.Length;
                match = pattern.Match(text, startIndex);
            }
            if (startIndex < text.Length)
            {
                builder.Append(text.Substring(startIndex));
            }
            return builder.ToString();
        }

        public static string UrlEncodeFileComponent(string text)
        {
            return UrlEncode(text, _fileComponentTextToEscape, '-', '_');
        }

        public static string UrlEncodePathComponent(string text)
        {
            return UrlEncode(text, _pathComponentTextToEscape, '+', '_');
        }
        #endregion

        #region ResolverUrl
        public static string ResolveUrl(string relativeUrl)
        {
            if (!TypeHelper.IsNullOrEmpty(relativeUrl) &&
                relativeUrl.StartsWith("~/"))
            {
                string[] strArray = relativeUrl.Split(new char[] { '?' });
                string str = VirtualPathUtility.ToAbsolute(strArray[0]);
                if (strArray.Length > 1)
                {
                    str = str + "?" + strArray[1];
                }
                return str;
            }
            return relativeUrl;
        }
        #endregion

        #region ApplicationPath
        public static string ApplicationPath
        {

            get
            {
                string applicationPath = "/";

                if (HttpContext.Current != null)
                    applicationPath = HttpContext.Current.Request.ApplicationPath;

                if (applicationPath == "/")
                {
                    return string.Empty;
                }
                else
                {
                    return applicationPath;
                }
            }
        }
        #endregion
    }
}
