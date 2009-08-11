using System;

namespace HHOnline.Framework
{
    public class Favorite
    {
        #region --Private Members--
        private int _favoriteID;
        private int _userID;
        private FavoriteType _favoriteType;
        private string _favoriteTitle;
        private int _relatedID;
        private string _favoriteUrl;
        private int _favoriteLevel;
        private string _favoriteMemo;
        private DateTime _createTime;
        private DateTime _updateTime;
        #endregion

        #region --Constructor--
        public Favorite()
        {
        }
        #endregion

        #region --Public Members--
        ///<summary>
        ///收藏项编号
        ///</summary>
        public int FavoriteID
        {
            get { return _favoriteID; }
            set { _favoriteID = value; }
        }

        ///<summary>
        ///公司用户编号
        ///</summary>
        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        ///<summary>
        ///收藏类型，1产品、2资讯、3外部
        ///</summary>
        public FavoriteType FavoriteType
        {
            get { return _favoriteType; }
            set { _favoriteType = value; }
        }

        ///<summary>
        ///收藏项标题
        ///</summary>
        public string FavoriteTitle
        {
            get { return _favoriteTitle; }
            set { _favoriteTitle = value; }
        }

        ///<summary>
        ///收藏产品或资讯的编号
        ///</summary>
        public int RelatedID
        {
            get { return _relatedID; }
            set { _relatedID = value; }
        }

        ///<summary>
        ///收藏项链接（站内支持更新）
        ///</summary>
        public string FavoriteUrl
        {
            get { return _favoriteUrl; }
            set { _favoriteUrl = value; }
        }

        ///<summary>
        ///收藏星级标示（关注度）
        ///</summary>
        public int FavoriteLevel
        {
            get { return _favoriteLevel; }
            set { _favoriteLevel = value; }
        }

        ///<summary>
        ///收藏备注
        ///</summary>
        public string FavoriteMemo
        {
            get { return _favoriteMemo; }
            set { _favoriteMemo = value; }
        }

        ///<summary>
        ///创建时间
        ///</summary>
        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        ///<summary>
        ///最后更新时间
        ///</summary>
        public DateTime UpdateTime
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }
        #endregion
    }
}
