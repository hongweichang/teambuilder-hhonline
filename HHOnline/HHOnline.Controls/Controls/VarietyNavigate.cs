using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class VarietyNavigate:Control
    {
        static VarietyNavigate()
        {
            ProductBrands.Updated += new EventHandler<EventArgs>(Brand_Updated);
        }
        protected static void Brand_Updated(object sender, EventArgs e)
        {
            _Cache.Remove((int)sender);
        }
        private int _BrandID = 0;
        static readonly string _href = "<a href=\"" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-brand{0}\">{1}</a>";
        public int BrandID
        {
            get { return _BrandID; }
            set {_BrandID = value; }
        }
        private static Dictionary<int, string> _Cache = new Dictionary<int, string>();
        public static object _lock = new object();
        string RenderHTML()
        {
            if (_Cache.ContainsKey(_BrandID))
                return _Cache[_BrandID];
            List<ProductBrand> brands = ProductBrands.GetProductBrands();
            StringBuilder sb = new StringBuilder();
            if (_BrandID == 0)
            {
                sb.Append("您的位置：<b>所有品牌</b>");
            }
            else
            {
                ProductBrand pb = ProductBrands.GetProductBrand(_BrandID);
                sb.Append("<b>"+pb.BrandName+"</b>");

                string _bId = string.Empty;
                List<ProductBrand> bs = GetSubBrand(pb.BrandName,brands);
                foreach(ProductBrand b in bs)
                {
                    _bId = GlobalSettings.Encrypt(b.BrandID.ToString());
                    sb.Insert(0, string.Format(_href, "&ID=" + _bId, b.BrandName) + ">>");
                }
                sb.Insert(0, "您的位置：" + string.Format(_href, "", "所有品牌") + ">>");
                if (!_Cache.ContainsKey(_BrandID))
                    lock (_lock)
                        if (!_Cache.ContainsKey(_BrandID))
                            _Cache.Add(_BrandID, sb.ToString());
            }
            return sb.ToString();
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
