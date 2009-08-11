using System;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using System.Web;
using System.Collections;
using System.Text;
using System.Collections.Generic;

namespace HHOnline.Framework
{
    /// <summary>
    /// 对请求公用属性和设置进行封装
    /// 对非Web Request请求也可以使用(HttpContext为Null)
    /// </summary>
    public class HHContext
    {
        #region Private Properties
        private HybridDictionary _items = new HybridDictionary();
        private NameValueCollection _queryString = null;
        private HttpContext _httpContext = null;
        private Uri _currentUri;
        private string _rawUrl;
        private SiteSettings _siteSettings;
        #endregion

        #region Core Properties
        /// <summary>
        /// QueryString
        /// </summary>
        public NameValueCollection QueryString
        {
            get { return _queryString; }
            set { _queryString = value; }
        }

        /// <summary>
        /// 模拟Context.Items
        /// </summary>
        public IDictionary Items
        {
            get { return _items; }
        }

        /// <summary>
        /// 提供直接访问.Items属性
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                return this.Items[key];
            }
            set
            {
                this.Items[key] = value;
            }
        }

        /// <summary>
        /// HttpContext
        /// </summary>
        public HttpContext Context
        {
            get
            {
                return _httpContext;
            }
        }

        /// <summary>
        /// 原始Url
        /// </summary>
        public string RawUrl
        {
            get
            {
                return _rawUrl;
            }
            set
            {
                _rawUrl = value;
            }
        }

        /// <summary>
        /// 当前Url
        /// </summary>
        public Uri CurrentUri
        {
            get
            {
                if (_currentUri == null)
                    _currentUri = new Uri("http://localhost/hhonline");

                return _currentUri;

            }
            set
            {
                _currentUri = value;
            }
        }

        /// <summary>
        /// 是否WebRequest
        /// </summary>
        public bool IsWebRequest
        {
            get { return this.Context != null; }
        }

        /// <summary>
        /// 站点配置
        /// </summary>
        public SiteSettings SiteSettings
        {
            get
            {
                if (_siteSettings == null)
                    _siteSettings = SiteSettingsManager.GetSiteSettings();
                return _siteSettings;
            }
            set
            {
                _siteSettings = value;
            }
        }
        #endregion

        #region .cntor'S
        private void Initialize(NameValueCollection qs, Uri uri, string rawUrl)
        {
            _queryString = qs;
            _currentUri = uri;
            _rawUrl = rawUrl;
        }

        /// <summary>
        /// HttpContext可用时调用
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="includeQS">是否包含查询字符串</param>
        private HHContext(HttpContext context, bool includeQS)
        {
            this._httpContext = context;
            if (includeQS)
            {
                Initialize(new NameValueCollection(context.Request.QueryString), context.Request.Url, context.Request.RawUrl);
            }
            else
            {
                Initialize(null, context.Request.Url, context.Request.RawUrl);
            }
        }

        private HHContext()
        {
            Initialize(new NameValueCollection(), null, string.Empty);
        }

        private static readonly string dataKey = "HHContextStore";

        [ThreadStatic]
        private static HHContext currentContext = null;

        public static HHContext Current
        {
            get
            {
                HttpContext ctx = HttpContext.Current;

                if (ctx != null)
                {
                    if (ctx.Items.Contains(dataKey))
                        return ctx.Items[dataKey] as HHContext;
                    else
                    {
                        HHContext context = new HHContext(ctx, true);
                        SaveContextToStore(context);
                        return context;
                    }
                }
                else if (currentContext == null)
                {
                    HHContext context = new HHContext();
                    SaveContextToStore(context);
                }
                if (currentContext == null)
                    throw new Exception("HttpContext.Current不可访问时,自动创建HHContext失败.");
                return currentContext;
            }
        }

        private static void SaveContextToStore(HHContext context)
        {
            if (context.IsWebRequest)
            {
                context.Context.Items[dataKey] = context;
            }
            else
            {
                currentContext = context;
            }
        }

        public static void Unload()
        {
            currentContext = null;
        }
        #endregion
    }
}
