using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;
using System.IO;
using System.Web;
using System.Net;
using System.Text.RegularExpressions;

namespace HHOnline.Framework.Web.HttpHandlers
{
    public class JsCssHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string path = context.Request["path"];
            if (!string.IsNullOrEmpty(path))
            {
                if (path.EndsWith(".js", StringComparison.OrdinalIgnoreCase) || path.EndsWith(".css", StringComparison.OrdinalIgnoreCase))
                {
                    string body = (string)context.Cache[path.ToLower()];
                    string contentType = GetContentType(path);
                    if (string.IsNullOrEmpty(body))
                    {
                        using (StreamReader sr = new StreamReader(context.Server.MapPath(path)))
                        {
                            body = sr.ReadToEnd();
                        }
                        if (contentType == "text/javascript" && GlobalSettings.CompressJs)
                            body = RemoveWhiteSpaceJs(body);
                        else if (contentType == "text/css" && GlobalSettings.CompressCss)
                            body = RemoveWhiteSpaceCss(body);
                        context.Cache.Add(path, body, new CacheDependency(context.Server.MapPath(path)),
                                DateTime.Now.ToUniversalTime().AddDays(1), TimeSpan.Zero, CacheItemPriority.High, null
                                );
                    }
                    context.Response.Clear();
                    SetHeaders(path, contentType);
                    context.Response.Write(body);
                    context.Response.End();
                }
            }
        }
        string GetContentType(string name)
        {
            string contentType = string.Empty;
            if (name.EndsWith(".js", StringComparison.OrdinalIgnoreCase))
                contentType = "text/javascript";
            else if (name.EndsWith(".css", StringComparison.OrdinalIgnoreCase))
                contentType = "text/css";
            return contentType;
        }
        void SetHeaders(string file, string contentType)
        {
            HttpContext context = HttpContext.Current;
            context.Response.ContentType = contentType;
            context.Response.Cache.VaryByHeaders["Accept-Encoding"] = true;
            DateTime date = DateTime.Now;
            if (context.Cache[file + "date"] != null)
                date = (DateTime)context.Cache[file + "date"];
            string etag = "\"" + date.GetHashCode() + "\"";
            string incomingetag = context.Request.Headers["If-None-Match"];
            context.Response.Cache.SetExpires(DateTime.Now.ToUniversalTime().AddDays(1));
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetMaxAge(new TimeSpan(1, 0, 0, 0));
            context.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            context.Response.Cache.SetETag(etag);
            if (string.Compare(incomingetag, etag) == 0)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotModified;
                context.Response.End();
            }
        }
        string RemoveWhiteSpaceJs(string body)
        {
            string[] lines = body.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            foreach (string line in lines)
            {
                if (line.Trim().Length > 0 && !line.Trim().StartsWith("//"))
                    sb.Append(line.Trim());
            }
            body = sb.ToString();
            body = Regex.Replace(body, @"^[\s]+|[ \f\r\t\v]+$", String.Empty);
            body = Regex.Replace(body, @"([+-])\n\1", "$1 $1");
            body = Regex.Replace(body, @"([^+-][+-])\n", "$1");
            body = Regex.Replace(body, @"([^+]) ?(\+)", "$1$2");
            body = Regex.Replace(body, @"(\+) ?([^+])", "$1$2");
            body = Regex.Replace(body, @"([^-]) ?(\-)", "$1$2");
            body = Regex.Replace(body, @"(\-) ?([^-])", "$1$2");
            body = Regex.Replace(body, @"\n([{}()[\],<>/*%&|^!~?:=.;+-])", "$1");
            body = Regex.Replace(body, @"(\W(if|while|for)\([^{]*?\))\n", "$1");
            body = Regex.Replace(body, @"(\W(if|while|for)\([^{]*?\))((if|while|for)\([^{]*?\))\n", "$1$3");
            body = Regex.Replace(body, @"([;}]else)\n", "$1 ");
            body = Regex.Replace(body, @"(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,}(?=&nbsp;)|(?<=&ndsp;)\s{2,}(?=[<])", String.Empty);
            return body;
        }
        string RemoveWhiteSpaceCss(string body)
        {
            body = body.Replace("  ", String.Empty);
            body = body.Replace(Environment.NewLine, String.Empty);
            body = body.Replace("\t", string.Empty);
            body = body.Replace(" {", "{");
            body = body.Replace(" :", ":");
            body = body.Replace(": ", ":");
            body = body.Replace(", ", ",");
            body = body.Replace("; ", ";");
            body = body.Replace(";}", "}");
            body = body.Replace(@"?", string.Empty);
            body = Regex.Replace(body, @"(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,}(?=&nbsp;)|(?<=&ndsp;)\s{2,}(?=[<])", String.Empty);
            body = Regex.Replace(body, @"/\*[\d\D]*?\*/", string.Empty);
            return body;
        }
        public bool IsReusable
        {
            get { return false; }
        }


    }
}
