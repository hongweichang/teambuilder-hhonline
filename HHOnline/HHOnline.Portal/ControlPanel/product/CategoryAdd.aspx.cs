using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;
using HHOnline.Framework.Web.Enums;
using HHOnline.Shops;

public partial class ControlPanel_product_CategoryAdd : HHPage
{
    private int parentID = 0;
    private int propertyID = 0;
    private int id = 0;
    private OperateType action = OperateType.Add;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            parentID = Convert.ToInt32(Request.QueryString["ParentID"]);
            propertyID = Convert.ToInt32(Request.QueryString["PropertyID"]);
            id = Convert.ToInt32(Request.QueryString["ID"]);
            if (id != 0)
                action = OperateType.Edit;
            else
                action = OperateType.Add;

            if (!IsPostBack && !IsCallback)
            {
                BindData();
            }
        }
        catch (Exception ex)
        {
            base.ExecuteJs("msg('" + ex.Message + "');", false);
        }
    }

    void BindData()
    {
        ProductCategory category = ProductCategories.GeCategory(id);
        ProductCategory parent = null;
        ProductProperty property = null;
        if (category != null)
        {
            btnPost.Text = "修改";
            txtCategoryName.Text = category.CategoryName;
            txtCategoryDesc.Text = category.CategoryDesc;
            txtDisplayOrder.Text = category.DisplayOrder.ToString();
            parent = ProductCategories.GeCategory(category.ParentID);
            property = ProductProperties.GetProperty(category.PropertyID);
        }
        if (parent == null)
            parent = ProductCategories.GeCategory(parentID);
        if (parent != null)
        {
            ltParCategory.Text = parent.CategoryName;
            ltParCategoryDesc.Text = parent.CategoryDesc;
        }
        else
        {
            parentName.Visible = false;
            parentDesc.Visible = false;
        }
        if (property == null)
            property = ProductProperties.GetProperty(propertyID);
        if (property != null)
        {
            ltPropertyName.Text = property.PropertyName;
        }
        else
        {
            propertyName.Visible = false;
        }
    }

    protected void btnPost_Click(object sender, EventArgs e)
    {
        ProductCategory category = null;
        if (action == OperateType.Add)
        {
            category = new ProductCategory();
            category.ParentID = parentID;
            category.PropertyID = propertyID;
        }
        else
        {
            category = ProductCategories.GeCategory(id);
        }
        category.CategoryName = txtCategoryName.Text;
        category.CategoryDesc = txtCategoryDesc.Text;
        category.DisplayOrder = Convert.ToInt32(txtDisplayOrder.Text);
        if (action == OperateType.Add)
        {
            DataActionStatus status = ProductCategories.Create(category);
            switch (status)
            {
                case DataActionStatus.DuplicateName:
                    mbMsg.ShowMsg("新增产品分类失败，存在同名产品分类！");
                    break;
                case DataActionStatus.UnknownFailure:
                    mbMsg.ShowMsg("新增产品分类失败，请联系管理员！");
                    break;
                case DataActionStatus.Success:
                default:
                    base.ExecuteJs("msg('操作成功，已成功增加一个新的产品分类！',true);", false);
                    break;
            }
        }
        else
        {
            DataActionStatus status = ProductCategories.Update(category);
            switch (status)
            {
                case DataActionStatus.DuplicateName:
                    mbMsg.ShowMsg("修改产品分类失败，存在同名产品分类！");
                    break;
                case DataActionStatus.UnknownFailure:
                    mbMsg.ShowMsg("修改产品分类失败，请联系管理员！");
                    break;
                case DataActionStatus.Success:
                default:
                    base.ExecuteJs("msg('操作成功，已成功修改产品分类信息！',true);", false);
                    break;
            }
        }
    }

    public override void OnPageLoaded()
    {
        this.PageInfoType = InfoType.PopWinInfo;
        if (action == OperateType.Add)
            this.ShortTitle = "新增产品分类";
        else
            this.ShortTitle = "修改产品分类";
        base.OnPageLoaded();
        SetValidator(true, true, 5000);
    }
    protected override void OnPagePermissionChecking()
    {
        if (action == OperateType.Add)
            this.PagePermission = "ProductCategoryModule-Add";
        else
            this.PagePermission = "ProductCategoryModule-Edit";
        base.OnPagePermissionChecking();
    }
}
