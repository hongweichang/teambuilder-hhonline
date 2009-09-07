using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Xml;
using System.Text;
using HHOnline.Common;
using HHOnline.Framework;
using HHOnline.Shops.Providers;
using HHOnline.Shops;

namespace HHOnline.Data
{
    public class SqlShopDataProvider : ShopDataProvider
    {
        #region Brand
        public override ProductBrand CreateUpdateBrand(ProductBrand brand, DataProviderAction action, out DataActionStatus status)
        {
            ELParameter paramID = null;
            if (action == DataProviderAction.Create)
            {
                paramID = new ELParameter("@BrandID", DbType.Int32, 4, ParameterDirection.Output);
            }
            else
            {
                paramID = new ELParameter("@BrandID", DbType.Int32, brand.BrandID);
            }

            ELParameter[] elParameters = new ELParameter[]{
                paramID,
                new ELParameter("@BrandName",DbType.String,brand.BrandName),
                new ELParameter("@BrandLogo",DbType.String,brand.BrandLogo),
                new ELParameter("@BrandTitle",DbType.String,brand.BrandTitle),
                new ELParameter("@BrandGroup",DbType.String,brand.BrandGroup),
                new ELParameter("@BrandAbstract",DbType.String,brand.BrandAbstract),
                new ELParameter("@BrandContent",DbType.String,brand.BrandContent),
                new ELParameter("@DisplayOrder",DbType.Int32,brand.DisplayOrder),
                new ELParameter("@BrandStatus",DbType.Int32,brand.BrandStatus),
                new ELParameter("@Operator",DbType.Int32,GlobalSettings.GetCurrentUser().UserID),
                new ELParameter("@PropertyNames",DbType.String,brand.GetSerializerData().Keys),
                new ELParameter("@PropertyValues",DbType.String,brand.GetSerializerData().Values),
                new ELParameter("@Action",DbType.Int32,action),
                };
            status = (DataActionStatus)Convert.ToInt32(
                DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ProductBrand_CreateUpdate", elParameters));
            if (action == DataProviderAction.Create && status == DataActionStatus.Success)
                brand.BrandID = Convert.ToInt32(paramID.Value);
            return brand;
        }

        public override DataActionStatus DeleteBrand(int brandID)
        {
            ELParameter paramID = new ELParameter("@BrandID", DbType.Int32, brandID);
            return (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ProductBrand_Delete", paramID));
        }

        public override ProductBrand GetBrand(int brandID)
        {
            ELParameter paramID = new ELParameter("@BrandID", DbType.Int32, brandID);
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ProductBrand_Get", paramID))
            {
                ProductBrand brand = null;
                if (dr.Read())
                {
                    brand = PopulateBrandFromIDataReader(dr);
                }
                return brand;
            }
        }

        public override List<ProductBrand> GetBrands()
        {
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ProductBrands_Get"))
            {
                List<ProductBrand> brands = new List<ProductBrand>();
                while (dr.Read())
                {
                    brands.Add(PopulateBrandFromIDataReader(dr));
                }
                return brands;
            }
        }
        #endregion

        #region Industry
        public override List<ProductIndustry> GetIndustries()
        {
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ProductIndustries_Get"))
            {
                List<ProductIndustry> industries = new List<ProductIndustry>();
                while (dr.Read())
                {
                    industries.Add(PopulateIndustryFromIDataReader(dr));
                }
                return industries;
            }
        }

        public override List<ProductIndustry> GetIndustriesByProductID(int productID)
        {
            ELParameter paramID = new ELParameter("@ProductID", DbType.String, productID);
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ProductIndustries_GetByProductID", paramID))
            {
                List<ProductIndustry> industries = new List<ProductIndustry>();
                while (dr.Read())
                {
                    industries.Add(PopulateIndustryFromIDataReader(dr));
                }
                return industries;
            }
        }

        public override ProductIndustry CreateUpdateIndustry(ProductIndustry industry, DataProviderAction action, out DataActionStatus status)
        {
            ELParameter paramID = null;
            if (action == DataProviderAction.Create)
            {
                paramID = new ELParameter("@IndustryID", DbType.Int32, 4, ParameterDirection.Output);
            }
            else
            {
                paramID = new ELParameter("@IndustryID", DbType.Int32, industry.IndustryID);
            }
            ELParameter[] elParameters = new ELParameter[]{
	            paramID,
                new ELParameter("@IndustryName",DbType.String,industry.IndustryName),
                new ELParameter("@IndustryLogo",DbType.String,industry.IndustryLogo),
                new ELParameter("@IndustryTitle",DbType.String,industry.IndustryTitle),
                new ELParameter("@IndustryAbstract",DbType.String,industry.IndustryAbstract),
                new ELParameter("@IndustryContent",DbType.String,industry.IndustryContent),
                new ELParameter("@ParentID",DbType.Int32,DataHelper.IntOrNull(industry.ParentID)),
                new ELParameter("@DisplayOrder",DbType.Int32,industry.DisplayOrder),
                new ELParameter("@IndustryStatus",DbType.Int32,industry.IndustryStatus),
                new ELParameter("@Operator",DbType.Int32,GlobalSettings.GetCurrentUser().UserID),
                new ELParameter("@PropertyNames",DbType.String,industry.GetSerializerData().Keys),
                new ELParameter("@PropertyValues",DbType.String,industry.GetSerializerData().Values),
                new ELParameter("@Action",DbType.Int32,action),
            };
            status = (DataActionStatus)Convert.ToInt32(
                DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ProductIndustry_CreateUpdate", elParameters));
            if (action == DataProviderAction.Create && status == DataActionStatus.Success)
                industry.IndustryID = Convert.ToInt32(paramID.Value);
            return industry;
        }

        public override DataActionStatus DeleteIndustry(int industryID)
        {
            ELParameter paramID = new ELParameter("@IndustryID", DbType.Int32, industryID);
            return (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ProductIndustry_Delete", paramID));
        }
        #endregion

        #region Category
        public override ProductCategory CreateUpdateCategory(ProductCategory category, DataProviderAction action, out DataActionStatus status)
        {
            ELParameter paramID = null;
            if (action == DataProviderAction.Create)
                paramID = new ELParameter("@CategoryID", DbType.Int32, 4, ParameterDirection.Output);
            else
                paramID = new ELParameter("@CategoryID", DbType.Int32, category.CategoryID);

            ELParameter[] elParameters = new ELParameter[]{
                paramID,
                new ELParameter("@CategoryName",DbType.String,category.CategoryName),
                new ELParameter("@CategoryDesc",DbType.String,category.CategoryDesc),
                new ELParameter("@ParentID",DbType.Int32,DataHelper.IntOrNull(category.ParentID)),
                new ELParameter("@PropertyID",DbType.Int32,category.PropertyID),
                new ELParameter("@DisplayOrder",DbType.Int32,category.DisplayOrder),
                new ELParameter("@CategoryStatus",DbType.Int32,category.CategoryStatus),
                new ELParameter("@Operator",DbType.Int32,GlobalSettings.GetCurrentUser().UserID),
                new ELParameter("@Action",DbType.Int32,action)
            };
            status = (DataActionStatus)Convert.ToInt32(
                DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ProductCategory_CreateUpdate", elParameters));
            if (action == DataProviderAction.Create && status == DataActionStatus.Success)
                category.CategoryID = Convert.ToInt32(paramID.Value);
            return category;
        }

        public override DataActionStatus DeleteCategory(int categoryID)
        {
            ELParameter paramID = new ELParameter("@CategoryID", DbType.Int32, categoryID);
            return (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ProductCategory_Delete", paramID));
        }

        public override DataActionStatus DeleteCategory(string categoryIDList)
        {
            ELParameter paramID = new ELParameter("@CategoryIDList", DbType.String, categoryIDList);
            return (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ProductCategories_Delete", paramID));
        }

        public override ProductCategory GetCategory(int categoryID)
        {
            ELParameter paramID = new ELParameter("@CategoryID", DbType.Int32, categoryID);
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ProductCategory_Get"))
            {
                ProductCategory category = null;
                if (dr.Read())
                {
                    category = PopulateCategoryFromIDataReader(dr);
                }
                return category;
            }
        }

        public override List<ProductCategory> GetCategories()
        {
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ProductCategories_Get"))
            {
                List<ProductCategory> categories = new List<ProductCategory>();
                while (dr.Read())
                {
                    categories.Add(PopulateCategoryFromIDataReader(dr));
                }
                return categories;
            }
        }

        public override List<ProductCategory> GetCategoriesByPY(string firstLetter)
        {
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@FirstLetter",DbType.String,firstLetter)
            };
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ProductCategories_GetByPY", elParameters))
            {
                List<ProductCategory> categories = new List<ProductCategory>();
                while (dr.Read())
                {
                    categories.Add(PopulateCategoryFromIDataReader(dr));
                }
                return categories;
            }
        }

        public override List<ProductCategory> GetCategoreisByProductID(int productID)
        {
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@ProductID",DbType.Int32,productID)
            };
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ProductCategories_GetByProductID", elParameters))
            {
                List<ProductCategory> categories = new List<ProductCategory>();
                while (dr.Read())
                {
                    categories.Add(PopulateCategoryFromIDataReader(dr));
                }
                return categories;
            }
        }
        #endregion

        #region Property
        public override ProductProperty CreateUpdateProperty(ProductProperty property, DataProviderAction action, out DataActionStatus status)
        {
            ELParameter paramID = null;
            if (action == DataProviderAction.Create)
                paramID = new ELParameter("@PropertyID", DbType.Int32, 4, ParameterDirection.Output);
            else
                paramID = new ELParameter("@PropertyID", DbType.Int32, property.PropertyID);

            ELParameter[] elParameters = new ELParameter[]{
	            paramID,
                new ELParameter("@PropertyName",DbType.String,property.PropertyName),
                new ELParameter("@PropertyDesc",DbType.String,property.PropertyDesc),
                new ELParameter("@CategoryID",DbType.Int32,property.CategoryID),
                new ELParameter("@DimensionEnabled",DbType.Int32,property.DimensionEnabled),
                new ELParameter("@SubCategoryHidden",DbType.Int32,property.SubCategoryHidden),
                new ELParameter("@DisplayOrder",DbType.Int32,property.DisplayOrder),
                new ELParameter("@PropertyStatus",DbType.Int32,property.PropertyStatus),
                new ELParameter("@Operator",DbType.Int32,GlobalSettings.GetCurrentUser().UserID),
                new ELParameter("@Action",DbType.Int32,action)
            };
            status = (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure,
                "sp_ProductProperty_CreateUpdate", elParameters));

            if (action == DataProviderAction.Create && status == DataActionStatus.Success)
                property.PropertyID = Convert.ToInt32(paramID.Value);

            return property;
        }

        public override DataActionStatus DeleteProperty(string propertyIDList)
        {
            ELParameter paramID = new ELParameter("@PropertyIDList", DbType.String, propertyIDList);
            return (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ProductProperties_Delete", paramID));
        }

        public override ProductProperty GetProperty(int propertyID)
        {
            ELParameter paramID = new ELParameter("@PropertyID", DbType.Int32, propertyID);
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ProductProperty_Get", paramID))
            {
                ProductProperty property = null;
                if (dr.Read())
                {
                    property = PopulatePropertyFromIDataReader(dr);
                }
                return property;
            }
        }

        public override List<ProductProperty> GetPropertiesByCategoryID(int categoryID)
        {
            ELParameter paramID = new ELParameter("@CategoryID", DbType.Int32, categoryID);
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ProductProperties_GetByCategoryID", paramID))
            {
                List<ProductProperty> properties = new List<ProductProperty>();
                while (dr.Read())
                {
                    properties.Add(PopulatePropertyFromIDataReader(dr));
                }
                return properties;
            }
        }

        public override List<ProductProperty> GetPropertiesByProductID(int productID)
        {
            ELParameter paramID = new ELParameter("@ProductID", DbType.Int32, productID);
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ProductProperties_GetByProductID", paramID))
            {
                List<ProductProperty> properties = new List<ProductProperty>();
                while (dr.Read())
                {
                    properties.Add(PopulatePropertyValueFromIDataReader(dr));
                }
                return properties;
            }
        }

        public override List<ProductProperty> GetParentPropertiesByCategoryID(int categoryID)
        {
            ELParameter paramID = new ELParameter("@CategoryID", DbType.Int32, categoryID);
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ProductProperties_GetParentByCategoryID", paramID))
            {
                List<ProductProperty> properties = new List<ProductProperty>();
                while (dr.Read())
                {
                    properties.Add(PopulatePropertyFromIDataReader(dr));
                }
                return properties;
            }
        }
        #endregion

        #region Model
        public override ProductModel CreateUpdateModel(ProductModel model, DataProviderAction action)
        {
            ELParameter paramID = null;
            if (action == DataProviderAction.Create)
            {
                paramID = new ELParameter("@IndustryID", DbType.Int32, 4, ParameterDirection.Output);
            }
            else
            {
                paramID = new ELParameter("@IndustryID", DbType.Int32, model.ModelID);
            }

            ELParameter[] elParameters = new ELParameter[]{
                paramID,
                new ELParameter("@ProductID",DbType.Int32,model.ProductID),
                new ELParameter("@ModelCode",DbType.String,model.ModelCode),
                new ELParameter("@ModelName",DbType.String,model.ModelName),
                new ELParameter("@ModelDesc",DbType.String,model.ModelDesc),
                new ELParameter("@ModelStatus",DbType.Int32,model.ModelStatus),
                new ELParameter("@Operator",DbType.Int32,GlobalSettings.GetCurrentUser().UserID),
                new ELParameter("@PropertyNames",DbType.String,model.GetSerializerData().Keys),
                new ELParameter("@PropertyValues",DbType.String,model.GetSerializerData().Values),
                new ELParameter("@Action",DbType.Int32,action)
            };
            DataHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_ProductModel_CreateUpdate", elParameters);
            if (action == DataProviderAction.Create)
                model.ModelID = Convert.ToInt32(paramID.Value);
            return model;
        }

        public override bool DeleteModel(int modelID)
        {
            ELParameter paramID = new ELParameter("@ModelID", DbType.Int32, modelID);
            return Convert.ToInt32(DataHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_ProductBrand_Delete", paramID)) == 1;
        }

        public override ProductModel GetModel(int modelID)
        {
            ELParameter paramID = new ELParameter("@ModelID", DbType.Int32, modelID);
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ProductModel_Get"))
            {
                ProductModel model = new ProductModel();
                if (dr.Read())
                {
                    model = PopulateProductModelFromIDataReader(dr);
                }
                return model;
            }
        }

        public override List<ProductModel> GetModelsByProductID(int productID)
        {
            ELParameter paramID = new ELParameter("@ProductID", DbType.Int32, productID);
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ProductModels_GetByProductID"))
            {
                List<ProductModel> models = new List<ProductModel>();
                while (dr.Read())
                {
                    models.Add(PopulateProductModelFromIDataReader(dr));
                }
                return models;
            }
        }
        #endregion

        #region Product
        public override Product CreateUpdateProduct(Product product, string categoryIDList, string industryIDList,
            List<ProductProperty> properties, DataProviderAction action, out DataActionStatus status)
        {
            ELParameter paramID = null;
            if (action == DataProviderAction.Create)
            {
                paramID = new ELParameter("@ProductID", DbType.Int32, 4, ParameterDirection.Output);
            }
            else
            {
                paramID = new ELParameter("@ProductID", DbType.Int32, product.ProductID);
            }
            ELParameter[] elParameters = new ELParameter[]{
                    paramID,
                    new ELParameter("@ProductCode",DbType.String,product.ProductCode),
                    new ELParameter("@ProductName",DbType.String,product.ProductName),
                    new ELParameter("@ProductDesc",DbType.String,product.ProductDesc),
                    new ELParameter("@ProductAbstract",DbType.String,product.ProductAbstract),
                    new ELParameter("@ProductContent",DbType.String,product.ProductContent),
                    new ELParameter("@BrandID",DbType.Int32,DataHelper.IntOrNull(product.BrandID)),
                    new ELParameter("@ProductKeywords",DbType.String,product.ProductKeywords),
                    new ELParameter("@ProductStatus",DbType.Int32,product.ProductStatus),
                      new ELParameter("@DisplayOrder",DbType.Int32,product.DisplayOrder),
                    new ELParameter("@Operator",DbType.Int32,GlobalSettings.GetCurrentUser().UserID),
                    new ELParameter("@PropertyNames",DbType.String,product.GetSerializerData().Keys),
                    new ELParameter("@PropertyValues",DbType.String,product.GetSerializerData().Keys),
                    new ELParameter("@CategoryIDList",DbType.String,categoryIDList),
                    new ELParameter("@IndustryIDList",DbType.String,industryIDList),
                    new ELParameter("@PropertyIDList",DbType.String,ConvertProductPropertiesToXML(properties)),
                    new ELParameter("@Action",DbType.Int32,action),
                };
            status = (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure,
                "sp_Product_CreateUpdate", elParameters));
            if (action == DataProviderAction.Create && status == DataActionStatus.Success)
                product.ProductID = Convert.ToInt32(paramID.Value);
            return product;
        }

        private static object ConvertProductPropertiesToXML(List<ProductProperty> properties)
        {
            if (properties == null || properties.Count == 0)
                return DBNull.Value;
            StringWriter sw = new StringWriter();
            using (XmlTextWriter writer = new XmlTextWriter(sw))
            {
                writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"gb2312\"");
                writer.WriteStartElement("Properties");
                foreach (ProductProperty property in properties)
                {
                    writer.WriteStartElement("Property");
                    writer.WriteStartAttribute("PropertyID");
                    writer.WriteString(property.PropertyID.ToString());
                    writer.WriteEndAttribute();
                    writer.WriteStartAttribute("PropertyName");
                    writer.WriteString(property.PropertyName);
                    writer.WriteEndAttribute();
                    writer.WriteStartAttribute("PropertyValue");
                    writer.WriteString(property.PropertyValue);
                    writer.WriteEndAttribute();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Close();
            }
            return sw.ToString();
        }

        public override Product GetProduct(int productID)
        {
            ELParameter paramID = new ELParameter("@ProductID", DbType.Int32, productID);
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Product_Get", paramID))
            {
                Product product = null;
                if (dr.Read())
                {
                    product = PopulateProductFromIDataReader(dr);
                }
                return product;
            }
        }

        public override DataActionStatus DeleteProduct(int productID)
        {
            ELParameter paramID = new ELParameter("@ProductID", DbType.String, productID);
            return (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_Product_Delete", paramID));
        }

        public override List<Product> GetProducts(ProductQuery query, out int totalRecord)
        {
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@PageIndex",DbType.Int32,DataHelper.GetSafeSqlInt(query.PageIndex)),
                new ELParameter("@PageSize",DbType.Int32,DataHelper.GetSafeSqlInt(query.PageSize)),
                new ELParameter("@SqlPopulate",DbType.String,QueryGenerator.BuildProductQuery(query))
            };
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Products_Get", elParameters))
            {
                List<Product> productList = new List<Product>();
                while (dr.Read())
                    productList.Add(PopulateProductFromIDataReader(dr));
                dr.NextResult();
                dr.Read();
                totalRecord = DataRecordHelper.GetInt32(dr, 0);
                return productList;
            }
        }
        #endregion

        #region ProductPicture
        public override ProductPicture CreateUpdatePicture(ProductPicture picture, DataProviderAction action, out DataActionStatus status)
        {
            ELParameter paramID = null;
            if (action == DataProviderAction.Create)
                paramID = new ELParameter("@PictureID", DbType.Int32, 4, ParameterDirection.Output);
            else
                paramID = new ELParameter("@PictureID", DbType.Int32, picture.PictureID);

            ELParameter[] elParameters = new ELParameter[]{
	            paramID,
                new ELParameter("@ProductID",DbType.Int32,picture.ProductID),
                new ELParameter("@ModelID",DbType.Int32,picture.ModelID),
                new ELParameter("@DisplayOrder",DbType.Int32,picture.DisplayOrder),
                new ELParameter("@PictureName",DbType.String,picture.PictureName),
                new ELParameter("@PictureFile",DbType.String,picture.PictureFile),
                new ELParameter("@PictureStatus",DbType.Int32,picture.PictureStatus),
                new ELParameter("@Operator",DbType.Int32,GlobalSettings.GetCurrentUser().UserID),
                new ELParameter("@Action",DbType.Int32,action),
                };
            status = (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(
                CommandType.StoredProcedure, "sp_ProductPicture_CreateUpdate", elParameters));
            if (action == DataProviderAction.Create && status == DataActionStatus.Success)
                picture.PictureID = Convert.ToInt32(paramID.Value);
            return picture;
        }

        public override ProductPicture GetPicture(int pictureID)
        {
            ELParameter paramID = new ELParameter("@PictureID", DbType.Int32, pictureID);
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ProductPicture_Get", paramID))
            {
                ProductPicture picture = null;
                if (dr.Read())
                {
                    picture = PopulateProductPictureFromIDataReader(dr);
                }
                return picture;
            }
        }

        public override DataActionStatus DeletePicture(int pictureID)
        {
            ELParameter paramID = new ELParameter("@PictureID", DbType.Int32, pictureID);
            return (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ProductPicture_Delete", paramID));
        }

        public override List<ProductPicture> GetPicturesByProductID(int productID)
        {
            ELParameter paramID = new ELParameter("@ProductID", DbType.Int32, productID);
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ProductPictures_Get", paramID))
            {
                List<ProductPicture> pictures = new List<ProductPicture>();
                while (dr.Read())
                {
                    pictures.Add(PopulateProductPictureFromIDataReader(dr));
                }
                return pictures;
            }
        }

        public override ProductPicture GetDefaultPicture(int productID)
        {
            ELParameter paramID = new ELParameter("@ProductID", DbType.Int32, productID);
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Product_GetDefaultPicture", paramID))
            {
                ProductPicture picture = null;
                if (dr.Read())
                {
                    picture = PopulateProductPictureFromIDataReader(dr);
                }
                return picture;
            }
        }
        #endregion

        #region ProductPrice
        public override ProductPrice CreateUpdatePrice(ProductPrice price, DataProviderAction action, out DataActionStatus status)
        {
            ELParameter paramID = null;
            if (action == DataProviderAction.Create)
            {
                paramID = new ELParameter("@PriceID", DbType.Int32, 4, ParameterDirection.Output);
            }
            else
            {
                paramID = new ELParameter("@PriceID", DbType.Int32, price.PriceID);
            }
            ELParameter[] elParameters = new ELParameter[]{
                paramID,
                new ELParameter("@ProductID",DbType.Int32,price.ProductID),
                new ELParameter("@ModelID",DbType.Int32,price.ModelID),
                new ELParameter("@DeliverySpan",DbType.String,price.DeliverySpan),
                new ELParameter("@WarrantySpan",DbType.String,price.WarrantySpan),
                new ELParameter("@IncludeFreight",DbType.Int32,price.IncludeFreight),
                new ELParameter("@IncludeTax",DbType.Int32,price.IncludeTax),
                new ELParameter("@ApplyTaxRate",DbType.Decimal,price.ApplyTaxRate),
                new ELParameter("@QuoteMOQ",DbType.Int32,price.QuoteMOQ),
                new ELParameter("@PriceMarket",DbType.Decimal,price.PriceMarket),
                new ELParameter("@PricePromotion",DbType.Decimal,price.PricePromotion),
                new ELParameter("@PriceFloor",DbType.Decimal,price.PriceFloor),
                new ELParameter("@PricePurchase",DbType.Decimal,price.PricePurchase),
                new ELParameter("@PriceGradeA",DbType.Decimal,price.PriceGradeA),
                new ELParameter("@PriceGradeB",DbType.Decimal,price.PriceGradeB),
                new ELParameter("@PriceGradeC",DbType.Decimal,price.PriceGradeC),
                new ELParameter("@PriceGradeD",DbType.Decimal,price.PriceGradeD),
                new ELParameter("@PriceGradeE",DbType.Decimal,price.PriceGradeE),
                new ELParameter("@SupplyRegion",DbType.Int32,DataHelper.IntOrNull(price.SupplyRegion)),
                new ELParameter("@QuoteFrom",DbType.DateTime,price.QuoteFrom),
                new ELParameter("@QuoteEnd",DbType.DateTime,price.QuoteEnd),
                new ELParameter("@QuoteRenewal",DbType.Int32,price.QuoteRenewal),
                new ELParameter("@SupplyStatus",DbType.Int32,price.SupplyStatus),
                new ELParameter("@Operator",DbType.Int32,GlobalSettings.GetCurrentUser().UserID),
                new ELParameter("@PropertyNames",DbType.String,price.GetSerializerData().Keys),
                new ELParameter("@PropertyValues",DbType.String,price.GetSerializerData().Values),
                new ELParameter("@Action",DbType.Int32,action)
            };
            status = (DataActionStatus)Convert.ToInt32(
            DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ProductPrice_CreateUpdate", elParameters));
            if (action == DataProviderAction.Create && status == DataActionStatus.Success)
                price.PriceID = Convert.ToInt32(paramID.Value);
            return price;
        }

        public override DataActionStatus DeletePrice(int priceID)
        {
            ELParameter paramID = new ELParameter("@PriceID", DbType.Int32, priceID);
            return (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ProductPrice_Delete", paramID));
        }

        public override ProductPrice GetPrice(int priceID)
        {
            ELParameter paramID = new ELParameter("@PriceID", DbType.Int32, priceID);
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ProductPrice_Get", paramID))
            {
                ProductPrice price = null;
                if (dr.Read())
                {
                    price = PopulateProductPriceFromIDataReader(dr);
                }
                return price;
            }
        }

        public override List<ProductPrice> GetPrices(int productID)
        {
            ELParameter paramID = new ELParameter("@ProductID", DbType.Int32, productID);
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ProductPrices_Get", paramID))
            {
                List<ProductPrice> prices = new List<ProductPrice>();
                while (dr.Read())
                {
                    prices.Add(PopulateProductPriceFromIDataReader(dr));
                }
                return prices;
            }
        }

        public override decimal? GetGradePrice(List<string> filters, int productID, UserLevel level)
        {
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@SqlPopulate",DbType.String,QueryGenerator.BuilderProductPriceQuery(filters,productID,level)),   
            };
            object value = DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ProductPrice_GetMemberPrice", elParameters);
            if (value != null)
                return Convert.ToDecimal(value);
            else
                return null;
        }

        public override decimal? GetMarketPrice(string areaIDList, int productID)
        {
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@AreaList", DbType.String, areaIDList),
                new ELParameter("@ProductID", DbType.Int32, productID),
              };
            object value = DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ProductPrice_GetMarketPrice", elParameters);
            if (value != null && value != DBNull.Value)
                return Convert.ToDecimal(value);
            else
                return null;
        }

        public override decimal? GetDefaultPrice(int productID)
        {
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@ProductID", DbType.Int32, productID),
              };
            object value = DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_ProductPrice_GetDefaultPrice", elParameters);
            if (value != null && value != DBNull.Value)
                return Convert.ToDecimal(value);
            else
                return null;
        }
        #endregion


    }
}
