using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Framework;

public partial class Pages_Profiles_AddFav : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.Identity.IsAuthenticated)
        {
            throw new HHException(ExceptionType.NoMasterError, "只有登录用户才能进行收藏操作！");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string t = Request.QueryString["type"];
            int id = int.Parse(Request.QueryString["id"]);
            FavoriteType ft = FavoriteType.Outside;
            if (t == "p")
                ft = FavoriteType.Product;
            else if (t == "n")
                ft = FavoriteType.Article;
            else
                throw new HHException(ExceptionType.NoMasterError, "待收藏链接类型错误！");
            Favorite f = new Favorite();
            f.CreateTime = DateTime.Now;
            f.FavoriteLevel = int.Parse(hfRate.Value);
            f.FavoriteMemo = txtDesc.Text.Trim();
            f.FavoriteTitle = txtTitle.Text.Trim();
            f.FavoriteType = ft;
            f.FavoriteUrl = string.Empty;
            f.RelatedID = id;
            f.UpdateTime = DateTime.Now;
            f.UserID = Profile.AccountInfo.UserID;
            if (Favorites.AddFavorite(f))
                base.ExecuteJs("msg('成功收藏此产品/资讯信息！')", false);
            else
                base.ExecuteJs("msg('收藏对象无法保存，请联系管理员！')", false);

        }
        catch (Exception ex)
        {
            throw new HHException(ExceptionType.NoMasterError, ex.Message); 
        }
    }
    public override void OnPageLoaded()
    {

        this.PageInfoType = InfoType.IframeInfo;
        SetValidator(true, true, 5000);
        this.AddJavaScriptInclude("scripts/jquery.rating.js", false, false);
        this.AddJavaScriptInclude("scripts/pages/addfav.aspx.js", false, false);
    }
}
