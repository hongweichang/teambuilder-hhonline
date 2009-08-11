using System;

namespace HHOnline.Framework
{
    /// <summary>
    /// 用户账号状态
    /// </summary>
    public enum AccountStatus
    {
        /// <summary>
        /// 被删除
        /// </summary>
        Deleted = 0,

        /// <summary>
        /// 已审核
        /// </summary>
        Authenticated = 1,
        
        /// <summary>
        /// 待审核
        /// </summary>
        ApprovalPending = 2,
        
        /// <summary>
        /// 审核未通过
        /// </summary>
        Disapproved = 3,
        
        /// <summary>
        /// 锁定
        /// </summary>
        Lockon = 4,
        
        /// <summary>
        /// 匿名
        /// </summary>
        Anonymous = 5,
        
        /// <summary>
        /// 所有
        /// </summary>
        All = 99
    }
}
