using System;
using System.Web;
using log4net;

namespace HHOnline.Framework
{
    public class HHException : Exception
    {
        private ExceptionType exceptionType;
        private string message;
        private string httpReferrer = string.Empty;
        private string httpVerb = string.Empty;
        private string ipAddress = string.Empty;
        private string httpPathAndQuery = string.Empty;
        private string stackTrace = string.Empty;
        private string userAgent = string.Empty;
        private DateTime dateCreated;
        private DateTime dateLastOccurred;
        private int frequency = 0;
        private int exceptionID = 0;

        private static readonly ILog log = LogManager.GetLogger("ExceptionLogger");

        public HHException(ExceptionType t)
            : base()
        {
            Init();
            this.exceptionType = t;
        }

        public HHException(ExceptionType t, string message)
            : base(message)
        {
            Init();
            this.exceptionType = t;
            this.message = message;
        }

        public HHException(ExceptionType t, string message, Exception inner)
            : base(message, inner)
        {
            Init();
            this.exceptionType = t;
            this.message = message;
        }

        public ExceptionType ExceptionType
        {
            get
            {
                return this.exceptionType;
            }
        }

        public string UserAgent
        {
            get { return userAgent; }
            set { userAgent = value; }
        }

        public int Category
        {
            get { return (int)exceptionType; }
            set { exceptionType = (ExceptionType)value; }
        }

        public string IPAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        public string HttpReferrer
        {
            get { return httpReferrer; }
            set { httpReferrer = value; }
        }

        public string HttpVerb
        {
            get { return httpVerb; }
            set { httpVerb = value; }
        }

        public string HttpPathAndQuery
        {
            get { return httpPathAndQuery; }
            set { httpPathAndQuery = value; }
        }

        public DateTime DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }

        public DateTime DateLastOccurred
        {
            get { return dateLastOccurred; }
            set { dateLastOccurred = value; }
        }

        public int Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }

        public string LoggedStackTrace
        {
            get
            {
                return stackTrace;
            }
            set
            {
                stackTrace = value;
            }
        }

        public int ExceptionID
        {
            get
            {
                return exceptionID;
            }
            set
            {
                exceptionID = value;
            }
        }

        public void Log()
        {
            log.Error(this);
        }

        public override string ToString()
        {
            switch (this.ExceptionType)
            {
                case ExceptionType.AccessDenied:
                    return string.Format("{0}{1}", base.ToString(), new System.Diagnostics.StackTrace());
                case ExceptionType.UnknownError:
                    return string.Format("{0}{1}", base.ToString(), this.StackTrace);
                default:
                    return base.ToString();

            }

        }

        private void Init()
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx != null && ctx.Request != null)
            {
                if (ctx.Request.UrlReferrer != null)
                    httpReferrer = ctx.Request.UrlReferrer.ToString();

                if (ctx.Request.UserAgent != null)
                    userAgent = ctx.Request.UserAgent;

                if (ctx.Request.UserHostAddress != null)
                    ipAddress = ctx.Request.UserHostAddress;

                if (ctx.Request.Url != null &&
                    ctx.Request.Url.PathAndQuery != null)
                    httpPathAndQuery = ctx.Request.Url.PathAndQuery;
            }
        }


    }
}
