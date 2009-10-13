using System;
using System.Collections.Specialized;
using HHOnline.Framework;

namespace HHOnline.Shops
{
    public enum ProviderFilter
    {
        All = 1,
        Inspect = 2,
        Deny = 3
    }
	/// <summary>
	/// 产品查询类
	/// </summary>
	public class ProductQuery
	{
		private int pageIndex = 0;
		private int pageSize = 20;
		private int? brandID;
		private int? categoryID;
		private int? propertyID;
		private int? industryID;
		private int? productID;
		private string productNameFilter;
		private string keywordsFilter;
		private bool? hasPictures;
		private bool? hasPublished;
		private bool? hasPrice;
		private decimal? productBeginPrice;
		private decimal? productEndPrice;
		private FocusType? focusType;
		private int? companyID;
        private ProviderFilter? providerFilter = null;
        public ProviderFilter? Filter
        {
            get { return providerFilter; }
            set { providerFilter = value; }
        }
		/// <summary>
		/// 产品排序依据
		/// </summary>
		public ProductOrderBy ProductOrderBy = ProductOrderBy.DisplayOrder;

		/// <summary>
		/// 排序方向
		/// </summary>
		public SortOrder SortOrder = SortOrder.Ascending;

		/// <summary>
		/// 页索引
		/// </summary>
		public int PageIndex
		{
			get
			{
				if (this.pageIndex >= 0)
				{
					return this.pageIndex;
				}
				return 0;
			}
			set
			{
				this.pageIndex = value;
			}
		}

		/// <summary>
		/// 供应商ID
		/// </summary>
		public int? CompanyID
		{
			get { return companyID; }
			set { companyID = value; }
		}

		/// <summary>
		/// 页大小
		/// </summary>
		public int PageSize
		{
			get
			{
				return this.pageSize;
			}
			set
			{
				this.pageSize = value;
			}
		}

		/// <summary>
		/// 是否有图
		/// </summary>
		public bool? HasPictures
		{
			get
			{
				return hasPictures;
			}
			set
			{
				hasPictures = value;
			}
		}

		/// <summary>
		/// 是否已发布
		/// </summary>
		public bool? HasPublished
		{
			get
			{
				return hasPublished;
			}
			set
			{
				hasPublished = value;
			}
		}

		/// <summary>
		/// 是否已报价
		/// </summary>
		public bool? HasPrice
		{
			get
			{
				return hasPrice;
			}
			set
			{
				hasPrice = value;
			}
		}

		/// <summary>
		/// 关注类型
		/// </summary>
		public FocusType? FocusType
		{
			get
			{
				return focusType;
			}
			set
			{
				focusType = value;
			}
		}

		/// <summary>
		/// 产品名称过滤
		/// </summary>
		public string ProductNameFilter
		{
			get
			{
				return GlobalSettings.CleanSearchString(productNameFilter);
			}
			set
			{
				productNameFilter = value;
			}
		}

		/// <summary>
		/// 产品关键字过滤
		/// </summary>
		public string ProductKeywordsFilter
		{
			get
			{
				return GlobalSettings.CleanSearchString(keywordsFilter);
			}
			set
			{
				keywordsFilter = value;
			}
		}

		/// <summary>
		/// 根据标签过滤
		/// </summary>
		public string[] Tags = null;

		/// <summary>
		/// 产品编号
		/// </summary>
		public int? ProductID
		{
			get
			{
				return productID;
			}
			set
			{
				productID = value;
			}
		}

		/// <summary>
		/// 产品品牌
		/// </summary>
		public int? BrandID
		{
			get
			{
				return brandID;
			}
			set
			{
				brandID = value;
			}
		}
		/// <summary>
		/// 产品属性
		/// </summary>
		public int? PropertyID
		{
			get
			{
				return propertyID;
			}
			set
			{
				propertyID = value;
			}
		}
		/// <summary>
		/// 产品行业
		/// </summary>
		public int? IndustryID
		{
			get
			{
				return industryID;
			}
			set
			{
				industryID = value;
			}
		}

		/// <summary>
		/// 产品分类
		/// </summary>
		public int? CategoryID
		{
			get
			{
				return categoryID;
			}
			set
			{
				categoryID = value;
			}
		}

		/// <summary>
		/// 价格开始区间
		/// </summary>
		public decimal? ProductBeginPrice
		{
			get
			{
				return productBeginPrice;
			}
			set
			{
				productBeginPrice = value;
			}
		}

		/// <summary>
		/// 价格结束区间
		/// </summary>
		public decimal? ProductEndPrice
		{
			get
			{
				return productEndPrice;
			}
			set
			{
				productEndPrice = value;
			}
		}

		/// <summary>
		/// ProductQuery CacheKey
		/// </summary>
		/// <param name="userQuery"></param>
		/// <returns></returns>
		public string GetQueryKey()
		{
			return CacheKeyManager.ProductListKey + string.Format(
				"PI{0}PS{1}SB{2}SO{3}PN{4}PK{5}DI{6}BI{7}CI{8}HP{9}PR{10}PB{11}FT{12}SI{13}",
				this.PageIndex,
				this.PageSize,
				this.ProductOrderBy,
				this.SortOrder,
				this.ProductNameFilter,
				this.ProductKeywordsFilter,
				this.ProductID.HasValue ? this.ProductID.Value : -1,
				this.BrandID.HasValue ? this.BrandID.Value : -1,
				this.CategoryID.HasValue ? this.CategoryID.Value : -1,
				this.HasPictures.HasValue ? Convert.ToInt32(this.HasPictures.Value) : -1,
				this.HasPrice.HasValue ? Convert.ToInt32(this.HasPrice.Value) : -1,
				this.HasPublished.HasValue ? Convert.ToInt32(this.HasPublished.Value) : -1,
				this.FocusType.HasValue ? ((int)this.FocusType.Value) : -1,
				this.CompanyID.HasValue ? this.CompanyID.Value : -1
				);
		}

		/// <summary>
		/// 根据QueryString生成查询对象
		/// </summary>
		/// <param name="queryString"></param>
		/// <returns></returns>
		public static ProductQuery GetQueryFromQueryString(NameValueCollection queryString)
		{
			ProductQuery query = new ProductQuery();
            if (!GlobalSettings.IsNullOrEmpty(queryString["p"]))
            {
                try
                {
                    query.Filter = (ProviderFilter)(int.Parse(queryString["p"]));
                }
                catch
                {
                    query.Filter = null;
                }
            }
			//ProductName
			if (!GlobalSettings.IsNullOrEmpty(queryString["pn"]))
				query.ProductNameFilter = queryString["pn"];
			//ProductID
			if (!GlobalSettings.IsNullOrEmpty(queryString["di"]))
			{
				query.ProductID = Convert.ToInt32(queryString["di"]);
				Product product = Products.GetProduct(query.ProductID.Value);
				if (product != null)
				{
					query.ProductNameFilter = product.ProductName;
				}
			}

			//BrandID
			if (!GlobalSettings.IsNullOrEmpty(queryString["bi"]))
				query.BrandID = Convert.ToInt32(queryString["bi"]);

			//IndustryID
			if (!GlobalSettings.IsNullOrEmpty(queryString["ii"]))
				query.IndustryID = Convert.ToInt32(queryString["ii"]);

			//CategoryID
			if (!GlobalSettings.IsNullOrEmpty(queryString["ci"]))
				query.CategoryID = Convert.ToInt32(queryString["ci"]);

			//CompanyID
			if (!GlobalSettings.IsNullOrEmpty(queryString["si"]))
				query.CompanyID = Convert.ToInt32(queryString["si"]);

			//HasPictures
			if (!GlobalSettings.IsNullOrEmpty(queryString["hp"]))
			{
				int pi = Convert.ToInt32(queryString["hp"]);
				query.HasPictures = (pi == 1);
			}

			//HasPrice
			if (!GlobalSettings.IsNullOrEmpty(queryString["pr"]))
			{
				int pr = Convert.ToInt32(queryString["pr"]);
				query.HasPrice = (pr == 1);
			}

			//HasPublished
			if (!GlobalSettings.IsNullOrEmpty(queryString["pb"]))
			{
				int pb = Convert.ToInt32(queryString["pb"]);
				query.HasPublished = (pb == 1);
			}

			//ProductOrderBy
			try
			{
				ProductOrderBy sortBy = (ProductOrderBy)Enum.Parse(typeof(ProductOrderBy), GlobalSettings.IsNullOrEmpty(queryString["sb"]) ? "1" : int.Parse(queryString["sb"]).ToString(), true);
				query.ProductOrderBy = sortBy;
			}
			catch
			{
				query.ProductOrderBy = ProductOrderBy.DisplayOrder;
			}

			// Sort Order
			try
			{
				SortOrder sortOrder = (SortOrder)Enum.Parse(typeof(SortOrder), GlobalSettings.IsNullOrEmpty(queryString["so"]) ? "1" : int.Parse(queryString["so"]).ToString(), true);
				query.SortOrder = sortOrder;
			}
			catch
			{
				query.SortOrder = SortOrder.Ascending;
			}
			return query;
		}
	}
}
