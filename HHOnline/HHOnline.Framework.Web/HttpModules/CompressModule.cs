using System;
using System.Web;
using System.Web.UI;
using System.IO.Compression;

namespace HHOnline.Framework.Web.HttpModules
{
    public class CompressModule:IHttpModule
    {
        public void Dispose() { }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(context_PreRequestHandlerExecute);
        }

        private const string GZIP = "gzip";
        private const string DEFLATE = "deflate";

        void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            if (app.Context.CurrentHandler is Page && app.Request["HTTP_X_MICROSOFTAJAX"] == null)
            {
                if (IsEncodingAccept(GZIP))
                {
                    app.Response.Filter = new GZipStream(app.Response.Filter, CompressionMode.Compress);
                    SetEncoding(GZIP);
                }
                else if (IsEncodingAccept(DEFLATE))
                {
                    app.Response.Filter = new DeflateStream(app.Response.Filter, CompressionMode.Compress);
                    SetEncoding(DEFLATE);
                }
            }
        }

        bool IsEncodingAccept(string encoding)
        {
            HttpContext context = HttpContext.Current;
            return (context.Request.Headers["Accept-encoding"] != null && context.Request.Headers["Accept-encoding"].Contains(encoding));
        }
        void SetEncoding(string encoding)
        {
            HttpContext.Current.Response.AppendHeader("Content-encoding", encoding);
        }
    }
}
