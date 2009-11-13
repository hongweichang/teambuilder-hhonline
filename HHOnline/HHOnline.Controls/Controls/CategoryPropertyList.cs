using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class CategoryPropertyList:Control
    {
        static CategoryPropertyList()
        {
            ProductCategories.Updated += new EventHandler<EventArgs>(ProductCategories_Updated);
        }
        static void ProductCategories_Updated(object sender, EventArgs e)
        {
            int cId = 0;
            if(int.TryParse(sender.ToString(),out cId))
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
        private static Dictionary<int, string> _Cache = new Dictionary<int, string>();
        private int _CategoryID = 0;
        static readonly string _href = "<a href=\"" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-category&ID={0}&PropID={1}\">{2}</a>";
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
