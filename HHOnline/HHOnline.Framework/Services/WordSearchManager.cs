using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using HHOnline.Cache;
using HHOnline.Framework.Providers;


namespace HHOnline.Framework
{
    /// <summary>
    /// 查询管理类
    /// </summary>
    public class WordSearchManager
    {
        /// <summary>
        /// 查询关键字入库
        /// </summary>
        /// <param name="keyword"></param>
        public static void Insert(string keyword)
        {
            CommonDataProvider.Instance.InsertWordSearch(keyword);
        }

        /// <summary>
        /// 查询关键字统计
        /// </summary>
        public static void Statistic()
        {
            CommonDataProvider.Instance.StatisticWordSearch();
        }
        public static List<WordStatistic> GetStatistic(DateTime startTime, DateTime endTime)
        {
            return CommonDataProvider.Instance.GetStatistic(startTime,endTime);
        }
        public static bool DeleteWords(string word)
        {
            return CommonDataProvider.Instance.DeleteWords(word);
        }
        public static bool DeleteWords(int statisticID)
        {
            return CommonDataProvider.Instance.DeleteWords(statisticID);
        }
        public static bool UpdateWordHitCount(int statisticID, decimal hitCount)
        {
            return CommonDataProvider.Instance.UpdateWordHitCount(statisticID, hitCount);
        }

        public static bool SaveStatistic(WordStatistic ws)
        {
            return CommonDataProvider.Instance.SaveStatistic(ws);
        }
        /// <summary>
        /// 获取WordSuggest，根据Hitcount排序
        /// </summary>
        /// <param name="startLetter">首字母</param>
        /// <param name="topCount">Length</param>
        /// <returns></returns>
        public static List<string> GetWordSuggest(string startLetter, int topCount)
        {
            return CommonDataProvider.Instance.GetWordSuggest(startLetter, topCount);
        }
        /// <summary>
        /// 获取热字
        /// </summary>
        /// <returns></returns>
        public static List<string> GetHotWords()
        {
            return CommonDataProvider.Instance.GetHotWords();
        }
    }
}
