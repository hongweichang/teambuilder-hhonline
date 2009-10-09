using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    /// <summary>
    /// HomePage VarietyList
    /// </summary>
    public class HPIndustryList:UserControl
    {
        static HPIndustryList()
        {
            if (inds == null)
            {
                inds = ProductIndustries.GetChildIndustries(0);
            }
        }  
        private int _Columns = 2;
        public int Columns
        {
            get { return _Columns; }
            set { _Columns = value; }
        }
        private int _Max = 10;
        public int Max
        {
            get { return _Max; }
            set { _Max = value; }
        }
        private string _CssClass;
        public string CssClass
        {
            get { return _CssClass; }
            set { _CssClass = value; }
        }
        private static  List<ProductIndustry> inds = null;
        string RenderHTML()
        {
            string nav = GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-industry";
            if (inds == null || inds.Count == 0)
            {
                return "<div><span>没有显示的行业信息！</span></div>";
            }
           
            List<ProductIndustry> pis = null;
            ProductIndustry pi = null;
            StringBuilder sb = new StringBuilder();
            string indId = null;
            int curCount = inds.Count;
            inds = inds.GetRange(0, Math.Min(_Max, curCount));
            sb.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" class=\"" + _CssClass + "\">");
            for (int i = 0; i < inds.Count; i++)
            {
                pi = inds[i];
                indId = GlobalSettings.Encrypt(pi.IndustryID.ToString());
                if (i % _Columns == 0)
                    sb.AppendLine("<tr>");

                sb.AppendLine("<td>");
                sb.AppendLine("<div><a href=\"" + nav + "&ID=" + indId + "\" target=\"_blank\">" + pi.IndustryName + "</a></div>");
                pis = ProductIndustries.GetChildIndustries(pi.IndustryID);
                for (int j = 0; j < pis.Count;j++ )
                {
                    pi = pis[j];
                    indId = GlobalSettings.Encrypt(pi.IndustryID.ToString());
                    sb.AppendLine("<a href=\"" + nav + "&ID=" + indId + "\" target=\"_blank\">" + pi.IndustryName + "</a>");
                    if (j != pis.Count - 1)
                    {
                        sb.Append("&nbsp;|&nbsp;");
                    }
                }
                sb.AppendLine("</td>");

                if (i % _Columns == _Columns-1)
                    sb.AppendLine("</tr>");

            }
            sb.AppendLine("</table>");
            if (curCount > _Max)
                sb.Append("<div class=\"list-more\"><a href=\"" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-industry\" title=\"查看全部。。。\"></a></div>");
            return sb.ToString();
        }
        public override void RenderControl(HtmlTextWriter writer)
        {
            if (this.Visible)
            {
                string html = RenderHTML();

                writer.Write(html);
                writer.WriteLine(Environment.NewLine);
            }
        }
    }
}
