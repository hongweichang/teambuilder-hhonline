using System;

namespace HHOnline.Framework
{
    /// <summary>
    ///  查询关键字类
    /// </summary>
    [Serializable]
    public class WordSearch
    {

        #region --Private Members--
        private decimal _searchID;
        private string _searchWord;
        private DateTime _createTime;
        private int _createUser;
        #endregion

        #region --Constructor--
        public WordSearch()
        {
        }
        #endregion

        #region --Public Members--
        ///<summary>
        ///查询编号
        ///</summary>
        public decimal SearchID
        {
            get { return _searchID; }
            set { _searchID = value; }
        }

        ///<summary>
        ///查询关键字
        ///</summary>
        public string SearchWord
        {
            get { return _searchWord; }
            set { _searchWord = value; }
        }

        ///<summary>
        ///查询时间
        ///</summary>
        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        ///<summary>
        ///用户标识
        ///</summary>
        public int CreateUser
        {
            get { return _createUser; }
            set { _createUser = value; }
        }
        #endregion
    }
}
