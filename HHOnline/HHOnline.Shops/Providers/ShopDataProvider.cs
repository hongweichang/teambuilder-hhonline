using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Xml;
using HHOnline.Common;
using HHOnline.Framework;
using HHOnline.Framework.Providers;
using HHOnline.Shops.Enums;

namespace HHOnline.Shops.Providers
{
    /// <summary>
    /// Shop数据访问提供类
    /// </summary>
    public abstract class ShopDataProvider
    {
        #region cntor
        private static readonly ShopDataProvider _instance = null;

        static ShopDataProvider()
        {
            _instance = HHContainer.Create().Resolve<ShopDataProvider>();
        }

        public static ShopDataProvider Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        #region ProductBrand
        public abstract ProductBrand CreateUpdateBrand(ProductBrand brand, DataProviderAction action, out DataActionStatus status);

        public abstract DataActionStatus DeleteBrand(int brandID);

        public abstract ProductBrand GetBrand(int brandID);

        public abstract List<ProductBrand> GetBrands();

        public static ProductBrand PopulateBrandFromIDataReader(IDataReader dr)
        {
            ProductBrand brand = new ProductBrand();
            brand.BrandID = DataRecordHelper.GetInt32(dr, "BrandID");
            brand.BrandName = DataRecordHelper.GetString(dr, "BrandName");
            brand.BrandLogo = DataRecordHelper.GetString(dr, "BrandLogo");
            brand.BrandTitle = DataRecordHelper.GetString(dr, "BrandTitle");
            brand.BrandGroup = DataRecordHelper.GetString(dr, "BrandGroup");
            brand.BrandAbstract = DataRecordHelper.GetString(dr, "BrandAbstract");
            brand.BrandContent = DataRecordHelper.GetString(dr, "BrandContent");
            brand.DisplayOrder = DataRecordHelper.GetInt32(dr, "DisplayOrder");
            brand.BrandStatus = (ComponentStatus)DataRecordHelper.GetInt32(dr, "BrandStatus");
            brand.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime");
            brand.CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser");
            brand.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime");
            brand.UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser");
            brand.SetSerializerData(CommonDataProvider.PopulateSerializerDataIDataRecord(dr));
            return brand;
        }
        #endregion

        #region ProductIndustry
        public abstract List<ProductIndustry> GetIndustries();

        public abstract List<ProductIndustry> GetIndustriesByProductID(int productID);

        public abstract ProductIndustry CreateUpdateIndustry(ProductIndustry industry, DataProviderAction action, out DataActionStatus status);

        public abstract DataActionStatus DeleteIndustry(int industryID);

        public ProductIndustry PopulateIndustryFromIDataReader(IDataReader dr)
        {
            ProductIndustry industry = new ProductIndustry();
            industry.IndustryID = DataRecordHelper.GetInt32(dr, "IndustryID");
            industry.IndustryName = DataRecordHelper.GetString(dr, "IndustryName");
            industry.IndustryLogo = DataRecordHelper.GetString(dr, "IndustryLogo");
            industry.IndustryTitle = DataRecordHelper.GetString(dr, "IndustryTitle");
            industry.IndustryAbstract = DataRecordHelper.GetString(dr, "IndustryAbstract");
            industry.IndustryContent = DataRecordHelper.GetString(dr, "IndustryContent");
            industry.ParentID = DataRecordHelper.GetInt32(dr, "ParentID");
            industry.DisplayOrder = DataRecordHelper.GetInt32(dr, "DisplayOrder");
            industry.IndustryStatus = (ComponentStatus)DataRecordHelper.GetInt32(dr, "IndustryStatus");
            industry.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime");
            industry.CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser");
            industry.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime");
            industry.UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser");
            industry.SetSerializerData(CommonDataProvider.PopulateSerializerDataIDataRecord(dr));
            return industry;
        }
        #endregion

        #region ProductModel
        public abstract ProductModel CreateUpdateModel(ProductModel model, DataProviderAction action);

        public abstract bool DeleteModel(int modelID);

        public abstract ProductModel GetModel(int modelID);

        public abstract List<ProductModel> GetModelsByProductID(int productID);

        public ProductModel PopulateProductModelFromIDataReader(IDataReader dr)
        {
            ProductModel productModel = new ProductModel();
            productModel.ModelID = DataRecordHelper.GetInt32(dr, "ModelID");
            productModel.ProductID = DataRecordHelper.GetInt32(dr, "ProductID");
            productModel.ModelCode = DataRecordHelper.GetString(dr, "ModelCode");
            productModel.ModelName = DataRecordHelper.GetString(dr, "ModelName");
            productModel.ModelDesc = DataRecordHelper.GetString(dr, "ModelDesc");
            productModel.ModelStatus = (ComponentStatus)DataRecordHelper.GetInt32(dr, "ModelStatus");
            productModel.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime");
            productModel.CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser");
            productModel.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime");
            productModel.UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser");
            productModel.SetSerializerData(CommonDataProvider.PopulateSerializerDataIDataRecord(dr));
            return productModel;
        }
        #endregion

        #region ProductCategory
        public abstract ProductCategory CreateUpdateCategory(ProductCategory category, DataProviderAction action, out DataActionStatus status);

        public abstract DataActionStatus DeleteCategory(int categoryID);

        public abstract DataActionStatus DeleteCategory(string categoryIDList);

        public abstract ProductCategory GetCategory(int categoryID);

        public abstract List<ProductCategory> GetCategories();

        public abstract List<ProductCategory> GetCategoriesByPY(string firstLetter);

        public abstract List<ProductCategory> GetCategoreisByProductID(int productID);

        public static ProductCategory PopulateCategoryFromIDataReader(IDataReader dr)
        {
            ProductCategory category = new ProductCategory();
            category.CategoryID = DataRecordHelper.GetInt32(dr, "CategoryID");
            category.CategoryName = DataRecordHelper.GetString(dr, "CategoryName");
            category.CategoryDesc = DataRecordHelper.GetString(dr, "CategoryDesc");
            category.ParentID = DataRecordHelper.GetInt32(dr, "ParentID");
            category.PropertyID = DataRecordHelper.GetInt32(dr, "PropertyID");
            category.DisplayOrder = DataRecordHelper.GetInt32(dr, "DisplayOrder");
            category.CategoryStatus = (ComponentStatus)DataRecordHelper.GetInt32(dr, "CategoryStatus");
            category.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime");
            category.CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser");
            category.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime");
            category.UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser");
            return category;
        }

        #endregion

        #region ProductProperty
        public abstract ProductProperty CreateUpdateProperty(ProductProperty property, DataProviderAction action, out DataActionStatus status);

        public abstract DataActionStatus DeleteProperty(string propertyIDList);

        public abstract ProductProperty GetProperty(int propertyID);

        public abstract List<ProductProperty> GetPropertiesByCategoryID(int categoryID);

        public abstract List<ProductProperty> GetParentPropertiesByCategoryID(int categoryID);

        public abstract List<ProductProperty> GetPropertiesByProductID(int productID);

        public static ProductProperty PopulatePropertyValueFromIDataReader(IDataReader dr)
        {
            ProductProperty property = new ProductProperty();
            property.PropertyID = DataRecordHelper.GetInt32(dr, "PropertyID");
            property.PropertyName = DataRecordHelper.GetString(dr, "PropertyName");
            property.PropertyValue = DataRecordHelper.GetString(dr, "PropertyValue");
            return property;
        }

        public static ProductProperty PopulatePropertyFromIDataReader(IDataReader dr)
        {
            ProductProperty property = new ProductProperty();
            property.PropertyID = DataRecordHelper.GetInt32(dr, "PropertyID");
            property.PropertyName = DataRecordHelper.GetString(dr, "PropertyName");
            property.PropertyDesc = DataRecordHelper.GetString(dr, "PropertyDesc");
            property.CategoryID = DataRecordHelper.GetInt32(dr, "CategoryID");
            property.DimensionEnabled = (ComponentStatus)DataRecordHelper.GetInt32(dr, "DimensionEnabled");
            property.SubCategoryHidden = (SubCategoryHiddenType)DataRecordHelper.GetInt32(dr, "SubCategoryHidden");
            property.DisplayOrder = DataRecordHelper.GetInt32(dr, "DisplayOrder");
            property.PropertyStatus = (ComponentStatus)DataRecordHelper.GetInt32(dr, "PropertyStatus");
            property.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime");
            property.CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser");
            property.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime");
            property.UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser");
            return property;
        }

        #endregion

        #region ProductPicture
        public abstract ProductPicture CreateUpdatePicture(ProductPicture picture, DataProviderAction action, out DataActionStatus status);

        public abstract ProductPicture GetPicture(int pictureID);

        public abstract ProductPicture GetDefaultPicture(int productID);

        public abstract DataActionStatus DeletePicture(int pictureID);

        public abstract List<ProductPicture> GetPicturesByProductID(int productID);

        public static ProductPicture PopulateProductPictureFromIDataReader(IDataReader dr)
        {
            ProductPicture productPicture = new ProductPicture();
            productPicture.PictureID = DataRecordHelper.GetInt32(dr, "PictureID");
            productPicture.ProductID = DataRecordHelper.GetInt32(dr, "ProductID");
            productPicture.ModelID = DataRecordHelper.GetInt32(dr, "ModelID");
            productPicture.DisplayOrder = DataRecordHelper.GetInt32(dr, "DisplayOrder");
            productPicture.PictureName = DataRecordHelper.GetString(dr, "PictureName");
            productPicture.PictureFile = DataRecordHelper.GetString(dr, "PictureFile");
            productPicture.PictureStatus = (ComponentStatus)DataRecordHelper.GetInt32(dr, "PictureStatus");
            productPicture.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime");
            productPicture.CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser");
            productPicture.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime");
            productPicture.UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser");
            return productPicture;
        }
        #endregion

        #region Product
        public abstract Product CreateUpdateProduct(Product product, string categoryIDList, string industryIDList, List<ProductProperty> properties, DataProviderAction action, out DataActionStatus status);

        public abstract Product GetProduct(int productID);

        public abstract DataActionStatus DeleteProduct(int productID);

        public abstract List<Product> GetProducts(ProductQuery query, out int totalRecord);

        public static Product PopulateProductFromIDataReader(IDataReader dr)
        {
            Product product = new Product();
            product.ProductID = DataRecordHelper.GetInt32(dr, "ProductID");
            product.ProductCode = DataRecordHelper.GetString(dr, "ProductCode");
            product.ProductName = DataRecordHelper.GetString(dr, "ProductName");
            product.ProductDesc = DataRecordHelper.GetString(dr, "ProductDesc");
            product.ProductAbstract = DataRecordHelper.GetString(dr, "ProductAbstract");
            product.ProductContent = DataRecordHelper.GetString(dr, "ProductContent");
            product.BrandID = DataRecordHelper.GetInt32(dr, "BrandID");
            product.ProductKeywords = DataRecordHelper.GetString(dr, "ProductKeywords");
            product.ProductStatus = (ComponentStatus)DataRecordHelper.GetInt32(dr, "ProductStatus");
            product.DisplayOrder = DataRecordHelper.GetInt32(dr, "DisplayOrder");
            product.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime");
            product.CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser");
            product.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime");
            product.UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser");
            product.SetSerializerData(CommonDataProvider.PopulateSerializerDataIDataRecord(dr));
            return product;
        }
        #endregion

		#region ProductSupply

		public abstract ProductSupply GetProductSupply(int supplyID);

		public abstract ProductSupply GetProductSupply(int productID, int supplierID);

		public abstract ProductSupply UpdateProductSupply(ProductSupply ps, out DataActionStatus status);

		public static ProductSupply PopulateProductSupplyFromIDataReader(IDataReader dr)
		{
			ProductSupply result = new ProductSupply();

			result.ApplyTaxRate = DataRecordHelper.GetDecimal(dr, "ApplyTaxRate");
			result.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime");
			result.CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser");
			result.DeliverySpan = DataRecordHelper.GetString(dr, "DeliverySpan");
			result.IncludeFreight = (FreightIncludeType?)DataRecordHelper.GetNullableInt32(dr, "IncludeFreight");
			result.IncludeTax = (TaxIncludeType)DataRecordHelper.GetInt32(dr, "IncludeTax");
			result.ModelID = DataRecordHelper.GetNullableInt32(dr, "ModelID");
			result.ProductID = DataRecordHelper.GetInt32(dr, "ProductID");
			result.QuoteEnd = DataRecordHelper.GetDateTime(dr, "QuoteEnd");
			result.QuoteFrom = DataRecordHelper.GetDateTime(dr, "QuoteFrom");
			result.QuoteMOQ = DataRecordHelper.GetNullableInt32(dr, "QuoteMOQ");
			result.QuotePrice = DataRecordHelper.GetNullableDecimal(dr, "QuotePrice");
			result.QuoteRenewal = DataRecordHelper.GetInt32(dr, "QuoteRenewal");
			result.SupplierID = DataRecordHelper.GetInt32(dr, "SupplierID");
			result.SupplyID = DataRecordHelper.GetInt32(dr, "SupplyID");
			result.SupplyRegion = DataRecordHelper.GetNullableInt32(dr, "SupplyRegion");
			result.SupplyStatus = (ComponentStatus)DataRecordHelper.GetInt32(dr, "SupplyStatus");
			result.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime");
			result.UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser");
			result.WarrantySpan = DataRecordHelper.GetString(dr, "WarrantySpan");

			return result;
		}

		#endregion

		#region ProductPrice
		public abstract ProductPrice CreateUpdatePrice(ProductPrice price, DataProviderAction action, out DataActionStatus status);

        public abstract DataActionStatus DeletePrice(int priceID);

        public abstract ProductPrice GetPrice(int priceID);

        public abstract List<ProductPrice> GetPrices(int productID);

        public abstract decimal? GetMarketPrice(string areaIDList, int productID);

        public abstract decimal? GetGradePrice(List<string> filters, int productID, UserLevel level);

        public abstract decimal? GetDefaultPrice(int productID);

        public static ProductPrice PopulateProductPriceFromIDataReader(IDataReader dr)
        {
            ProductPrice productPrice = new ProductPrice();
            productPrice.PriceID = DataRecordHelper.GetInt32(dr, "PriceID");
            productPrice.ProductID = DataRecordHelper.GetInt32(dr, "ProductID");
            productPrice.ModelID = DataRecordHelper.GetInt32(dr, "ModelID");
            productPrice.DeliverySpan = DataRecordHelper.GetString(dr, "DeliverySpan");
            productPrice.WarrantySpan = DataRecordHelper.GetString(dr, "WarrantySpan");
            productPrice.IncludeFreight = (PriceIncludeType)DataRecordHelper.GetInt32(dr, "IncludeFreight");
            productPrice.IncludeTax = (PriceIncludeType)DataRecordHelper.GetInt32(dr, "IncludeTax");
            productPrice.ApplyTaxRate = DataRecordHelper.GetDecimal(dr, "ApplyTaxRate");
            productPrice.QuoteMOQ = DataRecordHelper.GetInt32(dr, "QuoteMOQ");
            productPrice.PriceMarket = DataRecordHelper.GetNullableDecimal(dr, "PriceMarket");
            productPrice.PricePromotion = DataRecordHelper.GetNullableDecimal(dr, "PricePromotion");
            productPrice.PriceFloor = DataRecordHelper.GetNullableDecimal(dr, "PriceFloor");
            productPrice.PricePurchase = DataRecordHelper.GetNullableDecimal(dr, "PricePurchase");
            productPrice.PriceGradeA = DataRecordHelper.GetNullableDecimal(dr, "PriceGradeA");
            productPrice.PriceGradeB = DataRecordHelper.GetNullableDecimal(dr, "PriceGradeB");
            productPrice.PriceGradeC = DataRecordHelper.GetNullableDecimal(dr, "PriceGradeC");
            productPrice.PriceGradeD = DataRecordHelper.GetNullableDecimal(dr, "PriceGradeD");
            productPrice.PriceGradeE = DataRecordHelper.GetNullableDecimal(dr, "PriceGradeE");
            productPrice.SupplyRegion = DataRecordHelper.GetInt32(dr, "SupplyRegion");
            productPrice.QuoteFrom = DataRecordHelper.GetDateTime(dr, "QuoteFrom");
            productPrice.QuoteEnd = DataRecordHelper.GetDateTime(dr, "QuoteEnd");
            productPrice.QuoteRenewal = DataRecordHelper.GetInt32(dr, "QuoteRenewal");
            productPrice.SupplyStatus = (ComponentStatus)DataRecordHelper.GetInt32(dr, "SupplyStatus");
            productPrice.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime");
            productPrice.CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser");
            productPrice.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime");
            productPrice.UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser");
            productPrice.SetSerializerData(CommonDataProvider.PopulateSerializerDataIDataRecord(dr));
            return productPrice;
        }
        #endregion

        #region ProductFocus
        public abstract ProductFocus CreateUpdateFocus(ProductFocus focus, DataProviderAction action, out DataActionStatus status);

        public abstract DataActionStatus DeleteFocus(int focusID);

        public abstract ProductFocus GetFocus(int focusID);

        public abstract List<ProductFocus> GetFocusList(FocusType focusType);


        public static ProductFocus PopulateProductFocusFromIDataReader(IDataReader dr)
        {
            ProductFocus productFocus = new ProductFocus();
            productFocus.FocusID = DataRecordHelper.GetInt32(dr, "FocusID");
            productFocus.FocusType = (FocusType)DataRecordHelper.GetInt32(dr, "FocusType");
            productFocus.FocusFrom = DataRecordHelper.GetDateTime(dr, "FocusFrom");
            productFocus.FocusEnd = DataRecordHelper.GetDateTime(dr, "FocusEnd");
            productFocus.ProductID = DataRecordHelper.GetInt32(dr, "ProductID");
            productFocus.ModelID = DataRecordHelper.GetInt32(dr, "ModelID");
            productFocus.DisplayOrder = DataRecordHelper.GetInt32(dr, "DisplayOrder");
            productFocus.FocusMemo = DataRecordHelper.GetString(dr, "FocusMemo");
            productFocus.FocusStatus = (ComponentStatus)DataRecordHelper.GetInt32(dr, "FocusStatus");
            productFocus.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime");
            productFocus.CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser");
            productFocus.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime");
            productFocus.UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser");
            productFocus.SetSerializerData(CommonDataProvider.PopulateSerializerDataIDataRecord(dr));
            return productFocus;
        }
        #endregion

        #region -Shopping-
        public abstract bool ShoppingAdd(Shopping shop);
        public abstract bool ShoppingTransfer(string oldUserID, string newUserID);
        public abstract bool ShoppingDelete(int shopID);
        public abstract bool ShoppingUpdate(Shopping shop);
        public abstract List<Shopping> ShoppingLoad(string userID);
        public static Shopping ReadShopping(IDataReader dr)
        {
            return new Shopping()
            {
                CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime"),
                ModelID = DataRecordHelper.GetInt32(dr, "ModelID"),
                ProductID = DataRecordHelper.GetInt32(dr, "ProductID"),
                Quantity = DataRecordHelper.GetInt32(dr, "Quantity"),
                ShoppingMemo = DataRecordHelper.GetString(dr, "ShoppingMemo"),
                ShopppingID = DataRecordHelper.GetInt32(dr, "ShoppingID"),
                UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime"),
                UserID = DataRecordHelper.GetString(dr, "UserID"),
            };
        }
        #endregion
    }
}
