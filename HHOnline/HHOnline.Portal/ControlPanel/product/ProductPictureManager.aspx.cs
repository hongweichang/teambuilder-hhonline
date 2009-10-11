using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Framework;

public partial class ControlPanel_product_ProductPictureManager : HHPage
{
    private string cookieName = CacheKeyManager.PagePrefix + "TemporaryAttachment/";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override void OnPageLoaded()
    {
        base.ExecuteJs("var flashSrc='" + GlobalSettings.RelativeWebRoot + "images/flash/jerichoupload.swf'", true);
        this.PageInfoType = InfoType.IframeInfo;
        User u = Profile.AccountInfo;
        if (u.UserType != UserType.CompanyUser ||
           (u.Company.CompanyType == CompanyType.Ordinary ||
           (u.Company.CompanyType == (CompanyType.Ordinary | CompanyType.Agent)))
           || u.IsManager != 1)
        {
            this.PagePermission = "ProductModule-Add";
        }
        this.ShortTitle = "图片上传";
        this.AddJavaScriptInclude("scripts/jquery.flash.js", false, false);
        Page.Form.Enctype = "multipart/form-data";
    }

}
