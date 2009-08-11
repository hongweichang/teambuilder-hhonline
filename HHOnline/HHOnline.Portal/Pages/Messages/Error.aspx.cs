using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class Pages_Messages_Error : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
            {
                HHException hhEx = FigureoutHHException(ex);
                Message msg = ResourceManager.GetMessage((int)hhEx.ExceptionType);

                ltTitle.Text = msg.Title;
                ltBody.Text = msg.Body;
                ltDetails.Text = hhEx.Message;
                Server.ClearError();
                Context.ClearError();
            }
            else
            {
                ltTitle.Text = "无错误";
                ltBody.Text = "未发现提示错误！";
                ltDetails.Text = "未发现提示错误！";
            }
            btnBack.PostBackUrl = GlobalSettings.RelativeWebRoot + "main.aspx";
            btnPostBack.PostBackUrl = Request.RawUrl;
        }
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "错误信息";
        this.SetTitle();
    }

    #region -PrivateMethod-
    private HHException FigureoutHHException(Exception ex)
    {
        HHException hhEx = null;
        if (ex is HHException)
        {
            hhEx = ex as HHException;
        }
        else
        {
            if (ex.InnerException is HHException)
            {
                hhEx = ex.InnerException as HHException;
            }
            else
            {
                if (ex.GetBaseException() is HHException)
                {
                    hhEx = ex.GetBaseException() as HHException;
                }
                else
                {
                    hhEx = new HHException(ExceptionType.UnknownError, "未知的错误！");
                }
            }
        }
        return hhEx;
    }
    #endregion
}
