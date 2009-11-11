using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class LetterList:Control
    {
        static LetterList()
        {
            ProductCategories.Updated += delegate { _Cache.Clear(); };
            ProductBrands.Updated += delegate { _Cache.Clear(); };
            ProductIndustries.Updated += delegate { _Cache.Clear(); };
        }
        #region -Properties-
        private static readonly string prefixCat = "Category";
        private static readonly string prefixBra = "Brand";
        private static readonly string prefixInd = "Industry";
        private static Dictionary<string, string> _Cache = new Dictionary<string, string>();
        private string _FirstLetter = "a";
        public string FirstLetter
        {
            get { return _FirstLetter; }
            set {_FirstLetter = value; }
        }
        private LettersType _letterType = LettersType.Category;
        public LettersType LetterType
        {
            get { return _letterType; }
            set {_letterType = value;}
        }
        public static object _lock = new object();
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
            string ck = prefixInd+_FirstLetter;
            if (_Cache.ContainsKey(ck))
                return _Cache[ck];
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
            if (!_Cache.ContainsKey(ck))
                lock (_lock)
                    if (!_Cache.ContainsKey(ck))
                        _Cache.Add(ck, sb.ToString());
            return sb.ToString();
        }
        #endregion

        #region -RenderBrandHTML-
        string RenderBrandHTML()
        {
            string ck = prefixBra + _FirstLetter;
            if (_Cache.ContainsKey(ck))
                return _Cache[ck];
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

            if (!_Cache.ContainsKey(ck))
                lock (_lock)
                    if (!_Cache.ContainsKey(ck))
                        _Cache.Add(ck, sb.ToString());
            return sb.ToString();
        }
        #endregion

        #region -RenderCategoryHTML-
        string RenderCategoryHTML()
        {
            string ck = prefixCat + _FirstLetter;
            if (_Cache.ContainsKey(ck))
                return _Cache[ck];
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

            if (!_Cache.ContainsKey(ck))
                lock (_lock)
                    if (!_Cache.ContainsKey(ck))
                        _Cache.Add(ck, sb.ToString());
            return sb.ToString();
        }
        #endregion

        public override void RenderControl(HtmlTextWriter writer)
        {
            if (this.Visible)
            {
                writer.Write(RenderHTML());
                writer.WriteLine(Environment.NewLine);
            }
        }
    }
}
