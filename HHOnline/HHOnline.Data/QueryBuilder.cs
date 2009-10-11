using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Framework;
using HHOnline.QueryBuilder;
using HHOnline.News.Components;
using HHOnline.Shops;

namespace HHOnline.Data
{
    /// <summary>
    /// 查询生成器
    /// </summary>
    public class QueryGenerator
    {
        private QueryGenerator()
        {
        }

        #region BuildAttachmentQuery

        public static string BuildAttachmentQuery(AttachmentQuery query)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SET Transaction Isolation Level Read UNCOMMITTED ");

            SelectQueryBuilder builder = new SelectQueryBuilder();
            builder.SelectFromTable("NAttachment");
            builder.SelectColumns("AttachmentID");
            builder.AddWhere("AttachmentStatus", Comparison.GreaterThan, 0);

            if (query.Name != null)
            {
                builder.AddWhere("AttachmentName", Comparison.Like, "%" + query.Name + "%");
            }

            if (query.ContentType != null)
            {
                builder.AddWhere("ContentType", Comparison.Like, "%" + query.ContentType + "%");
            }

            if (query.ContentStartSize.HasValue)
            {
                builder.AddWhere("ContentSize", Comparison.GreaterOrEquals, query.ContentStartSize);
            }

            if (query.ContentEndSize.HasValue)
            {
                builder.AddWhere("ContentSize", Comparison.LessOrEquals, query.ContentEndSize);
            }

            if (query.CreateStartTime.HasValue)
            {
                builder.AddWhere("CreateTime", Comparison.GreaterOrEquals, query.CreateStartTime);
            }

            if (query.CreateEndTime.HasValue)
            {
                builder.AddWhere("CreateTime", Comparison.LessOrEquals, query.CreateEndTime);
            }

            // 添加OrderBy
            switch (query.AttachmentOrderBy)
            {
                case HHOnline.News.enums.AttachmentOrderBy.ContentSize:
                    builder.AddOrderBy("ContentSize", (Sorting)query.SortOrder);
                    break;

                case HHOnline.News.enums.AttachmentOrderBy.ContentType:
                    builder.AddOrderBy("ContentType", (Sorting)query.SortOrder);
                    break;

                case HHOnline.News.enums.AttachmentOrderBy.CreateTime:
                    builder.AddOrderBy("CreateTime", (Sorting)query.SortOrder);
                    break;

                case HHOnline.News.enums.AttachmentOrderBy.Name:
                    builder.AddOrderBy("Name", (Sorting)query.SortOrder);
                    break;

                case HHOnline.News.enums.AttachmentOrderBy.UpdateTime:
                    builder.AddOrderBy("UpdateTime", (Sorting)query.SortOrder);
                    break;
            }

            return builder.BuildQuery();
        }

        #endregion

        #region BuildArticleQuery
        public static string BuildArticleQuery(ArticleQuery query)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SET Transaction Isolation Level Read UNCOMMITTED ");

            SelectQueryBuilder builder = new SelectQueryBuilder();
            builder.SelectFromTable("NArticle");
            builder.SelectColumns("ArticleID");
            builder.AddWhere("ArticleStatus", Comparison.NotEquals, (int)ComponentStatus.Deleted);

            if (query.CategoryID != null)
            {
                builder.AddWhere("CategoryID", Comparison.Equals, query.CategoryID.Value);
            }

            if (query.Title != null)
            {
                builder.AddWhere("ArticleTitle", Comparison.Like, "%" + query.Title + "%");
            }

            if (query.HitStartTimes.HasValue)
            {
                builder.AddWhere("HitTimes", Comparison.GreaterOrEquals, query.HitStartTimes.Value);
            }

            if (query.HitEndTimes.HasValue)
            {
                builder.AddWhere("HitTimes", Comparison.LessOrEquals, query.HitEndTimes.Value);
            }

            if (query.CreateStartTime.HasValue)
            {
                builder.AddWhere("CreateTime", Comparison.GreaterOrEquals, query.CreateStartTime);
            }

            if (query.CreateEndTime.HasValue)
            {
                builder.AddWhere("CreateTime", Comparison.LessOrEquals, query.CreateEndTime);
            }

            // 添加OrderBy
            switch (query.ArticleOrderBy)
            {
                case HHOnline.News.enums.ArticleOrderBy.Category:
                    builder.AddOrderBy("CategoryID", (Sorting)query.SortOrder);
                    break;

                case HHOnline.News.enums.ArticleOrderBy.HitTimes:
                    builder.AddOrderBy("HitTimes", (Sorting)query.SortOrder);
                    break;

                case HHOnline.News.enums.ArticleOrderBy.CreateTime:
                    builder.AddOrderBy("CreateTime", (Sorting)query.SortOrder);
                    break;

                case HHOnline.News.enums.ArticleOrderBy.Title:
                    builder.AddOrderBy("ArticleTitle", (Sorting)query.SortOrder);
                    break;

                case HHOnline.News.enums.ArticleOrderBy.UpdateTime:
                    builder.AddOrderBy("UpdateTime", (Sorting)query.SortOrder);
                    break;
            }

            return builder.BuildQuery();
        }
        #endregion

        #region BuildMemberQuery
        public static string BuildMemberQuery(UserQuery query)
        {
            return BuildMemberQuery(query, false);
        }

        public static string BuildMemberQuery(UserQuery query, bool shouldBuildCountQuery)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SET Transaction Isolation Level Read UNCOMMITTED ");

            SelectQueryBuilder builder = new SelectQueryBuilder();
            builder.SelectFromTable("vw_Users_FullUser");

            if (!shouldBuildCountQuery)
                builder.SelectColumns("UserID");
            else
                builder.SelectColumns("count(1)");

            //CompanyName
            if (query.CompanyID.HasValue)
            {
                builder.AddWhere("CompanyID", Comparison.Equals, query.CompanyID.Value);
                query.UserType = UserType.CompanyUser;
            }

            //OrganizationName
            if (query.OrganizationID.HasValue)
            {
                builder.AddWhere("OrganizationID", Comparison.Equals, query.OrganizationID.Value);
            }
            if (query.UserType == null || !query.UserType.HasValue)
            {
                query.UserType = UserType.InnerUser;
            }

            //RoleID
            if (query.RoleID.HasValue)
            {
                List<int> userIDList = Users.GetUsersInRole(query.RoleID.Value);
                if (userIDList.Count > 0)
                {
                    string[] userIDs = new string[userIDList.Count];
                    for (int i = 0; i < userIDList.Count; i++)
                        userIDs[i] = userIDList[i].ToString();
                    builder.AddWhere("UserID", Comparison.In, new SqlLiteral(string.Join(",", userIDs)));
                }
                else
                {
                    builder.AddWhere("1", Comparison.NotEquals, 1);
                }
            }

            //UserType
            if (query.UserType.HasValue)
            {
                builder.AddWhere("UserType", Comparison.Equals, (int)query.UserType.Value);
            }

            //DisplayNameFilter
            if (!GlobalSettings.IsNullOrEmpty(query.DisplayNameFilter))
                builder.AddWhere("DisplayName", Comparison.Like, "%" + query.DisplayNameFilter + "%");

            //EmailFilter
            if (!GlobalSettings.IsNullOrEmpty(query.EmailFilter))
                builder.AddWhere("Email", Comparison.Like, "%" + query.EmailFilter + "%");

            //LoginNameFilter
            if (!GlobalSettings.IsNullOrEmpty(query.LoginNameFilter))
                builder.AddWhere("LoginName", Comparison.Like, "%" + query.LoginNameFilter + "%");

            //AccountStatus
            if (query.AccountStatus != AccountStatus.All)
                builder.AddWhere("UserStatus", Comparison.Equals, (int)query.AccountStatus);

            //InavtiveSinceDate
            if (query.InactiveSinceDate.HasValue)
            {
                builder.AddWhere("LastActiveTime", Comparison.LessOrEquals, query.InactiveSinceDate.Value);
            }

            if (!shouldBuildCountQuery)
            {
                switch (query.SortBy)
                {
                    case SortUsersBy.Email:
                        builder.AddOrderBy("Email", (Sorting)query.SortOrder);
                        break;
                    case SortUsersBy.CreateDate:
                        builder.AddOrderBy("CreateTime", (Sorting)query.SortOrder);
                        break;
                    case SortUsersBy.DisplayName:
                        builder.AddOrderBy("DisplayName", (Sorting)query.SortOrder);
                        break;
                    case SortUsersBy.LastActiveDate:
                        builder.AddOrderBy("LastActiveTime", (Sorting)query.SortOrder);
                        break;
                }
            }
            return builder.BuildQuery();

        }
        #endregion

        #region BuildActivityQuery
        public static string BuildActivityQuery(UserActivityQuery query)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SET Transaction Isolation Level Read UNCOMMITTED ");

            SelectQueryBuilder builder = new SelectQueryBuilder();
            builder.SelectFromTable("SUserActivity");
            builder.SelectColumns("UserActivityID");
            if (query.UserID > 0)
                builder.AddWhere("ActivityUser", Comparison.Equals, query.UserID);

            WhereClause clause = builder.AddWhere("ActivityTime", Comparison.GreaterOrEquals, query.StartTime);
            clause.AddClause(LogicOperator.And, Comparison.LessOrEquals, query.EndTime);

            return builder.BuildQuery();

        }
        #endregion

        #region BuildProductQuery
        public static string BuildProductQuery(ProductQuery query)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SET Transaction Isolation Level Read UNCOMMITTED ");

            SelectQueryBuilder builder = new SelectQueryBuilder();

            builder.SelectFromTable("PProduct p");
            builder.SelectColumns("p.ProductID");
            builder.AddWhere("p.ProductStatus", Comparison.NotEquals, 0);

            //BrandID
            if (query.BrandID.HasValue)
            {
                builder.AddWhere("p.BrandID", Comparison.Equals, query.BrandID.Value);
            }

            //CategoryID
            if (query.CategoryID.HasValue)
            {
                builder.AddJoin(JoinType.InnerJoin, "PProductCategory pc", "pc.ProductID", Comparison.Equals, "p", "ProductID");
                builder.AddWhere("pc.CategoryID", Comparison.Equals, query.CategoryID.Value);
            }

            //CompanyID
            if (query.CompanyID.HasValue)
            {
                builder.AddJoin(JoinType.InnerJoin, "PProductSupply ps", "ps.ProductID", Comparison.Equals, "p", "ProductID");
                builder.AddWhere("ps.SupplierID", Comparison.Equals, query.CompanyID.Value);
            }

            //FocusType
            if (query.FocusType.HasValue)
            {
                SelectQueryBuilder builderFocus = new SelectQueryBuilder();
                builderFocus.Distinct = true;
                builderFocus.SelectFromTable("PProductFocus pf");
                builderFocus.SelectColumn("pf.ProductID");
                builderFocus.AddWhere("pf.FocusType", Comparison.Equals, (int)query.FocusType.Value);
                builderFocus.AddWhere("pf.FocusStatus", Comparison.GreaterThan, 0);
                builderFocus.AddWhere("pf.FocusFrom", Comparison.LessOrEquals, new SqlLiteral("getdate()"));
                builderFocus.AddWhere("pf.FocusEnd", Comparison.GreaterThan, new SqlLiteral("getdate()"));
                builder.AddWhere("p.ProductID", Comparison.In, new SqlLiteral(builderFocus.BuildQuery()));
            }

            //HasPictures
            if (query.HasPictures.HasValue)
            {
                SelectQueryBuilder builderPictures = new SelectQueryBuilder();
                builderPictures.Distinct = true;
                builderPictures.SelectFromTable("PProductPicture pp");
                builderPictures.SelectColumn("pp.ProductID");
                builderPictures.AddWhere("pp.PictureStatus", Comparison.GreaterThan, 0);
                if (query.HasPictures.Value)
                {

                    builder.AddWhere("p.ProductID", Comparison.In, new SqlLiteral(builderPictures.BuildQuery()));
                }
                else
                {
                    builder.AddWhere("p.ProductID", Comparison.NotIn, new SqlLiteral(builderPictures.BuildQuery()));
                }
            }

            //HasPublished
            if (query.HasPublished.HasValue)
            {
                if (query.HasPublished.Value)
                {
                    builder.AddWhere("p.ProductStatus", Comparison.Equals, (int)ComponentStatus.Enabled);
                }
                else
                {
                    builder.AddWhere("p.ProductStatus", Comparison.Equals, (int)ComponentStatus.Disabled);
                }
            }

            //HasPrice
            if (query.HasPrice.HasValue)
            {
                SelectQueryBuilder builderPrice = new SelectQueryBuilder();
                builderPrice.Distinct = true;
                builderPrice.SelectFromTable("PProductPrice pr");
                builderPrice.SelectColumn("pr.ProductID");
                builderPrice.AddWhere("pr.SupplyStatus", Comparison.GreaterThan, 0);
                if (query.HasPrice.Value)
                {
                    builder.AddWhere("p.ProductID", Comparison.In, new SqlLiteral(builderPrice.BuildQuery()));
                }
                else
                {
                    builder.AddWhere("p.ProductID", Comparison.NotIn, new SqlLiteral(builderPrice.BuildQuery()));
                }
            }

            //IndustryID
            if (query.IndustryID.HasValue)
            {
                builder.AddJoin(JoinType.InnerJoin, "PProductIndustry pi", "pi.ProductID", Comparison.Equals, "p", "ProductID");
                builder.AddWhere("pi.IndustryID", Comparison.Equals, query.IndustryID.Value);
            }

            //ProductID
            if (query.ProductID.HasValue)
            {
                builder.AddWhere("p.ProductID", Comparison.Equals, query.ProductID.Value);
            }

            //ProductName
            if (!GlobalSettings.IsNullOrEmpty(query.ProductNameFilter))
            {
                builder.AddWhere("p.ProductName", Comparison.Like, "%" + query.ProductNameFilter + "%");
            }

            //ProductKey
            if (!GlobalSettings.IsNullOrEmpty(query.ProductKeywordsFilter))
            {
                builder.AddWhere("p.ProductKeywords", Comparison.Like, "%" + query.ProductKeywordsFilter + "%");
            }

            //OrderBy
            switch (query.ProductOrderBy)
            {

                case ProductOrderBy.DataCreated:
                    builder.AddOrderBy("p.CreateTime", (Sorting)query.SortOrder);
                    break;
                case ProductOrderBy.ProductName:
                    builder.AddOrderBy("p.ProductName", (Sorting)query.SortOrder);
                    break;
                case ProductOrderBy.ProductStatus:
                    builder.AddOrderBy("p.ProductStatus", (Sorting)query.SortOrder);
                    break;
                case ProductOrderBy.BrandName:
                    builder.AddJoin(JoinType.InnerJoin, "PBrand pb", "pb.BrandID", Comparison.Equals, "p", "BrandID");
                    builder.AddOrderBy("pb.BrandName", (Sorting)query.SortOrder);
                    break;
                default:
                case ProductOrderBy.DisplayOrder:
                    builder.AddOrderBy("p.DisplayOrder", (Sorting)query.SortOrder);
                    break;
            }
            return builder.BuildQuery();
        }
        #endregion

        #region BuildProductPrice
        public static string BuilderProductPriceQuery(List<string> filters, int productID, UserLevel level)
        {
            StringBuilder sb = new StringBuilder();

            SelectQueryBuilder builder = new SelectQueryBuilder();
            builder.SelectFromTable("PProductPrice pr");
            builder.AddJoin(JoinType.InnerJoin, "PProduct p", "p.ProductID", Comparison.Equals, "pr", "ProductID");
            builder.AddJoin(JoinType.LeftJoin, "PProductIndustry ppi", "ppi.ProductID", Comparison.Equals, "p", "ProductID");
            builder.AddJoin(JoinType.LeftJoin, "dbo.PProductCategory pc", "pc.ProductID", Comparison.Equals, "p", "ProductID");
            switch (level)
            {
                case UserLevel.E:
                    builder.SelectColumns("PriceGradeE");
                    break;
                case UserLevel.D:
                    builder.SelectColumns("PriceGradeD");
                    break;
                case UserLevel.C:
                    builder.SelectColumns("PriceGradeC");
                    break;
                case UserLevel.B:
                    builder.SelectColumns("PriceGradeB");
                    break;
                case UserLevel.A:
                    builder.SelectColumns("PriceGradeA");
                    break;
            }
            builder.AddWhere("pr.SupplyStatus ", Comparison.GreaterThan, 0);
            builder.AddWhere("p.ProductID", Comparison.Equals, productID);
            sb.Append(builder.BuildQuery());

            //添加条件
            sb.Append(" ");
            sb.Append(LogicOperator.And.ToString());
            sb.Append(" (");
            if (filters.Count > 0)
            {

                for (int i = 0; i < filters.Count; i++)
                {
                    sb.Append(" ");
                    sb.Append(" (");
                    sb.Append(filters[i]);
                    if (i != filters.Count - 1)
                    {
                        sb.Append(LogicOperator.Or.ToString());
                    }
                    sb.Append(" )");
                }
                sb.Append(" )");
            }
            else
            {
                sb.Append(" 1<> 1)");
            }
            return sb.ToString();
        }
        #endregion

        #region -BuildFavorite-
        public static string BuildFavorite(FavoriteQuery query)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SET Transaction Isolation Level Read UNCOMMITTED ");

            SelectQueryBuilder builder = new SelectQueryBuilder();
            builder.SelectFromTable("UFavorite");
            builder.SelectColumns("FavoriteID");
            if (!string.IsNullOrEmpty(query.FavoriteTitleFilter))
                builder.AddWhere("FavoriteTitle", Comparison.Like, query.FavoriteTitleFilter);

            if (!string.IsNullOrEmpty(query.FavoriteMemoFilter))
                builder.AddWhere("FavoriteMemo", Comparison.Like, query.FavoriteMemoFilter);


            builder.AddWhere("FavoriteType", Comparison.NotEquals, 0);

            if (query.FavoriteType != null)
                builder.AddWhere("FavoriteType", Comparison.Equals, (int)query.FavoriteType);

            return builder.BuildQuery();
        }
        #endregion

        #region BuildSearch

        public static string BuildSearch(string startLetter, int topCount)
        {
            SelectQueryBuilder builder = new SelectQueryBuilder();
            builder.SelectFromTable("SWordStatistic ws");
            builder.SelectAllColumns();
            builder.TopRecords = topCount;
            if (!GlobalSettings.IsNullOrEmpty(startLetter))
                builder.AddWhere("ws.SearchWord", Comparison.Like, startLetter + "%");
            builder.AddOrderBy("ws.HitCount", Sorting.Descending);
            return builder.BuildQuery();
        }
        #endregion
    }
}
