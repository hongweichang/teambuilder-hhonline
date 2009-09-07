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
        static CategoryNavigate()
        {
            if (pcs == null)
            {
                pcs = ProductCategories.GetCategories();
            }
        }
        private static List<ProductCategory> pcs = null;
        private int _CategoryID = 0;
        static readonly string _href = "<a href=\"" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-category{0}\">{1}</a>";
        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
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
            string html = RenderHTML();

            writer.Write(html);
            writer.WriteLine(Environment.NewLine);
        }
    }
}
