using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;
using HHOnline.Shops;
using System.Drawing;
using System.Text;

public partial class ControlPanel_product_ProductQuickAdd : HHPage, ICallbackEventHandler
{
    OperateType action = OperateType.Add;
    int productID = 0;
    static string _url = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            productID = Convert.ToInt32(Request.QueryString["ID"].Replace("#", ""));
        }
        catch
        {
            productID = 0;
        }
        if (!IsPostBack && !IsCallback)
        {
            if (Request.UrlReferrer != null) _url = Request.UrlReferrer.ToString();
            BindBrands();
        }
        BindJson();
        if (productID != 0)
        {
            action = OperateType.Edit;
            pnlPrice.Visible = false;
            pnlNavigation.Visible = true;
            lnkSetFocus.PostBackUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-productfocusadd&ProductID=" + productID;
            lnkSetPrice.PostBackUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-productprice&ProductID=" + productID;
            if (!IsPostBack)
            {
                BindProductDetails();
                RenderJsonData();
            }
            base.ExecuteJs("var first=false;var uploaded = " + BindPictures() + ";var productId=" + productID + ";", true);
        }
        else
            action = OperateType.Add;

        base.ExecuteJs("var relativeUrl='" + GlobalSettings.RelativeWebRoot + "';", true);
    }

    #region -Events-
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(_url) && _url.ToLower().IndexOf(Request.RawUrl.ToLower()) < 0)
        {
            Response.Redirect(_url);
        }
        else
        {
            Response.Redirect(GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?product-product");
        }
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        string cats = GetCategoryIDList();
        if (string.IsNullOrEmpty(cats)) { mbMsg.ShowMsg("至少应选择一个产品分类！", Color.Red); return; }
        Product product = Products.GetProduct(productID);
        if (product == null)
            product = new Product();
        product.BrandID = Convert.ToInt32(ddlBrands.SelectedValue);
        product.DisplayOrder = 0;
        product.ProductCode = string.Empty;
        product.ProductAbstract = txtAbstract.Text;
        product.ProductContent = txtContent.Text;
        product.ProductKeywords = GlobalSettings.FormatKeywords(txtKeywords.Text);
        product.ProductName = txtProductName.Text.Trim();
        product.ProductStatus = (rdPublish.SelectedValue ? ComponentStatus.Enabled : ComponentStatus.Disabled);

        DataActionStatus status;
        if (productID == 0)
        {
            ProductPrice p = new ProductPrice();
            p.PriceMarket = string.IsNullOrEmpty(txtMarketPrice.Text) ? 0m : decimal.Parse(txtMarketPrice.Text);
            p.PricePromotion = string.IsNullOrEmpty(txtPromotionPrice.Text) ? 0m : decimal.Parse(txtPromotionPrice.Text);
            p.PriceGradeA = string.IsNullOrEmpty(txtPrice5.Text) ? 0m : decimal.Parse(txtPrice5.Text);
            p.PriceGradeB = string.IsNullOrEmpty(txtPrice4.Text) ? 0m : decimal.Parse(txtPrice4.Text); decimal.Parse(txtPrice4.Text);
            p.PriceGradeC = string.IsNullOrEmpty(txtPrice3.Text) ? 0m : decimal.Parse(txtPrice3.Text);
            p.PriceGradeD = string.IsNullOrEmpty(txtPrice2.Text) ? 0m : decimal.Parse(txtPrice2.Text);
            p.PriceGradeE = string.IsNullOrEmpty(txtPrice1.Text) ? 0m : decimal.Parse(txtPrice1.Text);
            status = Products.Create(product, (int)ddlFocusType.SelectedValue, cats, GetIndustryIDList(), p);
            switch (status)
            {
                case DataActionStatus.UnknownFailure:
                    //throw new HHException(ExceptionType.Failed, "新增产品失败，请联系管理员！");
                    mbMsg.ShowMsg("新增产品失败，请联系管理员！", Color.Red);
                    break;
                case DataActionStatus.Success:
                default:
                    mbMsg.ShowMsg("新增产品信息成功，修改部分信息可继续提交，无需其他操作请返回！", Color.Gray);
                    break;
            }
        }
        else
        {
            if (action == OperateType.Edit)
            {
                status = Products.Update(product, cats, GetIndustryIDList(), GetProperties());
                switch (status)
                {
                    case DataActionStatus.UnknownFailure:
                        throw new HHException(ExceptionType.Failed, "修改产品失败，请联系管理员！");
                    case DataActionStatus.Success:
                    default:
                        throw new HHException(ExceptionType.Success, "修改产品信息成功！");
                }
            }
        }
    }
    #endregion

    #region -Methods-
    void BindProductDetails()
    {
        Product p = Products.GetProduct(productID);
        txtProductName.Text = p.ProductName;
        ddlBrands.SelectedValue = p.BrandID.ToString();
        txtKeywords.Text = p.ProductKeywords;
        txtAbstract.Text = p.ProductAbstract;
        txtContent.Text = p.ProductContent;
        rdPublish.SelectedValue = (p.ProductStatus == ComponentStatus.Enabled);
    }
    #region -HTML-
    string catHtml = "<a id=\"catItem_{0}\" href=\"javascript:void(0)\" class=\"cat_list roundbg\"><span><span><span><span>" +
                        "<input type=\"hidden\" name=\"ids\" value=\"{0}\"/>" +
                        "<input type=\"hidden\" name=\"names\" value=\"{1}\"/>{1}" +
                        "<span class=\"close\" title=\"删除\" onclick=\"removeItem({0});\" onmouseover=\"this.className=\'close closeHover_CL\';\" onmouseout=\"this.className=\'close\';\" catId=\"{0}\"></span>" +
                    "</span></span></span></span></a>";
    string indHtml = "<a id=\"indItem_{0}\" href=\"javascript:void(0)\" class=\"cat_list roundbg\"><span><span><span><span>" +
                        "<input type=\"hidden\" name=\"ids\" value=\"{0}\"/>" +
                        "<input type=\"hidden\" name=\"names\" value=\"{1}\"/>{1}" +
                        "<span class=\"close\" title=\"删除\" onclick=\"removeIndItem({0});\" onmouseover=\"this.className=\'close closeHover_CL\';\" onmouseout=\"this.className=\'close\';\" indId=\"{0}\"></span>" +
                    "</span></span></span></span></a>";
    #endregion
    void RenderJsonData()
    {
        List<ProductCategory> pcs = ProductCategories.GetCategoreisByProductID(productID);
        StringBuilder sb1 = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        foreach (ProductCategory pc in pcs)
        {
            sb1.AppendFormat("[{0}]:", pc.CategoryID);
            sb2.AppendFormat(catHtml, pc.CategoryID, pc.CategoryName);
        }
        hfCategories.Value = sb1.ToString();
        hfCatHTML.Value = sb2.ToString();
        sb1 = new StringBuilder();
        sb2 = new StringBuilder();
        List<ProductIndustry> pis = ProductIndustries.GetIndustriesByProductID(productID);
        foreach (ProductIndustry pi in pis)
        {
            sb1.AppendFormat("[{0}]:", pi.IndustryID);
            sb2.AppendFormat(indHtml, pi.IndustryID, pi.IndustryName);
        }
        hfIndustries.Value = sb1.ToString();
        hfIndHTML.Value = sb2.ToString();
    }
    List<ProductProperty> GetProperties()
    {
        return ProductProperties.GetAllPropertyByCategoryIDList(GetCategoryIds());
    }
    List<int> GetCategoryIds()
    {
        if (hfCategories.Value == "") return null;
        string[] ids = hfCategories.Value.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
        if (ids.Length == 0) return null;
        List<int> _ids = new List<int>();
        foreach (string s in ids)
        {
            _ids.Add(int.Parse(s.TrimEnd(']').TrimStart('[')));
        }
        return _ids;
    }
    string GetCategoryIDList()
    {
        if (hfCategories.Value == "") return null;
        string[] ids = hfCategories.Value.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
        if (ids.Length == 0) return null;
        List<string> _ids = new List<string>();
        foreach (string s in ids)
        {
            _ids.Add(s.TrimEnd(']').TrimStart('['));
        }
        return string.Join(",", _ids.ToArray());
    }
    string GetIndustryIDList()
    {
        if (hfIndustries.Value == "") return string.Empty;
        string[] ids = hfIndustries.Value.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
        if (ids.Length == 0) return string.Empty;
        List<string> _ids = new List<string>();
        foreach (string s in ids)
        {
            _ids.Add(s.TrimEnd(']').TrimStart('['));
        }
        return string.Join(",", _ids.ToArray());
    }
    void BindBrands()
    {
        List<ProductBrand> brands = ProductBrands.GetProductBrands();
        ddlBrands.DataSource = brands;
        ddlBrands.DataTextField = "BrandName";
        ddlBrands.DataValueField = "BrandID";
        ddlBrands.DataBind();
        ddlBrands.Items.Insert(0, new ListItem("-无-", "None"));
    }
    #endregion

    #region -BindJson-
    void BindJson()
    {
        string script = Page.ClientScript.GetCallbackEventReference(this, "arg", "refreshBinder", "");
        base.ExecuteJs("function callServer(arg){" + script + "}", false);

        script = Page.ClientScript.GetCallbackEventReference(this, "arg", "deleteAttach", "");
        base.ExecuteJs("function callDeleting(arg){" + script + "}", false);

        script = Page.ClientScript.GetCallbackEventReference(this, "arg", "setDefault", "");
        base.ExecuteJs("function callDefault(arg){" + script + "}", false);

        base.ExecuteJs("var first=true;var uploaded = " + (action == OperateType.Add ? BindAttachment() : BindPictures()) + ";", true);
    }
    string BindAttachment()
    {

        List<TemporaryAttachment> attachments = TemporaryAttachments.GetTemporaryAttachments(Profile.AccountInfo.UserID, AttachmentType.ProductPhoto);
        if (attachments.Count > 0)
        {
            return Newtonsoft.Json.JavaScriptConvert.SerializeObject(attachments);
        }
        else
        {
            return "null";
        }
    }
    string BindPictures()
    {
        List<ProductPicture> pics = ProductPictures.GetPictures(productID);
        if (pics.Count > 0)
        {
            return Newtonsoft.Json.JavaScriptConvert.SerializeObject(pics);
        }
        else
        {
            return "null";
        }
    }
    #endregion

    #region -Override-
    public override void OnPageLoaded()
    {
        this.ShortTitle = "新增产品";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
        SetValidator(true, true, 5);
        this.AddJavaScriptInclude("scripts/pages/productquickadd.aspx.js", false, false);
    }

    protected override void OnPagePermissionChecking()
    {
        if (action == OperateType.Add)
            this.PagePermission = "ProductModule-Add";
        else
            this.PagePermission = "ProductModule-Edit";
        base.OnPagePermissionChecking();
    }
    #endregion

    #region -ICallbackEventHandler Members-
    string result = null;
    public string GetCallbackResult()
    {
        return result;
    }

    public void RaiseCallbackEvent(string eventArgument)
    {
        Guid id;
        TemporaryAttachment ta;
        if (eventArgument == "refreshBinder")
        {
            switch (action)
            {
                case OperateType.Add:
                    result = BindAttachment();
                    break;
                case OperateType.Edit:
                    result = BindPictures();
                    break;
            }
        }
        if (eventArgument.StartsWith("deleteAttach"))
        {
            try
            {
                switch (action)
                {
                    case OperateType.Add:
                        id = new Guid(eventArgument.Split(':')[1]);
                        TemporaryAttachments.Delete(id);
                        break;
                    case OperateType.Edit:
                        ProductPictures.Delete(int.Parse(eventArgument.Split(':')[1]));
                        break;
                }
                result = "0";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
        }
        if (eventArgument.StartsWith("setDefault"))
        {
            try
            {
                switch (action)
                {
                    case OperateType.Add:
                        id = new Guid(eventArgument.Split(':')[1]);
                        ta = TemporaryAttachments.GetTemporaryAttachments(Profile.AccountInfo.UserID, AttachmentType.ProductPhoto)[0];
                        ta.DisplayOrder = 100;
                        TemporaryAttachments.Update(ta);

                        ta = TemporaryAttachments.GetTemporaryAttachment(id);
                        ta.DisplayOrder = 0;
                        TemporaryAttachments.Update(ta);

                        result = BindAttachment();
                        break;
                    case OperateType.Edit:
                        int picId = int.Parse(eventArgument.Split(':')[1]);
                        ProductPicture pics = ProductPictures.GetPictures(productID)[0];
                        pics.DisplayOrder = 100;
                        ProductPictures.Update(pics);

                        pics = ProductPictures.GetPicture(picId);
                        pics.DisplayOrder = 0;
                        ProductPictures.Update(pics);

                        result = BindPictures();
                        break;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
        }
    }

    #endregion
}
