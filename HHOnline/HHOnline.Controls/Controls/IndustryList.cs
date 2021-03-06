﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using HHOnline.Shops;
using HHOnline.Framework;
using System.IO;

namespace HHOnline.Controls
{
    public class IndustryList :Control
    {
        static IndustryList() {
            ProductIndustries.Updated += delegate { _Html = string.Empty; };
        }
        private int _Num = 10;
        /// <summary>
        /// 显示的列表数
        /// </summary>
        public int Num
        {
            get { return _Num; }
            set { _Num = value; }
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
                            HtmlGenericControl ul = BindIndustryList();
                            StringWriter sw = new StringWriter();
                            ul.RenderControl(new HtmlTextWriter(sw));
                            _Html = sw.ToString();
                        }
                    }
                }
                return _Html;
            }
        }
        HtmlGenericControl BindIndustryList()
        {
            List<ProductIndustry> industriesTemp = ProductIndustries.GetProductIndustries();
            List<ProductIndustry> industries = industriesTemp.GetRange(0, Math.Min(_Num, industriesTemp.Count));

            if (industries.Count == 0)
            {
                HtmlGenericControl p = new HtmlGenericControl("P");
                p.InnerText = "没有行业应用信息！";
                return p;
            }

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.ID = "ulIndustryList";

            HtmlGenericControl li = null;
            HtmlAnchor anchor = null;
            foreach (var b in industries)
            {
                li = new HtmlGenericControl("LI");
                anchor = new HtmlAnchor();
                anchor.HRef = GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-industry&ID=" + GlobalSettings.Encrypt(b.IndustryID.ToString());
                anchor.Target = "_blank";
                anchor.InnerText = b.IndustryName;
                anchor.Title = b.IndustryTitle;
                li.Controls.Add(anchor);

                ul.Controls.Add(li);
            }
            return ul;
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            writer.Write(HTML);
            writer.Write(Environment.NewLine);
        }
    }
}
