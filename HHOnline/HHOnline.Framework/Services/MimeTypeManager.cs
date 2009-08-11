using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Framework
{
    public class MimeTypeManager
    {
        private static Dictionary<string, string> mimeTypes = null;

        static MimeTypeManager()
        {
            mimeTypes = new Dictionary<string, string>();
            mimeTypes.Add("csv", "application/vnd.ms-excel");
            mimeTypes.Add("css", "text/css");
            mimeTypes.Add("js", "text/javascript");
            mimeTypes.Add("doc", "application/msword");
            mimeTypes.Add("docx", "application/msword");
            mimeTypes.Add("gif", "image/gif");
            mimeTypes.Add("bmp", "image/bmp");
            mimeTypes.Add("htm", "text/html");
            mimeTypes.Add("html", "text/html");
            mimeTypes.Add("jpeg", "image/jpeg");
            mimeTypes.Add("jpg", "image/jpeg");
            mimeTypes.Add("pdf", "application/pdf");
            mimeTypes.Add("png", "image/png");
            mimeTypes.Add("ppt", "application/vnd.ms-powerpoint");
            mimeTypes.Add("rtf", "application/msword");
            mimeTypes.Add("txt", "text/plain");
            mimeTypes.Add("xls", "application/vnd.ms-excel");
            mimeTypes.Add("xml", "text/xml");
            mimeTypes.Add("wmv", "video/x-ms-wmv");
            mimeTypes.Add("wma", "video/x-ms-wmv");
            mimeTypes.Add("mpeg", "video/mpeg");
            mimeTypes.Add("mpg", "video/mpeg");
            mimeTypes.Add("mpa", "video/mpeg");
            mimeTypes.Add("mpe", "video/mpeg");
            mimeTypes.Add("mov", "video/quicktime");
            mimeTypes.Add("qt", "video/quicktime");
            mimeTypes.Add("avi", "video/x-msvideo");
            mimeTypes.Add("asf", "video/x-ms-asf");
            mimeTypes.Add("asr", "video/x-ms-asf");
            mimeTypes.Add("asx", "video/x-ms-asf");
            mimeTypes.Add("swf", "application/x-shockwave-flash");
        }

        /// <summary>
        /// 根据文件名称获取MimeType
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMimeType(string fileName)
        {
            int index = fileName.LastIndexOf('.');
            if (index > 0 && index > fileName.LastIndexOf('\\'))
            {
                string extension = fileName.Substring(index + 1).ToLower(System.Globalization.CultureInfo.InvariantCulture);
                if (mimeTypes != null && mimeTypes.ContainsKey(extension))
                    return mimeTypes[extension];
            }
            return "application/octet-stream";
        }

        /// <summary>
        /// 根据MimeType获取后缀
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        public static string GetExtension(string mimeType)
        {
            if (mimeTypes != null)
            {
                mimeType = mimeType.ToLower();

                foreach (string extension in mimeTypes.Keys)
                {
                    if (mimeTypes[extension].ToLower() == mimeType)
                        return extension;
                }
            }

            return "unknown";
        }
    }
}
