using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Framework
{
    /// <summary>
    /// 临时附件
    /// </summary>
    public class TemporaryAttachment
    {
        private IStorageFile file;
        private Guid attachmentID;
        private string fileName;
        private string friendlyFileName;
        private string contentType;
        private long contentSize;
        private bool isRemote;
        private int width;
        private int height;
        private int displayOrder;
        private int thumbnailWidth = 40;
        private int thumbnailHeight = 40;
        private AttachmentType attachmentType = AttachmentType.ProductPhoto;
        private int userID;

        /// <summary>
        /// 存储文件信息
        /// </summary>
        public IStorageFile File
        {
            get
            {
                if (file == null)
                {
                    FileStorageProvider fs = new FileStorageProvider(TemporaryAttachments.FileStoreKey);
                    file = fs.GetFile(TemporaryAttachments.MakePath(this), this.FileName);
                }
                return file;
            }
        }

        /// <summary>
        /// 获取下载地址
        /// </summary>
        public string Url
        {
            get
            {
                if (this.File != null)
                {
                    return FileStorageProvider.GetGenericDownloadUrl(File);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 临时附件ID
        /// </summary>
        public Guid AttachmentID
        {
            get
            {
                return this.attachmentID;
            }
            set
            {
                this.attachmentID = value;
            }
        }

        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID
        {
            get
            {
                return this.userID;
            }
            set
            {
                this.userID = value;
            }
        }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
            }
        }

        /// <summary>
        /// 文件友好名称
        /// </summary>
        public string FriendlyFileName
        {
            get
            {
                return this.friendlyFileName;
            }
            set
            {
                this.friendlyFileName = value;
            }
        }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string ContentType
        {
            get
            {
                return contentType;
            }
            set
            {
                contentType = value;
            }
        }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long ContentSize
        {
            get
            {
                return contentSize;
            }
            set
            {
                contentSize = value;
            }
        }

        /// <summary>
        /// 是否远程附件
        /// </summary>
        public bool IsRemote
        {
            get
            {
                return isRemote;
            }
            set
            {
                isRemote = value;
            }
        }

        /// <summary>
        /// 图像宽度
        /// </summary>
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        /// <summary>
        /// 图像高度
        /// </summary>
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        /// <summary>
        /// 排列序号
        /// </summary>
        public int DisplayOrder
        {
            get
            {
                return displayOrder;
            }
            set
            {
                displayOrder = value;
            }
        }

        /// <summary>
        /// 附件类型
        /// </summary>
        public AttachmentType AttachmentType
        {
            get
            {
                return attachmentType;
            }
            set
            {
                attachmentType = value;
            }
        }

        /// <summary>
        /// 缩略图宽度
        /// </summary>
        public int ThumbnailWidth
        {
            get
            {
                return thumbnailWidth;
            }
            set
            {
                thumbnailWidth = value;
            }
        }

        /// <summary>
        /// 缩略图高度
        /// </summary>
        public int ThumbnailHeight
        {
            get
            {
                return thumbnailHeight;
            }
            set
            {
                thumbnailHeight = value;
            }
        }

        /// <summary>
        /// 缩略图地址
        /// </summary>
        public string ThumbnailUrl
        {
            get
            {
                if (this.File != null)
                    return SiteUrlManager.GetResizedImageUrl(this.File, thumbnailWidth, thumbnailHeight);
                else
                    return SiteUrlManager.GetNoPictureUrl(thumbnailWidth, thumbnailHeight);
            }
        }
    }
}
