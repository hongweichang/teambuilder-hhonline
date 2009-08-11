using System;

namespace HHOnline.QueryBuilder
{
    /// <summary>
    ///  比较操作符 WHERE, HAVING and JOIN 子句
    /// </summary>
    public enum Comparison
    {
        /// <summary>
        /// 等于
        /// </summary>
        Equals,

        /// <summary>
        /// 不等于
        /// </summary>
        NotEquals,

        /// <summary>
        /// 相似于
        /// </summary>
        Like,

        /// <summary>
        /// 不相似
        /// </summary>
        NotLike,

        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan,

        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterOrEquals,

        /// <summary>
        /// 小于
        /// </summary>
        LessThan,

        /// <summary>
        /// 小于等于
        /// </summary>
        LessOrEquals,

        /// <summary>
        /// 在其中
        /// </summary>
        In,

        /// <summary>
        /// 不在其中
        /// </summary>
        NotIn,
    }
}
