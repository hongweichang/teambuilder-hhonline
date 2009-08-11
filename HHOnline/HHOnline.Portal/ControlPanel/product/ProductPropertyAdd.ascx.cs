using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using HHOnline.Framework;
using HHOnline.Shops;

public partial class ControlPanel_product_ProductPropertyAdd : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack && this.phProperty.Controls.Count == 0)
        {
            CreatePropertyControl();
        }
    }

    public List<int> CategoryIDList
    {
        get
        {
            List<int> categoryIDList = ViewState["CategoryIDList"] as List<int>;
            if (categoryIDList == null)
                return new List<int>();
            else
                return categoryIDList;
        }
        set
        {
            ViewState["CategoryIDList"] = value;
        }
    }

    public int ProductID
    {
        get
        {
            return (int)(ViewState["ProductID"] ?? 0);
        }
        set
        {
            ViewState["ProductID"] = value;
        }
    }

    public string GenerateID(int propertyID)
    {
        return "Property" + propertyID;
    }

    private bool CreatePropertyControl()
    {
        List<ProductProperty> selectedProperties = ProductProperties.GetPropertiesByProductID(ProductID);

        List<ProductProperty> properties = ProductProperties.GetAllPropertyByCategoryIDList(CategoryIDList);

        if (properties.Count > 0)
        {
            foreach (ProductProperty property in properties)
            {
                Literal ltBegin = new Literal();
                ltBegin.Text = "<li>";
                this.phProperty.Controls.Add(ltBegin);
                //label
                Label lblText = new Label();
                lblText.Text = property.PropertyName;
                this.phProperty.Controls.Add(lblText);

                //textbox
                TextBox txtValue = new TextBox();
                txtValue.ID = GenerateID(property.PropertyID);
                foreach (ProductProperty p in selectedProperties)
                {
                    if (p.PropertyID == property.PropertyID)
                    {
                        txtValue.Text = p.PropertyValue;
                        break;
                    }
                }
                this.phProperty.Controls.Add(txtValue);

                List<ProductCategory> subCategories = ProductCategories.GetCategoriesByPropertyID(property.PropertyID);

                if (subCategories.Count > 0)
                {
                    //ddl
                    DropDownList ddlSubCategory = new DropDownList();
                    ddlSubCategory.Items.Add(new ListItem("请选择", "0"));
                    foreach (ProductCategory category in subCategories)
                    {
                        ddlSubCategory.Items.Add(new ListItem(category.CategoryName, category.CategoryName.ToString()));
                    }
                    ddlSubCategory.Attributes.Add("onchange", "changeProperty(this)");
                    this.phProperty.Controls.Add(ddlSubCategory);
                }
                Literal ltEnd = new Literal();
                ltEnd.Text = "</li>";
                this.phProperty.Controls.Add(ltEnd);
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CreateControl()
    {
        this.phProperty.Controls.Clear();
        return CreatePropertyControl();
    }

    public List<ProductProperty> GetProperties()
    {
        List<ProductProperty> properties = ProductProperties.GetAllPropertyByCategoryIDList(CategoryIDList);
        if (properties.Count > 0)
        {
            foreach (ProductProperty property in properties)
            {
                TextBox txtValue = this.FindControl(GenerateID(property.PropertyID)) as TextBox;
                if (txtValue != null)
                    property.PropertyValue = txtValue.Text.Trim();
            }
        }
        return properties;
    }
}
