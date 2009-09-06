using System;
using System.Collections.Generic;
using System.IO;
using HHOnline.Common;
using HHOnline.Framework;
using HHOnline.Shops;
using Lucene.Net.Search;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.QueryParsers;
using Lucene.Net.Documents;

namespace HHOnline.SearchBarrel
{
    /// <summary>
    /// 产品查询管理类
    /// </summary>
    public class ProductSearchManager
    {
        #region Property
        protected static readonly int MaxNumFragmentsRequired = 2;

        protected static readonly string FragmentSeparator = "...";

        /// <summary>
        /// 索引文件目录名称
        /// </summary>
        public static readonly string IndexFileDirectory = "Product";

        /// <summary>
        /// 索引文件物理路径
        /// </summary>
        public static string PhysicalIndexDirectory
        {
            get
            {
                return Path.Combine(GlobalSettings.IndexDirectory, IndexFileDirectory);
            }
        }
        #endregion

        #region Search
        public static SearchResultDataSet<Product> Search(ProductQuery query)
        {
            //索引文件不存在时，返回null
            if (!GlobalSettings.CheckFileExist(PhysicalIndexDirectory))
                return new SearchResultDataSet<Product>();
            DateTime startTime = DateTime.Now;
            BooleanQuery currentQuery = new BooleanQuery();

            //BrandID
            if (query.BrandID.HasValue && query.BrandID.Value != 0)
            {
                Term brandIDTearm = new Term(ProductIndexField.BrandID, query.BrandID.ToString());
                Query brandIDQuery = new TermQuery(brandIDTearm);
                currentQuery.Add(brandIDQuery, BooleanClause.Occur.MUST);
            }

            //CategoryID
            if (query.CategoryID.HasValue && query.CategoryID.Value != 0)
            {
                Term categoryIDTearm = new Term(ProductIndexField.CategoryID, query.CategoryID.ToString());
                Query categoryIDQuery = new TermQuery(categoryIDTearm);
                currentQuery.Add(categoryIDQuery, BooleanClause.Occur.MUST);
            }

            //KeyWord
            if (!string.IsNullOrEmpty(query.ProductNameFilter))
            {
                query.ProductNameFilter = SearchHelper.LuceneKeywordsScrubber(query.ProductNameFilter);
                if (!string.IsNullOrEmpty(query.ProductNameFilter))
                {
                    string[] searchFieldsForKeyword = new string[2];
                    searchFieldsForKeyword[0] = ProductIndexField.ProductName;
                    searchFieldsForKeyword[1] = ProductIndexField.ProductAbstract;

                    MultiFieldQueryParser productNameQueryParser = new MultiFieldQueryParser(searchFieldsForKeyword, SearchHelper.GetChineseAnalyzer());
                    productNameQueryParser.SetLowercaseExpandedTerms(true);
                    productNameQueryParser.SetDefaultOperator(QueryParser.OR_OPERATOR);

                    string keyWordsSplit = SearchHelper.SplitKeywordsBySpace(query.ProductNameFilter);
                    Query productNameQuery = productNameQueryParser.Parse(keyWordsSplit);
                    currentQuery.Add(productNameQuery, BooleanClause.Occur.MUST);
                }
            }

            //Search
            IndexSearcher searcher = new IndexSearcher(PhysicalIndexDirectory);
            Hits hits = searcher.Search(currentQuery);
            SearchResultDataSet<Product> products = new SearchResultDataSet<Product>();
            int pageLowerBound = query.PageIndex * query.PageSize;
            int pageUpperBound = pageLowerBound + query.PageSize;
            if (pageUpperBound > hits.Length())
                pageUpperBound = hits.Length();

            //HighLight
            PanGu.HighLight.Highlighter highlighter = null;
            if (!string.IsNullOrEmpty(query.ProductNameFilter))
            {
                highlighter = new PanGu.HighLight.Highlighter(new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"#c60a00\">", "</font>"), new PanGu.Segment());
                highlighter.FragmentSize = 100;
            }
            for (int i = pageLowerBound; i < pageUpperBound; i++)
            {
                Product item = ConvertDocumentToProduct(hits.Doc(i));
                if (!string.IsNullOrEmpty(query.ProductNameFilter))
                {
                    string bestBody = null;
                    if (!string.IsNullOrEmpty(item.ProductAbstract) && item.ProductAbstract.Length > MaxNumFragmentsRequired)
                        bestBody = highlighter.GetBestFragment(query.ProductNameFilter, item.ProductAbstract);

                    if (!string.IsNullOrEmpty(bestBody))
                        item.ProductAbstract = bestBody;
                    else
                        item.ProductAbstract = HtmlHelper.TrimHtml(item.ProductAbstract, 100);

                    string bestSubject = null;
                    if (!string.IsNullOrEmpty(item.ProductName) && item.ProductName.Length > MaxNumFragmentsRequired)
                        bestSubject = highlighter.GetBestFragment(query.ProductNameFilter, item.ProductName);

                    if (!string.IsNullOrEmpty(bestSubject))
                        item.ProductName = bestSubject;
                }
                products.Records.Add(item);
            }
            searcher.Close();
            products.TotalRecords = hits.Length();

            DateTime endTime = DateTime.Now;
            products.SearchDuration = (endTime.Ticks - startTime.Ticks) / 1E7f;
            products.PageIndex = query.PageIndex;
            products.PageSize = query.PageSize;

            return products;
        }
        #endregion

        #region Insert
        /// <summary>
        /// 加入索引
        /// </summary>
        /// <param name="products">加入索引的产品集合</param>
        /// <returns></returns>
        public static bool Insert(List<Product> products)
        {
            return Insert(products, PhysicalIndexDirectory);
        }

        /// <summary>
        /// 在indexPath位置建立索引
        /// </summary>
        /// <param name="products">加入索引的产品集合</param>
        /// <param name="indexPath">索引所在路径</param>
        /// <returns></returns>
        public static bool Insert(List<Product> products, string indexPath)
        {
            bool indexFileIsExist = GlobalSettings.CheckFileExist(indexPath);
            if (!indexFileIsExist)
            {
                try
                {
                    GlobalSettings.EnsureDirectoryExists(indexPath);
                    //System.IO.Directory.CreateDirectory(indexPath);
                }
                catch { }
            }
            return Insert(products, indexPath, !indexFileIsExist);
        }

        /// <summary>
        /// 加入索引
        /// </summary>
        /// <param name="users">加入索引的产品集合</param>
        /// <param name="indexPath">索引所在路径</param>
        /// <param name="createIndexFile">是否创建索引文件</param>
        public static bool Insert(List<Product> products, string indexPath, bool createIndexFile)
        {
            if (products == null || products.Count == 0)
                return false;

            FSDirectory fsDir;
            IndexWriter fsWriter;

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
            fsWriter.SetMergeFactor(SearchHelper.MergeFactor);
            fsWriter.SetMaxMergeDocs(SearchHelper.MaxMergeDocs);
            fsWriter.SetMaxBufferedDocs(SearchHelper.MinMergeDocs);

            bool result = false;
            try
            {
                foreach (Product product in products)
                {
                    if (product != null)
                    {
                        Document doc = ConvertProductToDocument(product);
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
        #endregion

        #region Update
        /// <summary>
        /// 更新索引
        /// </summary>
        public static bool Update(List<Product> products)
        {
            if (products == null || products.Count == 0)
                return false;

            int[] productIDList = new int[products.Count];

            for (int i = 0; i < products.Count; i++)
            {
                productIDList[i] = products[i].ProductID;
            }

            bool result = Delete(productIDList, false);

            if (result)
                result = Insert(products);

            return result;
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除索引
        /// </summary>
        public static bool Delete(int[] productIDList)
        {
            return Delete(productIDList, true);
        }

        /// <summary>
        /// 删除索引
        /// </summary>
        public static bool Delete(int[] productIDList, bool needOptimize)
        {
            if (productIDList == null && productIDList.Length == 0)
                return false;
            Lucene.Net.Store.Directory fsDir = FSDirectory.GetDirectory(PhysicalIndexDirectory, false);
            IndexReader reader = IndexReader.Open(fsDir);

            bool result = false;
            try
            {
                for (int i = 0; i < productIDList.Length; i++)
                {
                    if (productIDList[i] != 0)
                    {
                        Term term = new Term(ProductIndexField.ProductID, productIDList[i].ToString());
                        reader.DeleteDocuments(term);
                    }
                }
                reader.Close();

                if (needOptimize)
                {
                    IndexWriter fsWriter = new IndexWriter(fsDir, SearchHelper.GetChineseAnalyzer(), false);
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

            ProductQuery query = new ProductQuery();
            query.HasPublished = true;
            query.PageSize = Int32.MaxValue;
            List<Product> productsForIndex = new List<Product>();
            List<Product> products = Products.GetProductList(query);

            //分多次进行索引
            int indexedCount = 0;
            for (int i = 0; i < products.Count; i++)
            {
                indexedCount += 1;
                if (products[i] != null)
                    productsForIndex.Add(products[i]);

                if (indexedCount >= 1000)
                {
                    Insert(productsForIndex, indexPath);
                    productsForIndex.Clear();
                    indexedCount = 0;
                }
            }
            Insert(products, indexPath);
        }

        public static void InitializeIndex()
        {
            InitializeIndex(PhysicalIndexDirectory);
        }
        #endregion

        #region Convert
        /// <summary>
        /// 将产品转换为索引文档
        /// </summary>
        /// <param name="pu"></param>
        /// <returns></returns>
        public static Document ConvertProductToDocument(Product product)
        {
            Document doc = new Document();
            Field field;

            field = new Field(ProductIndexField.ProductID, product.ProductID.ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED);
            doc.Add(field);

            field = new Field(ProductIndexField.ProductName, product.ProductName.ToString(), Field.Store.YES, Field.Index.TOKENIZED);
            doc.Add(field);

            field = new Field(ProductIndexField.ProductContent, product.ProductContent.ToString(), Field.Store.YES, Field.Index.TOKENIZED);
            doc.Add(field);

            field = new Field(ProductIndexField.ProductAbstract, product.ProductAbstract.ToString(), Field.Store.YES, Field.Index.TOKENIZED);
            doc.Add(field);

            if (!GlobalSettings.IsNullOrEmpty(product.ProductKeywords))
            {
                foreach (string tagName in product.ProductKeywords.Split(';'))
                {
                    field = new Field(ProductIndexField.ProductKeywords, tagName, Field.Store.YES, Field.Index.TOKENIZED);
                    field.SetBoost(2.0F);
                    doc.Add(field);
                }
            }

            field = new Field(ProductIndexField.DateCreated, DateTools.DateToString(DateTime.Now, DateTools.Resolution.DAY), Field.Store.YES, Field.Index.TOKENIZED);
            doc.Add(field);

            field = new Field(ProductIndexField.BrandID, product.BrandID.ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED);
            doc.Add(field);

            field = new Field(ProductIndexField.BrandName, product.BrandName.ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED);
            doc.Add(field);

            return doc;
        }

        /// <summary>
        /// 将索引文档转换为产品
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static Product ConvertDocumentToProduct(Document doc)
        {
            Product product = new Product();
            product.ProductID = Convert.ToInt32(doc.Get(ProductIndexField.ProductID));
            product.ProductName = doc.Get(ProductIndexField.ProductName);
            product.BrandID = Convert.ToInt32(doc.Get(ProductIndexField.BrandID));
            product.ProductContent = doc.Get(ProductIndexField.ProductContent);
            product.ProductAbstract = doc.Get(ProductIndexField.ProductAbstract);
            product.CreateTime = DateTools.StringToDate(doc.Get(ProductIndexField.DateCreated));
            string[] tagNames = doc.GetValues(ProductIndexField.ProductKeywords);
            product.ProductKeywords = string.Join(";", tagNames);
            return product;
        }
        #endregion
    }
}
