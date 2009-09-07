<%@ Application Language="C#" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="HHOnline.Framework" %>
<%@ Import Namespace="HHOnline.Task" %>
<%@ Import Namespace="System.Reflection" %>
<%@ Import Namespace="HHOnline.Framework.Web" %>
<%@ Import Namespace="HHOnline.Framework.Web.Pages" %>
<%@ Import Namespace="System.Collections.Generic" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        //在应用程序启动时运行的代码

        //配置log4net
        log4net.Config.XmlConfigurator.Configure(new FileInfo(GlobalSettings.MapPath("~/log4net.config")));

        //配置Lucene分词
        PanGu.Segment.Init(GlobalSettings.MapPath("~/Utility/PanGu.xml"));

        //启动后台任务
        XmlNode node = HHConfiguration.GetConfig().GetConfigSection("HHOnline/Tasks");
        TaskManager.Initialize(node);
        TaskManager.Instance().Start();

        //记录开始运行
        new HHException(ExceptionType.ApplicationStart, "网站开始运行").Log();
    }
    void Application_OnPostAuthenticateRequest(object sender, EventArgs e)
    {
        System.Security.Principal.IPrincipal user = HttpContext.Current.User;
        if (user.Identity.IsAuthenticated && user.Identity.AuthenticationType == "Forms")
        {
            FormsIdentity formIdentity = user.Identity as FormsIdentity;
            HHIdentity identity = new HHIdentity(formIdentity.Ticket);
            List<HHOnline.Permission.Components.ModuleActionKeyValue> moduleActions = HHOnline.Permission.Services.PermissionManager.ModuleActionKeyValues(user.Identity.Name);

            HHPrincipal principal = new HHPrincipal(identity, moduleActions);
            HttpContext.Current.User = principal;
            System.Threading.Thread.CurrentPrincipal = principal;
        }
    }
    void Application_End(object sender, EventArgs e)
    {
        //在应用程序关闭时运行的代码
        string errorMessage = "网站停止运行";
        HttpRuntime runtime = (HttpRuntime)typeof(HttpRuntime).InvokeMember("_theRuntime", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetField, null, null, null);
        if (runtime != null)
        {
            string shutDownMessage = (string)runtime.GetType().InvokeMember("_shutDownMessage", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, runtime, null);
            string shutDownStack = (string)runtime.GetType().InvokeMember("_shutDownStack", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, runtime, null);
            errorMessage = string.Format("{0}\r\n\r\n_shutDownMessage={1}\r\n\r\n_shutDownStack={2}", errorMessage, shutDownMessage, shutDownStack);
        }
        new HHException(ExceptionType.ApplicationStart, errorMessage).Log();
    }

    void Application_Error(object sender, EventArgs e)
    {
        //在出现未处理的错误时运行的代码

        HttpApplication application = (HttpApplication)sender;
        HttpContext context = application.Context;

        Exception ex = context.Server.GetLastError();
        HHException hhException = null;
        if (ex is HHException)
        {
            hhException = ex as HHException;
        }
        else if (ex is HttpException)
        {
            HttpException he = ex as HttpException;
            if (he.GetHttpCode() == 404)
                return;
        }

        if (hhException == null)
        {
            if (ex.GetBaseException() != null && ex.GetBaseException() is HHException)
            {
                hhException = ex.GetBaseException() as HHException;
            }
            else if (ex.InnerException != null && ex.InnerException is HHException)
            {
                hhException = ex.GetBaseException() as HHException;
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                    hhException = new HHException(ExceptionType.UnknownError, ex.Message, ex.InnerException);
                else
                    hhException = new HHException(ExceptionType.UserUnAuthenticated, "用户未登录，无法完成所请求的操作！");
            }
        }

        int et = (int)hhException.ExceptionType;
        //messages
        if (et >= 512 && et < 1024)
        {
            Server.Transfer("~/pages/messages/info.aspx");
        }
        else if (et == 1027)
        {
            Server.Transfer("~/pages/messages/normalinfo.aspx");
        }
        else
        {
            hhException.Log();
            Server.Transfer("~/pages/messages/error.aspx");
        }

    }

    void Session_Start(object sender, EventArgs e)
    {
        //在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e)
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    }
       
</script>

