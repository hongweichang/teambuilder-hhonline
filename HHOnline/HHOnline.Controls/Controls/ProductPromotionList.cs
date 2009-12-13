using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;
using System.Configuration;

namespace HHOnline.Controls
{
    public class ProductPromotionList:Control
    {
        public ProductPromotionList()
        {
            Products.Updated += delegate { _Cache.Clear(); };
        }
        private FocusType? _ProductType = null;
        public FocusType? ProductType
        {
            get { return _ProductType; }
            set {_ProductType = value; }
        }
        private int _Columns = 5;
        public int Columns
        {
            get { return _Columns; }
            set { _Columns = value; }
        }
        private int _Max = 12;
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
        private static Dictionary<FocusType, string> _Cache = new Dictionary<FocusType, string>();
        public static object _lock = new object();
        public string RenderHTML()
        {
            FocusType ft = (FocusType)_ProductType;
            if (_Cache.ContainsKey(ft))
                return _Cache[ft];
            ProductQuery pq = new ProductQuery();
            pq.FocusType = _ProductType;
            pq.HasPublished = true;
            List<Product> ps = Products.GetProductList(pq);
            
            string nav = GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-product";
            if (ps == null || ps.Count == 0)
            {
                switch (_ProductType)
                {
                    case FocusType.New:
                        return "<div class=\"nopiInfo\"><span>当前没有相应的新进产品信息！</span></div>";
                    case FocusType.Hot:
                        return "<div class=\"nopiInfo\"><span>当前没有相应的热销产品信息！</span></div>";
                    case FocusType.Recommend:
                        return "<div class=\"nopiInfo\"><span>当前没有相应的推荐产品信息！</span></div>";
                    case FocusType.Promotion:
                        return "<div class=\"nopiInfo\"><span>当前没有相应的促销产品信息！</span></div>";
                    default:
                        return "<div class=\"nopiInfo\"><span>当前没有相应的产品信息！</span></div>";
                }
            }
            List<Product> psList = null;
            Product p = null;
            StringBuilder sb = new StringBuilder();
            int curCount = ps.Count;
            psList = ps.GetRange(0, Math.Min(curCount, _Max));
            sb.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" class=\""+_CssClass+"\">");
            string pId = string.Empty;
            int length = psList.Count;

            for (int i = 0; i < psList.Count; i++)
            {
                p = psList[i];
                if (i % _Columns == 0)
                    sb.AppendLine("<tr>");
                pId = GlobalSettings.Encrypt(p.ProductID.ToString());
                sb.AppendLine("<td>");
                sb.AppendLine("<div class=\"piThumbnail\">"+
                                "<a href=\"" + nav + "&ID=" + pId + "\" target=\"_blank\">"+
                                    "<div style=\"background-image:url("+p.GetDefaultImageUrl(100,100)+")\" title=\"" + p.ProductName + "\" >"+
                                    "</div>"+
                                "</a>"+
                               "</div>");

                sb.AppendLine("<div class=\"piProductName\" title='"+p.ProductName+"'>" + GlobalSettings.SubString(p.ProductName,15) + "</div>");
                sb.AppendLine("<div class=\"piPrice\">" + GetPrice(p.ProductID) + "</div>");
                sb.AppendLine("</td>");

                if (i % _Columns == _Columns-1)
                    sb.AppendLine("</tr>");

            }

            sb.AppendLine("</table>");

            if (curCount > _Max)
                sb.Append("<div class=\"list-more\"><a target=\"_blank\" href=\"" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-productfocus&&t="+(int)this.ProductType+"\" title=\"查看全部。。。\"></a></div>");
            if (!_Cache.ContainsKey(ft))
                lock (_lock)
                    if (!_Cache.ContainsKey(ft))
                        _Cache.Add(ft, sb.ToString());
            return sb.ToString();
        }
        List<Product> __ps = null;
        bool IsPromote(int pId)
        {

            ProductQuery pq = new ProductQuery();
            pq.FocusType = FocusType.Promotion;
            pq.HasPublished = true;
            if (__ps == null)
                __ps = Products.GetProductList(pq);
            if (__ps == null || __ps.Count == 0) return false;
            else
            {
                foreach (Product p in __ps)
                {
                    if (p.ProductID == pId) return true;
                }
            }
            return false;
        }
        string GetPrice(int pId)
        {
            decimal? price1 = null;
            decimal? price2 = null;
            decimal? price3 = null;
            decimal? p = null;
            if (Context.User.Identity.IsAuthenticated)
            {                
                SettingsPropertyValueCollection spvc = this.Context.Profile.PropertyValues;
                User u = spvc["AccountInfo"].PropertyValue as User;
                price1 = ProductPrices.GetPriceMarket(u.UserID, pId);
                price2 = ProductPrices.GetPriceMember(u.UserID, pId);
                price3 = IsPromote(pId) ? ProductPrices.GetPricePromote(u.UserID, pId) : null;
                p = GlobalSettings.GetMinPrice(price1, price2);
                return GlobalSettings.GetPrice(p, price3);
            }
            else
            {
                price1 = ProductPrices.GetPriceDefault(pId);
                price3 = IsPromote(pId) ? ProductPrices.GetPricePromote(0, pId) : null;
                return GlobalSettings.GetPrice(price1, price3);
            }
        }
       
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
