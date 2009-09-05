using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class CategoryPropertyList:UserControl
    {
        static CategoryPropertyList()
        {
            if (pcs == null)
            {
                pcs = ProductCategories.GetCategories();
            }
        }
        private static List<ProductCategory> pcs = null;
        private int _CategoryID = 0;
        static readonly string _href = "<a href=\""+GlobalSettings.RelativeWebRoot+"pages/product-category&ID={0}&PropID={1}\">{2}</a>";
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
                return "无";
            }
            else
            {
                List<ProductProperty> props = ProductProperties.GetAllPropertyByCategoryID(_CategoryID);
                if (props == null || props.Count == 0)
                {
                    return "无";
                }
                sb.Append("<div class=\"" + _CssClass + "\">");
                for (int i = 0; i < props.Count; i++)
                {
                    sb.AppendFormat(_href, GlobalSettings.Encrypt(_CategoryID.ToString()), GlobalSettings.Encrypt(props[i].PropertyID.ToString()), props[i].PropertyName);
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
