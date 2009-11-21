using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using HHOnline.Framework.Web.Pages;
using HHOnline.Shops;
using System.Configuration;
using System.Collections.Specialized;

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
            try
            {
                string curUser = string.Empty;
                if (context.Profile.IsAnonymous)
                {
                    curUser = context.Profile.UserName;
                }
                else
                {
                    curUser = Users.GetUniqueId(context.Profile.UserName).ToString();
                }
                NameValueCollection req = context.Request.QueryString;
                string action = req["action"];
                switch (action)
                {
                    case "getFriendLinks":
                        try
                        {
                            suc = true;
                            msg = Newtonsoft.Json.JavaScriptConvert.SerializeObject(FriendLinks.FriendLinkGet());
                        }
                        catch (Exception ex)
                        {
                            suc = false;
                            msg = ex.Message;
                        }
                        break;
                    case "addShopcart":
                        int pid = int.Parse(req["d"]);
                        int amount = int.Parse(req["c"]);
                        int mid = int.Parse(req["m"]);
                        Shopping shop = new Shopping()
                        {
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now,
                            ModelID = mid,
                            ProductID = pid,
                            Quantity = amount,
                            ShoppingMemo = string.Empty,
                            UserID = curUser
                        };
                        suc = Shoppings.ShoppingAdd(shop);
                        break;
                    case "getPSuggestion":
                        string v = req["value"];
                        List<string> sw = WordSearchManager.GetWordSuggest(v, 10);
                        suc = true;
                        msg = Newtonsoft.Json.JavaScriptConvert.SerializeObject(sw);
                        break;
                    case "getStatistic":
                        List<WordStatistic> wss = WordSearchManager.GetStatistic(GlobalSettings.MinValue, GlobalSettings.MaxValue);
                        wss = wss.GetRange(0, Math.Min(10, wss.Count));
                        suc=true;
                        msg = Newtonsoft.Json.JavaScriptConvert.SerializeObject(wss);
                        break;
                }

                context.Response.Write("{msg:'" + msg + "',suc:" + suc.ToString().ToLower() + "}");
            }
            catch (Exception ex)
            {
                context.Response.Write("{msg:'" + ex.Message + "',suc:false}");
            }
        }
    }
}
