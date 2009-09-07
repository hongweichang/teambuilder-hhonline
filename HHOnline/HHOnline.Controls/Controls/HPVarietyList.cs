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
    public class HPVarietyList:UserControl
    {
        static HPVarietyList()
        {
            if (brands == null)
            {
                brands = ProductBrands.GetProductBrands();
            }
        }  
        private int _Columns = 2;
        public int Columns
        {
            get { return _Columns; }
            set { _Columns = value; }
        }
        private string _CssClass;
        public string CssClass
        {
            get { return _CssClass; }
            set { _CssClass = value; }
        }
        private static  List<ProductBrand> brands = null;
        string RenderHTML()
        {
            string nav = GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-brand";
            if (brands == null || brands.Count == 0)
            {
                return "<div><span>没有显示的品牌信息！</span></div>";
            }
            List<string> brandGroup = ProductBrands.GetBrandGroup();
            List<ProductBrand> pb = null;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" class=\"" + _CssClass + "\">");
            string bId = string.Empty;
            for (int i = 0; i < brandGroup.Count; i++)
            {
                if (i % _Columns == 0)
                    sb.AppendLine("<tr>");

                sb.AppendLine("<td>");
                sb.AppendLine("<div><a href=\"javascript:void(0)\">" + brandGroup[i] + "</a></div>");
                pb = GetSubBrand(brandGroup[i]);
                ProductBrand _pb = null;
                for (int j = 0; j < pb.Count; j++)
                {
                    _pb = pb[j];
                    bId = GlobalSettings.Encrypt(_pb.BrandID.ToString());
                    sb.AppendLine("<a href=\"" + nav + "&ID=" + bId + "\" target=\"_blank\">" + _pb.BrandName + "</a>");
                    if (j != pb.Count - 1)
                    {
                        sb.Append("&nbsp;|&nbsp;");
                    }
                }
                sb.AppendLine("</td>");

                if (i % _Columns == 1)
                    sb.AppendLine("</tr>");

            }
            sb.AppendLine("</table>");
            return sb.ToString();
        }
        private List<ProductBrand> GetSubBrand(string groupName)
        {
            List<ProductBrand> bs = new List<ProductBrand>();
            foreach (ProductBrand b in brands)
            {
                if (b.BrandGroup == groupName) { bs.Add(b); }
            }
            return bs;
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
