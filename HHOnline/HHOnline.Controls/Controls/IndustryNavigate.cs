using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class IndustryNavigate:UserControl
    {
        static IndustryNavigate()
        {
            if (inds == null)
            {
                inds = ProductIndustries.GetChildIndustries(0);
            }
        }
        private static List<ProductIndustry> inds = null;
        private int _IndustryID = 0;
        static readonly string _href = "<a href=\"" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-industry{0}\">{1}</a>";
        public int IndustryID
        {
            get { return _IndustryID; }
            set { _IndustryID = value; }
        }

        string RenderHTML()
        {
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
                    sb.Insert(0, string.Format(_href, "&ID=" + GlobalSettings.Encrypt(pi.IndustryID.ToString()), pi.IndustryName) + ">>");
                    parId = pi.ParentID;
                }
                sb.Insert(0, "您的位置：" + string.Format(_href, "", "所有行业") + ">>");
            }
            return sb.ToString();
        }
        public override void RenderControl(HtmlTextWriter writer)
        {
            string html = RenderHTML();

            writer.Write(html);
            writer.WriteLine(Environment.NewLine);
        }
    }
}
