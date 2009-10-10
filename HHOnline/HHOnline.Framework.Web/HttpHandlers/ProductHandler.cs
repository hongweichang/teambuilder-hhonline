using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using HHOnline.Framework.Web.Pages;
using HHOnline.Shops;
using System.Configuration;

namespace HHOnline.Framework.Web.HttpHandlers
{
    public class ProductHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string msg = string.Empty;
            bool suc = false;
            string formatStr = "{msg:'{0}',suc:{1}}";
            SettingsPropertyValueCollection spvc = context.Profile.PropertyValues;
            User u = spvc["AccountInfo"].PropertyValue as User;
            string action = context.Request.QueryString["action"];
            switch (action)
            {
                case "addShopcar":

                    break;
            }
        }
    }
}
