using System;
using System.Collections.Generic;
using System.IO;
using HHOnline.Common;
using HHOnline.Framework;
using HHOnline.News;
using HHOnline.News.Components;
using HHOnline.News.Services;
using Lucene.Net.Search;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.QueryParsers;
using Lucene.Net.Documents;

namespace HHOnline.SearchBarrel
{
    /// <summary>
    /// 用于Lucene索引文件添加、修改、删除、搜索
    /// </summary>
    public class NewsSearchManager
    {
        #region Property
        protected static readonly int MaxNumFragmentsRequired = 2;

        private static BaseSearchSetting searchSetting;

        /// <summary>
        /// 查询设置
        /// </summary>
        public static BaseSearchSetting SearchSetting
        {
            get
            {
                if (searchSetting == null)
                {
                    SearchConfiguration.GetConfig().SearchSettings.TryGetValue("newsSearchSetting", out searchSetting);
                    if (searchSetting == null)
                        searchSetting = new NewsSearchSetting();
                }
                return searchSetting;
            }
        }

        /// <summary>
        /// 索引文件物理路径
        /// </summary>
        public static string PhysicalIndexDirectory
        {
            get
            {
                return SearchSetting.PhysicalIndexDirectory;
            }
        }
        #endregion

        #region Search
        public static SearchResultDataSet<Article> Search(ArticleQuery query)
        {
            //索引文件不存在时，返回null
            if (!GlobalSettings.CheckFileExist(PhysicalIndexDirectory))
                return new SearchResultDataSet<Article>();
            DateTime startTime = DateTime.Now;
            BooleanQuery currentQuery = new BooleanQuery();

            //CategoryID
            if (query.CategoryID.HasValue && query.CategoryID.Value != 0)
            {
                Term categoryIDTearm = new Term(NewsIndexField.CategoryID, query.CategoryID.ToString());
                Query categoryIDQuery = new TermQuery(categoryIDTearm);
                currentQuery.Add(categoryIDQuery, BooleanClause.Occur.MUST);
            }

            //KeyWord
            if (!string.IsNullOrEmpty(query.Title))
            {
                query.Title = SearchHelper.LuceneKeywordsScrubber(query.Title);
                if (!string.IsNullOrEmpty(query.Title))
                {
                    string[] searchFieldsForKeyword = new string[4];
                    searchFieldsForKeyword[0] = NewsIndexField.Title;
                    searchFieldsForKeyword[1] = NewsIndexField.SubTitle;
                    searchFieldsForKeyword[2] = NewsIndexField.Abstract;
                    searchFieldsForKeyword[3] = NewsIndexField.Keywords;

                    MultiFieldQueryParser articleWordQueryParser = new MultiFieldQueryParser(searchFieldsForKeyword, SearchHelper.GetChineseAnalyzer());
                    articleWordQueryParser.SetLowercaseExpandedTerms(true);
                    articleWordQueryParser.SetDefaultOperator(QueryParser.OR_OPERATOR);

                    string keyWordsSplit = SearchHelper.SplitKeywordsBySpace(query.Title);
                    Query articleWordQuery = articleWordQueryParser.Parse(keyWordsSplit);
                    currentQuery.Add(articleWordQuery, BooleanClause.Occur.MUST);
                }
            }

            //Search
            IndexSearcher searcher = new IndexSearcher(PhysicalIndexDirectory);
            Hits hits = searcher.Search(currentQuery);
            SearchResultDataSet<Article> articles = new SearchResultDataSet<Article>();
            int pageLowerBound = query.PageIndex * query.PageSize;
            int pageUpperBound = pageLowerBound + query.PageSize;
            if (pageUpperBound > hits.Length())
                pageUpperBound = hits.Length();

            //HighLight
            PanGu.HighLight.Highlighter highlighter = null;
            if (!string.IsNullOrEmpty(query.Title))
            {
                highlighter = new PanGu.HighLight.Highlighter(new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"#c60a00\">", "</font>"), new PanGu.Segment());
                highlighter.FragmentSize = 100;
            }
            for (int i = pageLowerBound; i < pageUpperBound; i++)
            {
                Article item = ConvertDocumentToArticle(hits.Doc(i));
                if (!string.IsNullOrEmpty(query.Title))
                {
                    string bestBody = null;
                    if (!string.IsNullOrEmpty(item.Abstract) && item.Abstract.Length > MaxNumFragmentsRequired)
                        bestBody = highlighter.GetBestFragment(query.Title, item.Abstract);

                    if (!string.IsNullOrEmpty(bestBody))
                        item.Abstract = bestBody;
                    else
                        item.Abstract = HtmlHelper.TrimHtml(item.Abstract, 100);

                    string bestSubject = null;
                    if (!string.IsNullOrEmpty(item.Title) && item.Title.Length > MaxNumFragmentsRequired)
                        bestSubject = highlighter.GetBestFragment(query.Title, item.Title);

                    if (!string.IsNullOrEmpty(bestSubject))
                        item.Title = bestSubject;
                }
                articles.Records.Add(item);
            }
            searcher.Close();
            articles.TotalRecords = hits.Length();

            DateTime endTime = DateTime.Now;
            articles.SearchDuration = (endTime.Ticks - startTime.Ticks) / 1E7f;
            articles.PageIndex = query.PageIndex;
            articles.PageSize = query.PageSize;

            return articles;
        }
        #endregion

        #region Insert
        /// <summary>
        /// 加入索引
        /// </summary>
        /// <param name="products">加入索引的产品集合</param>
        /// <returns></returns>
        public static bool Insert(List<Article> articles)
        {
            return Insert(articles, PhysicalIndexDirectory);
        }

        /// <summary>
        /// 在indexPath位置建立索引
        /// </summary>
        /// <param name="products">加入索引的产品集合</param>
        /// <param name="indexPath">索引所在路径</param>
        /// <returns></returns>
        public static bool Insert(List<Article> articles, string indexPath)
        {
            bool indexFileIsExist = GlobalSettings.CheckFileExist(indexPath);
            if (!indexFileIsExist)
            {
                try
                {
                    GlobalSettings.EnsureDirectoryExists(indexPath);
                }
                catch { }
            }
            return Insert(articles, indexPath, !indexFileIsExist);
        }

        /// <summary>
        /// 加入索引
        /// </summary>
        /// <param name="users">加入索引的产品集合</param>
        /// <param name="indexPath">索引所在路径</param>
        /// <param name="createIndexFile">是否创建索引文件</param>
        public static bool Insert(List<Article> articles, string indexPath, bool createIndexFile)
        {
            if (articles == null || articles.Count == 0)
                return false;

            //Unlock Temp
            if (IndexReader.IsLocked(Path.GetTempPath()))
                IndexReader.Unlock(FSDirectory.GetDirectory(Path.GetTempPath(), false));

            FSDirectory fsDir = null;
            IndexWriter fsWriter = null;

            bool result = false;
            try
            {
                if (createIndexFile)
                {
                    fsDir = FSDirectory.GetDirectory(indexPath, true);
                    fsWriter = new IndexWriter(fsDir, SearchHelper.GetChineseAnalyzer(), true);
                }
                else
                {
                    fsDir = FSDirectory.GetDirectory(indexPath, false);
                    fsWriter = new IndexWriter(fsDir, SearchHelper.GetChineseAnalyzer(), false);
                }
                if (IndexReader.IsLocked(fsDir))
                    IndexReader.Unlock(fsDir);

                fsWriter.SetMergeFactor(SearchHelper.MergeFactor);
                fsWriter.SetMaxMergeDocs(SearchHelper.MaxMergeDocs);
                fsWriter.SetMaxBufferedDocs(SearchHelper.MinMergeDocs);

                foreach (Article article in articles)
                {
                    if (article != null)
                    {
                        Document doc = ConvertArticleToDocument(article);
                        if (doc != null)
                            fsWriter.AddDocument(doc);
                    }
                }
                fsWriter.Optimize();
                result = true;
            }
            catch (System.IO.IOException ex)
            {
                try
                {
                    string message = ex.Message;
                    if (message.IndexOf("@") >= 0)
                        message = message.Substring(message.IndexOf("@") + 1);
                    File.Delete(message);
                }
                catch
                {

                }
                throw ex;
            }
            finally
            {
                if (fsWriter != null)
                    fsWriter.Close();
            }
            return result;
        }
        #endregion

        #region InitializeIndex
        /// <summary>
        /// 初始化索引
        /// </summary>
        /// <param name="indexPath"></param>
        public static void InitializeIndex(string indexPath)
        {

            try
            {
                string[] oldIndexFiles = System.IO.Directory.GetFiles(indexPath, "*.*");
                foreach (var oldIndexFile in oldIndexFiles)
                {
                    System.IO.File.Delete(oldIndexFile);
                }
            }
            catch
            {

            }
            List<Article> articlesForIndex = new List<Article>();
            List<Article> articles = ArticleManager.GetAllArticles();

            //分多次进行索引
            int indexedCount = 0;
            for (int i = 0; i < articles.Count; i++)
            {
                indexedCount += 1;
                if (articles[i] != null)
                    articlesForIndex.Add(articles[i]);

                if (indexedCount >= 1000)
                {
                    Insert(articlesForIndex, indexPath);
                    articlesForIndex.Clear();
                    indexedCount = 0;
                }
            }
            Insert(articlesForIndex, indexPath);
        }

        /// <summary>
        /// 初始化索引
        /// </summary>
        public static void InitializeIndex()
        {
            try
            {
                InitializeIndex(PhysicalIndexDirectory);
            }
            catch (Exception ex)
            {
                new HHException(ExceptionType.UnknownError, ex.Message, ex).Log();
            }
        }
        #endregion

        #region Convert
        /// <summary>
        /// 将产品转换为索引文档
        /// </summary>
        /// <param name="pu"></param>
        /// <returns></returns>
        public static Document ConvertArticleToDocument(Article article)
        {
            Document doc = new Document();
            Field field;

            field = new Field(NewsIndexField.ArticleID, article.ID.ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED);
            doc.Add(field);

            field = new Field(NewsIndexField.Title, article.Title, Field.Store.YES, Field.Index.TOKENIZED);
            doc.Add(field);

            field = new Field(NewsIndexField.SubTitle, article.SubTitle, Field.Store.YES, Field.Index.TOKENIZED);
            doc.Add(field);

            field = new Field(NewsIndexField.Content, article.Content, Field.Store.YES, Field.Index.TOKENIZED);
            doc.Add(field);

            field = new Field(NewsIndexField.Abstract, article.Abstract, Field.Store.YES, Field.Index.TOKENIZED);
            doc.Add(field);

            if (!GlobalSettings.IsNullOrEmpty(article.Keywords))
            {
                foreach (string tagName in article.Keywords.Split(';'))
                {
                    field = new Field(NewsIndexField.Keywords, tagName, Field.Store.YES, Field.Index.TOKENIZED);
                    field.SetBoost(2.0F);
                    doc.Add(field);
                }
            }

            field = new Field(NewsIndexField.Date, DateTools.DateToString(DateTime.Now, DateTools.Resolution.DAY), Field.Store.YES, Field.Index.TOKENIZED);
            doc.Add(field);

            field = new Field(NewsIndexField.CategoryID, article.Category.ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED);
            doc.Add(field);

            return doc;
        }

        /// <summary>
        /// 将索引文档转换为产品
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static Article ConvertDocumentToArticle(Document doc)
        {
            Article article = new Article();
            article.ID = Convert.ToInt32(doc.Get(NewsIndexField.ArticleID));
            article.Abstract = doc.Get(NewsIndexField.Abstract);
            article.Author = doc.Get(NewsIndexField.Author);
            article.Content = doc.Get(NewsIndexField.Content);
            article.Category = Convert.ToInt32(doc.Get(NewsIndexField.CategoryID));
            article.CopyFrom = doc.Get(NewsIndexField.CopyFrom);
            article.Date = DateTools.StringToDate(doc.Get(NewsIndexField.Date));
            string[] tagNames = doc.GetValues(NewsIndexField.Keywords);
            article.Keywords = string.Join(";", tagNames);
            return article;
        }
        #endregion
    }
}
