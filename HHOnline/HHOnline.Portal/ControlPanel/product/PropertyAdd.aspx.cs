using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;
using HHOnline.Framework.Web.Enums;
using HHOnline.Shops;

public partial class ControlPanel_product_PropertyAdd : HHPage
{
    private int categoryID = 0;
    private int id = 0;
    private OperateType action = OperateType.Add;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            categoryID = Convert.ToInt32(Request.QueryString["CategoryID"]);
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
        ProductProperty property = ProductProperties.GetProperty(id);

        ProductCategory category = null;
        if (property != null)
        {
            btnPost.Text = "修改";
            this.txtPropertyDesc.Text = property.PropertyDesc;
            this.txtPropertyName.Text = property.PropertyName;
            this.txtDisplayOrder.Text = property.DisplayOrder.ToString();
            this.scHidden.SelectedValue = property.SubCategoryHidden;
            category = ProductCategories.GetCategory(property.CategoryID);
        }
        if (category == null)
            category = ProductCategories.GetCategory(categoryID);

        if (category != null)
        {
            ltParCategory.Text = category.CategoryName;
            ltParCategoryDesc.Text = category.CategoryDesc;
        }
        else
        {
            parentName.Visible = false;
            parentDesc.Visible = false;
        }
    }

    protected void btnPost_Click(object sender, EventArgs e)
    {
        ProductProperty property = null;
        if (action == OperateType.Add)
        {
            property = new ProductProperty();
            property.CategoryID = categoryID;
        }
        else
        {
            property = ProductProperties.GetProperty(id);
        }
        property.PropertyName = this.txtPropertyName.Text;
        property.PropertyDesc = this.txtPropertyDesc.Text;
        property.SubCategoryHidden = this.scHidden.SelectedValue;
        property.DisplayOrder = Convert.ToInt32(txtDisplayOrder.Text);
        if (action == OperateType.Add)
        {
            DataActionStatus status = ProductProperties.Create(property);
            switch (status)
            {
                case DataActionStatus.DuplicateName:
                    mbMsg.ShowMsg("新增产品分类属性失败，在该分类下存在同名产品分类属性！");
                    break;
                case DataActionStatus.UnknownFailure:
                    mbMsg.ShowMsg("新增产品分类属性失败，请联系管理员！");
                    break;
                case DataActionStatus.Success:
                default:
                    base.ExecuteJs("msg('操作成功，已成功增加一个新的产品分类属性！',true);", false);
                    break;
            }
        }
        else
        {
            DataActionStatus status = ProductProperties.Update(property);
            switch (status)
            {
                case DataActionStatus.DuplicateName:
                    mbMsg.ShowMsg("修改产品分类属性失败，在该分类下存在同名产品分类属性！");
                    break;
                case DataActionStatus.UnknownFailure:
                    mbMsg.ShowMsg("修改产品分类属性失败，请联系管理员！");
                    break;
                case DataActionStatus.Success:
                default:
                    base.ExecuteJs("msg('操作成功，已成功修改产品分类属性！',true);", false);
                    break;
            }
        }
    }

    public override void OnPageLoaded()
    {
        this.PageInfoType = InfoType.PopWinInfo;
        if (action == OperateType.Add)
            this.ShortTitle = "新增分类属性";
        else
            this.ShortTitle = "修改分类属性";

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
