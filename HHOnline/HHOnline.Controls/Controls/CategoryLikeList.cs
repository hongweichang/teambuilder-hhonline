using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class CategoryLikeList:UserControl
    {
        static CategoryLikeList()
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
                return "<div class=\"" + _CssClass + "\">暂无相关类别信息！</div>";
            }
            else
            {
                ProductCategory curCat = ProductCategories.GetCategory(_CategoryID);
               
                int parId = curCat.ParentID;
                string _catId = string.Empty;
                if (parId != 0)
                {
                    List<ProductCategory> subCats = ProductCategories.GetChidCategories(parId);
                    if (subCats == null || subCats.Count == 0 || (subCats.Count==1&&subCats[0].CategoryID == _CategoryID))
                    {
                        return "<div class=\"" + _CssClass + "\">暂无相关类别信息！</div>";
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
                else
                {
                    return "<div class=\"" + _CssClass + "\">暂无相关类别信息！</div>";
                }
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
