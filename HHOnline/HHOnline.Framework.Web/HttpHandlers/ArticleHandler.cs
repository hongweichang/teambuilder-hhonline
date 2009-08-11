using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using HHOnline.Framework.Web.Pages;
using HHOnline.News.Services;

namespace HHOnline.Framework.Web.HttpHandlers
{
	public class ArticleHandler : IHttpHandler
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
					case "DeleteArticleCategory":
						msg = DeleteAC(principal, context, ref result);
						break;

					case "DeleteArticle":
						msg = DeleteA(principal, context, ref result);
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
		string DeleteAC(HHPrincipal principal, HttpContext context, ref bool result)
		{
			string msg = string.Empty;

			if (principal.IsInRole("NewsCategoryModule-Delete"))
			{
				DataActionStatus s = ArticleManager.DeleteCategories(context.Request["categoryIds"]);
				switch (s)
				{
					case DataActionStatus.Success:
						msg = "已成功删除所选的资讯分类！";
						result = true;
						break;

					case DataActionStatus.RelationshipExist:
						result = false;
						msg = "当前资讯下存在关联数据[子分类/资讯]，无法被删除！";
						break;

					case DataActionStatus.UnknownFailure:
						result = false;
						msg = "删除资讯信息时发生了未知的错误！";
						break;
				}
			}
			else
			{
				throw new Exception("您没有执行此操作的权限！");
			}
			return msg;
		}
		string DeleteA(HHPrincipal principal, HttpContext context, ref bool result)
		{
			string msg = string.Empty;

			if (principal.IsInRole("ArticleModule-Delete"))
			{

				DataActionStatus s = ArticleManager.DeleteArticles(context.Request["newsIds"]);
				switch (s)
				{
					case DataActionStatus.Success:
						msg = "已成功删除所选的资讯！";
						result = true;
						break;
					case DataActionStatus.RelationshipExist:
						result = false;
						msg = "资讯下存在关联数据[子分类/资讯]，无法被删除！";
						break;
					case DataActionStatus.UnknownFailure:
						result = false;
						msg = "删除资讯信息时发生了未知的错误！";
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
