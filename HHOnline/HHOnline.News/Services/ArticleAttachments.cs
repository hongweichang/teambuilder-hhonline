using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Framework;
using HHOnline.News.Components;
using HHOnline.News.Providers;

namespace HHOnline.News.Services
{
	public class ArticleAttachments
	{
		public static string FileStoreKey = "ArticleAttachmentPicture";

		//public static IStorageFile GetDefaultPicture(int productID)
		//{
		//    return ShopDataProvider.Instance.GetDefaultPicture(productID);
		//}

		public static string MakePath(int productID)
		{
			return GlobalSettings.MakePath(productID);
		}

		public static DataActionStatus DeleteAttachment(int id)
		{
			DataActionStatus result = ArticleAttachmentProvider.Instance.DeleteArticleAttachment(id);

			return result;
		}

		public static ArticleAttachment GetAttachment(int id)
		{
			ArticleAttachment result = ArticleAttachmentProvider.Instance.GetArticleAttachment(id);

			return result;
		}

		public static PagingDataSet<ArticleAttachment> GetAttachments(AttachmentQuery query)
		{
			int totalRecods;
			List<ArticleAttachment> attachmentList = ArticleAttachmentProvider.Instance.GetAllArticleAttachments(query, out totalRecods);
			PagingDataSet<ArticleAttachment> articles = new PagingDataSet<ArticleAttachment>();
			articles.Records = attachmentList;
			articles.TotalRecords = totalRecods;

			return articles;
		}

		/// <summary>
		/// 添加附件
		/// </summary>
		/// <param name="info"></param>
		/// <param name="?"></param>
		/// <returns></returns>
		public static ArticleAttachment AddArticleAttachment(ArticleAttachment info, out DataActionStatus status)
		{
			ArticleAttachment result = ArticleAttachmentProvider.Instance.CreateUpdateArticleAttachment(info, DataProviderAction.Create, out status);

			return result;
		}

		/// <summary>
		/// 更新附件
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public static DataActionStatus UpdateArticleAttachment(ArticleAttachment info)
		{
			DataActionStatus result;
			ArticleAttachmentProvider.Instance.CreateUpdateArticleAttachment(info, DataProviderAction.Update, out result);

			return result;
		}
	}
}
