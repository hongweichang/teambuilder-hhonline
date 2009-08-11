using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;

namespace HHOnline.Framework.Web.HttpHandlers
{
    public class FileSystemStorageHttpHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            FileSystemStorageFile file = FileStorageProvider.GetStorageFileByUrl(context.Request.Path) as FileSystemStorageFile;
            if (file == null)
            {
                //文件未找到
                context.Response.StatusCode = 404;
                return;
            }
            DateTime lastModified = (new FileInfo(file.FullLocalPath)).LastWriteTime;
            DateTime currentLastModifiedDate = DateTime.MinValue;
            //文件未被修改过
            DateTime.TryParse(context.Request.Headers["If-Modified-Since"] ?? "", out currentLastModifiedDate);
            if (Math.Abs(((TimeSpan)lastModified.ToUniversalTime().Subtract(currentLastModifiedDate.ToUniversalTime())).TotalSeconds) <= 1)
            {
                context.Response.StatusCode = 304;
                context.Response.Status = "304 Not Modified";
                return;
            }
            long eTag = 0;
            if (long.TryParse(context.Request.Headers["If-None-Match"] ?? "", out eTag))
            {
                currentLastModifiedDate = new DateTime(eTag);
                if (lastModified == currentLastModifiedDate)
                {
                    context.Response.StatusCode = 304;
                    context.Response.Status = "304 Not Modified";
                    return;
                }
            }

            //设置客户端设置
            context.Response.ContentType = MimeTypeManager.GetMimeType(file.FileName);
            context.Response.Cache.SetAllowResponseInBrowserHistory(true);
            context.Response.Cache.SetLastModified(lastModified.ToUniversalTime());
            context.Response.Cache.SetETag(lastModified.Ticks.ToString());
            string disposition;
            if (context.Response.ContentType == "application/pdf" || context.Response.ContentType == "application/octet-stream")
                disposition = "attachment";
            else
                disposition = "inline";
            //设置文件名称
            if (context.Request.Browser.Browser.IndexOf("Netscape") != -1)
                context.Response.AddHeader("Content-disposition", disposition + "; filename*0*="
                    + context.Server.UrlEncode(GlobalSettings.UrlDecodeFileComponent(file.FileName)).Replace("+", "%20") + "");
            else
                context.Response.AddHeader("Content-disposition", disposition + "; filename="
                    + context.Server.UrlEncode(GlobalSettings.UrlDecodeFileComponent(file.FileName)).Replace("+", "%20") + "");


            if (!file.FullLocalPath.StartsWith(@"\"))
                context.Response.TransmitFile(file.FullLocalPath);
            else
            {
                context.Response.AddHeader("Content-Length", file.ContentLength.ToString("0"));
                context.Response.Buffer = false;
                context.Response.BufferOutput = false;

                using (Stream s = new FileStream(file.FullLocalPath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[64 * 1024];
                    int read;
                    while ((read = s.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (!context.Response.IsClientConnected)
                            break;

                        context.Response.OutputStream.Write(buffer, 0, read);
                        context.Response.OutputStream.Flush();
                    }

                    context.Response.OutputStream.Flush();
                    context.Response.Flush();
                    context.Response.Close();
                    s.Close();
                }
            }
        }
    }
}
