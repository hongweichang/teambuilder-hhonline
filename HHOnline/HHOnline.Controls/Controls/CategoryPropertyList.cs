using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class CategoryPropertyList:UserControl
    {
        static CategoryPropertyList()
        {
            ProductCategories.Updated += delegate { _Html = null; };
        }
        private List<ProductCategory> pcs = null;
        private int _CategoryID = 0;
        static readonly string _href = "<a href=\"" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-category&ID={0}&PropID={1}\">{2}</a>";
        public int CategoryID
        {
            get { return _CategoryID; }
            set {
                if (value != _CategoryID)
                {
                    _Html = null;
                }
                _CategoryID = value; }
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
            if (pcs == null)
            {
                pcs = ProductCategories.GetCategories();
            }
            StringBuilder sb = new StringBuilder();
            if (_CategoryID == 0)
            {
                return "无";
            }
            else
            {
                List<ProductProperty> props = ProductProperties.GetAllPropertyByCategoryID(_CategoryID);
                if (props == null || props.Count == 0)
                {
                    return "无";
                }
                sb.Append("<div class=\"" + _CssClass + "\">");
                for (int i = 0; i < props.Count; i++)
                {
                    sb.AppendFormat(_href, GlobalSettings.Encrypt(_CategoryID.ToString()), GlobalSettings.Encrypt(props[i].PropertyID.ToString()), props[i].PropertyName);
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
