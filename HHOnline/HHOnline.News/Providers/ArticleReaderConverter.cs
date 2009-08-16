using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using HHOnline.Common;
using HHOnline.Framework;
using HHOnline.Framework.Providers;
using HHOnline.News.Components;

namespace HHOnline.News.Services
{
	/// <summary>
	/// 文章转换辅助类
	/// </summary>
	public static class ArticleReaderConverter
	{
		/// <summary>
		/// 获取文章
		/// </summary>
		/// <param name="dr"></param>
		public static Article ParseArticle(IDataReader dr)
		{
			Article result = new Article()
			{
				ID = DataRecordHelper.GetInt32(dr, "ArticleID"),
				Title = DataRecordHelper.GetString(dr, "ArticleTitle"),
				SubTitle = DataRecordHelper.GetString(dr, "ArticleSubtitle"),
				Abstract = DataRecordHelper.GetString(dr, "ArticleAbstract"),
				Content = DataRecordHelper.GetString(dr, "ArticleContent"),
				Date = DataRecordHelper.GetDateTime(dr, "ArticleDate"),
				CopyFrom = DataRecordHelper.GetString(dr, "ArticleCopyFrom"),
				Author = DataRecordHelper.GetString(dr, "ArticleAuthor"),
				Keywords = DataRecordHelper.GetString(dr, "ArticleKeywords"),
				Image = DataRecordHelper.GetInt32(dr, "ArticleImageID"),
				Category = DataRecordHelper.GetInt32(dr, "CategoryID"),
				DisplayOrder = DataRecordHelper.GetInt32(dr, "DisplayOrder"),
				ArticleMemo = DataRecordHelper.GetString(dr, "ArticleMemo"),
				Status = (ComponentStatus)DataRecordHelper.GetInt32(dr, "ArticleStatus"),
				HitTimes = DataRecordHelper.GetInt32(dr, "HitTimes"),
				CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime"),
				CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser"),
				UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime"),
				UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser"),
			};

			result.SetSerializerData(CommonDataProvider.PopulateSerializerDataIDataRecord(dr));

			return result;
		}

		/// <summary>
		/// 资源
		/// </summary>
		/// <param name="dr"></param>
		/// <returns></returns>
		public static ArticleAttachment ParseArticleAttachment(IDataReader dr)
		{
			ArticleAttachment result = new ArticleAttachment();

			result.ID = DataRecordHelper.GetInt32(dr, "AttachmentID");
			result.FileName = DataRecordHelper.GetString(dr, "AttachmentFile");
			result.Name = DataRecordHelper.GetString(dr, "AttachmentName");
			result.ContentType = DataRecordHelper.GetString(dr, "ContentType");
			result.ContentSize = DataRecordHelper.GetInt32(dr, "ContentSize");
			result.IsRemote = DataRecordHelper.GetInt32(dr, "IsRemote") > 0;
			result.ImageWidth = DataRecordHelper.GetNullableInt32(dr, "ImageWidth");
			result.ImageHeight = DataRecordHelper.GetNullableInt32(dr, "ImageHeight");
			result.Desc = DataRecordHelper.GetString(dr, "AttachmentDesc");
			result.Memo = DataRecordHelper.GetString(dr, "AttachmentMemo");
			result.Status = (ComponentStatus)DataRecordHelper.GetInt32(dr, "AttachmentStatus");
			result.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime");
			result.CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser");
			result.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime");
			result.UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser");

			return result;
		}

		/// <summary>
		/// 分类
		/// </summary>
		/// <param name="dr"></param>
		/// <returns></returns>
		public static ArticleCategory ParseArticleCategory(IDataReader dr)
		{
			return new ArticleCategory()
			{
				ID = DataRecordHelper.GetInt32(dr, "CategoryID"),
				Name = DataRecordHelper.GetString(dr, "CategoryName"),
				Description = DataRecordHelper.GetString(dr, "CategoryDesc"),
				Memo = DataRecordHelper.GetString(dr, "CategoryMemo"),
				ParentID = DataRecordHelper.GetNullableInt32(dr, "ParentID"),
				DisplayOrder = DataRecordHelper.GetInt32(dr, "DisplayOrder"),
				Status = (ComponentStatus)DataRecordHelper.GetInt32(dr, "CategoryStatus"),
				CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime"),
				CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser"),
				UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime"),
				UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser"),
			};
		}

		/// <summary>
		/// 标题前缀
		/// </summary>
		/// <param name="dr"></param>
		/// <returns></returns>
		public static TitlePrefixes ParseTitlePrefixes(IDataReader dr)
		{
			return new TitlePrefixes()
			{
				ID = DataRecordHelper.GetInt32(dr, "TitleID"),
				Name = DataRecordHelper.GetString(dr, "Name"),
			};
		}
	}
}
