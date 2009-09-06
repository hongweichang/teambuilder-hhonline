using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class VarietyNavigate:UserControl
    {
        static VarietyNavigate()
        {
            if (brands == null)
            {
                brands = ProductBrands.GetProductBrands();
            }
        }
        private static List<ProductBrand> brands = null;
        private int _BrandID = 0;
        static readonly string _href = "<a href=\""+GlobalSettings.RelativeWebRoot+"pages/product-brand{0}\">{1}</a>";
        public int BrandID
        {
            get { return _BrandID; }
            set { _BrandID = value; }
        }

        string RenderHTML()
        {
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
                List<ProductBrand> bs = GetSubBrand(pb.BrandName);
                foreach(ProductBrand b in bs)
                {
                    _bId = GlobalSettings.Encrypt(b.BrandID.ToString());
                    sb.Insert(0, string.Format(_href, "&ID=" + _bId, b.BrandName) + ">>");
                }
                sb.Insert(0, "您的位置：" + string.Format(_href, "", "所有品牌") + ">>");
            }
            return sb.ToString();
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
            string html = RenderHTML();

            writer.Write(html);
            writer.WriteLine(Environment.NewLine);
        }
    }
}
