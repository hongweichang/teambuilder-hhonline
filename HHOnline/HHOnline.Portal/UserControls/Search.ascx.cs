using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using System.Text;

public partial class UserControls_Search : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindHotSearch();
    }
    void BindHotSearch()
    {
        List<String> searchWords = WordSearchManager.GetHotWords();
        if (searchWords.Count == 0)
            ltHotSearch.Text = "--";
        else
        {
            searchWords = searchWords.GetRange(0,Math.Min(5, searchWords.Count));
            string src = "<a href=\"" + GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-search&w={2}\" title=\"{0}\">{1}</a>";
            StringBuilder sb=new StringBuilder();
            foreach (string s in searchWords)
            {
                sb.AppendFormat(src, s, GlobalSettings.SubString(s, 10), HttpUtility.UrlEncode(s));
            }
            ltHotSearch.Text = sb.ToString();
        }
    }
}
