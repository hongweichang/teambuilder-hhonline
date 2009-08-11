using System;
using System.Collections.Generic;
using System.Web;
using HHOnline.Cache;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    /// <summary>
    /// 收藏管理
    /// </summary>
    public class Favorites
    {
        #region GetFavorite
        /// <summary>
        /// 根据FavoriteID获取Favorite信息
        /// </summary>
        /// <param name="favoriteID"></param>
        /// <returns></returns>
        public static Favorite GetFavorite(int favoriteID)
        {
            Favorite favorite = null;
            string cacheKey = CacheKeyManager.GetFavoriteKey(favoriteID);

            if (HttpContext.Current != null)
                favorite = HttpContext.Current.Items[cacheKey] as Favorite;
            if (favorite != null)
                return favorite;

            favorite = HHCache.Instance.Get(cacheKey) as Favorite;
            if (favorite == null)
            {
                favorite = CommonDataProvider.Instance.GetFavorite(favoriteID);
                if (HttpContext.Current != null)
                    HttpContext.Current.Items[cacheKey] = favorite;
                HHCache.Instance.Insert(cacheKey, favorite);
            }
            return favorite;
        }
        #endregion

        #region Add/UpdateFavorite
        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="favorite"></param>
        /// <returns></returns>
        public static bool AddFavorite(Favorite favorite)
        {
            CommonDataProvider.Instance.CreateUpdateFavorite(favorite, DataProviderAction.Create);
            RefreshCachedFavorite();
            return true;
        }

        /// <summary>
        /// 更新收藏
        /// </summary>
        /// <param name="favorite"></param>
        /// <returns></returns>
        public static bool UpdateFavorite(Favorite favorite)
        {
            CommonDataProvider.Instance.CreateUpdateFavorite(favorite, DataProviderAction.Update);
            RefreshCachedFavorite(favorite);
            return true;
        }
        #endregion

        #region GetFavorites
        /// <summary>
        /// 获取Query获取收藏列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static PagingDataSet<Favorite> GetFavorites(FavoriteQuery query)
        {
            PagingDataSet<Favorite> favorites = null;
            string cacheKey = CacheKeyManager.GetFavoriteQueryKey(query);

            if (HttpContext.Current != null)
                favorites = HttpContext.Current.Items[cacheKey] as PagingDataSet<Favorite>;
            if (favorites != null)
                return favorites;

            favorites = HHCache.Instance.Get(cacheKey) as PagingDataSet<Favorite>;
            if (favorites == null)
            {
                int totalRecods;
                List<Favorite> favoriteList = CommonDataProvider.Instance.GetFavorites(query, out totalRecods);
                favorites.Records = favoriteList;
                favorites.TotalRecords = totalRecods;
                if (HttpContext.Current != null)
                    HttpContext.Current.Items[cacheKey] = favorites;
                HHCache.Instance.Insert(cacheKey, favorites);
            }
            return favorites;
        }
        #endregion

        #region  Favorite Cache Management
        /// <summary>
        /// 清除CacheKey
        /// </summary>
        /// <param name="favorite"></param>
        internal static void RefreshCachedFavorite(Favorite favorite)
        {
            HHCache.Instance.Remove(CacheKeyManager.GetFavoriteKey(favorite.FavoriteID));
            HHCache.Instance.Remove(CacheKeyManager.GetFavoritePrefix(favorite.UserID));
            RefreshCachedFavorite();
        }

        internal static void RefreshCachedFavorite()
        {
            HHCache.Instance.Remove(CacheKeyManager.FavoriteListPrefix);
        }

        /// <summary>
        /// 将收藏缓存
        /// </summary>
        /// <param name="favorite"></param>
        internal static void AddCachedFavorite(Favorite favorite)
        {
            HHCache.Instance.Insert(CacheKeyManager.GetFavoriteKey(favorite.FavoriteID), favorite);
        }

        /// <summary>
        /// 添加收藏列表到缓存
        /// </summary>
        /// <param name="users"></param>
        internal static void AddCachedFavorite(List<Favorite> favoriteList)
        {
            foreach (Favorite favorite in favoriteList)
            {
                AddCachedFavorite(favorite);
            }
        }
        #endregion
    }
}
