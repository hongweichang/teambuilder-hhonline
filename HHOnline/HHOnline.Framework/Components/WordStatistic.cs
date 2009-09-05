using System;

namespace HHOnline.Framework
{
    /// <summary>
    /// 查询关键字统计类
    /// </summary>
    [Serializable]
    public class WordStatistic
    {
        #region --Private Members--
        private int _statisticID;
        private string _searchWord;
        private decimal _hitCount;
        private DateTime _createTime;
        private DateTime _updateTime;
        #endregion

        #region --Constructor--
        public WordStatistic()
        {
        }
        #endregion

        #region --Public Members--
        ///<summary>
        ///关键字编号
        ///</summary>
        public int StatisticID
        {
            get { return _statisticID; }
            set { _statisticID = value; }
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
        ///命中次数
        ///</summary>
        public decimal HitCount
        {
            get { return _hitCount; }
            set { _hitCount = value; }
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
        ///更新时间
        ///</summary>
        public DateTime UpdateTime
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }
        #endregion
    }
}
