using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class CategoryNavigate:UserControl
    {
        public CategoryNavigate()
        {
            
        }
        private int _CategoryID = 0;
        static readonly string _href = "<a href=\"" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-category{0}\">{1}</a>";
        public int CategoryID
        {
            get { return _CategoryID; }
            set {
                if (value != _CategoryID)
                {
                    _Html = null;
                }
                _CategoryID = value; }
        }
       public static object _lock = new object();
        private static string _Html;
        public string HTML
        {
            get
            {
                if (string.IsNullOrEmpty(_Html))
                {
                    lock (_lock)
                    {
                        if (string.IsNullOrEmpty(_Html))
                        {
                            _Html = RenderHTML();
                        }
                    }
                }
                return _Html;
            }
        }
        string RenderHTML()
        {
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
                    sb.Insert(0, string.Format(_href, "&ID=" + _catId, curCat.CategoryName) + ">>");
                    parId = curCat.ParentID;
                }
                sb.Insert(0, "您的位置："+string.Format(_href, "", "所有类别") + ">>");
            }
            return sb.ToString();
        }
        public override void RenderControl(HtmlTextWriter writer)
        {
            writer.Write(HTML);
            writer.WriteLine(Environment.NewLine);
        }
    }
}
