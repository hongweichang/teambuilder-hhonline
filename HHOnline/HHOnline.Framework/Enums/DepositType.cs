using System;

namespace HHOnline.Framework
{
    /// <summary>
    /// 保证金类型
    /// </summary>
    public enum DepositType
    {
        /// <summary>
        /// 代理保证金
        /// </summary>
        Agent = 1,
        /// <summary>
        /// 供应商保证金
        /// </summary>
        Provider = 2,
        /// <summary>
        /// 混合保证金
        /// </summary>
        Complex = 3
    }
}
