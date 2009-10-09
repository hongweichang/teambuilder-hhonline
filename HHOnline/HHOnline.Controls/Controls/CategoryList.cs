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
        public string RenderHTML()
        {
            string nav = GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-category";
            if (pcs == null || pcs.Count == 0)
            {
                return "<div><span>没有可用的分类信息！</span></div>";
            }
            StringBuilder sb = new StringBuilder();
            List<ProductCategory> pcList = new List<ProductCategory>();
            ProductCategory pc = null;
            for (int i = 0; i < pcs.Count; i++)
            {
                pc = pcs[i];
                if (pc.ParentID == 0)
                {
                    pcList.Add(pc);
                }
            }
            int curCount = pcList.Count;
            pcList = pcList.GetRange(0, Math.Min(curCount, _Max));
            sb.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" class=\""+_CssClass+"\">");
            string catId = string.Empty;
            int length = pcList.Count;
            int dev = length / _Columns;
            int left = length % _Columns;
            if (dev != length)
            {
                for (int i = 0; i < dev; i++)
                {
                    pc = pcList[i];
                    sb.Append("<tr>");
                    sb.Append(BindCategory(pc, nav));
                    pc = pcList[i + dev + left];
                    sb.Append(BindCategory(pc, nav));
                    sb.Append("</tr>");
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    pc = pcList[i];
                    sb.Append("<tr>");
                    sb.Append(BindCategory(pc, nav));
                    sb.Append("</tr>");
                }
            }

            if (left > 0)
            {
                sb.Append("<tr>");
                for (int j = left; j > 0; j--)
                {
                    pc = pcList[length - j];
                    sb.Append(BindCategory(pc, nav));
                }
                for (int j = 0; j < _Columns - left; j++)
                {
                    sb.Append("<td>&nbsp;</td>");
                }
                sb.Append("</tr>");
            }

            #region -Ignore-
            /*
            for (int i = 0; i < pcList.Count; i++)
            {
                pc = pcList[i];
                if (i % _Columns == 0)
                    sb.AppendLine("<tr>");
                catId = GlobalSettings.Encrypt(pc.CategoryID.ToString());
                sb.AppendLine("<td>");
                sb.AppendLine("<div><a href=\"" + nav + "&ID=" + catId + "\" target=\"_blank\">" + pc.CategoryName + "</a></div>");
                pcSubList = GetSubCategories(pc.CategoryID);
                for (int j = 0; j < pcSubList.Count; j++)
                {
                    pc = pcSubList[j];
                    catId = GlobalSettings.Encrypt(pc.CategoryID.ToString());
                    sb.AppendLine("<a href=\"" + nav + "&ID=" + catId + "\" target=\"_blank\">" + pc.CategoryName + "</a>");
                    if (j != pcSubList.Count - 1)
                    {
                        sb.Append("&nbsp;|&nbsp;");
                    }
                }
                sb.AppendLine("</td>");

                if (i % _Columns == 1)
                    sb.AppendLine("</tr>");

            }
             * */
            #endregion

            sb.AppendLine("</table>");

            if (curCount > _Max)
                sb.Append("<div class=\"list-more\"><a href=\"" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-category\" title=\"查看全部。。。\"></a></div>");
            return sb.ToString();
        }
        string BindCategory(ProductCategory pc,string nav)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<td>");
            string catId = GlobalSettings.Encrypt(pc.CategoryID.ToString());
            sb.AppendLine("<div><a href=\"" + nav + "&ID=" + catId + "\" target=\"_blank\">" + pc.CategoryName + "</a></div>");
            List<ProductCategory> pcSubList = GetSubCategories(pc.CategoryID);
            for (int j = 0; j < pcSubList.Count; j++)
            {
                pc = pcSubList[j];
                catId = GlobalSettings.Encrypt(pc.CategoryID.ToString());
                sb.AppendLine("<a href=\"" + nav + "&ID=" + catId + "\" target=\"_blank\">" + pc.CategoryName + "</a>");
                if (j != pcSubList.Count - 1)
                {
                    sb.Append("&nbsp;|&nbsp;");
                }
            }
            sb.AppendLine("</td>");
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
