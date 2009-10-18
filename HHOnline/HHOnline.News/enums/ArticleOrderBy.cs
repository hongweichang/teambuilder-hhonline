using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.News.enums
{
	/// <summary>
	/// 资讯排序依据 
	/// </summary>
	public enum ArticleOrderBy
	{
		/// <summary>
		/// 按标题排序
		/// </summary>
		Title,

		/// <summary>
		/// 创建时间
		/// </summary>
		CreateTime,

		/// <summary>
		/// 修改时间
		/// </summary>
		UpdateTime,

		/// <summary>
		/// 点击次数
		/// </summary>
		HitTimes,

		/// <summary>
		/// 分类
		/// </summary>
		Category,
	}
}
