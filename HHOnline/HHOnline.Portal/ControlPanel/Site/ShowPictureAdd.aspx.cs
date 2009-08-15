using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Framework.Web;

public partial class ControlPanel_Site_ShowPictureAdd : HHPage
{
    OperateType action;
    Guid showPictureID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            action = OperateType.Add;
        }
        else
        {
            action = OperateType.Edit;
            showPictureID = new Guid(Request.QueryString["ID"]);
        }
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
        ShowPicture showPicture = null;
        if (action == OperateType.Edit)
        {
            showPicture = ShowPictures.Get(showPictureID);
        }
        else
        {
            showPicture = new ShowPicture();
        }
        this.txtDescription.Text = showPicture.Description;
        this.txtLink.Text = showPicture.Link;
        this.txtTitle.Text = showPicture.Title;
        this.txtDisplayOrder.Text = showPicture.DisplayOrder.ToString();
        if (showPicture.File != null)
            this.imgLogo.ImageUrl = SiteUrlManager.GetResizedImageUrl(showPicture.File, (int)imgLogo.Width.Value, (int)imgLogo.Height.Value);
        else
            this.imgLogo.ImageUrl = SiteUrlManager.GetNoPictureUrl((int)imgLogo.Width.Value, (int)imgLogo.Height.Value);
    }

    public void btnPost_Click(object sender, EventArgs e)
    {
        ShowPicture showPicture = null;
        if (action == OperateType.Add)
        {
            showPicture = new ShowPicture();
        }
        else
        {
            showPicture = ShowPictures.Get(showPictureID);
        }
        showPicture.Description = this.txtDescription.Text;
        showPicture.Link = this.txtLink.Text;
        showPicture.Title = this.txtTitle.Text;
        showPicture.DisplayOrder = Convert.ToInt32(this.txtDisplayOrder.Text);
        if (fuLogo.PostedFile != null && fuLogo.PostedFile.ContentLength > 0)
        {
            showPicture.FileName = Path.GetFileName(fuLogo.PostedFile.FileName);
        }
        if (action == OperateType.Add)
        {
            ShowPictures.Create(showPicture, fuLogo.PostedFile.InputStream);
            mbMessage.ShowMsg("新增展示图片信息成功，可继续填写新展示图片信息，若完成请返回！", Color.Navy);
        }
        else if (action == OperateType.Edit)
        {
            ShowPictures.Update(showPicture, fuLogo.PostedFile.InputStream);
            mbMessage.ShowMsg("修改展示图片信息成功，可继续修改展示图片信息，若完成请返回！", Color.Navy);
        }
        BindData();
    }


    public override void OnPageLoaded()
    {
        if (action == OperateType.Add)
            this.ShortTitle = "新增展示图片";
        else
            this.ShortTitle = "编辑展示图片";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
    }

    protected override void OnPagePermissionChecking()
    {
        //if (action == OperateType.Add)
        //    this.PagePermission = "SiteSettingModule-Add";
        //else
        //    this.PagePermission = "SiteSettingModule-Edit";
        //base.OnPagePermissionChecking();
    }
}
