using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Framework;
using HHOnline.News.Components;

namespace HHOnline.News.Providers
{
	/// <summary>
	/// 文章分类驱动
	/// </summary>
	public abstract class ArticleCategoryProvider
	{
		private static readonly ArticleCategoryProvider instance;

		static ArticleCategoryProvider()
        {
			instance = HHContainer.Create().Resolve<ArticleCategoryProvider>();
        }

        /// <summary>
        /// 获取文章驱动
        /// </summary>
		public static ArticleCategoryProvider Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// 获取所有文章
        /// </summary>
        /// <returns></returns>
		public abstract List<ArticleCategory> GetAllArticleCategories();

		/// <summary>
		/// 获取制定文章
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public abstract ArticleCategory GetArticleCategory(int id);

		/// <summary>
		/// 创建或更新文章
		/// </summary>
		/// <param name="article"></param>
		/// <param name="action"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public abstract ArticleCategory CreateUpdateArticleCategory(ArticleCategory article, DataProviderAction action, out DataActionStatus status);

		/// <summary>
		/// 删除文章
		/// </summary>
		/// <param name="article"></param>
		/// <returns></returns>
		public abstract DataActionStatus DeleteArticleCategory(int id);

		/// <summary>
		/// 批量删除分类
		/// </summary>
		/// <param name="categoryIDList"></param>
		/// <returns></returns>
		public abstract DataActionStatus DeleteArticleCategories(string categoryIDList);
	}
}
