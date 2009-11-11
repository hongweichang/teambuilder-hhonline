using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class VarietyLikeList:Control
    {
        static VarietyLikeList()
        {
            ProductBrands.Updated += new EventHandler<EventArgs>(Brand_Updated);
        }
        protected static void Brand_Updated(object sender, EventArgs e)
        {
            _Cache.Remove((int)sender);
        }

        private int _BrandID = 0;
        static readonly string _href = "<a href=\"" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-brand&ID={0}\">{1}</a>";
        public int BrandID
        {
            get { return _BrandID; }
            set { _BrandID = value; }
        }

        private string _CssClass;
        public string CssClass
        {
            get { return _CssClass; }
            set { _CssClass = value; }
        }
        public static object _lock = new object();
        private static Dictionary<int, string> _Cache = new Dictionary<int, string>();
        string RenderHTML()
        {
            if (_Cache.ContainsKey(_BrandID))
                return _Cache[_BrandID];
            if (_BrandID == 0)
            {
                return "<div class=\"" + _CssClass + "\"><span>暂无相关品牌信息！</span></div>";
            }
            else
            {
                List<ProductBrand> brands = ProductBrands.GetProductBrands();
                StringBuilder sb = new StringBuilder();
                ProductBrand pb = ProductBrands.GetProductBrand(_BrandID);

                List<ProductBrand> bs = GetSubBrand(pb.BrandGroup, brands);
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
                if (!_Cache.ContainsKey(_BrandID))
                    lock (_lock)
                        if (!_Cache.ContainsKey(_BrandID))
                            _Cache.Add(_BrandID, sb.ToString());
                return sb.ToString();
            }
        }

        private List<ProductBrand> GetSubBrand(string groupName,List<ProductBrand> brands)
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
            writer.Write(RenderHTML());
            writer.WriteLine(Environment.NewLine);
        }
    }
}
