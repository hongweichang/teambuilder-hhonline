using System;
using System.Collections.Generic;
using System.IO;
using HHOnline.Framework;
using HHOnline.Shops;
using Lucene.Net.Search;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.QueryParsers;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;

namespace HHOnline.SearchBarrel
{
    /// <summary>
    /// 产品查询管理类
    /// </summary>
    public class ProductSearchManager
    {
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
            return Insert(products, indexPath, true);
        }

        /// <summary>
        /// 加入索引
        /// </summary>
        /// <param name="users">加入索引的产品集合</param>
        /// <param name="indexPath">索引所在路径</param>
        /// <param name="createIndexFile">是否创建索引文件</param>
        public static bool Insert(List<Product> product, string indexpath, bool createIndexFile)
        {
            return true;
        }

        /// <summary>
        /// 更新索引
        /// </summary>
        public bool Update(IList<Product> products)
        {
            return true;
        }

        /// <summary>
        /// 删除索引
        /// </summary>
        public bool Delete(int[] productIDList)
        {
            return Delete(productIDList, true);
        }

        /// <summary>
        /// 删除索引
        /// </summary>
        public bool Delete(int[] productIDList, bool needOptimize)
        {
            return true;
        }

        /// <summary>
        /// 将产品转换为索引文档
        /// </summary>
        /// <param name="pu"></param>
        /// <returns></returns>
        public static Document ConvertProductToDocument(Product pu)
        {
            return null;
        }

        /// <summary>
        /// 将索引文档转换为产品
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static Product ConvertDocumentToProduct(Document doc)
        {
            return null;
        }
    }
}
