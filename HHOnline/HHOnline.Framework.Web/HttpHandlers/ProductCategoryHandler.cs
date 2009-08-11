using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using HHOnline.Framework.Web.Pages;
using HHOnline.Shops;

namespace HHOnline.Framework.Web.HttpHandlers
{
    public class ProductCategoryHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get { return false; }
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
                    case "DeleteCategory":
                        msg = DeleteCategory(principal, context, ref result);
                        break;
                    case "DeleteProperty":
                        msg = DeleteProperty(principal, context, ref result);
                        break;
                    //case "ValidUser":
                    //    msg = ValidUser(context, ref result);
                    //    break;
                }
                msg = "{suc:" + result.ToString().ToLower() + ",msg:'" + msg + "'}";
            }
            catch (Exception ex)
            {
                msg = "{suc:false,msg:'" + ex.Message + "'}";
            }
            context.Response.Write(msg);
        }

        private string DeleteCategory(HHPrincipal principal, HttpContext context, ref bool result)
        {
            string msg = string.Empty;

            if (principal.IsInRole("ProductCategoryModule-Delete"))
            {
                DataActionStatus s = ProductCategories.Delete(context.Request["categoryID"]);
                switch (s)
                {
                    case DataActionStatus.Success:
                        msg = "已成功删除所选的产品分类！";
                        result = true;
                        break;
                    case DataActionStatus.RelationshipExist:
                        result = false;
                        msg = "产品分类下存在关联数据[子分类/属性/商品]，无法被删除！";
                        break;
                    case DataActionStatus.UnknownFailure:
                        result = false;
                        msg = "删除产品分类信息时发生了未知的错误！";
                        break;
                }
            }
            else
            {
                throw new Exception("您没有执行此操作的权限！");
            }
            return msg;
        }

        private string DeleteProperty(HHPrincipal principal, HttpContext context, ref bool result)
        {
            string msg = string.Empty;

            if (principal.IsInRole("ProductCategoryModule-Delete"))
            {
                DataActionStatus s = ProductProperties.Delete(context.Request["propertyID"]);
                switch (s)
                {
                    case DataActionStatus.Success:
                        msg = "已成功删除所选的产品分类属性！";
                        result = true;
                        break;
                    case DataActionStatus.RelationshipExist:
                        result = false;
                        msg = "产品分类属性下存在关联数据[子分类]，无法被删除！";
                        break;
                    case DataActionStatus.UnknownFailure:
                        result = false;
                        msg = "删除产品分类属性时发生了未知的错误！";
                        break;
                }
            }
            else
            {
                throw new Exception("您没有执行此操作的权限！");
            }
            return msg;
        }

    }
}
