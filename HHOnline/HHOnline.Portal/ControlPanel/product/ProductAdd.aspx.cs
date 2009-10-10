using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Shops;

public partial class ControlPanel_product_ProductAdd : HHPage, ICallbackEventHandler
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
            BindTreeView();
            BindDetail();
            if (!string.IsNullOrEmpty(hfTradeList.Value))
            {
                base.ExecuteJs("var tns = $('#tradeNames');var l = tns.next().next();if (l.val() != '') {tns.html(l.val());}", false);
            }
        }


        BindJson();

        if (productID != 0)
        {
            action = OperateType.Edit;
            btnModel.Visible = true;
            this.mvProductAdd.SetActiveView(vwProductDetail);
            if (!IsPostBack)
            {
                BindProduct();
                BindIndustries();
            }
            base.ExecuteJs("var first=false;var uploaded = " + BindPictures() + ";var productId=" + productID + ";", true);
        }
        else
            action = OperateType.Add;

        BindCategory();
    }
    void BindModels()
    {
        gvCurrentModel.DataSource = ProductModels.GetModelsByProductID(productID);
        gvCurrentModel.DataBind();
  
    }
    #region BindJson
    void BindJson()
    {
        string script = Page.ClientScript.GetCallbackEventReference(this, "arg", "refreshBinder", "");
        base.ExecuteJs("function callServer(arg){" + script + "}", false);

        script = Page.ClientScript.GetCallbackEventReference(this, "arg", "deleteAttach", "");
        base.ExecuteJs("function callDeleting(arg){" + script + "}", false);

        script = Page.ClientScript.GetCallbackEventReference(this, "arg", "setDefault", "");
        base.ExecuteJs("function callDefault(arg){" + script + "}", false);

        base.ExecuteJs("var first=true;var uploaded = " + BindAttachment() + ";", true);
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
            return "''";
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
            return "''";
        }
    }
    #endregion

    #region -ICallbackEventHandler Members-

    public string GetCallbackResult()
    {
        return result;
    }
    string result = string.Empty;
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

    #region BindCategoryData
    void BindTreeView()
    {
        this.tvCategories.Nodes.Clear();
        List<ProductCategory> categories = ProductCategories.GetChidCategories(0);
        TreeNode tn = null;

        foreach (ProductCategory pc in categories)
        {
            tn = CreateNode(pc.CategoryName, pc.CategoryID.ToString(), pc.CategoryDesc);
            AddChildCategory(tn, pc);
            this.tvCategories.Nodes.Add(tn);
        }
        this.tvCategories.ExpandAll();
    }

    private void AddChildCategory(TreeNode parent, ProductCategory category)
    {
        foreach (ProductCategory pc in ProductCategories.GetChidCategories(category.CategoryID))
        {
            TreeNode tn = CreateNode(pc.CategoryName, pc.CategoryID.ToString(), pc.CategoryDesc);
            parent.ChildNodes.Add(tn);
            AddChildCategory(tn, pc);
        }
    }

    private bool CheckSelected(TreeNode tn)
    {
        List<ProductCategory> selectedCategories = ProductCategories.GetCategoreisByProductID(productID);
        foreach (ProductCategory category in selectedCategories)
        {
            if (category.CategoryID.ToString() == tn.Value)
                return true;
        }
        return false;
    }

    private TreeNode CreateNode(string text, string value, string description)
    {
        TreeNode tn = new TreeNode(text, value);
        tn.ToolTip = description;
        tn.ShowCheckBox = true;
        if (productID > 0)
            tn.Checked = CheckSelected(tn);
        tn.NavigateUrl = "javascript:void(0)";
        tn.Collapse();
        return tn;
    }
    #endregion

    #region BindProductData
    void BindIndustries()
    {
        List<ProductIndustry> pis = ProductIndustries.GetIndustriesByProductID(productID);
        string ids = string.Empty;
        string ns = string.Empty;
        foreach (ProductIndustry p in pis)
        {
            ids += "[" + p.IndustryID + "]";
            ns += "<li rel='" + p.IndustryID + "' title='" + p.IndustryTitle + "'>" + p.IndustryName +
                        "<a title='删除' href='javascript:void(0)' >&nbsp;</a>" +
                    "</li>";
        }
        hfTrade.Value = ids;
        hfTradeList.Value = ns;
    }
    private void BindProduct()
    {
        BindBand();
        BindProperty();
    }
    private void BindDetail()
    {
        Product product = Products.GetProduct(productID);
        if (product != null)
        {
            txtProductName.Text = product.ProductName;
            txtProductContent.Text = product.ProductContent;
            txtProductAbstract.Text = product.ProductAbstract;
            txtKeyWords.Text = product.ProductKeywords;
            txtDisplayOrder.Text = product.DisplayOrder.ToString();
            csProduct.SelectedValue = product.ProductStatus;
        }
    }

    private void BindCategory()
    {
        LiteralControl lc = new LiteralControl();
        this.phCategoryName.Controls.Add(lc);
        lc.Text = GetCategoryText();
    }

    private void BindBand()
    {
        List<ProductBrand> brands = ProductBrands.GetProductBrands();
        ddlProductBrand.DataSource = brands;
        ddlProductBrand.DataTextField = "BrandName";
        ddlProductBrand.DataValueField = "BrandID";
        ddlProductBrand.DataBind();
        ddlProductBrand.Items.Insert(0, new ListItem("   ", "0"));
        if (productID > 0)
        {
            Product product = Products.GetProduct(productID);
            ListItem item = ddlProductBrand.Items.FindByValue(product.BrandID.ToString());
            if (item != null)
                item.Selected = true;
        }
    }

    private void BindProperty()
    {
        ucProductProperty.CategoryIDList = GetCategoryID();
        ucProductProperty.ProductID = productID;
        if (ucProductProperty.CreateControl())
            rowProductProperty.Visible = true;
        else
            rowProductProperty.Visible = false;
    }

    private string GetCategoryIDList()
    {
        List<string> categoryList = new List<string>();
        foreach (int categoryID in GetCategoryID())
            categoryList.Add(categoryID.ToString());
        return string.Join(",", categoryList.ToArray());
    }

    private List<int> GetCategoryID()
    {
        List<int> categoryIDList = new List<int>();

        TreeNodeCollection collection = this.tvCategories.CheckedNodes;

        foreach (TreeNode node in collection)
        {
            categoryIDList.Add(Convert.ToInt32(node.Value));
        }

        return categoryIDList;
    }

    private string GetIndustryIDList()
    {
        Regex regex = new Regex(@"[(\d)*]", RegexOptions.None);
        List<string> industryIDList = new List<string>();
        string industryID = hfTrade.Value;
        MatchCollection matchs = regex.Matches(industryID);
        foreach (Match match in matchs)
        {
            if (match.Success)
            {
                GroupCollection gc = match.Groups;
                industryIDList.Add(gc[0].Value);
            }
        }
        return string.Join(",", industryIDList.ToArray());
    }
    #endregion

    #region GetCategory
    private string GetCategoryText()
    {
        StringBuilder sb = new StringBuilder("<ul>");
        TreeNodeCollection collection = this.tvCategories.CheckedNodes;
        foreach (TreeNode tn in collection)
        {
            sb.Append(GetCategoryText(tn));
        }
        sb.Append("</ul>");
        ViewState["Category"] = sb.ToString();
        return sb.ToString();
    }

    private string GetCategoryText(TreeNode tn)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(tn.Text);
        while (tn.Parent != null)
        {
            builder.Insert(0, tn.Parent.Text + ">>");
            tn = tn.Parent;
        }
        builder.Insert(0, "<li>");
        builder.Append("</li>");
        return builder.ToString();
    }

    #endregion

    #region Button Event
    void BindToModel()
    {
        mvProductAdd.SetActiveView(vwProductModel);
        BindModels();
    }
    protected void btnSaveModel_Click(object sender, EventArgs e)
    {
        ProductModel pm = new ProductModel();
        pm.CreateTime = DateTime.Now;
        pm.CreateUser = Profile.AccountInfo.UserID;
        pm.ModelCode = txtCode.Text.Trim();
        pm.ModelDesc = txtModelDesc.Text.Trim();
        pm.ModelName = txtModelName.Text.Trim();
        pm.ModelStatus = ComponentStatus.Enabled;
        pm.ProductID = productID;
        pm.UpdateTime = DateTime.Now;
        pm.UpdateUser = Profile.AccountInfo.UserID;
        ProductModels.Create(pm);
        txtModelDesc.Text = string.Empty;
        txtModelName.Text = string.Empty;
        txtCode.Text = string.Empty;
        txtCode.Focus();
        BindToModel();
        }

    protected void gvCurrentModel_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvCurrentModel.EditIndex = -1;
        BindToModel();
    }

    protected void gvCurrentModel_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow gvr = gvCurrentModel.Rows[e.RowIndex];
        int k = int.Parse(gvCurrentModel.DataKeys[e.RowIndex].Value.ToString());
        string name = (gvr.FindControl("txtInnerModelName") as TextBox).Text.Trim();
        string code = (gvr.FindControl("txtInnerModelCode") as TextBox).Text.Trim();
        string desc = (gvr.FindControl("txtInnerDesc") as TextBox).Text.Trim();
        ProductModel pm = ProductModels.GetModel(k);
        pm.ModelName = name;
        pm.ModelCode = code;
        pm.ModelDesc = desc;
        pm.UpdateUser = Profile.AccountInfo.UserID;
        pm.UpdateTime = DateTime.Now;
        ProductModels.Update(pm);
        gvCurrentModel.EditIndex = -1;
        BindToModel();
    }

    protected void gvCurrentModel_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvCurrentModel.EditIndex = e.NewEditIndex;
        BindToModel();
        SetValidator(true, true, 5);
    }

    protected void gvCurrentModel_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ProductModels.Delete(int.Parse(gvCurrentModel.DataKeys[e.RowIndex].Value.ToString()));
        BindToModel();
    }
    protected void btnBackProductInfo_Click(object sender, EventArgs e)
    {
        mvProductAdd.SetActiveView(vwProductDetail);
    }
    protected void btnModel_Click(object sender, EventArgs e)
    {
        BindToModel();
        txtCode.Focus();
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (this.tvCategories.CheckedNodes.Count == 0)
        {
            mbMessage.ShowMsg("请选择产品分类信息！", Color.Red);
            return;
        }
        else
        {
            mbMessage.Visible = false;
            this.mvProductAdd.SetActiveView(vwProductDetail);
            BindProduct();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.mvProductAdd.SetActiveView(vwProductCategoies);
    }
    protected void btnBackToProduct_Click(object sender, EventArgs e)
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Product product = Products.GetProduct(productID);
        if (product == null)
            product = new Product();
        product.BrandID = Convert.ToInt32(ddlProductBrand.SelectedValue);
        product.DisplayOrder = Convert.ToInt32(txtDisplayOrder.Text.Trim());
        product.ProductCode = string.Empty;
        product.ProductAbstract = txtProductAbstract.Text;
        product.ProductContent = txtProductContent.Text;
        product.ProductKeywords = txtKeyWords.Text;
        product.ProductName = txtProductName.Text;
        product.ProductStatus = csProduct.SelectedValue;

        DataActionStatus status;
        if (productID == 0)
        {
            status = Products.Create(product, GetCategoryIDList(), GetIndustryIDList(), ucProductProperty.GetProperties());
            switch (status)
            {
                case DataActionStatus.UnknownFailure:
                    throw new HHException(ExceptionType.Failed, "新增产品失败，请联系管理员！");
                case DataActionStatus.Success:
                default:
                    this.mvProductAdd.SetActiveView(vwProductCategoies);
                    throw new HHException(ExceptionType.Success, "新增产品信息成功，可继续【填写新产品信息】或通过产品管理面板进入【产品编辑】页对此产品进行【型号管理】！");
            }
        }
        else
        {
            status = Products.Update(product, GetCategoryIDList(), GetIndustryIDList(), ucProductProperty.GetProperties());
            switch (status)
            {
                case DataActionStatus.UnknownFailure:
                    throw new HHException(ExceptionType.Failed, "修改产品失败，请联系管理员！");
                case DataActionStatus.Success:
                default:
                    this.mvProductAdd.SetActiveView(vwProductCategoies);
                    throw new HHException(ExceptionType.Success, "修改产品信息成功！");
            }
        }
    }
    #endregion

    #region Override
    public override void OnPageLoaded()
    {
        if (action == OperateType.Add)
            this.ShortTitle = "新增产品";
        else
            this.ShortTitle = "编辑产品";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
        this.PageInfoType = InfoType.PopWinInfo;
        this.AddJavaScriptInclude("scripts/pages/productadd.aspx.js", false, false);
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


}
