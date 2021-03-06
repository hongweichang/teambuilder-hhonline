﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.SearchBarrel
{
    /// <summary>
    /// 产品索引字段
    /// </summary>
    public class ProductIndexField
    {
        public static readonly string ProductID;

        public static readonly string ProductAbstract;

        public static readonly string ProductName;

        public static readonly string ProductContent;

        public static readonly string BrandID;

        public static readonly string BrandName;

        public static readonly string CategoryID;

        public static readonly string CategoryName;

        public static readonly string ProductKeywords;

        public static readonly string DateCreated;

        static ProductIndexField()
        {
            ProductID = "ProductID";

            ProductName = "ProductName";

            ProductAbstract = "ProductAbstract";

            ProductContent = "ProductContent";

            BrandID = "BrandID";

            BrandName = "BrandName";

            CategoryID = "CategoryID";

            CategoryName = "CategoryName";

            ProductKeywords = "Productkeywords";

            DateCreated = "DateCreated";
        }
    }
}
