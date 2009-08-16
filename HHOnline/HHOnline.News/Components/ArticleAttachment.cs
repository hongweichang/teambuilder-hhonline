using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Framework;
using HHOnline.News.Services;

namespace HHOnline.News.Components
{
	/// <summary>
	/// 资讯附件
	/// </summary>
	[Serializable]
	public class ArticleAttachment
	{
		/// <summary>
		/// 附件ID
		/// </summary>
		public int ID { get; set; }
		/// <summary>
		/// 文件名
		/// </summary>
		public string FileName { get; set; }
		/// <summary>
		/// 附件名
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 附件类型
		/// </summary>
		public string ContentType { get; set; }
		/// <summary>
		/// 附件大小
		/// </summary>
		public int ContentSize { get; set; }
		/// <summary>
		/// 图像宽度
		/// </summary>
		public int? ImageWidth { get; set; }
		/// <summary>
		/// 图像高度
		/// </summary>
		public int? ImageHeight { get; set; }
		/// <summary>
		/// 描述
		/// </summary>
		public string Desc { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public string Memo { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		public ComponentStatus Status { get; set; }
		/// <summary>
		/// 是否远程文件
		/// </summary>
		public bool IsRemote { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 上传用户
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

		private IStorageFile defaultImageFile;
		private IStorageFile file;

		/// <summary>
		/// 存储文件信息
		/// </summary>
		public IStorageFile File
		{
			get
			{
				if (file == null)
				{
					if (this.ID > 0)
					{
						file = FileStorageProvider.Instance(ArticleAttachments.FileStoreKey)
							.GetFile(ArticleAttachments.MakePath(this.ID), this.FileName);
					}
				}

				return file;
			}
			set
			{
				file = value;
			}
		}

		/// <summary>
		/// 默认图片文件
		/// </summary>
		public IStorageFile DefaultImageFile
		{
			get
			{
				if (defaultImageFile == null)
				{
					defaultImageFile = File;
				}
				return defaultImageFile;
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
			if (width == 0 || height == 0)
			{
				if (DefaultImageFile != null)
					return FileStorageProvider.GetGenericDownloadUrl(DefaultImageFile);
				else
					return SiteUrlManager.GetNoPictureUrl();
			}
			else
			{
				if (DefaultImageFile == null)
				{
					return SiteUrlManager.GetNoPictureUrl(width, height);
				}
				else
				{
					return SiteUrlManager.GetResizedImageUrl(DefaultImageFile, width, height);
				}
			}
		}
	}
}
