using System;

namespace HHOnline.Framework
{
    /// <summary>
    /// 数据库增/改返回状态
    /// </summary>
    public enum DataActionStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 名称已存在
        /// </summary>
        DuplicateName = 1,

        /// <summary>
        /// 存在关联数据
        /// </summary>
        RelationshipExist = 2,

        /// <summary>
        /// 存在子节点
        /// </summary>
        ChildExist = 3,

        /// <summary>
        /// 未知错误失败
        /// </summary>
        UnknownFailure = 99,
    }
}
