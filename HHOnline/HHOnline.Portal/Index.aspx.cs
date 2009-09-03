using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Framework.Web;
using HHOnline.Cache;
using HHOnline.Permission.Components;
using HHOnline.Permission.Services;
using System.Text.RegularExpressions;
using HHOnline.Shops;
using HHOnline.News.Components;
using HHOnline.News.Services;

public partial class Index : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Bind();

            //lblRes.Text = GlobalSettings.CompressCss.ToString();
            //txtSiteName.Text = GlobalSettings.Decrypt("ZGibuGFjjFKUDnANElA8RA==");
        }
    }

    public override void OnPageLoaded()
    {
        this.AddJavaScriptInclude("scripts/jquery.datepick.js", false, true);
        this.ShortTitle = "HomePage";
        base.OnPageLoaded();
    }

    protected void Bind()
    {
        //lblRes.Text = ResourceManager.GetString("AddAComment");
        SiteSettings settings = SiteSettingsManager.GetSiteSettings();
        txtSiteName.Text = settings.SiteName;
        txtSmtpServer.Text = settings.SmtpServer;
        txtUserName.Text = settings.SmtpServerUserName;
        txtPwd.Text = settings.SmtpServerPassword;

        DropDownList1.DataSource = StrategyFactory.GetStrategies();
        DropDownList1.DataTextField = "Text";
        DropDownList1.DataValueField = "Name";
        DropDownList1.DataBind();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        SiteSettings settings = SiteSettingsManager.GetSiteSettings();
        settings.SiteName = txtSiteName.Text;
        settings.SmtpServer = txtSmtpServer.Text;
        settings.SmtpServerUserName = txtUserName.Text;
        settings.SmtpServerPassword = txtPwd.Text;
        SiteSettingsManager.Save(settings);
        lblTip.Text = "Save Success！";
    }



    protected void Button2_Click(object sender, EventArgs e)
    {
        User user = new User();
        user.UserName = "believe3303";
        user.UserType = UserType.InnerUser;
        user.Title = "CEO";
        user.Password = "12345";
        Company company = new Company();
        company.CompanyName = "CompanyName";
        company.CompanyStatus = CompanyStatus.Authenticated;
        company.Address = "Address12";
        company.CompanyRegion = 1;

        CreateUserStatus status = Users.Create(user, company);
        switch (status)
        {
            case CreateUserStatus.DisallowedUsername:
                lblTip.Text = "DisallowedUserName";
                break;
            case CreateUserStatus.DuplicateUserName:
                lblTip.Text = "DuplicateUserName";
                break;
            case CreateUserStatus.Success:
                lblTip.Text = "Success";
                break;
            default:
                lblTip.Text = "Success";
                break;
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        LoginUserStatus status = Users.ValidateUser(txtUserName.Text, txtPwd.Text);
        switch (status)
        {
            case LoginUserStatus.Success:
                lblTip.Text = "Login Success";
                break;
            case LoginUserStatus.InvalidCredentials:
                lblTip.Text = "Pwd Error";
                break;
            default:
                lblTip.Text = "Banned";
                break;
        }
    }

    protected void txtPwd_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        if (Users.ChangePassword(txtUserName.Text, txtPwd.Text, txtNewPwd.Text))
        {
            lblTip.Text = "Change Sucess";
        }
        else
        {
            lblTip.Text = "Change Fail";
        }

    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        User user = Users.GetUser("believe3301");
        user.Title = "UserTitle";
        user.Remark = DateTime.Now.ToString();
        if (Users.UpdateUser(user))
            lblTip.Text = "Update Sucess";
        else
            lblTip.Text = "Update Fail";
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        if (Users.DeleteUser("believe3301"))
        {
            lblTip.Text = "Delete Sucess";
        }
        else
        {
            lblTip.Text = "Delete Fail";
        }
    }

    protected void Button7_Click(object sender, EventArgs e)
    {
        //UserQuery userQuery = new UserQuery();
        //userQuery.SortOrder = SortOrder.Ascending;
        //userQuery.SortBy = SortUsersBy.DisplayName;
        List<User> lstUsers = Users.GetUsers();
        foreach (User user in lstUsers)
            lblTip.Text += user.UserName + ",";
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        List<string> lstNames = Users.GetInactiveUsers(System.Web.Profile.ProfileAuthenticationOption.All, new DateTime(2009, 11, 2));
        foreach (string user in lstNames)
            lblTip.Text += user + ",";
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        CompanyQualification qualification = new CompanyQualification();
        qualification.CompanyID = 1;
        qualification.QualificationDesc = "Qualification Desc";
        qualification.QualificationName = System.IO.Path.GetFileName(FileUpload1.FileName);

        CompanyQualifications.AddFile(qualification, FileUpload1.PostedFile.InputStream);
    }

    protected void Button10_Click(object sender, EventArgs e)
    {
        Company company = new Company();
        company.CompanyName = "CompanyName";
        company.CompanyType = CompanyType.Agent;
        company.CompanyStatus = CompanyStatus.Authenticated;
        if (Companys.Create(company) == CreateCompanyStatus.Success)
            lblTip.Text = "Create Success:CompanyID =" + company.CompanyID;
        else
            lblTip.Text = "Create Fail";
    }

    protected void Button11_Click(object sender, EventArgs e)
    {
        Company company = Companys.GetCompany("CompanyName");
        company.Remark = DateTime.Now.ToString();
        if (Companys.UpdateCompany(company))
            lblTip.Text = "Update Success";
        else
            lblTip.Text = "Update Fail";
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        if (Companys.DeleteCompany("CompanyName"))
            lblTip.Text = "Delete success";
        else
            lblTip.Text = "Delete Fail";
    }

    protected void Button13_Click(object sender, EventArgs e)
    {
        Company company = Companys.GetCompany("CompanyName");
        if (company != null)
            lblTip.Text = company.CreateTime.ToString();
    }
    protected void Button14_Click(object sender, EventArgs e)
    {
        CompanyQualification qualification = CompanyQualifications.GetCompanyQualification(6);
        HyperLink1.NavigateUrl = qualification.Url;
    }
    protected void Button15_Click(object sender, EventArgs e)
    {
        List<CompanyQualification> lstQualification = CompanyQualifications.GetCompanyQualifications(1);
        foreach (CompanyQualification qualification in lstQualification)
        {
            HyperLink hp = new HyperLink();
            hp.NavigateUrl = qualification.Url;
            hp.Text = qualification.QualificationName;
            phLinks.Controls.Add(hp);
        }
    }
    protected void Button16_Click(object sender, EventArgs e)
    {
        throw new ArgumentNullException("Argument Banned");
    }
    protected void Button17_Click(object sender, EventArgs e)
    {
        this.lblTips.Text = GlobalSettings.FormatTags(Tags.GetTags());
    }
    protected void Button18_Click(object sender, EventArgs e)
    {
        this.lblTips.Text = GlobalSettings.FormatTags(Tags.GetTagsByArticle(4));
    }


    protected void Button19_Click(object sender, EventArgs e)
    {
        Tags.UpdateTagArticle(4, txtSmtpServer.Text);
    }
    protected void Button20_Click(object sender, EventArgs e)
    {
        //List<Action> actions = PermissionManager.LoadAllActions();
        //lblTip.Text = actions.Count.ToString();
        //throw new HHException(ExceptionType.Success, "你已经成功的载入了首页信息！");

        List<string> result = new List<string>();
        string sql = "ProductID=1 and RegionName='and' and Status=0 and Name like '%tiger%'";

        string pattern = "(\\w+)\\s*(=|like|is)\\s*('?%?\\w+%?'?)";
        Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        MatchCollection matchs = regex.Matches(sql);
        foreach (Match match in matchs)
        {
            if (match.Success)
            {
                GroupCollection gc = match.Groups;
                //gc[1] gc[3]
                foreach (Group g in gc)
                {
                    result.Add(g.Value);
                }
            }
        }

        Response.Write("");
        //if (Regex.IsMatch(sql, pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace))
        //{
        //    MatchCollection mc = Regex.Matches(sql, pattern, RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
        //    List<string> result = new List<string>();
        //    foreach (Match m in mc)
        //    {
        //        result.Add(m.Value);
        //    }
        //}
    }

    int i = 0;

    protected void Button21_Click(object sender, EventArgs e)
    {
        i = Convert.ToInt32(ViewState["Key"]);
        HHCache.Instance.Insert(
            CacheKeyManager.GetUserKey(i), DateTime.Now.ToString());
        ViewState["Key"] = ++i;
        lblCacheTip.Text = "Success";
    }
    protected void Button22_Click(object sender, EventArgs e)
    {
        lblCacheTip.Text = HHCache.Instance.GetCount(CacheKeyManager.UserPrefix).ToString();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        HHOnline.Cache.HHCache.Instance.Remove(CacheKeyManager.UserPrefix);
        lblTip.Text = "Clear Success！";
    }
    protected void Button23_Click(object sender, EventArgs e)
    {
        Organization org = new Organization();
        org.OrganizationName = txtSmtpServer.Text;
        Organizations.CreateOrganization(org);
    }
    protected void Button25_Click(object sender, EventArgs e)
    {
        foreach (Organization org in Organizations.GetAllOrganizations())
            Label2.Text += org.OrganizationName + ";";

    }
    protected void Button26_Click(object sender, EventArgs e)
    {
        Label3.Text = "";
        Area area = Areas.GetArea(6);
        Label3.Text = area.RegionName;
    }
    protected void Button27_Click(object sender, EventArgs e)
    {
        Label3.Text = "";
        List<Area> child = Areas.GetChildAreas(1);
        foreach (Area area in child)
            Label3.Text += area.RegionName + ";";
    }


    protected void Button28_Click(object sender, EventArgs e)
    {
        Label3.Text = "";
        List<Area> child = Areas.GetAllChildAreas(504);
        foreach (Area area in child)
            Label3.Text += area.RegionName + ";";
    }
    protected void Button29_Click(object sender, EventArgs e)
    {
        BaseViews views = ViewsFactory.GetViews(typeof(HHOnline.News.ArticleViews));
        views.AddViewCount(4);
    }
    protected void Button30_Click(object sender, EventArgs e)
    {
        StrategySet set = new StrategySet();

        IGradeStrategy strategy = StrategyFactory.GetGradeStrategy(this.DropDownList1.SelectedValue);

        strategy.Value = this.DropDownList2.SelectedValue;


        this.Label4.Text = strategy.BuildQuery();

        CustomerGrade cg = new CustomerGrade();
        cg.CompanyID = 2;
        cg.GradeLevel = UserLevel.D;
        cg.GradeLimit = strategy.BuildQuery();
        CustomerGradeManager.Create(cg);

    }
    protected void Button31_Click(object sender, EventArgs e)
    {
        StrategySet set = new StrategySet();
        set.ReFill(@"RegionID = BeiJING AND IndustryID = JSJ AND BrandID = 'AMD' ");
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.DropDownList1.SelectedIndex == 4)
        {
            DropDownList2.DataSource = StrategyFactory.GetGradeStrategy(this.DropDownList1.SelectedValue).GetValueRange();
            DropDownList2.DataTextField = "Text";
            DropDownList2.DataValueField = "Name";
            DropDownList2.DataBind();

        }
    }
    protected void Button32_Click(object sender, EventArgs e)
    {
        ProductBrand brand = new ProductBrand();
        brand.BrandLogo = System.IO.Path.GetFileName(FileUpload2.FileName);
        brand.BrandTitle = "AND MS";
        brand.DisplayOrder = 1;
        brand.BrandContent = "品牌";
        brand.BrandName = "AND SM";
        ProductBrands.Create(brand, FileUpload2.FileContent);
    }
    protected void Button33_Click(object sender, EventArgs e)
    {
        ProductBrand brand = ProductBrands.GetProductBrand(10);
        Image1.ImageUrl = SiteUrlManager.GetResizedImageUrl(brand.File, 80, 80);
        HyperLink2.NavigateUrl = brand.Url;
        HyperLink2.Text = brand.BrandLogo;
    }
    protected void Button34_Click(object sender, EventArgs e)
    {
        ProductIndustry industry = new ProductIndustry();
        //industry.IndustryLogo
    }
    protected void Button36_Click(object sender, EventArgs e)
    {
        Users.GetUser(200);
        CacheManager.GetCacheKeyDom();
    }
    protected void Button37_Click(object sender, EventArgs e)
    {
        List<ArticleCategory> categories = ArticleManager.GetAllCategories();
    }
    protected void Button38_Click(object sender, EventArgs e)
    {
        List<Article> articles = ArticleManager.GetAllArticles();
    }
    protected void Button39_Click(object sender, EventArgs e)
    {
        List<Area> lstParentArea = Areas.GetParentArea(16);
        foreach (Area area in lstParentArea)
            lblCacheTip.Text += area.RegionID + ",";
    }
    protected void Button40_Click(object sender, EventArgs e)
    {

    }
    protected void Button41_Click(object sender, EventArgs e)
    {
        decimal? price = ProductPrices.GetPriceMarket(10, 23);
        lblCacheTip.Text = price.HasValue ? price.Value.ToString() : "询价";
        price = ProductPrices.GetPriceMember(10, 23);
        Label5.Text = price.HasValue ? price.Value.ToString() : "询价";
    }
}
