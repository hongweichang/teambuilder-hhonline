using System;

namespace HHOnline.Framework
{
    /// <summary>
    /// 企业客户级别申请审核状态
    /// </summary>
    public enum PendingStatus
    {
        /// <summary>
        /// 待审核（挂起状态）
        /// </summary>
        Pending = 1,
        /// <summary>
        /// 拒绝
        /// </summary>
        Deny = 2,
        /// <summary>
        /// 审核通过
        /// </summary>
        Inspect = 3
    }
}
