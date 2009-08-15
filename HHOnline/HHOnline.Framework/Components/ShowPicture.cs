using System;
using System.Xml;
using System.Xml.Serialization;


namespace HHOnline.Framework
{
    /// <summary>
    /// 展示图片
    /// </summary>
    [Serializable]
    public class ShowPicture : IComparable
    {
        /// <summary>
        /// 展示图片ID
        /// </summary>
        public Guid ShowPictureID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 图片转向链接
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 排列顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        private IStorageFile _file = null;

        /// <summary>
        /// 存储文件信息
        /// </summary>
        [XmlIgnore]
        public IStorageFile File
        {
            get
            {
                if (_file == null)
                {
                    _file = FileStorageProvider.Instance(ShowPictures.FileStoreKey)
                        .GetFile("", this.FileName);
                }
                return _file;
            }
            set
            {
                _file = value;
            }
        }

        /// <summary>
        /// 图片地址
        /// </summary>
        [XmlIgnore]
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

        public int CompareTo(object obj)
        {
            if (!(obj is ShowPicture))
                throw new ArgumentException("Specified object is not of type ShowPicture");
            ShowPicture picture = (ShowPicture)obj;
            return this.DisplayOrder.CompareTo(picture.DisplayOrder);
        }
    }
}
