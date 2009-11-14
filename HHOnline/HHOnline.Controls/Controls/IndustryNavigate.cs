using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class IndustryNavigate:Control
    {
        private static Dictionary<int, string> _Cache = new Dictionary<int, string>();
        private int _IndustryID = 0;
        static readonly string _href = "<a href=\"" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-industry{0}\">{1}</a>";
        public int IndustryID
        {
            get { return _IndustryID; }
            set { _IndustryID = value; }
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
            if (_Cache.ContainsKey(_IndustryID))
                return _Cache[_IndustryID];
            List<ProductIndustry> inds = ProductIndustries.GetChildIndustries(0);
            StringBuilder sb = new StringBuilder();
            if (_IndustryID == 0)
            {
                sb.Append("您的位置：<b>所有行业</b>");
            }
            else
            {
                ProductIndustry pi = ProductIndustries.GetProductIndustry(_IndustryID);
                sb.Append("<b>" + pi.IndustryName + "</b>");
                int parId = pi.ParentID;
                while (parId != 0)
                {
                    pi = ProductIndustries.GetProductIndustry(parId);
                    if (pi == null) break;
                    sb.Insert(0, string.Format(_href, "&ID=" + GlobalSettings.Encrypt(pi.IndustryID.ToString()), pi.IndustryName) + ">>");
                    parId = pi.ParentID;
                }
                sb.Insert(0, "您的位置：" + string.Format(_href, "", "所有行业") + ">>");
                if (!_Cache.ContainsKey(_IndustryID))
                    lock (_lock)
                        if (!_Cache.ContainsKey(_IndustryID))
                            _Cache.Add(_IndustryID, sb.ToString());

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
