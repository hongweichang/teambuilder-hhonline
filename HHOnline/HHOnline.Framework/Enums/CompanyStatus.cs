using System;

namespace HHOnline.Framework
{
    public enum CompanyStatus
    {
        /// <summary>
        /// 公司被删除
        /// </summary>
        Deleted =0,
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
        /// 公司停用
        /// </summary>
        Lockon = 4,
        /// <summary>
        /// 业务逻辑使用（与数据库无关）
        /// </summary>
        None=5

    }
}
