using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using HHOnline.Framework;

public partial class Utility_InsertMedia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fuFile.PostedFile.ContentLength > 0)
        {
            string path = "Uploads";
            string filename = Path.GetFileName(fuFile.PostedFile.FileName);
            SiteFiles.AddFile(fuFile.PostedFile.InputStream, path, filename);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "InsertMedia",
                string.Format("<script type=\"text/javascript\">\n// <![CDATA[\nProcessContent('{0}');\n// ]]>\n</script>", GetEmbedCode(path, filename)));
        }
    }

    private string GetEmbedCode(string path, string filename)
    {


        SiteFile file = SiteFiles.GetFile(path, filename);
        if (file != null)
        {
            if (!GlobalSettings.IsImage(filename))
                return string.Format("<a href=\"{0}\">{1}</a>", FileStorageProvider.GetGenericDownloadUrl(file.File), filename);
            else
            {
                int width = 200;
                int height = 200;
                try
                {
                    width = int.Parse(this.txtWidth.Text);
                }
                catch { }
                try
                {
                    height = int.Parse(this.txtHeight.Text);
                }
                catch { }

                return GlobalSettings.ResizeImageHtml(SiteUrlManager.GetResizedImageUrl(file.File, width, height), width, height, false);
            }

        }
        else
        {
            return string.Empty;
        }
    }
}
