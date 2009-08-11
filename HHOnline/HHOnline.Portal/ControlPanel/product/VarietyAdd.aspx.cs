using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Framework;
using HHOnline.Shops;

public partial class ControlPanel_product_VarietyAdd : HHPage
{
    int brandID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        brandID = Convert.ToInt32(Request.QueryString["ID"]);
        if (!IsPostBack && !IsCallback)
        {
            BindGroupList();

            BindData();
            if (Request.UrlReferrer != null)
            {
                btnPostBack.PostBackUrl = Request.UrlReferrer.ToString();
                btnPostBack.Visible = true;
            }
            btnPost.Visible = true;
        }
    }

    void BindGroupList()
    {
        List<string> groups = ProductBrands.GetBrandGroup();
        ddlBrandGroup.Items.Clear();
        ddlBrandGroup.Items.Add(new ListItem("新建分组"));
        newGroupRow.Style.Clear();
        groupSelectRow.Style.Clear();
        if (groups == null || groups.Count == 0)
        {
            //newGroupRow.Style.Clear();
            //groupSelectRow.Style.Clear();
            groupSelectRow.Style.Add("visibility", "hidden");
            groupSelectRow.Style.Add("position", "absolute");
        }
        else
        {
            newGroupRow.Style.Add("visibility", "hidden");
            newGroupRow.Style.Add("position", "absolute");
            foreach (string group in groups)
            {
                ddlBrandGroup.Items.Add(new ListItem(group));
                if (ddlBrandGroup.Items.Count == 2)
                {
                    ddlBrandGroup.SelectedItem.Selected = false;
                    ddlBrandGroup.Items[1].Selected = true;
                }
            }
        }
    }

    void BindData()
    {
        if (brandID != 0)
        {
            btnPost.Text = "更新";
        }
        ProductBrand brand = ProductBrands.GetProductBrand(brandID);
        if (brand == null)
            brand = new ProductBrand();

        this.txtBrandAbstract.Text = brand.BrandAbstract;
        this.txtBrandContent.Text = brand.BrandContent;
        if (!string.IsNullOrEmpty(brand.BrandGroup))
        {
            ddlBrandGroup.SelectedItem.Selected = false;
            ddlBrandGroup.Items.FindByText(brand.BrandGroup).Selected = true;
        }
        this.txtBrandName.Text = brand.BrandName;
        this.txtBrandTitle.Text = brand.BrandTitle;
        this.txtDisplayOrder.Text = brand.DisplayOrder.ToString();
        csBrand.SelectedValue = brand.BrandStatus;
        if (brand.File != null)
            this.imgLogo.ImageUrl = SiteUrlManager.GetResizedImageUrl(brand.File, (int)imgLogo.Width.Value, (int)imgLogo.Height.Value);
        else
            this.imgLogo.ImageUrl = SiteUrlManager.GetNoPictureUrl((int)imgLogo.Width.Value, (int)imgLogo.Height.Value);
    }

    public void btnPost_Click(object sender, EventArgs e)
    {
        ProductBrand brand = null;
        if (brandID == 0)
        {
            brand = new ProductBrand();
        }
        else
        {
            brand = ProductBrands.GetProductBrand(brandID);
        }
        brand.BrandAbstract = this.txtBrandAbstract.Text;
        brand.BrandContent = this.txtBrandContent.Text;
        if (ddlBrandGroup.SelectedIndex == 0)
            brand.BrandGroup = this.txtBrandGroup.Text;
        else
            brand.BrandGroup = this.ddlBrandGroup.Text;
        brand.BrandName = this.txtBrandName.Text;
        brand.BrandTitle = this.txtBrandTitle.Text;
        brand.DisplayOrder = Convert.ToInt32(this.txtDisplayOrder.Text);
        brand.BrandStatus = csBrand.SelectedValue;
        if (fuLogo.PostedFile != null && fuLogo.PostedFile.ContentLength > 0)
        {
            brand.BrandLogo = Path.GetFileName(fuLogo.PostedFile.FileName);
        }
        DataActionStatus status;
        if (brandID == 0)
        {
            status = ProductBrands.Create(brand, fuLogo.PostedFile.InputStream);

            switch (status)
            {
                case DataActionStatus.DuplicateName:
                    mbMessage.ShowMsg("新增产品品牌失败，存在同名产品品牌！", Color.Red);
                    break;
                case DataActionStatus.UnknownFailure:
                    mbMessage.ShowMsg("新增产品品牌失败，请联系管理员！", Color.Red);
                    break;
                case DataActionStatus.Success:
                default:
                    mbMessage.ShowMsg("新增产品品牌成功，可继续填写新品牌信息，若完成请返回！", Color.Navy);
                    break;
            }

        }
        else
        {
            status = ProductBrands.Update(brand, fuLogo.PostedFile.InputStream);

            switch (status)
            {
                case DataActionStatus.DuplicateName:
                    mbMessage.ShowMsg("修改产品品牌失败，存在同名产品品牌！", Color.Red);
                    break;
                case DataActionStatus.UnknownFailure:
                    mbMessage.ShowMsg("修改产品品牌失败，请联系管理员！", Color.Red);
                    break;
                case DataActionStatus.Success:
                default:
                    mbMessage.ShowMsg("修改产品品牌成功，可继续修改品牌信息，若完成请返回！", Color.Navy);
                    break;
            }
            //if (status == DataActionStatus.Success)
            //{
            //    BindGroupList();
            //    BindData();
            //}
        }
        if (status == DataActionStatus.Success)
        {
            BindGroupList();
            BindData();
        }
    }

    public override void OnPageLoaded()
    {
        if (brandID == 0)
            this.ShortTitle = "新增品牌";
        else
            this.ShortTitle = "编辑品牌";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
        this.PageInfoType = InfoType.PopWinInfo;
    }

    protected override void OnPagePermissionChecking()
    {
        if (brandID == 0)
            this.PagePermission = "VarietyModule-Add";
        else
            this.PagePermission = "VarietyModule-Edit";
        base.OnPagePermissionChecking();
    }
}
