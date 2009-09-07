using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.News.enums;
using HHOnline.Framework;
using System.Collections.Specialized;

namespace HHOnline.News.Components
{
	/// <summary>
	/// 附件查询类
	/// </summary>
	public class AttachmentQuery
	{
		private int pageIndex = 0;
		private int pageSize = 20;

		/// <summary>
		/// 页索引
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
			set
			{
				this.pageIndex = value;
			}
		}

		/// <summary>
		/// 页大小
		/// </summary>
		public int PageSize
		{
			get
			{
				return this.pageSize;
			}
			set
			{
				this.pageSize = value;
			}
		}

		/// <summary>
		/// 附件排序依据
		/// </summary>
		public AttachmentOrderBy AttachmentOrderBy = AttachmentOrderBy.UpdateTime;
		/// <summary>
		/// 排序方向
		/// </summary>
		public SortOrder SortOrder = SortOrder.Descending;
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }

		public string ContentType { get; set; }
		/// <summary>
		/// 内容开始大小
		/// </summary>
		public int? ContentStartSize { get; set; }
		/// <summary>
		/// 内容结束大小
		/// </summary>
		public int? ContentEndSize { get; set; }
		/// <summary>
		/// 创建开始时间
		/// </summary>
		public DateTime? CreateStartTime { get; set; }
		/// <summary>
		/// 创建结束时间
		/// </summary>
		public DateTime? CreateEndTime { get; set; }

		/// <summary>
		/// 根据QueryString生成查询对象
		/// </summary>
		/// <param name="queryString"></param>
		/// <returns></returns>
		public static AttachmentQuery GetQueryFromQueryString(NameValueCollection queryString)
		{
			AttachmentQuery result = new AttachmentQuery();

			if (!string.IsNullOrEmpty(queryString["name"]))
			{
				result.Name = queryString["name"];
			}

			if (!string.IsNullOrEmpty(queryString["css"]))
			{
				result.ContentStartSize = int.Parse(queryString["css"]);
			}

			if (!string.IsNullOrEmpty(queryString["ces"]))
			{
				result.ContentEndSize = int.Parse(queryString["ces"]);
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
				AttachmentOrderBy sortBy = (AttachmentOrderBy)Enum.Parse(typeof(AttachmentOrderBy), GlobalSettings.IsNullOrEmpty(queryString["sb"]) ? "1" : int.Parse(queryString["sb"]).ToString(), true);
				result.AttachmentOrderBy = sortBy;
			}
			catch
			{
				result.AttachmentOrderBy = AttachmentOrderBy.CreateTime;
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
