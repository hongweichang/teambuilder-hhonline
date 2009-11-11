using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using HHOnline.Shops;
using HHOnline.Framework;
using System.IO;
using HHOnline.News.Components;
using HHOnline.News.Services;

namespace HHOnline.Controls
{
    public enum LettersType
    {
        Category = 0,
        Brand = 1,
        Industry = 2
    }
    public class LettersCollectionList : Control
    {
        private string _CssName;
        public string CssName
        {
            get { return _CssName; }
            set { _CssName = value; }
        }
        private LettersType _LetterType = LettersType.Category;
        public LettersType LetterType
        {
            get { return _LetterType; }
            set
            {
                _LetterType = value;
            }
        }
        public object _lock = new object();
        private  string[] letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private static Dictionary<LettersType, string> _Cache = new Dictionary<LettersType, string>();
        string RenderHTML()
        {
            if (_Cache.ContainsKey(_LetterType))
                return _Cache[_LetterType];
            else
            {
                HtmlGenericControl ul = BindLetterList();
                StringWriter sw = new StringWriter();
                ul.RenderControl(new HtmlTextWriter(sw));
                string s = sw.ToString();

                if (!_Cache.ContainsKey(_LetterType))
                {
                    lock (_lock)
                    {
                        if (!_Cache.ContainsKey(_LetterType))
                            _Cache.Add(_LetterType, s);
                    }
                }
                return s;
            }
        }
        HtmlGenericControl BindLetterList()
        {
            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Attributes.Add("class", this.CssName);

            HtmlGenericControl li = null;
            HtmlAnchor anchor = null;
            foreach (string s in letters)
            {
                li = new HtmlGenericControl("LI");
                anchor = new HtmlAnchor();
                anchor.HRef = GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-guidbyletter&t=" + (int)_LetterType
                                + "&w=" + s;
                anchor.InnerText = s;
                anchor.Title = "按字母" + s + "检索。";
                anchor.Target = "_blank";
                li.Controls.Add(anchor);

                ul.Controls.Add(li);
            }
            return ul;
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            writer.Write(RenderHTML());
            writer.Write(Environment.NewLine);
        }
    }
}
