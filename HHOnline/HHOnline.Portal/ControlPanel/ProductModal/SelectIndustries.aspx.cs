using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Shops;

public partial class ControlPanel_ProductModal_SelectIndustries : HHPage, ICallbackEventHandler
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindJSON();
    }
    void BindJSON()
    {
        string script = Page.ClientScript.GetCallbackEventReference(this, "arg", "refreshBinder", "");
        base.ExecuteJs("function callServer(arg){" + script + "}", false);
    }
    string GetCategories(int pId)
    {
        List<ProductIndustry> inds = ProductIndustries.GetProductIndustries();
        List<ProductIndustry> _inds = new List<ProductIndustry>();
        foreach (ProductIndustry pi in inds)
        {
            if (pi.ParentID == pId)
            {
                _inds.Add(pi);
            }
        }
        return Newtonsoft.Json.JavaScriptConvert.SerializeObject(_inds);
    }
    string GetNavigations(int pId)
    {
        List<ProductIndustry> _inds = new List<ProductIndustry>();
        if (pId != 0)
        {
            ProductIndustry pi = ProductIndustries.GetProductIndustry(pId);
            _inds.Add(pi);
            while (pi.ParentID != 0)
            {
                pi = ProductIndustries.GetProductIndustry(pi.ParentID);
                _inds.Add(pi);
            }
        }
        return Newtonsoft.Json.JavaScriptConvert.SerializeObject(_inds);
    }
    public override void OnPageLoaded()
    {
        AddJavaScriptInclude("scripts/pages/selectindustries.aspx.js", false, false);
    }

    #region -ICallbackEventHandler Members-
    string result = null;
    public string GetCallbackResult()
    {
        return result;
    }

    public void RaiseCallbackEvent(string eventArgument)
    {
        int pId = int.Parse(eventArgument);
        result = "{cats:" + GetCategories(pId) + ",nav:" + GetNavigations(pId) + "}";
    }

    #endregion
}