using System;

namespace HHOnline.QueryBuilder
{
    /// <summary>
    /// 连接操作符 JOIN子句
    /// </summary>
    public enum JoinType
    {
        /// <summary>
        /// 内连接
        /// </summary>
        InnerJoin,

        /// <summary>
        ///外连接 
        /// </summary>
        OuterJoin,

        /// <summary>
        /// 左连接
        /// </summary>
        LeftJoin,

        /// <summary>
        /// 右连接
        /// </summary>
        RightJoin
    }
}
