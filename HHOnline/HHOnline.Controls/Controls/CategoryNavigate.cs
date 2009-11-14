using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class CategoryNavigate:Control
    {
        private int _CategoryID = 0;
        private static Dictionary<int, string> _Cache = new Dictionary<int, string>();
        static readonly string _href = "<a href=\"" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-category{0}\">{1}</a>";
        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        public static object _lock = new object();
        string RenderHTML()
        {
            if (_Cache.ContainsKey(_CategoryID))
                return _Cache[_CategoryID];
            StringBuilder sb = new StringBuilder();
            if (_CategoryID == 0)
            {
                sb.Append("您的位置：<b>所有类别</b>");
            }
            else
            {
                ProductCategory curCat = ProductCategories.GetCategory(_CategoryID);
                sb.Append("<b>"+curCat.CategoryName+"</b>");
                int parId = curCat.ParentID;
                string _catId = string.Empty;
                while (parId != 0)
                {
                    _catId = GlobalSettings.Encrypt(parId.ToString());
                    curCat = ProductCategories.GetCategory(parId);
                    if (curCat == null) break;
                    sb.Insert(0, string.Format(_href, "&ID=" + _catId, curCat.CategoryName) + ">>");
                    parId = curCat.ParentID;
                }
                sb.Insert(0, "您的位置："+string.Format(_href, "", "所有类别") + ">>");
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
