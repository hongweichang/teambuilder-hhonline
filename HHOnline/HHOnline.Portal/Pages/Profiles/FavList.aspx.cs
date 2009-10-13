using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class Pages_Profiles_FavList : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindFavs();
    }
    void BindFavs()
    {
        FavoriteQuery fq = new FavoriteQuery();
        fq.PageSize = int.MaxValue;
        fq.UserID = Profile.AccountInfo.UserID;
        fq.FavoriteTitleFilter = txtName.Text.Trim();
        if(ddlType.SelectedIndex!=0)
            fq.FavoriteType = (FavoriteType)int.Parse(ddlType.SelectedValue);
        List<Favorite> favs = Favorites.GetFavorites(fq).Records;
        egvFavs.DataSource = favs;
        egvFavs.DataBind();
    }
    public string GetFavoriteType(object favType)
    {
        FavoriteType ft = (FavoriteType)favType;
        switch (ft)
        {
            case FavoriteType.Product:
                return "产品";
            case FavoriteType.Article:
                return "资讯";
                break;
            case FavoriteType.Outside:
                return "外部链接";
                break;
        }
        return "未知";
    }

    protected void egvFavs_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Favorites.DeleteFavorite(int.Parse(egvFavs.DataKeys[e.RowIndex].Value.ToString()));
        BindFavs();
    }
    public string GetView(object pid, object favType)
    {
        string id = GlobalSettings.Encrypt(pid.ToString());
        FavoriteType ft = (FavoriteType)favType;
        switch (ft)
        {
            case FavoriteType.Product:
                return GlobalSettings.RelativeWebRoot + "pages/view.aspx?product-product&ID=" +id;
            case FavoriteType.Article:
                return GlobalSettings.RelativeWebRoot + "pages/view.aspx?news-newsdetail&id=" + id;
                break;
        }
        return "#";
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindFavs();
    }
    protected void egvFavs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        egvFavs.PageIndex = e.NewPageIndex;
        BindFavs();
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "收藏夹";
        this.SetTabName(this.ShortTitle);
        this.SetTitle();
        this.AddJavaScriptInclude("scripts/pages/favlist.aspx.js", false, false);
    }
}
