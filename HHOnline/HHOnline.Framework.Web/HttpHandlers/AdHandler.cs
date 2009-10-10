using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;

namespace HHOnline.Framework.Web.HttpHandlers
{
    public class AdHandler:IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            List<ShowPicture> picTemp = ShowPictures.GetShowPictures();
            List<ShowPicture> pics = picTemp.GetRange(0, Math.Min(5, picTemp.Count));

            context.Response.Write(Newtonsoft.Json.JavaScriptConvert.SerializeObject(pics));
        }
    }
}
