using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.News.Components
{
	/// <summary>
	/// 标题前缀
	/// </summary>
	[Serializable]
	public class TitlePrefixes
	{
		/// <summary>
		/// 前缀ID
		/// </summary>
		public int ID { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
	}
}
