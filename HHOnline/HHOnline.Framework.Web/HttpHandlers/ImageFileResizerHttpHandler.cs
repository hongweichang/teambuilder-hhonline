using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Web;

namespace HHOnline.Framework.Web.HttpHandlers
{
    public class ImageFileResizerHttpHandler : IHttpHandler
    {

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string path = context.Request.Path;
            path = path.Substring(path.IndexOf("__size/") + 7);
            string[] imageSize = path.Substring(0, path.IndexOf("/__key/")).Split('x');
            int width = Convert.ToInt32(imageSize[0]);
            int height = Convert.ToInt32(imageSize[1]);
            path = path.Substring(path.IndexOf("__key/") + 6);

            string fileStoreKey = path.Substring(0, path.IndexOf('/'));
            string fileName = path.Substring(path.LastIndexOf('/') + 1);

            if (path.Length - (fileStoreKey.Length + fileName.Length + 2) <= 0)
                path = string.Empty;
            else
                path = path.Substring(fileStoreKey.Length + 1, path.Length - (fileStoreKey.Length + fileName.Length + 2));

            fileStoreKey = GlobalSettings.UrlDecodePathComponent(fileStoreKey);
            fileName = GlobalSettings.UrlDecodeFileComponent(fileName);
            path = GlobalSettings.UrlDecodePathComponent(path);

            context.Response.Cache.SetAllowResponseInBrowserHistory(true);
            context.Response.Cache.SetValidUntilExpires(true);
            context.Response.Cache.SetExpires(DateTime.Now.AddDays(1));
            context.Response.Cache.SetLastModified(DateTime.Now);
            IStorageFile file = GetResizedFileUrl(fileStoreKey, path, fileName, width, height);
            if (file != null)
            {
                context.Response.Redirect(file.GetDownloadUrl());
                context.Response.StatusCode = 301;
            }
            else
                context.Response.StatusCode = 404;

        }

        public IStorageFile GetResizedFileUrl(string fileStoreKey, string path, string fileName, int width, int height)
        {
            string resizedFileName = fileName + "-" + width.ToString() + "x" + height.ToString() + ".";
            string resizedPath = Path.Combine(path, "ResizeImages");
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
            FileStorageProvider fileProvider = new FileStorageProvider(fileStoreKey);
            IStorageFile file = fileProvider.GetFile(resizedPath, resizedFileName);
            if (file == null)
            {
                IStorageFile originalFile = null;

                if (fileProvider != null)
                    originalFile = fileProvider.GetFile(path, fileName);

                if (originalFile != null)
                {
                    using (Stream sourceStream = originalFile.OpenReadStream())
                    {
                        using (Stream imageStream = GlobalSettings.ResizeImage(sourceStream, height, width, format))
                        {
                            file = fileProvider.AddUpdateFile(resizedPath, resizedFileName, imageStream);
                            imageStream.Close();
                        }
                        sourceStream.Close();
                    }
                }
            }
            return file;
        }
    }
}
