using System;

namespace HHOnline.Framework
{
    /// <summary>
    /// 创建公司状态
    /// </summary>
    public enum CreateCompanyStatus
    {
        Success = 0,

        DuplicateCompanyName = 1,

        UnknownFailure = 99,
    }
}
