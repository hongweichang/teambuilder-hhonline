using System;


namespace HHOnline.Shops
{
    /// <summary>
    /// 产品排序依据
    /// </summary>
    public enum ProductOrderBy
    {
        /// <summary>
        /// 显示顺序
        /// </summary>
        DisplayOrder,

        /// <summary>
        /// 产品名称
        /// </summary>
        ProductName,  
        
        /// <summary>
        /// 品牌名称
        /// </summary>
        BrandName,

        /// <summary>
        /// 创建时间
        /// </summary>
        DataCreated,

        /// <summary>
        /// 价格
        /// </summary>
        Price,

        /// <summary>
        /// 产品发布状态
        /// </summary>
        ProductStatus,

    }
}
