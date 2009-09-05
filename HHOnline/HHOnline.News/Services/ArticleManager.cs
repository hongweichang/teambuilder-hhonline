using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Framework;
using HHOnline.Cache;
using HHOnline.News.Providers;
using HHOnline.News.Components;

namespace HHOnline.News.Services
{
	/// <summary>
	/// 资讯管理模块
	/// </summary>
	public class ArticleManager
	{
		/// <summary>
		/// 资讯缓存关键字
		/// </summary>
		private static string NewsManagerCacheKey = "HHOnline/News/";

		private ArticleManager()
		{
			NewsManagerCacheKey = CacheKeyManager.NewsPrefix;
		}

		private delegate void CacheDelegate(ref object list, params object[] args);
		private static List<T> CacheInstance<T>(CacheDelegate cacheDelegate, string cacheKey, params object[] args)
			where T : new()
		{
			cacheKey = NewsManagerCacheKey + cacheKey;
			object instances = HHCache.Instance.Get(cacheKey);
			if (instances == null)
			{
				cacheDelegate(ref instances, args);
				HHCache.Instance.Max(cacheKey, instances);
			}
			return instances as List<T>;
		}

		/// <summary>
		/// 获取分类文章总数
		/// </summary>
		/// <param name="categoryID"></param>
		/// <returns></returns>
		public static int GetCategoryArticlesCount(int categoryID)
		{
			int result = ArticleCategoryProvider.Instance.GetCategoryArticlesCount(categoryID);
			return result;
		}

		/// <summary>
		/// 增加点击率
		/// </summary>
		/// <param name="articleID"></param>
		/// <returns></returns>
		public static int IncreaseHitTimes(int articleID)
		{
			int result = ArticleProvider.Instance.IncreaseHitTimes(articleID);

			return result;
		}

		/// <summary>
		/// 批量删除文章
		/// </summary>
		/// <param name="articleIDList"></param>
		/// <returns></returns>
		public static DataActionStatus DeleteArticles(string articleIDList)
		{
			DataActionStatus result = ArticleProvider.Instance.DeleteArticles(articleIDList);
			if (result == DataActionStatus.Success)
			{
				HHCache.Instance.Remove(NewsManagerCacheKey + "AllArticles");
			}

			return result;
		}

		/// <summary>
		/// 删除文章 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static DataActionStatus DeleteArticle(int id)
		{
			DataActionStatus result = ArticleProvider.Instance.DeleteArticle(id);
			if (result == DataActionStatus.Success)
			{
				HHCache.Instance.Remove(NewsManagerCacheKey + "AllArticles");
			}

			return result;
		}

		/// <summary>
		/// 批量删除分类 
		/// </summary>
		/// <param name="categoryIDList"></param>
		/// <returns></returns>
		public static DataActionStatus DeleteCategories(string categoryIDList)
		{
			DataActionStatus result = ArticleCategoryProvider.Instance.DeleteArticleCategories(categoryIDList);
			if (result == DataActionStatus.Success)
			{
				HHCache.Instance.Remove(NewsManagerCacheKey + "AllCategories");
			}

			return result;
		}

		/// <summary>
		/// 删除分类
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static DataActionStatus DeleteCategory(int id)
		{
			DataActionStatus result = ArticleCategoryProvider.Instance.DeleteArticleCategory(id);
			if (result == DataActionStatus.Success)
			{
				HHCache.Instance.Remove(NewsManagerCacheKey + "AllCategories");
			}

			return result;
		}

		/// <summary>
		/// 添加文章
		/// </summary>
		/// <param name="info"></param>
		/// <param name="?"></param>
		/// <returns></returns>
		public static Article AddArticle(Article info, out DataActionStatus status)
		{
			Article result = ArticleProvider.Instance.CreateUpdateArticle(info, DataProviderAction.Create, out status);
			HHCache.Instance.Remove(NewsManagerCacheKey + "AllArticles");

			return result;
		}

		/// <summary>
		/// 更新文章
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public static DataActionStatus UpdateArticle(Article info)
		{
			DataActionStatus result;
			ArticleProvider.Instance.CreateUpdateArticle(info, DataProviderAction.Update, out result);

			if (result == DataActionStatus.Success)
			{
				HHCache.Instance.Remove(NewsManagerCacheKey + "AllArticles");
			}

			return result;
		}

		/// <summary>
		/// 更新文章分类
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public static DataActionStatus UpdateArticleCategory(ArticleCategory info)
		{
			DataActionStatus result;
			ArticleCategoryProvider.Instance.CreateUpdateArticleCategory(info, DataProviderAction.Update, out result);

			if (result == DataActionStatus.Success)
			{
				HHCache.Instance.Remove(NewsManagerCacheKey + "AllCategories");
			}

			return result;
		}

		/// <summary>
		/// 添加文章分类 
		/// </summary>
		/// <param name="organization"></param>
		/// <returns></returns>
		public static ArticleCategory AddArticleCategory(ArticleCategory info, out DataActionStatus status)
		{
			ArticleCategory result;

			result = ArticleCategoryProvider.Instance.CreateUpdateArticleCategory(info, DataProviderAction.Create, out status);
			HHCache.Instance.Remove(NewsManagerCacheKey + "AllCategories");

			return result;
		}

		/// <summary>
		/// 获取子分类
		/// </summary>
		/// <param name="parentID"></param>
		/// <returns></returns>
		public static List<ArticleCategory> GetChildCategories(int parentID)
		{
			List<ArticleCategory> result = new List<ArticleCategory>();
			List<ArticleCategory> categories = GetAllCategories();

			foreach (ArticleCategory category in categories)
			{
				if (category.ParentID == parentID)
				{
					result.Add(category);
				}
			}

			return result;
		}

		/// <summary>
		/// 获取资讯分类信息
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static ArticleCategory GetArticleCategory(int id)
		{
			return ArticleCategoryProvider.Instance.GetArticleCategory(id);
		}

		/// <summary>
		/// 获取所有资讯分类
		/// </summary>
		/// <returns></returns>
		public static List<ArticleCategory> GetAllCategories()
		{
			//return CacheInstance<ArticleCategory>(GetAllCategoriesMethod, "AllCategories");
			return ArticleCategoryProvider.Instance.GetAllArticleCategories();
		}

		static void GetAllCategoriesMethod(ref object list, params object[] ActionID)
		{
			list = ArticleCategoryProvider.Instance.GetAllArticleCategories();
		}

		/// <summary>
		/// 获取资讯
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Article GetArticle(int id)
		{
			return ArticleProvider.Instance.GetArticle(id);
		}

		/// <summary>
		/// 获取所有资讯
		/// </summary>
		/// <returns></returns>
		public static List<Article> GetAllArticles()
		{
			return CacheInstance<Article>(GetAllArticlesMethod, "AllArticles");
		}

		static void GetAllArticlesMethod(ref object list, params object[] ActionID)
		{
			list = ArticleProvider.Instance.GetAllArticles();
		}

		/// <summary>
		/// 根据查询获取文章信息集合
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public static PagingDataSet<Article> GetArticles(ArticleQuery query)
		{
			int totalRecods;
			List<Article> articleList = ArticleProvider.Instance.GetArticles(query, out totalRecods);
			PagingDataSet<Article> articles = new PagingDataSet<Article>();
			articles.Records = articleList;
			articles.TotalRecords = totalRecods;

			return articles;
		}


        #region -EventHandler-
        public static EventHandler<EventArgs> Updated;
        protected static void OnUpdated()
        {
            if (Updated != null)
            {
                Updated(null, EventArgs.Empty);
            }
        }
        #endregion
	}
}
