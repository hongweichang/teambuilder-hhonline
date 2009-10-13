using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class SupplyList:UserControl
    {
        public SupplyList()
        {
            ProductQuery q = new ProductQuery();
            q.PageSize = int.MaxValue;
            q.Filter = ProviderFilter.Deny;
            q.SortOrder = SortOrder.Descending;
            q.ProductOrderBy = ProductOrderBy.DataCreated;
            cs = Products.GetProductList(q);
        }
        private static List<Product> cs = null;
        private int _ItemCount = 10;
        private string _format = "<li><div class=\"companyList_col1\" title=\"{0}\">{1}</div><div class=\"companyList_col2\">{2}</div></li>";
        public int ItemCount
        {
            get { return _ItemCount; }
            set { _ItemCount = value; }
        }
        public string RenderHTML()
        {
            string nav = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-product";
           
            StringBuilder sb = new StringBuilder();
            int count = cs.Count;
            if (count == 0)
            {
                sb.Append("<div class=\"empty_cl\">暂无待审核供应商录入产品！</div>");
            }
            else
            {
                int min = Math.Min(count, _ItemCount);
                sb.Append("<ul>");
                for (int i = 0; i < min; i++)
                {

                    sb.AppendFormat(_format,
                            cs[i].ProductName,
                            GlobalSettings.SubString(cs[i].ProductName, 20),
                            cs[0].CreateTime.ToShortDateString()
                            );
                }
                sb.Append("</ul>");
                if (min < count)
                {
                    sb.Append("<a class=\"clExec\" onclick=\"resolvePending('" + nav + "')\" href=\"#\" >还有<span>" + (count - min) + "</span>条待审核供应商录入产品，马上处理！</a>");
                }
                else
                {
                    sb.Append("<a class=\"clExec\" onclick=\"resolvePending('" + nav + "')\" href=\"#\">马上处理此<span>" + count + "</span>条待审核供应商录入产品！</a>");
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
