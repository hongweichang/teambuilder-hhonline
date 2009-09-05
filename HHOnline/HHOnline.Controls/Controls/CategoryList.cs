using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class CategoryList:UserControl
    {
        static CategoryList()
        {
            if (pcs == null)
            {
                pcs = ProductCategories.GetCategories();
            }
        }
        private static List<ProductCategory> pcs = null;

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
        public string RenderHTML()
        {
            string nav = GlobalSettings.RelativeWebRoot + "pages/product-category";
            if (pcs == null || pcs.Count == 0)
            {
                return "<div>没有可用的分类信息！</div>";
            }
            StringBuilder sb = new StringBuilder();
            List<ProductCategory> pcList = new List<ProductCategory>();
            ProductCategory pc = null;
            List<ProductCategory> pcSubList = null;
            for (int i = 0; i < pcs.Count; i++)
            {
                pc = pcs[i];
                if (pc.ParentID == 0)
                {
                    pcList.Add(pc);
                }
            }
            sb.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" class=\""+_CssClass+"\">");
            string catId = string.Empty;
            for (int i = 0; i < pcList.Count; i++)
            {
                pc = pcList[i];
                if (i % _Columns == 0)
                    sb.AppendLine("<tr>");
                catId =  GlobalSettings.Encrypt(pc.CategoryID.ToString());
                sb.AppendLine("<td>");
                sb.AppendLine("<div><a href=\""+nav+"&ID=" + catId + "\" target=\"_blank\">" + pc.CategoryName + "</a></div>");
                pcSubList = GetSubCategories(pc.CategoryID);
                for (int j = 0; j < pcSubList.Count; j++)
                {
                    pc = pcSubList[j];
                    catId = GlobalSettings.Encrypt(pc.CategoryID.ToString());
                    sb.AppendLine("<a href=\"" + nav + "&ID=" + catId + "\" target=\"_blank\">" + pc.CategoryName + "</a>");
                    if (j != pcSubList.Count-1)
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

        List<ProductCategory> GetSubCategories(int catId)
        {
            List<ProductCategory> pcList = new List<ProductCategory>();
            ProductCategory pc = null;

            for (int i = 0; i < pcs.Count; i++)
            {
                pc = pcs[i];
                if (pc.ParentID == catId)
                {
                    pcList.Add(pc);
                }
            }
            return pcList;
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
