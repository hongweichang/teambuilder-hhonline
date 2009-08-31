using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Common;
using System.ComponentModel;

namespace HHOnline.Framework
{
    /// <summary>
    /// 缓存关键字管理
    /// </summary>
    public class CacheKeyManager
    {
        #region Prefix
        /// <summary>
        /// 系统设置缓存前缀
        /// </summary>
        [Description("系统设置")]
        public static readonly string FrameworkPrefix = "HHOnline/Framework/";

        /// <summary>
        /// 用户缓存前缀
        /// </summary>
        [Description("用户信息")]
        public static readonly string UserPrefix = "HHOnline/User/";

        /// <summary>
        /// 公司前缀
        /// </summary>
        [Description("公司信息")]
        public static readonly string CompanyPrefix = "HHOnline/Company/";

        /// <summary>
        /// 产品前缀
        /// </summary>
        [Description("产品信息")]
        public static readonly string ProductPrefix = "HHOnline/Product/";

        /// <summary>
        /// 权限前缀
        /// </summary>
        [Description("权限信息")]
        public static readonly string PemissionPrefix = "HHOnline/Permission/";

        /// <summary>
        /// 资讯前缀
        /// </summary>
        [Description("资讯信息")]
        public static readonly string NewsPrefix = "HHOnline/News/";

        /// <summary>
        /// 页面缓存前缀
        /// </summary>
        //[Description("页面缓存")]
        public static readonly string PagePrefix = "HHOnline/WebPage/";
        #endregion

        #region Framework
        /// <summary>
        /// 活动前缀
        /// </summary>
        public static readonly string ActivityPrefix = FrameworkPrefix + "Activity/";

        /// <summary>
        /// 站点地图前缀
        /// </summary>
        //[Description("站点地图")]
        public static readonly string SiteMapPrefix = FrameworkPrefix + "SiteMap/";

        /// <summary>
        /// 站点设置Cachekey
        /// </summary>
        public static readonly string SiteSettingsKey = FrameworkPrefix + "SiteSettings";

        /// <summary>
        /// 站点配置Cachekey
        /// </summary>
        public static readonly string HHConfigurationKey = FrameworkPrefix + "HHConfiguration";

        /// <summary>
        /// 地址重写Cachekey
        /// </summary>
        public static readonly string HHUrlRewriteKey = FrameworkPrefix + "HHUrlRewrite";
        /// <summary>
        /// 菜单配置Cachekey
        /// </summary>
        public static readonly string MenuKey = FrameworkPrefix + "Menus";

        /// <summary>
        /// 语言配置Cachekey
        /// </summary>
        public static readonly string ResourceKey = FrameworkPrefix + "SupportedLanguages";

        /// <summary>
        /// GlobalApplication CacheKey
        /// </summary>
        public static readonly string GlobalApplicationKey = FrameworkPrefix + "GlobalApplication";

        /// <summary>
        /// 禁用名Cachekey
        /// </summary>
        public static readonly string DisallowedNamesKey = FrameworkPrefix + "DisallowedNamesKey";

        public static readonly string AreasKey = FrameworkPrefix + "Areas";

        public static string GetAreaKey(int areaID)
        {
            return FrameworkPrefix + "AreaID-" + areaID.ToString();
        }

        public static string GetAreaKey(string distinctCode)
        {
            return FrameworkPrefix + "Area-DistinctCode" + distinctCode;
        }
        #endregion

        #region User
        /// <summary>
        /// 用户Cachekey
        /// </summary>
        /// <param name="userName">userName</param>
        /// <returns>Cachekey</returns>
        public static string GetUserKey(string userName)
        {
            if (TypeHelper.IsNullOrEmpty(userName))
                throw new ArgumentException("userName");
            return UserPrefix + "UserName-" + userName.ToLower();
        }

        /// <summary>
        /// 用户Cachekey
        /// </summary>
        /// <param name="userID">userID</param>
        /// <returns>Cachekey</returns>
        public static string GetUserKey(int userID)
        {
            return UserPrefix + "UserID-" + userID.ToString();
        }

        public static readonly string UserListPrefix = UserPrefix + "UserList/";

        /// <summary>
        /// UserQuery CacheKey
        /// </summary>
        /// <param name="userQuery"></param>
        /// <returns></returns>
        public static string GetUserQueryKey(UserQuery userQuery)
        {
            return UserListPrefix + string.Format(
                "PI{0}PS{1}SB{2}SO{3}Email{4}DN{5}LN{6}AS{7}IS{8}UT{9}CN{10}ON{11}RID{12}",
                userQuery.PageIndex,
                userQuery.PageSize,
                userQuery.SortBy,
                userQuery.SortOrder,
                userQuery.EmailFilter,
                userQuery.DisplayNameFilter,
                userQuery.LoginNameFilter,
                userQuery.AccountStatus,
                userQuery.InactiveSinceDate.HasValue ? userQuery.InactiveSinceDate.Value.Ticks : GlobalSettings.MinValue.Ticks,
                userQuery.UserType.HasValue ? (int)userQuery.UserType.Value : -1,
                userQuery.CompanyID.HasValue ? userQuery.CompanyID.Value : -1,
                userQuery.OrganizationID.HasValue ? userQuery.OrganizationID.Value : -1,
                userQuery.RoleID.HasValue ? userQuery.RoleID.Value : -1);
        }

        public static readonly string OrganizationKey = UserPrefix + "Organizations";

        public static string GetUserGradeKey(int gradeID)
        {
            return UserPrefix + "UserGrade/GradeID-" + gradeID;
        }

        public static string GetUserGradeKeyByUserID(int userID)
        {
            return GetUserKey(userID) + "/UserGrades";
        }
        #endregion

        #region Company
        /// <summary>
        /// 公司Cachekey
        /// </summary>
        /// <param name="companyName">companyName</param>
        /// <returns>Cachekey</returns>
        public static string GetCompanyKey(string companyName)
        {
            if (TypeHelper.IsNullOrEmpty(companyName))
                throw new ArgumentException("companyName");
            return CompanyPrefix + "CompanyName-" + companyName.ToLower();
        }

        /// <summary>
        /// 公司Cachekey
        /// </summary>
        /// <param name="companyID">companyID</param>
        /// <returns>Cachekey</returns>
        public static string GetCompanyKey(int companyID)
        {
            return CompanyPrefix + "CompanyID-" + companyID.ToString();
        }

        /// <summary>
        /// 资质文件CacheKey
        /// </summary>
        /// <param name="qualificationID">qualificationID</param>
        /// <returns>Cachekey</returns>
        public static string GetQualificationKey(int qualificationID)
        {
            return CompanyPrefix + "QualificationID-" + qualificationID.ToString();
        }

        /// <summary>
        /// 客户级别CacheKey
        /// </summary>
        /// <param name="gradeID"></param>
        /// <returns></returns>
        public static string GetCustomerGradeKey(int gradeID)
        {
            return CompanyPrefix + "CustomerGrade/GradeID-" + gradeID;
        }

        /// <summary>
        /// 客户级别CacheKey
        /// </summary>
        public static string GetCustomerGradeKeyByCompanyID(int companyID)
        {
            return GetCompanyKey(companyID) + "/CustomerGrades";
        }
        #endregion

        #region Tag
        /// <summary>
        /// Tags CacheKey
        /// </summary>
        /// <returns></returns>
        public static string GetTagKey()
        {
            return FrameworkPrefix + "Tags";
        }

        /// <summary>
        /// TagArticle CacheKey
        /// </summary>
        /// <param name="tagArticle"></param>
        /// <returns></returns>
        public static string GetTagArticleKey(int tagArticleID)
        {
            return NewsPrefix + "ArticleID-" + tagArticleID;
        }

        /// <summary>
        /// TagProduct CacheKey
        /// </summary>
        /// <param name="tagProductID"></param>
        /// <returns></returns>
        public static string GetTagProductKey(int tagProductID)
        {
            return ProductPrefix + "Product-ID" + tagProductID;
        }
        #endregion

        #region Activity
        public static readonly string ActivityItemKey = FrameworkPrefix + "ActivityItem";

        public static string GetUserActivityQueryKey(UserActivityQuery userActivityQuery)
        {
            return ActivityPrefix + string.Format(
              "PI{0}PS{1}UID{2}ST{3}ET{4}",
              userActivityQuery.PageIndex,
              userActivityQuery.PageSize,
              userActivityQuery.UserID,
              userActivityQuery.StartTime.Ticks,
              userActivityQuery.EndTime.Ticks);
        }
        #endregion

        #region Favorite
        public static readonly string FavoriteListPrefix = UserPrefix + "Favorites/All/";

        /// <summary>
        /// FavoriteQuery CacheKey
        /// </summary>
        /// <param name="favoriteQuery"></param>
        /// <returns></returns>
        public static string GetFavoriteQueryKey(FavoriteQuery favoriteQuery)
        {
            return GetFavoritePrefix(favoriteQuery) + string.Format("PI{0}PS{1}UID{2}FTP{3}FT{4}FM{5}",
                favoriteQuery.PageIndex,
                favoriteQuery.PageSize,
                favoriteQuery.UserID,
                favoriteQuery.FavoriteType.HasValue ? (Int32)favoriteQuery.FavoriteType.Value : -1,
                favoriteQuery.FavoriteTitleFilter,
                favoriteQuery.FavoriteMemoFilter
                );
        }

        /// <summary>
        /// FavoriteQuery获取缓存头信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetFavoritePrefix(FavoriteQuery favoriteQuery)
        {
            if (favoriteQuery.UserID < 0)
                return FavoriteListPrefix;
            else
                return UserPrefix + string.Format("Favorites/UseID-{0}/", favoriteQuery.UserID);
        }

        /// <summary>
        /// 根据UserID 获取缓存头信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetFavoritePrefix(int userID)
        {
            if (userID < 0)
                return UserPrefix + "Favorites/All/";
            else
                return UserPrefix + string.Format("Favorites/UseID-{0}/", userID);
        }

        /// <summary>
        /// Favorite CacheKey
        /// </summary>
        /// <param name="favoriteID"></param>
        /// <returns></returns>
        public static string GetFavoriteKey(int favoriteID)
        {
            return UserPrefix + "Favorites/FavoriteID-" + favoriteID;
        }
        #endregion

        #region ProductBrand
        public static readonly string ProductBrandKey = ProductPrefix + "ProductBrand/Brands";

        public static string GetProductBrandKey(int brandID)
        {
            return ProductPrefix + "ProductBrand/BrandID-" + brandID.ToString();
        }

        public static string GetBrandKeyByProductID(int productID)
        {
            return GetProductKey(productID) + "/Brands";
        }

        public static readonly string ProductBrandXpath = ProductPrefix + "/Brands";
        #endregion

        #region ProductIndustry
        public static readonly string ProductIndustryKey = ProductPrefix + "ProductIndustry/Industries";

        public static readonly string ProductHierIndustryKey = ProductPrefix + "ProductIndustry/ProductHierIndustry/Industries";

        public static string GetIndustryKeyByProductID(int productID)
        {
            return GetProductKey(productID) + "/Industries";
        }

        public static readonly string ProductIndustryXpath = ProductPrefix + "/Industries";
        #endregion

        #region ProductModel
        public static string GetModelKeyByID(int modelID)
        {
            return ProductPrefix + "ProductModel/ModelID-" + modelID;
        }

        public static string GetModelKeyByProductID(int productID)
        {
            return GetProductKey(productID) + "/Models";
        }
        #endregion

        #region ProductProperty
        public static string GetPropertyKeyByID(int propertyID)
        {
            return ProductPrefix + "ProductProperty/PropertyID-" + propertyID;
        }

        public static string GetPropertyKeyByProductID(int productID)
        {
            return GetProductKey(productID) + "/Properties";
        }

        public static string GetPropertyKeyByCategoryID(int categoryID)
        {
            return GetCategoryKeyByID(categoryID) + "/Properties";
        }

        public static string GetParentPropertyKeyByCategoryID(int categoryID)
        {
            return GetCategoryKeyByID(categoryID) + "/Parent/Properties";
        }

        public static readonly string ProductPropertyXpath = ProductPrefix + "/Properties";
        #endregion

        #region ProductCategory
        public static readonly string ProductCategoryKey = ProductPrefix + "ProductCategory/Categories";

        public static string GetCategoryKeyByPY(string firstLetter)
        {
            return ProductPrefix + "ProductCategory/" + firstLetter + "/Categories";
        }

        public static string GetCategoryKeyByProductID(int productID)
        {
            return GetProductKey(productID) + "/Categories";
        }

        public static readonly string ProductCategoryXpath = ProductPrefix + "/Categories";

        public static string GetCategoryKeyByID(int categoryID)
        {
            return ProductPrefix + "ProductCategory/CategoryID-" + categoryID;
        }
        #endregion

        #region ProductPicture
        public static string GetPictureKey(int pictureID)
        {
            return ProductPrefix + "ProductPicture/PictureID-" + pictureID;
        }

        public static string GetPictureKeyByProductID(int productID)
        {
            return GetProductKey(productID) + "/Pictures";
        }
        #endregion

        #region Product
        public static readonly string ProductListKey = ProductPrefix + "ProductList/";

        public static string GetProductKey(int productID)
        {
            return ProductPrefix + "ProductID-" + productID.ToString();
        }
        #endregion
    }
}
