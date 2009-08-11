using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Shops;
using HHOnline.Framework.Web;

public partial class ControlPanel_Product_Trade : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            BindData();
            BindLinkButton();
        }
    }

    public override void OnPageLoaded()
    {
        this.PagePermission = "TradeModule-View";
        this.ShortTitle = "行业管理";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
    }

    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "TradeModule-View";
        e.CheckPermissionControls.Add("TradeModule-Add", lbNewIndustry);
        base.OnPermissionChecking(e);
    }


    void BindData()
    {
        this.egvIndustries.DataSource = ProductIndustries.GetHierarchyIndustries();
        this.egvIndustries.DataBind();
    }

    void BindLinkButton()
    {
        lbNewIndustry.PostBackUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-tradeadd";
    }

    protected void egvIndustries_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ProductIndustry industry = e.Row.DataItem as ProductIndustry;
            Image industryLogo = e.Row.FindControl("IndustryLogo") as Image;
            if (industryLogo != null)
            {
                if (industry.File != null)
                    industryLogo.ImageUrl = SiteUrlManager.GetResizedImageUrl(industry.File, (int)industryLogo.Width.Value, (int)industryLogo.Height.Value);
                else
                    industryLogo.ImageUrl = SiteUrlManager.GetNoPictureUrl((int)industryLogo.Width.Value, (int)industryLogo.Height.Value);
            }
            HyperLink hyName = e.Row.FindControl("hlIndustryName") as HyperLink;
            if (hyName != null)
            {
                hyName.Text = industry.IndustryName;
                hyName.NavigateUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-product&ii=" + industry.IndustryID;
            }
        }
    }

    protected void egvIndustries_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int industryID = (int)egvIndustries.DataKeys[e.RowIndex].Value;
        DataActionStatus status = ProductIndustries.Delete(industryID);
        switch (status)
        {
            case DataActionStatus.RelationshipExist:
                throw new HHException(ExceptionType.Failed, "此行业信息下存在关联商品，无法直接删除(请先删除此行业信息下关联商品)！");
            case DataActionStatus.ChildExist:
                throw new HHException(ExceptionType.Failed, "此行业信息下存在子行业信息，无法直接删除(请先删除子行业信息)");
            case DataActionStatus.UnknownFailure:
                throw new HHException(ExceptionType.Failed, "删除行业信息失败，请确认此行业信息存在，并状态正常！");
            default:
            case DataActionStatus.Success:
                BindData();
                break;
        }
    }

    protected void egvIndustries_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Response.Redirect(GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-tradeadd&ID=" + egvIndustries.DataKeys[e.RowIndex].Value);
    }

    protected void egvIndustries_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        egvIndustries.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void egvIndustries_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddChild")
        {
            GridViewRow row = ((LinkButton)e.CommandSource).Parent.Parent.Parent.Parent as GridViewRow;
            if (row != null)
            {
                int index = row.RowIndex;
                Response.Redirect(GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-tradeadd&ParentID=" + egvIndustries.DataKeys[index].Value);
            }
        }
    }
}
