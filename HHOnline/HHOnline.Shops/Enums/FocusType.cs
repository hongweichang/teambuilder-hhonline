using System;


namespace HHOnline.Shops
{
    /// <summary>
    ///    关注类型，1新品上架、2热卖产品、3最受关注、4促销产品
    /// </summary>
    public enum FocusType
    {
        /// <summary>
        /// 新品上架
        /// </summary>
        New = 1,

        /// <summary>
        ///热卖产品 
        /// </summary>
        Hot = 2,

        /// <summary>
        ///推荐产品 
        /// </summary>
        Recommend = 3,

        /// <summary>
        /// 促销产品
        /// </summary>
        Promotion = 4
    }
}
