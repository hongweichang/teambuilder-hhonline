using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using HHOnline.News.enums;
using HHOnline.Framework;

namespace HHOnline.News.Components
{
	/// <summary>
	/// 文章查询接口
	/// </summary>
	public class ArticleQuery
	{
		private int pageIndex = 0;

		/// <summary>
		/// 页面索引
		/// </summary>
		public int PageIndex
		{
			get
			{
				if (this.pageIndex >= 0)
				{
					return this.pageIndex;
				}

				return 0;
			}
			set { pageIndex = value; }
		}
		private int pageSize = 20;

		/// <summary>
		/// 页面大小
		/// </summary>
		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value; }
		}

		/// <summary>
		/// 标题
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// 组织机构ID
		/// </summary>
		public int? CategoryID { get; set; }
		/// <summary>
		/// 创建开始时间
		/// </summary>
		public DateTime? CreateStartTime { get; set; }
		/// <summary>
		/// 创建结束时间
		/// </summary>
		public DateTime? CreateEndTime { get; set; }

		/// <summary>
		/// 点击次数开始
		/// </summary>
		public int? HitStartTimes { get; set; }
		/// <summary>
		/// 点击次数结束
		/// </summary>
		public int? HitEndTimes { get; set; }

		/// <summary>
		/// 文章排序依据
		/// </summary>
		public ArticleOrderBy ArticleOrderBy = ArticleOrderBy.UpdateTime;

		/// <summary>
		/// 排序方向
		/// </summary>
		public SortOrder SortOrder = SortOrder.Descending;

		/// <summary>
		/// 根据QueryString生成查询对象
		/// </summary>
		/// <param name="queryString"></param>
		/// <returns></returns>
		public static ArticleQuery GetQueryFromQueryString(NameValueCollection queryString)
		{
			ArticleQuery result = new ArticleQuery();

			if (!string.IsNullOrEmpty(queryString["title"]))
			{
				result.Title = queryString["title"];
			}

			if (!string.IsNullOrEmpty(queryString["cat"]))
			{
				string catStr = queryString["cat"];
				if (catStr != "-1")
				{
					result.CategoryID = int.Parse(catStr);
				}
			}

			if (!string.IsNullOrEmpty(queryString["hst"]))
			{
				result.HitStartTimes = int.Parse(queryString["hst"]);
			}

			if (!string.IsNullOrEmpty(queryString["het"]))
			{
				result.HitEndTimes = int.Parse(queryString["het"]);
			}

			if (!string.IsNullOrEmpty(queryString["cst"]))
			{
				result.CreateStartTime = DateTime.Parse(queryString["cst"]);
			}

			if (!string.IsNullOrEmpty(queryString["cet"]))
			{
				result.CreateEndTime = DateTime.Parse(queryString["cet"]);
			}

			// OrderBy
			try
			{
				ArticleOrderBy sortBy = (ArticleOrderBy)Enum.Parse(typeof(ArticleOrderBy), GlobalSettings.IsNullOrEmpty(queryString["sb"]) ? "1" : int.Parse(queryString["sb"]).ToString(), true);
				result.ArticleOrderBy = sortBy;
			}
			catch
			{
				result.ArticleOrderBy = ArticleOrderBy.CreateTime;
			}

			// Sort Order
			try
			{
				SortOrder sortOrder = (SortOrder)Enum.Parse(typeof(SortOrder), GlobalSettings.IsNullOrEmpty(queryString["so"]) ? "1" : int.Parse(queryString["so"]).ToString(), true);
				result.SortOrder = sortOrder;
			}
			catch
			{
				result.SortOrder = SortOrder.Descending;
			}

			return result;
		}
	}
}
