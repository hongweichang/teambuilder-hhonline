using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Framework;
using HHOnline.News.Components;

namespace HHOnline.News.Providers
{
	public abstract class ArticleAttachmentProvider
	{
		private static readonly ArticleAttachmentProvider instance;

		static ArticleAttachmentProvider()
		{
			instance = HHContainer.Create().Resolve<ArticleAttachmentProvider>();
		}

		public static ArticleAttachmentProvider Instance
		{
			get { return instance; }
		}

		public abstract List<ArticleAttachment> GetAllArticleAttachments(AttachmentQuery query, out int totalRecord);

		public abstract List<ArticleAttachment> GetAllArticleAttachments();

		public abstract ArticleAttachment GetArticleAttachment(int id);

		public abstract ArticleAttachment CreateUpdateArticleAttachment(ArticleAttachment info, DataProviderAction action, out DataActionStatus status);

		public abstract DataActionStatus DeleteArticleAttachment(int id);

		public abstract DataActionStatus DeleteArticleAttachments(string ids);
	}
}
