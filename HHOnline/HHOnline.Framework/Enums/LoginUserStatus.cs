using System;


namespace HHOnline.Framework
{
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum LoginUserStatus
    {
        /// <summary>
        /// 用户名密码不匹配
        /// </summary>
        InvalidCredentials = 0,

        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 等待审核
        /// </summary>
        AccountPending = 2,

        /// <summary>
        /// 禁止
        /// </summary>
        AccountBanned = 3,

        /// <summary>
        /// 未通过审核
        /// </summary>
        AccountDisapproved = 4,

        /// <summary>
        /// 被删除
        /// </summary>
        Deleted = 5,

        /// <summary>
        /// 未知错误
        /// </summary>
        UnknownError = 100
    }
}
