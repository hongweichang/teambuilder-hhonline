using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Shops;
using System.Text;
using HHOnline.Framework;

public partial class ControlPanel_ProductModal_SelectCategories :HHPage,ICallbackEventHandler
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
    string GetCategories(int pId) {
        List<ProductCategory> pcs = ProductCategories.GetCategories();
        List<ProductCategory> _pcs = new List<ProductCategory>();
        foreach (ProductCategory pc in pcs) {
            if (pc.ParentID == pId) {
                _pcs.Add(pc);
            }
        }
        return Newtonsoft.Json.JavaScriptConvert.SerializeObject(_pcs);
    }
    string GetNavigations(int pId) {
        List<ProductCategory> pcs = new List<ProductCategory>();
        if (pId != 0)
        {
            ProductCategory pc = ProductCategories.GetCategory(pId);
            pcs.Add(pc);
            while (pc.ParentID != 0)
            {
                pc = ProductCategories.GetCategory(pc.ParentID);
                pcs.Add(pc);
            }
        }
        return Newtonsoft.Json.JavaScriptConvert.SerializeObject(pcs);
    }
    public override void OnPageLoaded() {
        AddJavaScriptInclude("scripts/pages/selectcategories.aspx.js", false, false);
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
