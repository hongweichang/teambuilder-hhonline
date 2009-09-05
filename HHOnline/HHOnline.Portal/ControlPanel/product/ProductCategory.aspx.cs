using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Permission.Components;
using HHOnline.Framework;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Shops;

public partial class ControlPanel_product_ProductCategory : HHPage
{
    #region Private Members
    private string nodeState = CacheKeyManager.PagePrefix + "SelectedCategoryValue/";
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            BindCategory();
        }
        SetValue();

        base.ExecuteJs("var productUrl = '" + GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-product';", true);
    }
    #endregion

    #region BindData
    /// <summary>
    /// 设置值
    /// </summary>
    void SetValue()
    {
        ExecuteJs("window.$selectNodeId=" + this.tvCategory.SelectedValue, false);
    }

    /// <summary>
    /// 加载数据
    /// </summary>
    void BindCategory()
    {
        TreeNode root = new TreeNode("所有分类", "0", GlobalSettings.RelativeWebRoot + "images/default/cat.gif");
        TreeNode tn = null;
        this.tvCategory.Nodes.Add(root);
        List<ProductCategory> categories = ProductCategories.GetChidCategories(0);
        foreach (ProductCategory category in categories)
        {
            tn = new TreeNode(category.CategoryName, category.CategoryID.ToString(), GlobalSettings.RelativeWebRoot + "images/default/cat.gif");
            tn.ToolTip = category.CategoryDesc;
            //tn.CollapseAll();
            LoadChild(tn);
            root.ChildNodes.Add(tn);
            //mbMsg.ShowMsg("请在产品分类树中选择您将要执行操作的分类");
        }

        HttpCookie cache = HHCookie.GetCookie(nodeState + Profile.AccountInfo.UserName);
        int categoryID = 0;
        if (cache != null)
            categoryID = Convert.ToInt32(cache.Value);

        CheckNode(tvCategory.Nodes, categoryID.ToString());
        BindDetail(categoryID);
    }

    /// <summary>
    /// 加载子节点
    /// </summary>
    /// <param name="node"></param>
    void LoadChild(TreeNode node)
    {
        List<ProductCategory> categories = ProductCategories.GetChidCategories(int.Parse(node.Value));
        if (categories != null && categories.Count > 0)
        {
            TreeNode tn = null;
            foreach (ProductCategory c in categories)
            {
                if (c.ParentID == int.Parse(node.Value))
                {
                    tn = new TreeNode(c.CategoryName, c.CategoryID.ToString(), GlobalSettings.RelativeWebRoot + "images/default/cat.gif");
                    tn.ToolTip = c.CategoryDesc;
                    LoadChild(tn);
                    node.ChildNodes.Add(tn);
                }
            }
        }
    }

    /// <summary>
    /// 判断选中节点
    /// </summary>
    /// <param name="nodes"></param>
    /// <param name="value"></param>
    void CheckNode(TreeNodeCollection nodes, string value)
    {
        if (nodes != null && nodes.Count > 0)
        {
            foreach (TreeNode root in nodes)
            {
                if (value == root.Value)
                {
                    root.Select();
                    ExpandParent(root);
                }
                else
                {
                    CheckNode(root.ChildNodes, value);
                }
            }
        }
    }

    /// <summary>
    /// 展开父节点
    /// </summary>
    /// <param name="tn"></param>
    void ExpandParent(TreeNode tn)
    {
        TreeNode node = tn;
        while (node != null && node.Parent != null)
        {
            node = node.Parent;
            if (node != null)
            {
                node.Expand();
            }
        }
    }

    /// <summary>
    /// 绑定详细信息
    /// </summary>
    /// <param name="categoryID"></param>
    void BindDetail(int categoryID)
    {
        BindChildCategory(categoryID);
        BindProperty(categoryID);
    }

    /// <summary>
    /// 绑定子分类
    /// </summary>
    void BindChildCategory(int categoryID)
    {
        rpChildCategory.DataSource = ProductCategories.GetChidCategories(categoryID);
        rpChildCategory.DataBind();
    }
    /// <summary>
    /// 绑定分类属性
    /// </summary>
    void BindProperty(int categoryID)
    {
        rpProperties.DataSource = ProductProperties.GetPropertiesByCategoryID(categoryID);
        rpParentProperties.DataSource = ProductProperties.GetParentPropertiesByCategoryID(categoryID);
        rpProperties.DataBind();
        rpParentProperties.DataBind();
    }
    #endregion

    #region Event
    protected void tvCategory_SelectedNodeChanged(object sender, EventArgs e)
    {
        TreeNode node = this.tvCategory.SelectedNode;
        node.Expand();
        HHCookie.AddCookie(nodeState + Profile.AccountInfo.UserName, node.Value, DateTime.Now.AddMinutes(1));
        BindDetail(Convert.ToInt32(node.Value));
    }
    #endregion

    #region -Override-
    public override void OnPageLoaded()
    {
        base.OnPageLoaded();
        this.PageInfoType = InfoType.IframeInfo;

        AddJavaScriptInclude("scripts/jquery.jmodal.js", false, true);
        AddJavaScriptInclude("scripts/pages/productcategory.aspx.js", false, false);
        
    }
    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "ProductCategoryModule-View";
        e.CheckPermissionControls.Add("ProductCategoryModule-Add", lbAddCategory);
        e.CheckPermissionControls.Add("ProductCategoryModule-Delete", lbDeleteCategory);
        e.CheckPermissionControls.Add("ProductCategoryModule-Edit", lbUpdateCategory);
        base.OnPermissionChecking(e);
    }
    #endregion
}
