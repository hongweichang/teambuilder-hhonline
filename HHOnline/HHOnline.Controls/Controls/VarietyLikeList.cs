using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class VarietyLikeList:UserControl
    {
        static VarietyLikeList()
        {
            ProductBrands.Updated += delegate { _Html = null; };
        }
        private List<ProductBrand> brands = null;
        private int _BrandID = 0;
        static readonly string _href = "<a href=\"" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-brand&ID={0}\">{1}</a>";
        public int BrandID
        {
            get { return _BrandID; }
            set {
                if (_BrandID != value)
                {
                    _Html = null;
                }
                _BrandID = value; }
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
            if (brands == null)
            {
                brands = ProductBrands.GetProductBrands();
            }
            StringBuilder sb = new StringBuilder();
            if (_BrandID == 0)
            {
                return "<div class=\"" + _CssClass + "\"><span>暂无相关品牌信息！</span></div>";
            }
            else
            {
                ProductBrand pb = ProductBrands.GetProductBrand(_BrandID);

                List<ProductBrand> bs = GetSubBrand(pb.BrandGroup);
                if (bs == null || bs.Count == 0 || (bs.Count == 1 && bs[0].BrandID == _BrandID))
                {
                    return "<div class=\"" + _CssClass + "\"><span>暂无相关品牌信息！</span></div>";
                }
                sb.Append("<div class=\"" + _CssClass + "\">");
                ProductQuery query;
                int count = 0;
                foreach (ProductBrand b in bs)
                {
                    if (b.BrandID != _BrandID)
                    {
                        count = 0;
                        query = new ProductQuery();
                        query.BrandID = b.BrandID;
                        try
                        {
                            count = Products.GetProducts(query).Records.Count;
                        }
                        catch { count = 0; }
                        sb.AppendFormat(_href, GlobalSettings.Encrypt(b.BrandID.ToString()), b.BrandName + "(" + count + ")");
                    }
                }
                    sb.Append("</div>");
                return sb.ToString();
            }
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
            writer.Write(HTML);
            writer.WriteLine(Environment.NewLine);
        }
    }
}
