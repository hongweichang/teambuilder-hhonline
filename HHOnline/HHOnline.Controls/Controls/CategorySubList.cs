using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class CategorySubList:UserControl
    {
        static CategorySubList()
        {
            if (pcs == null)
            {
                pcs = ProductCategories.GetCategories();
            }
        }
        private static List<ProductCategory> pcs = null;
        private int _CategoryID = 0;
        static readonly string _href = "<a href=\""+GlobalSettings.RelativeWebRoot+"pages/product-category&ID={0}\">{1}</a>";
        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
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
            if (_CategoryID == 0)
            {
                return "<div class=\"" + _CssClass + "\"><span>暂无子分类信息！</span></div>";
            }
            else
            {
                string _catId = string.Empty;
                ProductCategory curCat = null;
                List<ProductCategory> subCats = ProductCategories.GetChidCategories(_CategoryID);
                if (subCats == null || subCats.Count == 0)
                {
                    return "<div class=\"" + _CssClass + "\"><span>暂无子分类信息！</span></div>";
                }
                sb.Append("<div class=\"" + _CssClass + "\">");
                ProductQuery query;
                int count = 0;
                for (int i = 0; i < subCats.Count; i++)
                {
                    curCat = subCats[i];
                    if (curCat.CategoryID != _CategoryID)
                    {
                        count = 0;
                        query = new ProductQuery();
                        query.CategoryID = curCat.CategoryID;
                        try
                        {
                            count = Products.GetProducts(query).Records.Count;
                        }
                        catch { count = 0; }
                        sb.AppendFormat(_href, GlobalSettings.Encrypt(curCat.CategoryID.ToString()), curCat.CategoryName + "(" + count + ")");
                    }
                }
                sb.Append("</div>");
            }
            return sb.ToString();
        }
        public override void RenderControl(HtmlTextWriter writer)
        {
            string html = RenderHTML();

            writer.Write(html);
            writer.WriteLine(Environment.NewLine);
        }
    }
}
