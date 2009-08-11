using System;


namespace HHOnline.Framework
{
    /// <summary>
    /// 公司类型
    /// </summary>
    [Flags]
    public enum CompanyType
    {
        /// <summary>
        /// 普通公司
        /// </summary>
        Ordinary = 1,

        /// <summary>
        /// 代理
        /// </summary>
        Agent = 2,

        /// <summary>
        /// 供应商
        /// </summary>
        Provider = 4

    }
}
