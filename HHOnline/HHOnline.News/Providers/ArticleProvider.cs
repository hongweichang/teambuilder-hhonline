using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HHOnline.Framework;
using HHOnline.News.Components;

namespace HHOnline.News.Providers
{
    /// <summary>
    /// 文章驱动
    /// </summary>
    public abstract class ArticleProvider
    {
        private static readonly ArticleProvider instance;

        static ArticleProvider()
        {
            instance = HHContainer.Create().Resolve<ArticleProvider>();
        }

        /// <summary>
        /// 获取文章驱动
        /// </summary>
        public static ArticleProvider Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// 获取所有文章
        /// </summary>
        /// <returns></returns>
        public abstract List<Article> GetAllArticles();

		/// <summary>
		/// 增加访问率
		/// </summary>
		/// <returns></returns>
		public abstract int IncreaseHitTimes(int articleID);

		/// <summary>
		/// 获取制定文章
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public abstract Article GetArticle(int id);

		/// <summary>
		/// 创建或更新文章
		/// </summary>
		/// <param name="article"></param>
		/// <param name="action"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public abstract Article CreateUpdateArticle(Article article, DataProviderAction action, out DataActionStatus status);

		/// <summary>
		/// 删除文章
		/// </summary>
		/// <param name="article"></param>
		/// <returns></returns>
		public abstract DataActionStatus DeleteArticle(int articleID);

        /// <summary>
        /// 更新文章计数
        /// </summary>
        /// <param name="views"></param>
        public abstract void SaveViewList(Hashtable views);

		/// <summary>
		/// 获取文章列表
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public abstract List<Article> GetArticles(ArticleQuery query);

		/// <summary>
		/// 获取文章列表
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public abstract List<Article> GetArticles(ArticleQuery query, out int totalRecord);

		/// <summary>
		/// 批量删除文章
		/// </summary>
		/// <param name="articleIDList"></param>
		/// <returns></returns>
		public abstract DataActionStatus DeleteArticles(string articleIDList);
	}
}
