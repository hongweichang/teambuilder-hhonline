using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Web.Security;
using System.Web;

namespace HHOnline.Framework.Web.Pages
{
    public class HHIdentity : IIdentity
    {
        private string cookiePrefix = "HHOnline_Cookie_";
        private FormsAuthenticationTicket ticket = null;
        private HttpContext context = HttpContext.Current;
        Dictionary<string, string> dics = new Dictionary<string, string>();
        public HHIdentity(FormsAuthenticationTicket ticket)
        {
            this.ticket = ticket;
            cookiePrefix = HHConfiguration.GetConfig()["cookiePrefix"].ToString();
            dics.Add("Email", cookiePrefix + "Email");
        }

        #region -IIdentity Member-
        private string authenticationType = "HHOnline";
        public string AuthenticationType
        {
            get { return authenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string Name
        {
            get { return this.ticket.Name; }
        }

        #endregion

        #region -Custom Fields-
        public string Email
        {
            get
            {
                HttpCookie cookie = context.Request.Cookies[dics["Email"]];

                if (cookie == null || String.IsNullOrEmpty(cookie.Value))
                {
                    string value = "angelxiongjun[at]163.com";   //from database
                    SetCookie("Email", value);
                }

                return cookie.Value;
            }
        }
        #endregion

        private void SetCookie(string name,string value)
        {
            HttpCookie cookie = new HttpCookie(dics[name], value);
            cookie.Expires = DateTime.Now.AddDays(1);
            context.Response.Cookies.Add(cookie);
        }
    }
}
