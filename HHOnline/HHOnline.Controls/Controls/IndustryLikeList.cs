using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class IndustryLikeList:Control
    {
        static IndustryLikeList()
        {
            ProductIndustries.Updated += new EventHandler<EventArgs>(Industry_Updated);
        }
        static void Industry_Updated(object sender, EventArgs e)
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
        public static object _lock = new object();
        private static Dictionary<int, string> _Cache = new Dictionary<int, string>();
        private int _IndustryID = 0;
        static readonly string _href = "<a href=\"" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-industry&ID={0}\">{1}</a>";
        public int IndustryID
        {
            get { return _IndustryID; }
            set {_IndustryID = value; }
        }

        private string _CssClass;
        public string CssClass
        {
            get { return _CssClass; }
            set { _CssClass = value; }
        }
        string RenderHTML()
        {
            if (_Cache.ContainsKey(_IndustryID))
                return _Cache[_IndustryID];
            List<ProductIndustry>  inds = ProductIndustries.GetChildIndustries(0);
            StringBuilder sb = new StringBuilder();
            if (_IndustryID == 0)
            {
                return "<div class=\"" + _CssClass + "\"><span>暂无相关行业信息！</span></div>";
            }
            else
            {
                ProductIndustry pi = ProductIndustries.GetProductIndustry(_IndustryID);
                if (pi.ParentID == 0)
                {
                    return "<div class=\"" + _CssClass + "\"><span>此行业为顶级行业分类！</span></div>";
                }
                List<ProductIndustry> pis = ProductIndustries.GetChildIndustries(pi.ParentID);

                if (pis == null || pis.Count == 0 || (pis.Count == 1 && pis[0].IndustryID == _IndustryID))
                {
                    return "<div class=\"" + _CssClass + "\"><span>暂无相关行业信息！</span></div>";
                }
                sb.Append("<div class=\"" + _CssClass + "\">");
                ProductQuery query;
                int count = 0;
                PagingDataSet<Product> __ps = null;
                foreach (ProductIndustry p in pis)
                {
                    if (p.IndustryID != _IndustryID)
                    {
                        count = 0;
                        query = new ProductQuery();
                        query.IndustryID = p.IndustryID;
                        __ps = Products.GetProducts(query);
                        if (__ps != null&&__ps.Records!=null)
                        { 
                           count = Products.GetProducts(query).Records.Count; 
                        }
                        sb.AppendFormat(_href, GlobalSettings.Encrypt(p.IndustryID.ToString()), p.IndustryName + "(" + count + ")");
                    }
                }
                sb.Append("</div>");
                if (!_Cache.ContainsKey(_IndustryID))
                    lock (_lock)
                        if (!_Cache.ContainsKey(_IndustryID))
                            _Cache.Add(_IndustryID, sb.ToString());
                return sb.ToString();
            }
        }
        public override void RenderControl(HtmlTextWriter writer)
        {
            writer.Write(RenderHTML());
            writer.WriteLine(Environment.NewLine);
        }
    }
}
