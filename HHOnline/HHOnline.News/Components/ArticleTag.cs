using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.News.Components
{
	/// <summary>
	/// 文章Tag
	/// </summary>
	[Serializable]
	public class ArticleTag
	{
		/// <summary>
		/// Tag ID
		/// </summary>
		public int ID { get; set; }
		/// <summary>
		/// Tag Name
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 命中次数
		/// </summary>
		public int ItemCount { get; set; }
		/// <summary>
		/// 创建日期
		/// </summary>
		public DateTime DateCreated { get; set; }
	}
}
