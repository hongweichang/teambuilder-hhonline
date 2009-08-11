using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.SessionState;
using HHOnline.Shops;
using System.Configuration;

namespace HHOnline.Framework.Web.HttpHandlers
{
    public class UpdatePictureHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                HttpFileCollection files = context.Request.Files;
                HttpPostedFile f = null;
                ProductPicture p = null;

                for (int i = 0; i < files.Count; i++)
                {
                    f = files[i];
                    p = new ProductPicture();
                    p.PictureName = f.FileName;
                    p.PictureFile = f.FileName;
                    p.DisplayOrder = 100;
                    p.ProductID = int.Parse(context.Request["pictureid"]);
                    ProductPictures.Create(p, f.InputStream);
                }
            }
            catch (Exception ex)
            {
                throw new HHException(ExceptionType.UnknownError, ex.Message);
            }
        }
    }
}
