using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Common;
using Lucene.Net.Analysis;


namespace HHOnline.SearchBarrel
{
    /// <summary>
    /// 检索帮助类
    /// </summary>
    public class SearchHelper
    {
        public static readonly int MergeFactor = 10;

        public static readonly int MinMergeDocs = 100;

        public static readonly int MaxMergeDocs = 1000;

        /// <summary>
        /// 获取用于中文分词的Analyzer
        /// </summary>
        /// <returns>Analyzer</returns>
        public static Analyzer GetChineseAnalyzer()
        {
            return new Lucene.Net.Analysis.PanGu.PanGuAnalyzer();
        }

        /// <summary>
        /// 获取用于中文分词的Analyzer(不分词，只返回原始字符串)
        /// </summary>
        /// <returns>Analyzer</returns>
        public static Analyzer GetChineseAnalyzerOfUnTokenized()
        {
            return new Lucene.Net.Analysis.PanGu.PanGuAnalyzer(true);
        }

        /// <summary>
        /// 切分关键词(用空格分隔)
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public static string SplitKeywordsBySpace(string keywords)
        {
            StringBuilder result = new StringBuilder();

            Lucene.Net.Analysis.PanGu.PanGuTokenizer ktTokenizer = new Lucene.Net.Analysis.PanGu.PanGuTokenizer();
            ICollection<PanGu.WordInfo> words = ktTokenizer.SegmentToWordInfos(keywords);

            foreach (PanGu.WordInfo word in words)
            {
                if (word == null)
                    continue;

                result.AppendFormat("{0}^{1}.0 ", word.Word, (int)Math.Pow(3, word.Rank));
            }

            return result.ToString().Trim();
        }

        public static string LuceneKeywordsScrubber(string str)
        {
            return LuceneHelper.LuceneKeywordsScrubber(str);
        }

    }
}
