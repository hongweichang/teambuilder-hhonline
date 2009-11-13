using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class CategoryLikeList:Control
    {
        static CategoryLikeList()
        {
            ProductCategories.Updated += new EventHandler<EventArgs>(ProductCategories_Updated);
        }
        static void ProductCategories_Updated(object sender, EventArgs e)
        {
            int cId = 0;
            if (int.TryParse(sender.ToString(), out cId))
            {
                _Cache.Remove(cId);
            }
            else
            {
                string[] cidList = sender.ToString().Split(',');
                if (cidList.Length > 0)
                {
                    for (int i = 0; i < cidList.Length; i++)
                    {
                        if (int.TryParse(cidList[i], out cId))
                            _Cache.Remove(cId);
                    }
                }
            }
        }
        private List<ProductCategory> pcs = null;
        private int _CategoryID = 0;
        static readonly string _href = "<a href=\"" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-category&ID={0}\">{1}</a>";
        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        private static Dictionary<int, string> _Cache = new Dictionary<int, string>();
        private string _CssClass;
        public string CssClass
        {
            get { return _CssClass; }
            set { _CssClass = value; }
        }
        public static object _lock = new object();
        string RenderHTML()
        {
            if (_Cache.ContainsKey(_CategoryID))
                return _Cache[_CategoryID];
            if (pcs == null)
            {
                pcs = ProductCategories.GetCategories();
            }
            StringBuilder sb = new StringBuilder();
            if (_CategoryID == 0)
            {
                return "<div class=\"" + _CssClass + "\"><span>暂无相关类别信息！</span></div>";
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
                        return "<div class=\"" + _CssClass + "\"><span>暂无相关类别信息！</span></div>";
                    }
                    sb.Append("<div class=\"" + _CssClass + "\">");
                    ProductQuery query;
                    int count = 0;
                    PagingDataSet<Product> __ps = null;
                    for (int i = 0; i < subCats.Count; i++)
                    {
                        curCat = subCats[i];
                        if (curCat.CategoryID != _CategoryID)
                        {
                            count = 0;
                            query = new ProductQuery();
                            query.CategoryID = curCat.CategoryID;
                            __ps = Products.GetProducts(query);
                            if (__ps != null && __ps.Records != null)
                            {
                                count = Products.GetProducts(query).Records.Count;
                            }
                            sb.AppendFormat(_href, GlobalSettings.Encrypt(curCat.CategoryID.ToString()), curCat.CategoryName + "(" + count + ")");
                        }
                    }
                    sb.Append("</div>");
                    if (!_Cache.ContainsKey(_CategoryID))
                    {
                        lock (_lock)
                        {
                            if (!_Cache.ContainsKey(_CategoryID))
                                _Cache.Add(_CategoryID, sb.ToString());
                        }
                    }
                }
                else
                {
                    return "<div class=\"" + _CssClass + "\"><span>暂无相关类别信息！</span></div>";
                }
            }
            return sb.ToString();
        }
        public override void RenderControl(HtmlTextWriter writer)
        {
            writer.Write(RenderHTML());
            writer.WriteLine(Environment.NewLine);
        }
    }
}
