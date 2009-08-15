using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HHOnline.Framework
{
    /// <summary>
    /// 展示图像管理类
    /// </summary>
    public class ShowPictures
    {
        public static string FileStoreKey = "ShowPictures";

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="contentStream"></param>
        /// <returns></returns>
        public static DataActionStatus Create(ShowPicture picture, Stream contentStream)
        {
            SiteSettings setting = SiteSettingsManager.GetSiteSettings();
            picture.ShowPictureID = Guid.NewGuid();
            setting.ShowPictures.Add(picture);
            SiteSettingsManager.Save(setting);

            FileStorageProvider fs = new FileStorageProvider(FileStoreKey);
            fs.AddUpdateFile("", picture.FileName, contentStream);
            return DataActionStatus.Success;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pictureID"></param>
        /// <returns></returns>
        public static DataActionStatus Delete(Guid pictureID)
        {
            SiteSettings setting = SiteSettingsManager.GetSiteSettings();
            int index = setting.ShowPictures.FindIndex(delegate(ShowPicture p)
            {
                return p.ShowPictureID == pictureID;
            });
            setting.ShowPictures.RemoveAt(index);
            SiteSettingsManager.Save(setting);
            return DataActionStatus.Success;
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="pictureID"></param>
        /// <returns></returns>
        public static ShowPicture Get(Guid pictureID)
        {
            SiteSettings setting = SiteSettingsManager.GetSiteSettings();
            ShowPicture item = setting.ShowPictures.Find(delegate(ShowPicture p)
            {
                return p.ShowPictureID == pictureID;
            });
            return item;
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="contentStream"></param>
        /// <returns></returns>
        public static DataActionStatus Update(ShowPicture picture, Stream contentStream)
        {
            SiteSettings setting = SiteSettingsManager.GetSiteSettings();
            int index = setting.ShowPictures.FindIndex(delegate(ShowPicture p)
            {
                return p.ShowPictureID == picture.ShowPictureID;
            });
            setting.ShowPictures[index] = picture;

            SiteSettingsManager.Save(setting);

            FileStorageProvider fs = new FileStorageProvider(FileStoreKey);
            fs.AddUpdateFile("", picture.FileName, contentStream);

            return DataActionStatus.Success;
        }

        /// <summary>
        /// 获取所有展示图片信息
        /// </summary>
        /// <returns></returns>
        public static List<ShowPicture> GetShowPictures()
        {
            SiteSettings setting = SiteSettingsManager.GetSiteSettings();
            setting.ShowPictures.Sort();
            return setting.ShowPictures;
        }
    }
}
