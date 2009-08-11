using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HHOnline.Framework
{
    public class SiteUrlManager
    {
        /// <summary>
        /// 获取下载地址
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetGenericDownloadUrl(IStorageFile file)
        {
            string key = "__key/" + GlobalSettings.UrlEncodePathComponent(file.FileStoreKey);

            if (!string.IsNullOrEmpty(file.FilePath))
                key += "/" + GlobalSettings.UrlEncodePathComponent(file.FilePath);

            key += "/" + GlobalSettings.UrlEncodeFileComponent(file.FileName);

            string url = string.Format("attachment.axd/{0}", key);

            return url;
        }

        /// <summary>
        /// 获取可变大小图像地址
        /// </summary>
        /// <param name="file"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static string GetResizedImageUrl(IStorageFile file, int width, int height)
        {
            string key = "__size/" + width.ToString() + "x" + height.ToString();

            key += "/__key/" + GlobalSettings.UrlEncodePathComponent(file.FileStoreKey);

            if (!GlobalSettings.IsNullOrEmpty(file.FilePath))
                key += "/" + GlobalSettings.UrlEncodePathComponent(file.FilePath);

            key += "/" + GlobalSettings.UrlEncodeFileComponent(file.FileName);

            string url = string.Format("image.axd/{0}", key);

            return url;
        }

        /// <summary>
        /// 获取NoPictureUrl
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static string GetNoPictureUrl(int width, int height)
        {
            string fileName = "nopicture.gif";
            SiteFile file = SiteFiles.GetFile("NoPicture", fileName);
            if (file == null)
            {
                using (Stream sourceStream = new FileStream(GlobalSettings.MapPath(GetNoPictureUrl()), FileMode.Open, FileAccess.Read))
                {
                    SiteFiles.AddFile(sourceStream, "NoPicture", fileName);
                    file = SiteFiles.GetFile("NoPicture", fileName);
                }
            }
            return GetResizedImageUrl(file.File, width, height);
            /*
            string fileName = "nopicture.gif";

            string resizedFileName = fileName + "-" + width.ToString() + "x" + height.ToString() + ".";

            System.Drawing.Imaging.ImageFormat format;

            string extension = GlobalSettings.GetExtension(fileName).ToLower();

            if (extension == "png" || extension == "gif")
            {
                format = System.Drawing.Imaging.ImageFormat.Png;
                resizedFileName += "png";
            }
            else
            {
                format = System.Drawing.Imaging.ImageFormat.Jpeg;
                resizedFileName += "jpg";
            } 
            
            string url = "~/Utility/Images/" + resizedFileName;

            string path = GlobalSettings.MapPath(url);

            if (!System.IO.File.Exists(path))
            {

                using (Stream sourceStream = new FileStream(GlobalSettings.MapPath(GetNoPictureUrl()), FileMode.Open, FileAccess.Read))
                {
                    using (MemoryStream imageStream = GlobalSettings.ResizeImage(sourceStream, height, width, format))
                    {
                        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                        {
                            imageStream.WriteTo(fs);
                            fs.Close();
                        }
                    }
                }
            }
            return GlobalSettings.ResolveUrl(url);
             * */
        }

        /// <summary>
        /// 获取NoPictureUrl
        /// </summary>
        /// <returns></returns>
        public static string GetNoPictureUrl()
        {
            return GlobalSettings.ResolveUrl("~/Utility/Images/nopicture.gif");
        }

        /// <summary>
        /// 获取展示图片Url，大小为448*201
        /// </summary>
        /// <returns></returns>
        public static string GetShowPicture()
        {
            return GetShowPicture(448, 201);
        }

        /// <summary>
        /// 获取展示图片Url
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public static string GetShowPicture(int width, int height)
        {
            string fileName = "ad.jpg";
            SiteSettings settings = SiteSettingsManager.GetSiteSettings();
            if (!String.IsNullOrEmpty(settings.ShowPicture.Trim()))
                fileName = settings.ShowPicture.Trim();
            SiteFile file = SiteFiles.GetFile("ShowPicture", fileName);
            if (file == null)
            {
                using (Stream sourceStream = new FileStream(GlobalSettings.MapPath(GetDefaultShowPictureUrl()), FileMode.Open, FileAccess.Read))
                {
                    SiteFiles.AddFile(sourceStream, "ShowPicture", fileName);
                    file = SiteFiles.GetFile("ShowPicture", fileName);
                    settings.ShowPicture = string.Empty;
                    SiteSettingsManager.Save(settings);
                }
            }
            return GetResizedImageUrl(file.File, width, height);
        }

        public static string GetDefaultShowPictureUrl()
        {
            return GlobalSettings.ResolveUrl("~/Utility/Images/ad.jpg");
        }

        /// <summary>
        ///  获取插入媒体链接
        /// </summary>
        /// <returns></returns>
        public static string GetInsertMediaUrl()
        {
            return GlobalSettings.ResolveUrl("~/Utility/InsertMedia.aspx");
        }
    }
}
