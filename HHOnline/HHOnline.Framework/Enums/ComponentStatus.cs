using System;

namespace HHOnline.Framework
{
    /// <summary>
    /// 状态根据具体对象和业务需求，可能包括两种及以上的多种状态，具体状态参见数据表字段说明；
    /// 基本状态取值如下，1表示启用、2表示停用，0表示作废/删除
    /// </summary>
    public enum ComponentStatus
    {
        /// <summary>
        /// 删除/作废
        /// </summary>
        Deleted =0,

        /// <summary>
        /// 启用
        /// </summary>
        Enabled=1,

        /// <summary>
        /// 停用
        /// </summary>
        Disabled =2
    }
}
