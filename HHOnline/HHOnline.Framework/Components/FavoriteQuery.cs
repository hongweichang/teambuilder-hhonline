using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Framework
{
    /// <summary>
    /// 用户收藏查询类
    /// </summary>
    public class FavoriteQuery
    {
        private int userID = -1;
        private int pageIndex = 0;
        private int pageSize = 20;
        private FavoriteType? favoriteType;
        private string favoriteTitleFilter = string.Empty;
        private string favoriteMemoFilter = string.Empty;

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }

        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex
        {
            get
            {
                if (pageIndex >= 0)
                    return pageIndex;
                else
                    return 0;
            }
            set
            {
                pageIndex = value;
            }
        }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value;
            }
        }

        /// <summary>
        /// 收藏类型
        /// </summary>
        public FavoriteType? FavoriteType
        {
            get
            {
                return favoriteType;
            }
            set
            {
                favoriteType = value;
            }
        }

        /// <summary>
        /// 收藏标题筛选
        /// </summary>
        public string FavoriteTitleFilter
        {
            get
            {
                return favoriteTitleFilter;
            }
            set
            {
                favoriteTitleFilter = value;
            }
        }

        /// <summary>
        /// 收藏备注筛选
        /// </summary>
        public string FavoriteMemoFilter
        {
            get
            {
                return favoriteMemoFilter;
            }
            set
            {
                favoriteMemoFilter = value;
            }
        }
    }
}
