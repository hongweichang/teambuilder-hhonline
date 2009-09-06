using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class IndustryLikeList:UserControl
    {
        static IndustryLikeList()
        {
            if (inds == null)
            {
                inds = ProductIndustries.GetChildIndustries(0);
            }
        }
        private static List<ProductIndustry> inds = null;
        private int _IndustryID = 0;
        static readonly string _href = "<a href=\""+GlobalSettings.RelativeWebRoot+"pages/product-industry&ID={0}\">{1}</a>";
        public int IndustryID
        {
            get { return _IndustryID; }
            set { _IndustryID = value; }
        }

        private string _CssClass;
        public string CssClass
        {
            get { return _CssClass; }
            set { _CssClass = value; }
        }
        string RenderHTML()
        {
            StringBuilder sb = new StringBuilder();
            if (_IndustryID == 0)
            {
                return "<div class=\"" + _CssClass + "\"><span>暂无相关行业信息！</span></div>";
            }
            else
            {
                ProductIndustry pi = ProductIndustries.GetProductIndustry(_IndustryID);
                if (pi.ParentID == 0)
                {
                    return "<div class=\"" + _CssClass + "\"><span>此行业为顶级行业分类！</span></div>";
                }
                List<ProductIndustry> pis = ProductIndustries.GetChildIndustries(pi.ParentID);

                if (pis == null || pis.Count == 0 || (pis.Count == 1 && pis[0].IndustryID == _IndustryID))
                {
                    return "<div class=\"" + _CssClass + "\"><span>暂无相关行业信息！</span></div>";
                }
                sb.Append("<div class=\"" + _CssClass + "\">");
                ProductQuery query;
                int count = 0;
                foreach (ProductIndustry p in pis)
                {
                    if (p.IndustryID != _IndustryID)
                    {
                        count = 0;
                        query = new ProductQuery();
                        query.IndustryID = p.IndustryID;
                        try
                        {
                            count = Products.GetProducts(query).Records.Count;
                        }
                        catch { count = 0; }
                        sb.AppendFormat(_href, GlobalSettings.Encrypt(p.IndustryID.ToString()), p.IndustryName + "(" + count + ")");
                    }
                }
                    sb.Append("</div>");
                return sb.ToString();
            }
        }
        public override void RenderControl(HtmlTextWriter writer)
        {
            string html = RenderHTML();

            writer.Write(html);
            writer.WriteLine(Environment.NewLine);
        }
    }
}
