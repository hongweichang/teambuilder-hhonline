using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using HHOnline.Framework;
using HHOnline.News.Providers;
using HHOnline.News.Components;
using HHOnline.News.Services;
using HHOnline.Common;

namespace HHOnline.Data
{
	public class SqlArticleProvider : ArticleProvider
	{
		/// <summary>
		/// 获取所有文章
		/// </summary>
		/// <returns></returns>
		public override List<Article> GetAllArticles()
		{
			using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Article_GetAll"))
			{
				List<Article> result = new List<Article>();
				for (; dr.Read(); )
				{
					Article article = ArticleReaderConverter.ParseArticle(dr);
					result.Add(article);
				}

				return result;
			}
		}

		/// <summary>
		/// 获取制定文章
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public override Article GetArticle(int id)
		{
			ELParameter articleIDParam = new ELParameter("@ArticleID", DbType.Int32);
			articleIDParam.Value = id;

			using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Article_Get", articleIDParam))
			{
				Article result = null;
				if (dr.Read())
				{
					result = ArticleReaderConverter.ParseArticle(dr);
				}

				return result;
			}
		}

		/// <summary>
		/// 获取文章列表
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public override List<Article> GetArticles(ArticleQuery query)
		{
			int totalRecords;
			return GetArticles(query, out totalRecords);
		}

		/// <summary>
		/// 获取文章列表
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public override List<Article> GetArticles(ArticleQuery query, out int totalRecord)
		{
			ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@PageIndex",DbType.Int32,DataHelper.GetSafeSqlInt(query.PageIndex)),
                new ELParameter("@PageSize",DbType.Int32,DataHelper.GetSafeSqlInt(query.PageSize)),
                new ELParameter("@SqlPopulate",DbType.String,QueryGenerator.BuildArticleQuery(query))
            };

			using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Articles_Get", elParameters))
			{
				List<Article> articleList = new List<Article>();
				while (dr.Read())
					articleList.Add(ArticleReaderConverter.ParseArticle(dr));

				dr.NextResult();
				dr.Read();
				totalRecord = DataRecordHelper.GetInt32(dr, 0);

				return articleList;
			}
		}

		/// <summary>
		/// 创建或更新文章
		/// </summary>
		/// <param name="article"></param>
		/// <param name="action"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public override Article CreateUpdateArticle(Article article, DataProviderAction action, out DataActionStatus status)
		{
			SerializerData data = article.GetSerializerData();

			ELParameter[] parms = new ELParameter[]
			{
				action == DataProviderAction.Create ?
					new ELParameter("@ArticleID", DbType.Int32, 4, ParameterDirection.Output) :
					new ELParameter("@ArticleID", DbType.Int32, article.ID),
				new ELParameter("@Action", DbType.Int32, action),
				new ELParameter("@ArticleTitle", DbType.String, article.Title),
				new ELParameter("@CategoryID", DbType.Int32, article.Category),
				new ELParameter("@ArticleStatus", DbType.Int32, article.Status),
				new ELParameter("@HitTimes", DbType.Int32, article.HitTimes),
				new ELParameter("@ArticleSubtitle", DbType.String, article.SubTitle),
				new ELParameter("@ArticleAbstract", DbType.String, article.Abstract),
				new ELParameter("@ArticleContent", DbType.String, article.Content),
				new ELParameter("@ArticleDate", DbType.DateTime, article.Date == null ? (object)DBNull.Value : (object)article.Date),
				new ELParameter("@ArticleCopyFrom", DbType.String, article.CopyFrom),
				new ELParameter("@ArticleAuthor", DbType.String, article.Author),
				new ELParameter("@ArticleKeywords", DbType.String, article.Keywords),
				new ELParameter("@ArticleImageID", DbType.Int32, article.Image),
				new ELParameter("@DisplayOrder", DbType.Int32, article.DisplayOrder),
				new ELParameter("@ArticleMemo", DbType.String, article.ArticleMemo),
				new ELParameter("@User", DbType.Int32, GlobalSettings.GetCurrentUser().UserID),
				new ELParameter("@PropertyNames", DbType.String, data.Keys),
				new ELParameter("@PropertyValues", DbType.String, data.Values),
			};

			status = (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_Article_CreateUpdate", parms));
			if (status == DataActionStatus.Success && action == DataProviderAction.Create)
			{
				article.ID = Convert.ToInt32(parms[0].Value);
			}

			return article;
		}

		/// <summary>
		/// 删除文章
		/// </summary>
		/// <param name="article"></param>
		/// <returns></returns>
		public override DataActionStatus DeleteArticle(int articleID)
		{
			ELParameter articleIDParam = new ELParameter("@ArticleID", DbType.Int32);
			articleIDParam.Value = articleID;

			DataActionStatus result = (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_Article_Delete", articleIDParam));

			return result;
		}

		public override void SaveViewList(Hashtable views)
		{
			ELParameter paramArticleID = new ELParameter("@ArticleID", DbType.Int32);
			ELParameter paramViewCount = new ELParameter("@ViewCount", DbType.Int32);
			View v = null;

			foreach (int articleID in views.Keys)
			{
				v = views[articleID] as View;
				if (v != null)
				{
					paramArticleID.Value = v.RelatedID;
					paramViewCount.Value = v.Count;
					DataHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Article_View_Add", paramArticleID, paramViewCount);
				}
			}
		}

		/// <summary>
		/// 批量删除文章
		/// </summary>
		/// <param name="articleIDList"></param>
		/// <returns></returns>
		public override DataActionStatus DeleteArticles(string articleIDList)
		{
			ELParameter idParam = new ELParameter("@ArticleIDList", DbType.String);
			idParam.Value = articleIDList;

			return (DataActionStatus)DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_Articles_Delete", idParam);
		}
	}
}
