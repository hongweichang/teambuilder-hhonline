using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using HHOnline.Cache;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    /// <summary>
    /// 临时附件管理类
    /// </summary>
    public class TemporaryAttachments
    {
        public static string FileStoreKey = "TemporaryAttachment";

        /// <summary>
        /// 创建临时附件
        /// </summary>
        /// <param name="attachment"></param>
        public static void Create(TemporaryAttachment attachment, Stream stream)
        {
            if (GlobalSettings.IsImage(attachment.FileName))
            {
                try
                {
                    ImageInfo info = new ImageInfo(stream);
                    info.Check();
                    attachment.Width = info.Width;
                    attachment.Height = info.Height;
                }
                catch { }
            }
            attachment.ContentType = MimeTypeManager.GetMimeType(attachment.FileName);
            attachment.ContentSize = stream.Length;
            CommonDataProvider.Instance.CreateUpdateTemporaryAttachment(attachment, DataProviderAction.Create);
            if (stream != null)
            {
                FileStorageProvider fs = new FileStorageProvider(FileStoreKey);
                fs.AddUpdateFile(MakePath(attachment), attachment.FileName, stream);
            }
        }

        /// <summary>
        /// 更改临时附件
        /// </summary>
        /// <param name="attachment"></param>
        public static void Update(TemporaryAttachment attachment)
        {
            CommonDataProvider.Instance.CreateUpdateTemporaryAttachment(attachment, DataProviderAction.Update);
        }

        /// <summary>
        /// 获取附件信息
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="attachmentType">附件类型<paramref name="AttachmentType"/></param>
        /// <returns>附件信息集合</returns>
        public static List<TemporaryAttachment> GetTemporaryAttachments(int userID, AttachmentType attachmentType)
        {
            return CommonDataProvider.Instance.GetTemporaryAttachments(userID, attachmentType);
        }

        /// <summary>
        /// 根据临时Guid值获取附件信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static TemporaryAttachment GetTemporaryAttachment(Guid attachmentID)
        {
            return CommonDataProvider.Instance.GetTemporaryAttachment(attachmentID);
        }

        /// <summary>
        /// 删除附件信息
        /// </summary>
        /// <param name="attachmentID"></param>
        public static void Delete(Guid attachmentID)
        {
            TemporaryAttachment temporaryAttachment = GetTemporaryAttachment(attachmentID);
            if (temporaryAttachment != null)
            {

                CommonDataProvider.Instance.DeleteTemporaryAttachment(attachmentID);
                FileStorageProvider fs = new FileStorageProvider(FileStoreKey);
                fs.Delete(MakePath(temporaryAttachment), temporaryAttachment.FileName);
            }
        }

        /// <summary>
        /// 生成路径
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public static string MakePath(TemporaryAttachment attachment)
        {
            return FileStorageProvider.MakePath(new string[] { attachment.UserID.ToString(), attachment.AttachmentType.ToString() });
        }
    }
}
