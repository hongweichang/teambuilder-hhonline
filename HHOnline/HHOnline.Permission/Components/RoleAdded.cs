using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Permission.Components
{
    /// <summary>
    /// 角色操作结果
    /// </summary>
    public enum RoleOpts
    {
        /// <summary>
        /// 已存在
        /// </summary>
        Exist = 0,
        /// <summary>
        /// 失败
        /// </summary>
        Failed = 1,
        /// <summary>
        /// 成功
        /// </summary>
        Success = 2
    }
}
