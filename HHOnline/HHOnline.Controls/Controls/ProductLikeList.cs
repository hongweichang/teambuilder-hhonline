using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;
using System.Configuration;
using HHOnline.SearchBarrel;
using HHOnline.Common;

namespace HHOnline.Controls
{
    public class ProductLikeList:Control
    {
        public ProductLikeList()
        {
            Products.Updated += delegate { _Cache.Clear(); };
        }
        int maxCached = 100;
        private int _productID;
        public int ProductID
        {
            get { return _productID; }
            set { _productID = value; }
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
        private static Dictionary<int, string> _Cache = new Dictionary<int, string>();
        public static object _lock = new object();
        public string RenderHTML()
        {
            if (_Cache.ContainsKey(_productID))
                return _Cache[_productID];
            ProductQuery query = new ProductQuery();
            Product pT = Products.GetProduct(_productID);

            query.ProductNameFilter = pT.ProductName;
            query.PageSize = Int32.MaxValue;
            query.ProductOrderBy = ProductOrderBy.DisplayOrder;
            query.SortOrder = SortOrder.Descending;
            query.PageSize = int.MaxValue;
            query.HasPublished = true;
            SearchResultDataSet<Product> ps = ProductSearchManager.Search(query);
            if (ps == null || ps.Records == null || ps.Records.Count == 0||(ps.Records.Count==1&&ps.Records[0].ProductID==_productID))
            {
                return "<div class=\"nopiInfo\"><span>没有相关产品信息！</span></div>";
            }
            string nav = GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-product";
            List<Product> psList = null;
            Product p = null;
            StringBuilder sb = new StringBuilder();
            int curCount = ps.Records.Count;
            psList = ps.Records.GetRange(0, Math.Min(curCount, _Max));
            sb.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" class=\""+_CssClass+"\">");
            string pId = string.Empty;
            int length = psList.Count;

            for (int i = 0; i < psList.Count; i++)
            {
                p = psList[i];
                if (p.ProductID == _productID) continue;
                if (i % _Columns == 0)
                    sb.AppendLine("<tr>");
                pId = GlobalSettings.Encrypt(p.ProductID.ToString());
                sb.AppendLine("<td>");
                sb.AppendLine("<div class=\"piThumbnail\">"+
                                "<a href=\"" + nav + "&ID=" + pId + "\" target=\"_blank\">"+
                                    "<div style=\"background-image:url("+p.GetDefaultImageUrl(100,100)+")\" title=\"" + HtmlHelper.RemoveHtml(p.ProductName) + "\" >"+
                                    "</div>"+
                                "</a>"+
                               "</div>");

                sb.AppendLine("<div class=\"piPrice\">" + GetPrice(p.ProductID) + "</div>");
                sb.AppendLine("</td>");

                if (i % _Columns == _Columns-1)
                    sb.AppendLine("</tr>");

            }

            sb.AppendLine("</table>");
            if (!_Cache.ContainsKey(_productID))
                lock (_lock)
                    if (!_Cache.ContainsKey(_productID))
                    {
                        if (_Cache.Count > maxCached) {
                            _Cache.Clear();
                        }
                        _Cache.Add(_productID, sb.ToString());
                    }
            return sb.ToString();
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
                price3 = ProductPrices.GetPricePromote(u.UserID, pId);
                p = GlobalSettings.GetMinPrice(price1, price2);
                return GlobalSettings.GetPrice(p, price3);
            }
            else
            {
                price1 = ProductPrices.GetPriceDefault(pId);
                price3 = ProductPrices.GetPricePromote(0, pId);
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
