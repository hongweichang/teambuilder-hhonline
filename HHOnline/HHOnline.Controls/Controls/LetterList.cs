using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class LetterList:UserControl
    {
        static LetterList()
        {
            ProductCategories.Updated += delegate { _Html = null; };
            ProductBrands.Updated += delegate { _Html = null; };
            ProductIndustries.Updated += delegate { _Html = null; };
        }
        #region -Properties-
        private string _FirstLetter = "a";
        public string FirstLetter
        {
            get { return _FirstLetter; }
            set {
                if (_FirstLetter != value)
                {
                    _Html = null;
                }
                _FirstLetter = value; }
        }
        private LettersType _letterType = LettersType.Category;
        public LettersType LetterType
        {
            get { return _letterType; }
            set
            {
                if (_letterType != value)
                {
                    _Html = null;
                }
                _letterType = value;
            }
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
        private string _CssClass;
        public string CssClass
        {
            get { return _CssClass; }
            set { _CssClass = value; }
        }
        #endregion

        public string RenderHTML()
        {
            string html = string.Empty;
            switch (_letterType)
            {
                case LettersType.Category:
                    html= RenderCategoryHTML();
                    break;
                case LettersType.Brand:
                    html = RenderBrandHTML();
                    break;
                case LettersType.Industry:
                    html = RenderIndustryHTML();
                    break;
            }
            return html;
        }

        #region -RenderIndustryHTML-
        string RenderIndustryHTML()
        {
            List<ProductIndustry> inds = ProductIndustries.GetIndustriesByPY(_FirstLetter);
            string nav = GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-industry";
            if (inds == null || inds.Count == 0)
            {
                return "<div><span>没有可显示的行业信息！</span></div>";
            }
            StringBuilder sb = new StringBuilder();
            ProductIndustry pb = null;

            int curCount = inds.Count;
            string catId = string.Empty;
            sb.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" class=\"" + _CssClass + "\">");
            for (int i = 0; i < curCount; i++)
            {
                pb = inds[i];
                sb.Append("<tr><td>");
                catId = GlobalSettings.Encrypt(pb.IndustryID.ToString());
                sb.AppendLine("<div><a href=\"" + nav + "&ID=" + catId + "\" target=\"_blank\">" + pb.IndustryName + "</a></div>");
                sb.AppendLine("</td></tr>");
            }
            sb.AppendLine("</table>");

            return sb.ToString();
        }
        #endregion

        #region -RenderBrandHTML-
        string RenderBrandHTML()
        {
            List<ProductBrand> brands = ProductBrands.GetBrandsByPY(_FirstLetter);
            string nav = GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-brand";
            if (brands == null || brands.Count == 0)
            {
                return "<div><span>没有可显示的品牌信息！</span></div>";
            }
            StringBuilder sb = new StringBuilder();
            ProductBrand pb = null;

            int curCount = brands.Count;
            string catId = string.Empty;
            sb.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" class=\"" + _CssClass + "\">");
            for (int i = 0; i < curCount; i++)
            {
                pb = brands[i];
                sb.Append("<tr><td>");
                catId = GlobalSettings.Encrypt(pb.BrandID.ToString());
                sb.AppendLine("<div><a href=\"" + nav + "&ID=" + catId + "\" target=\"_blank\">" + pb.BrandName + "</a></div>");
                sb.AppendLine("</td></tr>");
            }
            sb.AppendLine("</table>");

            return sb.ToString();
        }
        #endregion

        #region -RenderCategoryHTML-
        string RenderCategoryHTML()
        {
            List<ProductCategory> pcs = ProductCategories.GetCategoreisByPY(_FirstLetter);
            string nav = GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-category";
            if (pcs == null || pcs.Count == 0)
            {
                return "<div><span>没有可显示的分类信息！</span></div>";
            }
            StringBuilder sb = new StringBuilder();
            ProductCategory pc = null;

            int curCount = pcs.Count;
            string catId = string.Empty;
            sb.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" class=\""+_CssClass+"\">");
            for (int i = 0; i < curCount; i++)
            {
                pc = pcs[i];
                sb.Append("<tr><td>");
                catId = GlobalSettings.Encrypt(pc.CategoryID.ToString());
                sb.AppendLine("<div><a href=\"" + nav + "&ID=" + catId + "\" target=\"_blank\">" + pc.CategoryName + "</a></div>");
                sb.AppendLine("</td></tr>");
            }
            sb.AppendLine("</table>");

            return sb.ToString();
        }
        #endregion

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
