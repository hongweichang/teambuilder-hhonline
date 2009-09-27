using System;
using System.Collections.Generic;
using HHOnline.Framework;

namespace HHOnline.SearchBarrel
{
    public class IndexReportManager
    {
        /// <summary>
        /// 获取所有索引报告
        /// </summary>
        /// <returns></returns>
        public static List<IndexFileReport> GetIndexReports()
        {
            List<IndexFileReport> lstFileReports = new List<IndexFileReport>();
            foreach (KeyValuePair<string, BaseSearchSetting> pair in SearchConfiguration.GetConfig().SearchSettings)
            {
                lstFileReports.Add(new IndexFileReport(pair.Key));
            }
            return lstFileReports;
        }

        /// <summary>
        /// 重建索引
        /// </summary>
        /// <param name="searchKey"></param>
        public static void BuildIndex()
        {
            foreach (IndexFileReport fileReport in GetIndexReports())
            {
                fileReport.BuildIndex();
            }
        }
    }
}
