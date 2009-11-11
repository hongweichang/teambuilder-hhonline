using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class CategorySubList:Control
    {
        static CategorySubList()
        {
            ProductCategories.Updated += new EventHandler<EventArgs>(ProductCategories_Updated);
        }
        static void ProductCategories_Updated(object sender, EventArgs e)
        {
            int cId = 0;
            try
            {
                cId = (int)sender;
                _Cache.Remove(cId);
            }
            catch
            {
                string[] cidList = sender.ToString().Split(',');
                foreach (string i in cidList)
                {
                    cId = int.Parse(i);
                    _Cache.Remove(cId);
                }
            }
        }
        private static Dictionary<int, string> _Cache = new Dictionary<int, string>();
        private int _CategoryID = 0;
        static readonly string _href = "<a href=\"" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-category&ID={0}\">{1}</a>";
        public int CategoryID
        {
            get { return _CategoryID; }
            set {_CategoryID = value; }
        }
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
            List<ProductCategory> pcs = ProductCategories.GetCategories();
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
            return sb.ToString();
        }
        public override void RenderControl(HtmlTextWriter writer)
        {
            writer.Write(RenderHTML());
            writer.WriteLine(Environment.NewLine);
        }
    }
}
