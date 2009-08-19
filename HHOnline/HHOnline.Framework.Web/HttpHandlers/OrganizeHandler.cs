using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using HHOnline.Framework.Web.Pages;
using System.Web.Security;
using System.Web.SessionState;

namespace HHOnline.Framework.Web.HttpHandlers
{
    public class OrganizeHandler:IHttpHandler,IRequiresSessionState
    {
        public bool IsReusable
        {
            get { return false;}
        }

        public void ProcessRequest(HttpContext context)
        {
            string msg = string.Empty;
            bool result = false;
            HHPrincipal principal = context.User as HHPrincipal;
            try
            {
                switch (context.Request["action"])
                {
                    case "DeleteOrganize":
                        msg = DeleteOrg(principal, context, ref result);
                        break;
                    case "DeleteUser":
                        msg=DeleteUser(principal, context, ref result);
                        break;
                    case "ValidUser":
                        msg = ValidUser(context,ref result);
                        break;
                    case "ValidName":
                        msg = ValidName(context, ref result);
                        break;
                }
                msg = "{suc:" + result.ToString().ToLower() + ",msg:'" + msg + "'}";
            }
            catch (Exception ex)
            {
                msg = "{suc:false,msg:'" + ex.Message + "'}";
            }
            context.Response.Write(msg);
        }

        #region -Methods-
        string DeleteOrg(HHPrincipal principal, HttpContext context,ref bool result)
        {
            string msg = string.Empty;

            if (principal.IsInRole("OrganizeModule-Delete"))
            {
                DataActionStatus s = Organizations.DeleteOrganization(context.Request["orgid"]);
                switch (s)
                {
                    case DataActionStatus.Success:
                        msg = "已成功删除所选的组织结构！";
                        result = true;
                        break;
                    case DataActionStatus.RelationshipExist:
                        result = false;
                        msg = "部门组织结构下存在关联数据[部门/用户]，无法被删除！";
                        break;
                    case DataActionStatus.UnknownFailure:
                        result = false;
                        msg = "删除组织结构信息时发生了未知的错误！";
                        break;
                }
            }
            else
            {
                throw new Exception("您没有执行此操作的权限！");
            }
            return msg;
        }
        string DeleteUser(HHPrincipal principal, HttpContext context, ref bool result)
        {
            string msg = string.Empty;
            if (principal.IsInRole("UserModule-Delete"))
            {
                DataActionStatus s = Users.DeleteUsers(context.Request["userid"]);
                switch (s)
                {
                    case DataActionStatus.Success:
                        msg = "已成功删除所选的用户信息！";
                        result = true;
                        break;
                    case DataActionStatus.UnknownFailure:
                        result = false;
                        msg = "删除用户信息时发生了未知的错误！";
                        break;
                }
            }
            else
            {
                throw new Exception("您没有执行此操作的权限！");
            }
            return msg;
        }
        string ValidUser(HttpContext context, ref bool result)
        {
            result = false;
            HttpRequest req = context.Request;
            string userName = req["userName"];
            string pwd = req["password"];
            bool rememberMe = bool.Parse(req["rememberMe"]);
            string validCode = req["validCode"];
            if (context.Session["Vcode"] == null)
            {
                throw new Exception("验证码已过期，请点击刷新！");
            }
            else
            {
                if (context.Session["Vcode"].ToString().Equals(validCode, StringComparison.CurrentCultureIgnoreCase))
                {
                    context.Session.Remove("Vcode");
                    User u = new User()
                    {
                        UserName = userName,
                        Password = pwd
                    };
                    switch (Users.ValidateUser(ref u))
                    {
                        case LoginUserStatus.InvalidCredentials:
                            throw new Exception("用户名密码不匹配，请重新登录！");
                        case LoginUserStatus.Success:
                            result = true;
                            HttpCookie cookie = FormsAuthentication.GetAuthCookie(u.UserName, true);
                            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                            FormsAuthenticationTicket newticket = new FormsAuthenticationTicket(
                                                                                            ticket.Version,
                                                                                            ticket.Name,
                                                                                            ticket.IssueDate,
                                                                                            ticket.Expiration,
                                                                                            ticket.IsPersistent,
                                                                                            DateTime.Now.ToShortDateString());
                            cookie.Value = FormsAuthentication.Encrypt(newticket);
                            HHCookie.AddCookie(cookie);
                            string url = FormsAuthentication.GetRedirectUrl(u.UserName, true);

                            if (rememberMe)
                            {
                                string userInfo = userName + ";" + pwd;
                                userInfo = GlobalSettings.Encrypt(userInfo);
                                HttpCookie c = new HttpCookie("HHOnline-UserInfo");
                                c.Values.Add("UserName", GlobalSettings.Encrypt(userName));
                                c.Values.Add("Password", GlobalSettings.Encrypt(pwd));
                                c.Expires = DateTime.Now.AddDays(365);
                                HHCookie.AddCookie(c);
                            }

                            switch (u.UserType)
                            {
                                case UserType.CompanyUser:
                                    return GlobalSettings.RelativeWebRoot + "main.aspx";
                                case UserType.InnerUser:
                                    return GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx";
                            }
                            break;
                        case LoginUserStatus.AccountPending:
                            throw new Exception("账号正在审核中，请稍后重试！");
                        case LoginUserStatus.AccountBanned:
                            throw new Exception("账号已经被禁止使用！");
                        case LoginUserStatus.AccountDisapproved:
                            throw new Exception("账号未通过审核，无法使用！");
                        case LoginUserStatus.Deleted:
                            throw new Exception("账号已经被删除，无法使用！");
                        case LoginUserStatus.UnknownError:
                            throw new Exception("登录过程中发生了异常，请联系管理员！");
                    }
                }
                else
                    throw new Exception("验证码错误，请重新输入！");
            }
            return "";
        }
        string ValidName(HttpContext context, ref bool result)
        {
            User u = Users.GetUser(context.Request["name"]);
            if (u == null)
            {
                result = true;
                return "用户名可以使用！";
            }
            else
            {
                result = false;
                return "用户名已经被注册！";
            }
        }
        #endregion
    }
}
