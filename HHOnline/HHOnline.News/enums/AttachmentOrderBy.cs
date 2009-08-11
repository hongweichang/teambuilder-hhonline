using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.News.enums
{
	/// <summary>
	/// 附件排序依据
	/// </summary>
	public enum AttachmentOrderBy
	{
		/// <summary>
		/// 按名称排序
		/// </summary>
		Name,

		/// <summary>
		/// MIME类型
		/// </summary>
		ContentType,

		/// <summary>
		/// 附件大小
		/// </summary>
		ContentSize,

		/// <summary>
		/// 创建时间
		/// </summary>
		CreateTime,

		/// <summary>
		/// 修改时间
		/// </summary>
		UpdateTime,
	}
}
