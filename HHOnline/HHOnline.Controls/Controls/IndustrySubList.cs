using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class IndustrySubList:UserControl
    {
        static IndustrySubList()
        {
            ProductIndustries.Updated += delegate { _Html = null; };
        }
        private List<ProductIndustry> inds = null;
        private int _IndustryID = 0;
        static readonly string _href = "<a href=\"" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-industry&ID={0}\">{1}</a>";
        public int IndustryID
        {
            get { return _IndustryID; }
            set {
                if (value != _IndustryID)
                {
                    _Html = null;
                }
                _IndustryID = value; }
        }
        private string _CssClass;
        public string CssClass
        {
            get { return _CssClass; }
            set { _CssClass = value; }
        }
        public static object _lock = new object();
        private static string _Html;
        public string HTML
        {
            get
            {
                if (string.IsNullOrEmpty(_Html))
                {
                    lock (_lock)
                    {
                        if (string.IsNullOrEmpty(_Html))
                        {
                            _Html = RenderHTML();
                        }
                    }
                }
                return _Html;
            }
        }
        string RenderHTML()
        {
            if (inds == null)
            {
                inds = ProductIndustries.GetChildIndustries(0);
            }
            StringBuilder sb = new StringBuilder();
            if (_IndustryID == 0)
            {
                return "<div class=\"" + _CssClass + "\"><span>暂无子行业信息！</span></div>";
            }
            else
            {
                string _indId = string.Empty;
                ProductIndustry pi = null;
                List<ProductIndustry> pis = ProductIndustries.GetChildIndustries(_IndustryID);
                if (pis == null || pis.Count == 0)
                {
                    return "<div class=\"" + _CssClass + "\"><span>暂无子行业信息！</span></div>";
                }
                sb.Append("<div class=\"" + _CssClass + "\">");
                ProductQuery query;
                int count = 0;
                for (int i = 0; i < pis.Count; i++)
                {
                    pi = pis[i];
                    count = 0;
                    query = new ProductQuery();
                    query.IndustryID = pi.IndustryID;
                    try
                    {
                        count = Products.GetProducts(query).Records.Count;
                    }
                    catch { count = 0; }
                    sb.AppendFormat(_href, GlobalSettings.Encrypt(pi.IndustryID.ToString()), pi.IndustryName + "(" + count + ")");
                }
                sb.Append("</div>");
            }
            return sb.ToString();
        }
        public override void RenderControl(HtmlTextWriter writer)
        {
            writer.Write(HTML);
            writer.WriteLine(Environment.NewLine);
        }
    }
}
