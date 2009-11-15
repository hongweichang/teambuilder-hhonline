using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Configuration;

namespace HHOnline.Framework.Web.HttpHandlers
{
    public class UploadHandler:IHttpHandler,IRequiresSessionState
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            int userId = 0;
            try
            {
                HttpFileCollection files = context.Request.Files;
                HttpPostedFile f = null;
                TemporaryAttachment ta = null;

                userId = int.Parse(context.Request["uid"]);
                for (int i = 0; i < files.Count;i++ )
                {
                    f = files[i];
                    ta = new TemporaryAttachment();
                    ta.FriendlyFileName = f.FileName;
                    ta.AttachmentID = Guid.NewGuid();
                    ta.FileName = f.FileName;
                    ta.DisplayOrder = 100;
                    ta.AttachmentType = AttachmentType.ProductPhoto;
                    ta.UserID = userId;
                    TemporaryAttachments.Create(ta, f.InputStream);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
