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

public partial class ControlPanel_product_TradeAdd : HHPage
{
    OperateType action;
    int industryID = 0;
    int parentID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        industryID = Convert.ToInt32(Request.QueryString["ID"]);
        parentID = Convert.ToInt32(Request.QueryString["ParentID"]);
        if (industryID != 0)
            action = OperateType.Edit;
        else
            action = OperateType.Add;

        if (!IsPostBack && !IsCallback)
        {
            BindData();
            if (Request.UrlReferrer != null)
            {
                btnPostBack.PostBackUrl = Request.UrlReferrer.ToString();
                btnPostBack.Visible = true;
            }
            btnPost.Visible = true;
        }
    }

    void BindData()
    {
        ProductIndustry industry = null;
        if (action == OperateType.Add)
        {
            industry = new ProductIndustry();
            industry.ParentID = parentID;
        }
        else
        {
            industry = ProductIndustries.GetProductIndustry(industryID);
            btnPost.Text = "更新";
        }

        if (industry.ParentID == 0)
        {
            parentRow.Visible = false;
        }
        else
        {
            parentRow.Visible = true;
            lblParentName.Text = ProductIndustries.GetProductIndustry(industry.ParentID).IndustryName;
        }

        this.txtIndustryAbstract.Text = industry.IndustryAbstract;
        this.txtIndustryContent.Text = industry.IndustryContent;
        this.txtIndustryName.Text = industry.IndustryName;
        this.txtIndustryTitle.Text = industry.IndustryTitle;
        this.txtDisplayOrder.Text = industry.DisplayOrder.ToString();
        this.csIndustry.SelectedValue = industry.IndustryStatus;
        if (industry.File != null)
            this.imgLogo.ImageUrl = SiteUrlManager.GetResizedImageUrl(industry.File, (int)imgLogo.Width.Value, (int)imgLogo.Height.Value);
        else
            this.imgLogo.ImageUrl = SiteUrlManager.GetNoPictureUrl((int)imgLogo.Width.Value, (int)imgLogo.Height.Value);
    }

    public void btnPost_Click(object sender, EventArgs e)
    {
        ProductIndustry industry = null;
        if (action == OperateType.Add)
        {
            industry = new ProductIndustry();
            industry.ParentID = parentID;
        }
        else
            industry = ProductIndustries.GetProductIndustry(industryID);

        industry.IndustryAbstract = this.txtIndustryAbstract.Text.Trim();
        industry.IndustryContent = this.txtIndustryContent.Text.Trim();
        industry.IndustryName = this.txtIndustryName.Text.Trim();
        industry.IndustryTitle = this.txtIndustryTitle.Text.Trim();
        industry.DisplayOrder = Convert.ToInt32(this.txtDisplayOrder.Text);
        industry.IndustryStatus = this.csIndustry.SelectedValue;
        if (fuLogo.PostedFile != null && fuLogo.PostedFile.ContentLength > 0)
        {
            industry.IndustryLogo = Path.GetFileName(fuLogo.PostedFile.FileName);
        }
        DataActionStatus status;
        if (action == OperateType.Add)
        {
            status = ProductIndustries.Create(industry, fuLogo.PostedFile.InputStream);
            switch (status)
            {
                case DataActionStatus.DuplicateName:
                    mbMessage.ShowMsg("新增行业信息失败，存在同名行业信息！", Color.Red);
                    break;
                case DataActionStatus.UnknownFailure:
                    mbMessage.ShowMsg("新增行业信息失败，请联系管理员！", Color.Red);
                    break;
                case DataActionStatus.Success:
                default:
                    mbMessage.ShowMsg("新增行业信息成功，可继续填写新行业信息，若完成请返回！", Color.Navy);
                    break;
            }
        }
        else
        {
            status = ProductIndustries.Update(industry, fuLogo.PostedFile.InputStream);
            switch (status)
            {
                case DataActionStatus.DuplicateName:
                    mbMessage.ShowMsg("修改行业信息失败，存在同名行业信息！", Color.Red);
                    break;
                case DataActionStatus.UnknownFailure:
                    mbMessage.ShowMsg("修改行业信息失败，请联系管理员！", Color.Red);
                    break;
                case DataActionStatus.Success:
                default:
                    mbMessage.ShowMsg("修改行业信息成功，可继续修改行业信息，若完成请返回！", Color.Navy);
                    break;
            }
        }
        if (status == DataActionStatus.Success)
            BindData();
    }

    public override void OnPageLoaded()
    {
        if (action == OperateType.Add)
            this.ShortTitle = "新增品牌";
        else
            this.ShortTitle = "编辑品牌";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
        this.PageInfoType = InfoType.PopWinInfo;
    }

    protected override void OnPagePermissionChecking()
    {
        if (action == OperateType.Add)
            this.PagePermission = "TradeModule-Add";
        else
            this.PagePermission = "TradeModule-Edit";
        base.OnPagePermissionChecking();
    }
}
