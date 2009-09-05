using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.News.Providers;
using HHOnline.News.Components;
using HHOnline.Framework;
using System.Data;
using HHOnline.News.Services;

namespace HHOnline.Data
{
	public class SqlArticleCategoryProvider : ArticleCategoryProvider
	{
		/// <summary>
		/// 获取所有文章
		/// </summary>
		/// <returns></returns>
		public override List<ArticleCategory> GetAllArticleCategories()
		{
			using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ArticleCategory_GetAll"))
			{
				List<ArticleCategory> result = new List<ArticleCategory>();

				for (; dr.Read(); )
				{
					ArticleCategory info = ArticleReaderConverter.ParseArticleCategory(dr);
					result.Add(info);
				}

				return result;
			}
		}

		/// <summary>
		/// 获取制定文章
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public override ArticleCategory GetArticleCategory(int id)
		{
			ELParameter idParam = new ELParameter("@CategoryID", DbType.Int32);
			idParam.Value = id;

			using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ArticleCategory_Get", idParam))
			{
				if (dr.Read())
				{
					return ArticleReaderConverter.ParseArticleCategory(dr);
				}

				return null;
			}
		}

		/// <summary>
		/// 创建或更新文章
		/// </summary>
		/// <param name="article"></param>
		/// <param name="action"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public override ArticleCategory CreateUpdateArticleCategory(ArticleCategory info, DataProviderAction action, out DataActionStatus status)
		{
			ELParameter[] parms = new ELParameter[]
			{
				action == DataProviderAction.Create ?
					new ELParameter("@CategoryID", DbType.Int32, 4, ParameterDirection.Output) :
					new ELParameter("@CategoryID", DbType.Int32, info.ID),
				new ELParameter("@CategoryName", DbType.String, info.Name),
				new ELParameter("@CategoryDesc", DbType.String, info.Description),
				new ELParameter("@CategoryMemo", DbType.String, info.Memo),
				new ELParameter("@ParentID", DbType.Int32, info.ParentID),
				new ELParameter("@DisplayOrder", DbType.Int32, info.DisplayOrder),
				new ELParameter("@CategoryStatus", DbType.Int32, info.Status),
				new ELParameter("@User", DbType.Int32, GlobalSettings.GetCurrentUser().UserID),
				new ELParameter("@Action", DbType.Int32, action),
			};

			status = (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ArticleCategory_CreateUpdate", parms));
			if (status == DataActionStatus.Success && action == DataProviderAction.Create)
			{
				info.ID = Convert.ToInt32(parms[0].Value);
			}

			return info;
		}

		/// <summary>
		/// 删除文章
		/// </summary>
		/// <param name="article"></param>
		/// <returns></returns>
		public override DataActionStatus DeleteArticleCategory(int id)
		{
			ELParameter idParam = new ELParameter("@CategoryID", DbType.Int32);
			idParam.Value = id;

			DataActionStatus result = (DataActionStatus) DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ArticleCategory_Delete", idParam);

			return result;
		}

		/// <summary>
		/// 获取分类文章总数
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public override int GetCategoryArticlesCount(int id)
		{
			ELParameter idParam = new ELParameter("@CategoryID", DbType.Int32, id);

			int result = Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ArticleCategory_GetArticlesCount", idParam));
			return result;
		}

		/// <summary>
		/// 批量删除分类
		/// </summary>
		/// <param name="categoryIDList"></param>
		/// <returns></returns>
		public override DataActionStatus DeleteArticleCategories(string categoryIDList)
		{
			ELParameter idParam = new ELParameter("@CategoryIDList", DbType.String);
			idParam.Value = categoryIDList;

			return (DataActionStatus)DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ArticleCategories_Delete", idParam);
		}
	}
}
