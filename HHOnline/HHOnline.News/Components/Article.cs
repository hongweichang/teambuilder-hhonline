using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Framework;
using HHOnline.News.Services;

namespace HHOnline.News.Components
{
	/// <summary>
	/// 文章信息类
	/// </summary>
	[Serializable]
	public class Article : ExtendedAttributes
	{
		/// <summary>
		/// 索引号
		/// </summary>
		public int ID { get; set; }
		/// <summary>
		/// 标题
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// 子标题
		/// </summary>
		public string SubTitle { get; set; }
		/// <summary>
		/// 摘要
		/// </summary>
		public string Abstract { get; set; }
		/// <summary>
		/// 内容
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		/// 发布时间
		/// </summary>
		public DateTime? Date { get; set; }
		/// <summary>
		/// 文章转摘自
		/// </summary>
		public string CopyFrom { get; set; }
		/// <summary>
		/// 作者
		/// </summary>
		public string Author { get; set; }
		/// <summary>
		/// 关键字
		/// </summary>
		public string Keywords { get; set; }
		/// <summary>
		/// 文章图像
		/// </summary>
		public int Image { get; set; }
		/// <summary>
		/// 分类
		/// </summary>
		public int Category { get; set; }
		/// <summary>
		/// 显示顺序
		/// </summary>
		public int DisplayOrder { get; set; }
		/// <summary>
		/// 描述
		/// </summary>
		public string ArticleMemo { get; set; }
		/// <summary>
		/// 记录状态
		/// </summary>
		public ComponentStatus Status { get; set; }
		/// <summary>
		/// 点击次数
		/// </summary>
		public int HitTimes { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 发布者
		/// </summary>
		public int CreateUser { get; set; }
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime UpdateTime { get; set; }
		/// <summary>
		/// 更新用户
		/// </summary>
		public int UpdateUser { get; set; }

		///// <summary>
		///// 存储文件信息
		///// </summary>
		//public IStorageFile File
		//{
		//    get
		//    {
		//        if (file == null)
		//        {
		//            if (this.ID > 0)
		//            {
		//                file = FileStorageProvider.Instance(ArticleAttachments.FileStoreKey)
		//                    .GetFile(ArticleAttachments.MakePath(this.ID), this.FileName);
		//            }
		//        }

		//        return file;
		//    }
		//    set
		//    {
		//        file = value;
		//    }
		//}

		///// <summary>
		///// 默认图片文件
		///// </summary>
		//public IStorageFile DefaultImageFile
		//{
		//    get
		//    {
		//        if (defaultImageFile == null)
		//        {
		//            defaultImageFile = File;
		//        }
		//        return defaultImageFile;
		//    }
		//}

		/// <summary>
		/// 获取资讯分类
		/// </summary>
		/// <returns></returns>
		public ArticleCategory CategoryObject
		{
			get
			{
				return ArticleManager.GetArticleCategory(Category);
			}
		}

		/// <summary>
		/// 获取默认图像
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public string GetDefaultImageUrl(int width, int height)
		{
			//if (width == 0 || height == 0)
			//{
			//    if (DefaultImageFile != null)
			//        return FileStorageProvider.GetGenericDownloadUrl(DefaultImageFile);
			//    else
			//        return SiteUrlManager.GetNoPictureUrl();
			//}
			//else
			//{
			//    if (DefaultImageFile == null)
			//    {
			//        return SiteUrlManager.GetNoPictureUrl(width, height);
			//    }
			//    else
			//    {
			//        return SiteUrlManager.GetResizedImageUrl(DefaultImageFile, width, height);
			//    }
			//}
			return SiteUrlManager.GetNoPictureUrl();
		}
	}
}
