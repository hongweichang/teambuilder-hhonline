using System;
using System.Collections.Generic;
using System.Text;

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
		/// 组织机构ID
		/// </summary>
		public int? CategoryID { get; set; }
	}
}
