using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Shops;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Framework;

public partial class Pages_Home_ProductCategories : HHPage
{
    List<ProductCategory> pcs = null;
    int maxCategories = 0;
    int maxSubcategories = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        maxCategories = int.Parse(HHConfiguration.GetConfig()["maxCategories"].ToString());
        maxSubcategories = int.Parse(HHConfiguration.GetConfig()["maxSubCategories"].ToString());
        if (!IsPostBack)
        {
            BindProductCategories();
        }
    }

    protected void dlProductCategories_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataList rep = (DataList)e.Item.FindControl("dlSubProductCategories");
            int catId = (int)dlProductCategories.DataKeys[e.Item.ItemIndex];
            rep.DataSource = BindSubProductCategories(catId);
            rep.DataBind();
        }
    }
    List<ProductCategory> BindSubProductCategories(int catId)
    {
        if (pcs == null)
        {
            pcs = ProductCategories.GetCategories();
        }
        List<ProductCategory> pcList = new List<ProductCategory>();
        ProductCategory pc = null;

        for (int i = 0; i < pcs.Count; i++)
        {
            pc = pcs[i];
            if (pc.ParentID == catId)
            {
                pcList.Add(pc);
            }
        }
        int curCount = pcList.Count;
        if (curCount > maxSubcategories)
            pcList = pcList.GetRange(0, maxSubcategories);
        return pcList;
    }

    void BindProductCategories()
    {
        pcs = ProductCategories.GetCategories();
        List<ProductCategory> pcList = new List<ProductCategory>();
        ProductCategory pc = null;

        for (int i = 0; i < pcs.Count; i++)
        {
            pc = pcs[i];
            if (pc.ParentID == 0)
            {
                pcList.Add(pc);
            }
        }
        int curCount = pcList.Count;
        if (curCount > maxCategories)
            pcList = pcList.GetRange(0, maxCategories);
        dlProductCategories.DataSource = pcList;
        dlProductCategories.DataBind();
    }
    public override void OnPageLoaded()
    {
        base.PageInfoType = InfoType.IframeInfo;
    }
}
