using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;
using System.Drawing;

public partial class Pages_Messages_normalinfo : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
            {
                HHException hhEx = FigureoutHHException(ex);

                mbMsg.ShowMsg(hhEx.Message, Color.Red);

                Server.ClearError();
                Context.ClearError();
            }
        }
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "错误提示";
      
        base.OnPageLoaded();
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
