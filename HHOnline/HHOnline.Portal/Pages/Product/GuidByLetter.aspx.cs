using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Controls;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

public partial class Pages_Product_GuidByLetter : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }

    }
    void BindData()
    {
        LettersType lt = LettersType.Category;
        NameValueCollection req = Request.QueryString;
        try { lt = (LettersType)(int.Parse(req["t"])); }
        catch { }

        string w = "a";
        if (!string.IsNullOrEmpty(req["w"]))
        {
            w = req["w"];
            if (char.IsLetter(w,0))
            {
                w = w[0].ToString();
            }
            else
                w = "a";
        }
        w = w.ToUpper();
        llCategory.LetterType = lt;
        llCategory.FirstLetter = w;
        ltLetterType.Text = "按首字母<span class=\"needed\">\"" + w + "\"</span>检索【" + GetDesc(lt) + "】";
    }
    string GetDesc(LettersType lt)
    {
        switch (lt)
        {
            case LettersType.Category:
                return "产品类别";
            case LettersType.Brand:
                return "产品品牌";
            case LettersType.Industry:
                return "产品行业";
        }
        return "——";
    }

    #region -BindData-
    string GetUrl()
    {
        if (Request.QueryString["sortby"] == null)
        {
            return Request.RawUrl.ToString();
        }
        else
        {
            string url = Request.RawUrl.ToString().Split('&')[0];
            foreach (string k in Request.QueryString.AllKeys)
            {
                if (k != "sortby")
                {
                    url += "&" + k + "=" + Request.QueryString[k];
                }
            }
            return url;
        }
    }
    #endregion

    public override void OnPageLoaded()
    {
        this.ShortTitle = "按字母检索";
       //// "        按首字母<span class=\"needed\">\"" + w + "\"</span>检索【" + GetDesc(lt) + "】"
       // string letter = this.llCategory.FirstLetter;
       // string stype = GetDesc(this.llCategory.LetterType);

       // string title = string.Empty, key = string.Format("{0}首字母检索{1}",stype,letter.ToUpper());

       // switch (lt)
       // {
       //     case LettersType.Category:
       //         return "产品类别";
       //     case LettersType.Brand:
       //         return "产品品牌";
       //     case LettersType.Industry:
       //         return "产品行业";
       // }
       // return "——";
       
       // this.AddKeywords(title);
        
        
        this.SetTitle();
    }
}
