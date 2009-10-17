using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using HHOnline.Common;
using System.Configuration.Provider;

namespace HHOnline.Framework
{
    /// <summary>
    /// 全局方法
    /// </summary>
    public class GlobalSettings
    {
        public static readonly string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly string encryptKey = HHConfiguration.GetConfig()["encryptKey"].ToString();

        #region -Helper Method-
        /// <summary>
        /// 判断文本是否为空
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(string text)
        {
            return TypeHelper.IsNullOrEmpty(text);
        }

        /// <summary>
        /// 对文本中包含的特殊字符进行HTML Encoded
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string EnsureHtmlEncoded(string text)
        {
            return WebHelper.EnsureHtmlEncoded(text);
        }

        /// <summary>
        /// 对路径进行反编码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UrlDecodePathComponent(string text)
        {
            return WebHelper.UrlDecodePathComponent(text);
        }

        /// <summary>
        /// 对路径进行编码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UrlEncodePathComponent(string text)
        {
            return WebHelper.UrlEncodePathComponent(text);
        }

        /// <summary>
        /// 对文件名进行编码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UrlEncodeFileComponent(string text)
        {
            return WebHelper.UrlEncodeFileComponent(text);
        }

        /// <summary>
        /// 对文件名进行反编码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UrlDecodeFileComponent(string text)
        {
            return WebHelper.UrlDecodeFileComponent(text);
        }

        /// <summary>
        /// 去除数据库特殊字符，防止SQL Injection
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static string CleanSearchString(string searchString)
        {
            return TypeHelper.CleanSearchString(searchString);
        }

        /// <summary>
        /// 获取文件存储位置
        /// </summary>
        /// <param name="fileStoreKey">文件Key</param>
        /// <returns></returns>
        public static string GetFileStorageFolder(string fileStoreKey)
        {
            string baseFolder = HHConfiguration.GetConfig()["fileStorePath"].ToString();
            baseFolder = MapPath(baseFolder);
            baseFolder = Path.Combine(baseFolder, fileStoreKey);
            EnsureDirectoryExists(baseFolder);
            return baseFolder;
        }

        /// <summary>
        /// 确保文件路径存在
        /// </summary>
        /// <param name="directoryName"></param>
        public static void EnsureDirectoryExists(string directoryName)
        {
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
        }

        /// <summary>
        /// 检查目录是否存在和是否存在文件
        /// </summary>
        public static bool CheckFileExist(string indexPath)
        {
            if (!System.IO.Directory.Exists(indexPath))
            {
                return false;
            }
            else
            {
                DirectoryInfo indexPathDirectoryInfo = new DirectoryInfo(indexPath);
                return (indexPathDirectoryInfo.GetFiles().Length > 0);
            }
        }

        #endregion

        #region -Web Path-
        /// <summary>
        /// 虚拟路径转换为服务器路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MapPath(string path)
        {
            HttpContext context = HttpContext.Current;

            if (context != null)
            {
                return context.Server.MapPath(path);
            }
            else
            {
                return PhysicalPath(path.Replace("/", Path.DirectorySeparatorChar.ToString()).Replace("~", ""));
            }
        }

        /// <summary>
        /// 虚拟路径转换为服务器路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string PhysicalPath(string path)
        {
            string rootPath = AppDomain.CurrentDomain.BaseDirectory;
            char dirSep = Path.DirectorySeparatorChar;
            rootPath = rootPath.Replace("/", dirSep.ToString());
            return string.Concat(rootPath.TrimEnd(dirSep), dirSep, path.TrimStart(dirSep));
        }

        private static string _RelativeWebRoot;
        /// <summary>
        /// 根目录的相对URL
        /// </summary>
        public static string RelativeWebRoot
        {
            get
            {
                if (string.IsNullOrEmpty(_RelativeWebRoot))
                    _RelativeWebRoot = VirtualPathUtility.ToAbsolute(HHConfiguration.GetConfig()["virtualPath"].ToString());
                return _RelativeWebRoot;
            }
        }
        /// <summary>
        /// 根目录的绝对URI
        /// </summary>
        public static Uri AbsoluteWebRoot
        {
            get
            {
                HttpContext context = HttpContext.Current;
                if (context == null)
                    throw new System.Net.WebException("The current HttpContext is null");

                if (context.Items["absoluteurl"] == null)
                    context.Items["absoluteurl"] = new Uri(context.Request.Url.GetLeftPart(UriPartial.Authority) + RelativeWebRoot);

                return context.Items["absoluteurl"] as Uri;
            }
        }
        #endregion

        #region -Compress-
        /// <summary>
        /// 是否压缩javascript
        /// </summary>
        public static bool CompressJs
        {
            get
            {
                return bool.Parse(HHConfiguration.GetConfig()["compressJs"].ToString());
            }
        }
        /// <summary>
        /// 是否压缩Css
        /// </summary>
        public static bool CompressCss
        {
            get
            {
                return bool.Parse(HHConfiguration.GetConfig()["compressCss"].ToString());
            }
        }
        #endregion

        #region -Password-
        /// <summary>
        /// 生成密码
        /// </summary>
        /// <returns></returns>
        public static string GeneratePassword()
        {
            //10位的密码
            int max = 10;
            Random rnd = new Random(DateTime.Now.Millisecond);
            string pwd = string.Empty;
            for (int i = 0; i < max; i++)
            {
                pwd += characters[rnd.Next(characters.Length - 1)];
            }
            return pwd;
        }
        /// <summary>
        /// 生成盐值
        /// </summary>
        /// <returns></returns>
        private static string GenerateSalt()
        {
            byte[] buffer = Encoding.Unicode.GetBytes(HHConfiguration.GetConfig()["encryptSalt"].ToString());
            return Convert.ToBase64String(buffer);
        }

        public static string EncodePassword(string pass)
        {
            string hashAlgorithmType = HHConfiguration.GetConfig()["encryptType"] != null ?
                HHConfiguration.GetConfig()["encryptType"].ToString() : "SHA1";
            return EncodePassword(pass, PasswordFormat.Hashed, hashAlgorithmType);
        }

        /// <summary>
        /// 密码加密
        /// </summary>
        /// <param name="pass"></param>
        /// <param name="passwordFormat">0:clear,1:hashed</param>
        /// <param name="hashAlgorithmType">md5/sha1/clear</param>
        /// <returns></returns>
        public static string EncodePassword(string pass, PasswordFormat passwordFormat, string hashAlgorithmType)
        {
            if (passwordFormat == PasswordFormat.Clear)
                return pass;
            if (TypeHelper.IsNullOrEmpty(pass))
                return string.Empty;
            string salt = GenerateSalt();
            byte[] bIn = Encoding.Unicode.GetBytes(pass);
            byte[] bSalt = Convert.FromBase64String(salt);
            byte[] bAll = new byte[bSalt.Length + bIn.Length];
            byte[] bRet = null;

            Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
            Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);
            if (passwordFormat == PasswordFormat.Hashed)
            {
                HashAlgorithm s = HashAlgorithm.Create(hashAlgorithmType);

                if (s == null)
                    throw new ProviderException("Could not create a hash algorithm");

                bRet = s.ComputeHash(bAll);
            }
            return Convert.ToBase64String(bRet);
        }
        #endregion

        #region -Get User-
        public static User GetCurrentUser()
        {
            HttpContext current = HttpContext.Current;
            User user = null;
            if ((current != null) && current.User.Identity.IsAuthenticated)
            {
                user = Users.GetUser(current.User.Identity.Name);
                if (user != null)
                {
                    return user;
                }
                FormsAuthentication.SignOut();
            }
            user = new User();
            user.UserID = 0;
            user.UserName = "anonymity";
            return user;
        }
        #endregion

        #region -DateTime-
        /// <summary>
        /// 系统处理最小时间
        /// </summary>
        public static DateTime MinValue
        {
            get
            {
                return new DateTime(2000, 1, 1);
            }
        }

        /// <summary>
        /// 系统处理最大时间
        /// </summary>
        public static DateTime MaxValue
        {
            get
            {
                return new DateTime(2999, 12, 31, 23, 59, 59);
            }
        }
        #endregion

        #region -Encrypt-
        static readonly string plusReplacer = "______hhonline_________________";
        /// <summary>
        /// 加密字符串 MD5+TRIPLE DES
        /// </summary>
        /// <param name="planText"></param>
        /// <returns></returns>
        public static string Encrypt(string planText)
        {
            return Encrypt(planText, true);
        }
        /// <summary>
        /// 加密字符串 MD5+TRIPLE DES
        /// </summary>
        /// <param name="planText"></param>
        /// <param name="filterPlus">是否替换特殊字符，如'+'，以免在Url传递中发生错误</param>
        /// <returns></returns>
        public static string Encrypt(string planText, bool filterPlus)
        {
            try
            {
                byte[] inputs = ASCIIEncoding.ASCII.GetBytes(planText);
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] buffer = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(encryptKey));
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                des.Key = buffer;
                des.Mode = CipherMode.ECB;

                string result = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(inputs, 0, inputs.Length));
                if (filterPlus)
                {
                    result = result.Replace("+", plusReplacer);
                }
                return HttpUtility.UrlEncode(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 解密字符串 MD5+TRIPLE DES
        /// </summary>
        /// <param name="planText"></param>
        /// <returns></returns>
        public static string Decrypt(string planText)
        {
            try
            {
                planText = HttpUtility.UrlDecode(planText);
                planText = planText.Replace(plusReplacer, "+");
                byte[] inputs = Convert.FromBase64String(planText);
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] buffer = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(encryptKey));
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                des.Key = buffer;
                des.Mode = CipherMode.ECB;
                return ASCIIEncoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(inputs, 0, inputs.Length));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region FormatTags
        public static string FormatTags(List<Tag> tagList)
        {
            StringBuilder tagString = new StringBuilder();

            if (tagList != null && tagList.Count > 0)
            {
                tagString.Append("标签：");

                for (int i = 0; i < tagList.Count; i++)
                {
                    if (i > 0)
                        tagString.Append(";");

                    tagString.Append("<a href=\"");
                    tagString.Append("#");
                    tagString.Append("\" rel=\"tag\">");
                    tagString.Append(tagList[i].Name);
                    tagString.Append("</a>");
                }
            }

            return tagString.ToString();
        }
        #endregion

        #region -MakePath
        public static string MakePath(int id)
        {
            string idString = id.ToString();
            idString = new String('0', 10 - idString.Length) + idString;

            return FileStorageProvider.MakePath(
                idString.Substring(0, 5),
                idString.Substring(5)
                );
        }
        #endregion

        #region Image Resizing
        /// <summary>
        /// 改变图片大小
        /// </summary>
        /// <param name="image"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public static void ResizeImageHtml(System.Web.UI.WebControls.Image image, int height, int width)
        {
            HttpContext context = HttpContext.Current;
            if (context == null || (context.Request.Browser.IsBrowser("IE") && context.Request.Browser.MajorVersion < 7))
            {
                if (height > 0)
                    image.Height = new System.Web.UI.WebControls.Unit(height);

                if (width > 0)
                    image.Width = new System.Web.UI.WebControls.Unit(width);
            }
            else
            {
                if (height > 0)
                    image.Style["max-height"] = height.ToString() + "px";

                if (width > 0)
                    image.Style["max-width"] = width.ToString() + "px";
            }
        }

        /// <summary>
        /// 改变图片大小
        /// </summary>
        /// <param name="url">图片路径</param>
        /// <param name="height">高度</param>
        /// <param name="width">宽度</param>
        /// <param name="useHtmlResizing">是否使用HtmlResizing</param>
        /// <returns>img Html</returns>
        public static string ResizeImageHtml(string url, int height, int width, bool useHtmlResizing)
        {
            StringBuilder img = new StringBuilder();
            img.Append("<img src=\"");
            img.Append(WebHelper.HtmlEncode(url));
            img.Append("\" border=0 alt=\"\"");

            if (useHtmlResizing && (width > 0 || height > 0))
            {
                HttpContext context = HttpContext.Current;

                if (context == null || (context.Request.Browser.IsBrowser("IE") && context.Request.Browser.MajorVersion < 7))
                {
                    if (height > 0)
                    {
                        img.Append(" height=\"");
                        img.Append(height.ToString());
                        img.Append("\"");
                    }

                    if (width > 0)
                    {
                        img.Append(" width=\"");
                        img.Append(width.ToString());
                        img.Append("\"");
                    }
                }
                else
                {
                    img.Append(" style=\"");

                    if (height > 0)
                    {
                        img.Append("max-height: ");
                        img.Append(height.ToString());
                        img.Append("px;");
                    }

                    if (width > 0)
                    {
                        img.Append("max-width: ");
                        img.Append(width.ToString());
                        img.Append("px;");
                    }

                    img.Append("\"");
                }
            }

            img.Append(" />");

            return img.ToString();
        }

        /// <summary>
        /// 改变图片大小
        /// </summary>
        /// <param name="stream">原始图片流</param>
        /// <param name="height">高度</param>
        /// <param name="width">宽度</param>
        /// <param name="isKeepScale">是否保持高宽比</param>
        /// <param name="format">图片格式</param>
        /// <param name="fileExtension">图片文件后缀</param>
        /// <returns>改变大小后的图片流</returns>
        private static MemoryStream ResizeImage(Stream stream, int height, int width, bool isKeepScale, ImageFormat format, out string fileExtension)
        {
            Bitmap bitmapOriginal = new Bitmap(stream);
            if (format == null)
            {
                if (bitmapOriginal.RawFormat.Guid == ImageFormat.Png.Guid || bitmapOriginal.RawFormat.Guid == ImageFormat.Gif.Guid)
                    format = ImageFormat.Png;
                else
                    format = ImageFormat.Jpeg;
            }

            fileExtension = format.ToString().ToLower();
            if (fileExtension == "jpeg")
                fileExtension = "jpg";

            Bitmap bitmapResize = bitmapOriginal;
            MemoryStream returnStream = new MemoryStream();

            float scale;
            if (width == 0)
                width = bitmapOriginal.Width;
            if (height == 0)
                height = bitmapOriginal.Height;
            if (bitmapOriginal.Height > height || bitmapOriginal.Width > width)
            {
                // Get the scale
                //
                int newWidth = 0;
                int newHeight = 0;
                if (isKeepScale)
                {
                    if (height == 0)
                        scale = (float)width / (float)bitmapOriginal.Width;
                    else if (width == 0)
                        scale = (float)height / (float)bitmapOriginal.Height;
                    else
                        scale = Math.Min((float)height / (float)bitmapOriginal.Height, (float)width / (float)bitmapOriginal.Width);

                    newWidth = (int)(scale * bitmapOriginal.Width);
                    newHeight = (int)(scale * bitmapOriginal.Height);
                }
                else
                {
                    newWidth = width;
                    newHeight = height;
                }
                bitmapResize = new Bitmap(newWidth, newHeight);

                using (Graphics g = Graphics.FromImage(bitmapResize))
                {
                    g.Clear(Color.Transparent);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(bitmapOriginal, 0, 0, newWidth, newHeight);
                }
            }

            ImageCodecInfo codecInfo = null;
            foreach (ImageCodecInfo ci in ImageCodecInfo.GetImageEncoders())
            {
                if (ci.FormatID == format.Guid)
                {
                    codecInfo = ci;
                    break;
                }
            }

            if (format.Guid == ImageFormat.Gif.Guid)
            {
                foreach (Color c in bitmapResize.Palette.Entries)
                {
                    if (c.A == 0)
                        bitmapResize.MakeTransparent(c);
                }
            }

            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 85L);

            bitmapResize.Save(returnStream, codecInfo, encoderParameters);

            if (bitmapResize != bitmapOriginal)
                bitmapResize.Dispose();

            bitmapOriginal.Dispose();

            return returnStream;
        }

        public static MemoryStream ResizeImage(Stream stream, int height, int width, bool isKeepScale, out string fileExtension)
        {
            return ResizeImage(stream, height, width, isKeepScale, null, out fileExtension);
        }

        public static MemoryStream ResizeImage(Stream stream, int height, int width, bool isKeepScale, ImageFormat format)
        {
            string fileExtesion;
            return ResizeImage(stream, height, width, isKeepScale, format, out fileExtesion);
        }

        public static MemoryStream ResizeImage(Stream stream, int height, int width, ImageFormat format)
        {
            return ResizeImage(stream, height, width, true, format);
        }

        #endregion

        #region Extension
        public static string GetExtension(string fileName)
        {
            Regex regex = new Regex(@"^.+\.([^\.]*)$", RegexOptions.None);

            Match m = regex.Match(fileName);
            if (m != null && m.Success)
                return m.Groups[1].Value;
            else
                return "";
        }
        #endregion

        #region Resolve
        /// <summary>
        /// ResolveUrl
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        public static string ResolveUrl(string relativeUrl)
        {
            return WebHelper.ResolveUrl(relativeUrl);
        }
        #endregion

        #region IsImage
        /// <summary>
        /// 判断是否图像文件
        /// </summary>
        /// <param name="fileName"></param>
        public static bool IsImage(string fileName)
        {
            return MimeTypeManager.GetMimeType(fileName).IndexOf("image") > -1;
        }
        #endregion

        #region SubString
        public static string SubString(string RawString, Int32 Length)
        {
            if (RawString.Length <= Length)
            {
                return RawString;
            }
            else
            {
                for (Int32 i = RawString.Length - 1; i >= 0; i--)
                {
                    if (System.Text.Encoding.GetEncoding("GB2312").GetByteCount(RawString.Substring(0, i)) < Length)
                    {
                        return RawString.Substring(0, i) + "...";
                    }
                }
                return "...";
            }
        }
        #endregion

        #region MaxMember
        private static int _MaxMember = 0;
        /// <summary>
        /// 企业客户最大自增会员数
        /// </summary>
        public static int MaxMember
        {
            get
            {
                if (_MaxMember == 0)
                {
                    _MaxMember = int.Parse(HHConfiguration.GetConfig()["maxMember"].ToString());
                }
                return _MaxMember;
            }
        }
        #endregion

        #region IndexFile
        /// <summary>
        /// 索引文件目录
        /// </summary>
        public static string IndexDirectory
        {
            get
            {
                return MapPath(HHConfiguration.GetConfig()["indexDirectory"].ToString());
            }
        }
        #endregion

        #region FileSize
        public static string FormatFriendlyFileSize(double fileSize)
        {
            if (fileSize > 0.0)
            {
                if (fileSize > 1048576.0)
                {
                    return string.Format("{0:F2}M", fileSize / 1048576.0);
                }
                return string.Format("{0:F2}K", fileSize / 1024.0);
            }
            return string.Empty;
        }
        #endregion

        #region -Prices-
        public static string GetPrice(params decimal?[] price)
        {
            return GetPrice(false, string.Empty, price);
        }
        public static string GetPrice(bool breakWord, params decimal?[] price)
        {
            return GetPrice(breakWord, string.Empty, price);
        }
        public static string GetPrice(string stringFormat, params decimal?[] price)
        {
            return GetPrice(false, stringFormat, price);
        }
        public static string GetPrice(bool breakWord, string stringFormat, params decimal?[] price)
        {
            if (string.IsNullOrEmpty(stringFormat))
                stringFormat = "<s>{0}</s>" + (breakWord ? "<br />" : "") + "{1}";
            if (price.Length == 1)
            {
                return (price[0] == null ? "需询价" : price[0].Value.ToString("c"));
            }
            else
            {
                decimal? p1 = price[0];
                decimal? p2 = price[1];
                if (p1 == null && p2 == null)
                    return "需询价";
                else
                {
                    if (p1 == null) return p2.Value.ToString("c");
                    else if (p2 == null) return p1.Value.ToString("c");
                    else
                    {
                        if (p1 < p2)
                            return string.Format(stringFormat, p2.Value.ToString("c"), p1.Value.ToString("c"));
                        else if (p1 > p2)
                            return string.Format(stringFormat, p1.Value.ToString("c"), p2.Value.ToString("c"));
                        else
                            return p1.Value.ToString("c");
                    }
                }
            }
        }
        public static decimal? GetMinPrice(decimal? p1,decimal? p2)
        {
            if (p1 == null && p2 == null)
                return null;
            else
            {
                if (p1 == null) return p2;
                else if (p2 == null) return p1;
                else
                {
                    if (p1 < p2)
                        return p1;
                    else if (p1 > p2)
                        return p2;
                    else
                        return p1;
                }
            }
        }
        #endregion
    }
}
