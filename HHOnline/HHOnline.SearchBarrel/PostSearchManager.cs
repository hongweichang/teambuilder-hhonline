using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Documents;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using HHOnline.Common;

namespace HHOnline.SearchBarrel
{
    /// <summary>
    /// 用于Lucene索引文件添加、修改、删除、搜索
    /// </summary>
    public class PostSearchManager
    {
        /*
        private static PostSearchManager instance = null;
        private static object lockObject = new object();

        private PostSearchManager() { }

        public static PostSearchManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                            instance = new PostSearchManager();
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 索引文件夹
        /// </summary>
        /// <returns></returns>
        public static string PhysicalIndexDirectory
        {
            get
            {
                return "~/IndexFiles/";
            }
        }

        //public List<SearchItem> Search(FullTextQuery query)
        //{
        //    if (!IndexFileIsExist(PhysicalIndexDirectory))
        //        return new List<SearchItem>();

        //    DateTime startTime = DateTime.Now;
        //    BooleanQuery currentQuery = new BooleanQuery();

        //    Query postKeywordQuery = null;
        //    if (!string.IsNullOrEmpty(query.Keyword))
        //    {
        //        query.Keyword = SearchHelper.LuceneKeywordsScrubber(query.Keyword);
        //        if (!string.IsNullOrEmpty(query.Keyword))
        //        {
        //            string[] searchFieldsForKeyword = new string[4];
        //            searchFieldsForKeyword[0] = "Subject";
        //            searchFieldsForKeyword[1] = "Body";
        //            searchFieldsForKeyword[2] = "Tags";
        //            searchFieldsForKeyword[3] = "Author";

        //            MultiFieldQueryParser keywordParser = new MultiFieldQueryParser(searchFieldsForKeyword, GetChineseAnalyzer());

        //            keywordParser.SetLowercaseExpandedTerms(true);
        //            keywordParser.SetDefaultOperator(QueryParser.OR_OPERATOR);

        //            string keyWordsOfSplit = SplitKeywordsBySpace(query.Keyword);
        //            postKeywordQuery = keywordParser.Parse(keyWordsOfSplit);

        //            currentQuery.Add(postKeywordQuery, BooleanClause.Occur.MUST);
        //        }
        //    }

        //    if (query.TagNames != null)
        //    {
        //        string tagNames = string.Join(" ", query.TagNames);
        //        tagNames = SearchHelper.LuceneKeywordsScrubber(tagNames);

        //        if (!string.IsNullOrEmpty(tagNames))
        //        {
        //            QueryParser tagNameQueryParser = new QueryParser("Tags", GetChineseAnalyzer());

        //            currentQuery.Add(tagNameQueryParser.Parse(tagNames), BooleanClause.Occur.MUST);
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(query.Author))
        //    {
        //        query.Author = SearchHelper.LuceneKeywordsScrubber(query.Author);
        //        if (!string.IsNullOrEmpty(query.Author))
        //        {
        //            QueryParser authorQueryParser = new QueryParser("Author", new WhitespaceAnalyzer());
        //            currentQuery.Add(authorQueryParser.Parse(query.Author), BooleanClause.Occur.MUST);
        //        }
        //    }

        //    IndexSearcher searcher = new IndexSearcher(PhysicalIndexDirectory);

        //    Hits hits = searcher.Search(currentQuery);

        //    IList<SearchItem> pds = new List<SearchItem>();

        //    int pageLowerBound = (query.PageIndex - 1) * query.PageSize;
        //    int pageUpperBound = pageLowerBound + query.PageSize;
        //    if (pageUpperBound > hits.Length())
        //        pageUpperBound = hits.Length();

        //    KTDictSeg.HighLight.Highlighter highlighter = null;
        //    if (!string.IsNullOrEmpty(query.Keyword))
        //    {
        //        highlighter = new KTDictSeg.HighLight.Highlighter(new KTDictSeg.HighLight.SimpleHTMLFormatter("<font color=\"#c60a00\">", "</font>"), new Lucene.Net.Analysis.KTDictSeg.KTDictSegTokenizer());
        //        highlighter.FragmentSize = 100;
        //    }
        //    return null;
        //    //for (int i = pageLowerBound; i < pageUpperBound; i++)
        //    //{
        //    //    NewsThread item = ConvertDocumentToNewsThread(hits.Doc(i));

        //    //    #region 搜索结果高亮显示
        //    //    if (!string.IsNullOrEmpty(query.Keyword))
        //    //    {
        //    //        string bestBody = null;
        //    //        if (!string.IsNullOrEmpty(item.Body) && item.Body.Length > MaxNumFragmentsRequired)
        //    //            bestBody = highlighter.GetBestFragment(query.Keyword, item.Body);

        //    //        if (!string.IsNullOrEmpty(bestBody))
        //    //            item.Body = bestBody;
        //    //        else
        //    //            item.Body = HtmlUtils.TrimHtml(item.Body, 100);

        //    //        string bestSubject = null;
        //    //        if (!string.IsNullOrEmpty(item.Title) && item.Title.Length > MaxNumFragmentsRequired)
        //    //            bestSubject = highlighter.GetBestFragment(query.Keyword, item.Title);

        //    //        if (!string.IsNullOrEmpty(bestSubject))
        //    //            item.Title = bestSubject;
        //    //    }
        //    //    #endregion


        //    //    pds.Records.Add(item);
        //    //}
        //    //searcher.Close();
        //    //pds.TotalRecords = hits.Length();

        //    //DateTime endTime = DateTime.Now;
        //    //pds.SearchDuration = (endTime.Ticks - startTime.Ticks) / 1E7f;

        //    //pds.PageIndex = query.PageIndex;
        //    //pds.PageSize = query.PageSize;

        //    //return pds;
        //}

        /// <summary>
        /// 切分关键词(用空格分隔)
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        protected static string SplitKeywordsBySpace(string keywords)
        {
            StringBuilder result = new StringBuilder();

            Lucene.Net.Analysis.KTDictSeg.KTDictSegTokenizer ktTokenizer = new Lucene.Net.Analysis.KTDictSeg.KTDictSegTokenizer();
            List<FTAlgorithm.T_WordInfo> words = ktTokenizer.SegmentToWordInfos(keywords);

            foreach (FTAlgorithm.T_WordInfo word in words)
            {
                if (word == null)
                    continue;

                result.AppendFormat("{0}^{1}.0 ", word.Word, (int)Math.Pow(3, word.Rank));
            }

            return result.ToString().Trim();
        }


        public static bool Insert(IList<SearchItem> items)
        {
            return Insert(items, PhysicalIndexDirectory);
        }

        public static bool Insert(IList<SearchItem> items, string indexPath)
        {
            bool indexFileIsExist = IndexFileIsExist(indexPath);
            if (!indexFileIsExist)
            {
                try
                {
                    System.IO.Directory.CreateDirectory(indexPath);
                }
                catch { }
            }

            return Insert(items, indexPath, !indexFileIsExist);
        }

        private static bool Insert(IList<SearchItem> items, string indexPath, bool createIndexFile)
        {
            if (items == null || items.Count == 0)
                return false;
            FSDirectory fsDir = FSDirectory.GetDirectory(indexPath, createIndexFile)
;
            IndexWriter fsWriter = new IndexWriter(fsDir, GetChineseAnalyzer(), createIndexFile);

            fsWriter.SetMergeFactor(10);
            fsWriter.SetMaxMergeDocs(1000);
            fsWriter.SetMaxBufferedDocs(100);
            bool result = false;
            try
            {
                foreach (SearchItem item in items)
                {
                    if (item != null)
                    {
                        Document doc = ConvertSearchItemToDocument(item);
                        if (doc != null)
                            fsWriter.AddDocument(doc);
                    }
                }
                fsWriter.Optimize();
                result = true;
            }
            finally
            {
                fsWriter.Close();
            }

            return result;

        }

        public static bool Update(IList<SearchItem> items)
        {
            if (items == null || items.Count == 0)
                return false;

            string[] ids = new string[items.Count];
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] != null)
                    ids[i] = items[i].PostID.ToString();
            }

            bool result = Delete(ids, false);

            if (result)
                result = Insert(items);

            return result;
        }

        public static bool Delete(string[] ids)
        {
            return Delete(ids, true);
        }

        public static bool Delete(string[] ids, bool needOptimize)
        {
            if (ids == null && ids.Length == 0)
                return false;

            Lucene.Net.Store.Directory fsDir = FSDirectory.GetDirectory(PhysicalIndexDirectory, false);
            IndexReader reader = IndexReader.Open(fsDir);

            bool result = false;
            try
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i] != null)
                    {
                        Term term = new Term("PostID", ids[i]);
                        reader.DeleteDocuments(term);
                    }
                }
                reader.Close();

                if (needOptimize)
                {
                    IndexWriter fsWriter = new IndexWriter(fsDir, GetChineseAnalyzer(), false);
                    fsWriter.Optimize();
                    fsWriter.Close();
                }

                result = true;
            }
            finally
            {
                fsDir.Close();
            }

            return result;
        }

        private static Document ConvertSearchItemToDocument(SearchItem item)
        {
            if (item == null)
                return null;
            Document doc = new Document();
            Field field;

            field = new Field("PostID", item.PostID.ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED);
            doc.Add(field);

            field = new Field("Author", item.Author, Field.Store.YES, Field.Index.UN_TOKENIZED);
            doc.Add(field);

            field = new Field("Body", item.Body, Field.Store.YES, Field.Index.TOKENIZED, Field.TermVector.YES);
            doc.Add(field);

            field = new Field("Subject", item.Subject, Field.Store.YES, Field.Index.TOKENIZED, Field.TermVector.YES);
            doc.Add(field);

            field = new Field("LastUpdatedDate", item.LastUpdatedDate.ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED);
            doc.Add(field);

            field = new Field("PostDate", item.PostDate.ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED);
            doc.Add(field);

            if (item.Tags != null)
            {
                foreach (string tagName in item.Tags)
                {
                    field = new Field("Tags", tagName, Field.Store.YES, Field.Index.TOKENIZED);
                    field.SetBoost(2.0F);
                    doc.Add(field);

                    field = new Field("TagKeywords", tagName, Field.Store.YES, Field.Index.UN_TOKENIZED);
                    field.SetBoost(2.0F);
                    doc.Add(field);
                }
            }
            return doc;
        }

        protected static bool IndexFileIsExist(string indexPath)
        {
            if (!System.IO.Directory.Exists(indexPath))
            {
                return false;
            }
            else
            {
                DirectoryInfo indexPathDirectoryInfo = new DirectoryInfo(indexPath);
                return (indexPathDirectoryInfo.GetFiles().Length > 0);
            }
        }

        /// <summary>
        /// 获取用于中文分词的Analyzer
        /// </summary>
        /// <returns>Analyzer</returns>
        protected static Analyzer GetChineseAnalyzer()
        {
            return new Lucene.Net.Analysis.KTDictSeg.KTDictSegAnalyzer();
        }

        /// <summary>
        /// 获取用于中文分词的Analyzer(不分词，只返回原始字符串)
        /// </summary>
        /// <returns>Analyzer</returns>
        protected static Analyzer GetChineseAnalyzerOfUnTokenized()
        {
            return new Lucene.Net.Analysis.KTDictSeg.KTDictSegAnalyzer(true);
        }
         * */
    }
}
