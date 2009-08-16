using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.News.Providers;
using HHOnline.News.Components;
using HHOnline.Framework;
using System.Data;
using HHOnline.News.Services;
using HHOnline.Common;

namespace HHOnline.Data
{
	public class SqlArticleAttachmentProvider : ArticleAttachmentProvider
	{
		public override List<ArticleAttachment> GetAllArticleAttachments()
		{
			using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ArticleAttachment_GetAll"))
			{
				List<ArticleAttachment> result = new List<ArticleAttachment>();
				for (; dr.Read(); )
				{
					ArticleAttachment article = ArticleReaderConverter.ParseArticleAttachment(dr);
					result.Add(article);
				}

				return result;
			}
		}

		public override List<ArticleAttachment> GetAllArticleAttachments(AttachmentQuery query, out int totalRecord)
		{
			ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@PageIndex",DbType.Int32,DataHelper.GetSafeSqlInt(query.PageIndex)),
                new ELParameter("@PageSize",DbType.Int32,DataHelper.GetSafeSqlInt(query.PageSize)),
                new ELParameter("@SqlPopulate",DbType.String,QueryGenerator.BuildAttachmentQuery(query))
            };

			using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ArticleAttachments_Get", elParameters))
			{
				List<ArticleAttachment> attachmentList = new List<ArticleAttachment>();
				while (dr.Read())
				{
					attachmentList.Add(ArticleReaderConverter.ParseArticleAttachment(dr));
				}

				dr.NextResult();
				dr.Read();
				totalRecord = DataRecordHelper.GetInt32(dr, 0);

				return attachmentList;
			}
		}

		public override ArticleAttachment GetArticleAttachment(int id)
		{
			ELParameter idParam = new ELParameter("@AttachmentID", DbType.Int32);
			idParam.Value = id;

			using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ArticleAttachment_Get", idParam))
			{
				ArticleAttachment result = null;
				if (dr.Read())
				{
					result = ArticleReaderConverter.ParseArticleAttachment(dr);
				}

				return result;
			}
		}

		public override ArticleAttachment CreateUpdateArticleAttachment(ArticleAttachment info, HHOnline.Framework.DataProviderAction action, out HHOnline.Framework.DataActionStatus status)
		{
			ELParameter[] parms = new ELParameter[]
			{
				action == DataProviderAction.Create ?
					new ELParameter("@AttachmentID", DbType.Int32, 4, ParameterDirection.Output) :
					new ELParameter("@AttachmentID", DbType.Int32, info.ID),
				new ELParameter("@AttachmentName", DbType.String, info.Name),
				new ELParameter("@AttachmentFile", DbType.String, info.FileName),
				new ELParameter("@ContentType", DbType.String, info.ContentType),
				new ELParameter("@ContentSize", DbType.Int32, info.ContentSize),
				new ELParameter("@IsRemote", DbType.Int32, info.IsRemote ? 1 : 0),
				new ELParameter("@ImageWidth", DbType.Int32, info.ImageWidth == null ? (object)DBNull.Value : (object)info.ImageWidth),
				new ELParameter("@ImageHeight", DbType.Int32, info.ImageHeight == null ? (object)DBNull.Value : (object)info.ImageHeight),
				new ELParameter("@AttachmentDesc", DbType.String, info.Desc),
				new ELParameter("@AttachmentMemo", DbType.String, info.Memo),
				new ELParameter("@AttachmentStatus", DbType.Int32, info.Status),
				new ELParameter("@User", DbType.Int32, GlobalSettings.GetCurrentUser().UserID),
				new ELParameter("@Action", DbType.Int32, action),
			};

			status = (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ArticleAttachment_CreateUpdate", parms));
			if (status == DataActionStatus.Success && action == DataProviderAction.Create)
			{
				info.ID = Convert.ToInt32(parms[0].Value);
			}

			return info;
		}

		public override DataActionStatus DeleteArticleAttachment(int id)
		{
			ELParameter idParam = new ELParameter("@AttachmentID", DbType.Int32);
			idParam.Value = id;

			DataActionStatus result = (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ArticleAttachment_Delete", idParam));

			return result;
		}

		public override DataActionStatus DeleteArticleAttachments(string ids)
		{
			ELParameter idParam = new ELParameter("@IDList", DbType.String);
			idParam.Value = ids;

			return (DataActionStatus)DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ArticleAttachments_Delete", idParam);
		}
	}
}
