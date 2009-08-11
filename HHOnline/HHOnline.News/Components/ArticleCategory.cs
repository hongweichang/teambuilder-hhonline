using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Framework;

namespace HHOnline.News.Components
{
	/// <summary>
	/// 文章分类
	/// </summary>
	[Serializable]
	public class ArticleCategory : ExtendedAttributes
	{
		/// <summary>
		/// 文章分类ID
		/// </summary>
		public int ID { get; set; }
		/// <summary>
		/// 分类名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 分类描述
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public string Memo { get; set; }
		/// <summary>
		/// 父分类ID
		/// </summary>
		public int? ParentID { get; set; }
		/// <summary>
		/// 显示排序索引
		/// </summary>
		public int DisplayOrder { get; set; }
		/// <summary>
		/// 分类状态
		/// </summary>
		public ComponentStatus Status { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 创建用户
		/// </summary>
		public int CreateUser { get; set; }
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime? UpdateTime { get; set; }
		/// <summary>
		/// 更新用户
		/// </summary>
		public int UpdateUser { get; set; }
	}
}
