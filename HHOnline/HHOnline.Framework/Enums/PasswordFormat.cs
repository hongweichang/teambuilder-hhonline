using System;

namespace HHOnline.Framework
{
    public enum PasswordFormat
    {
        /// <summary>
        /// 未加密
        /// </summary>
        Clear = 0,

        /// <summary>
        /// 加密
        /// </summary>
        Hashed = 1
    }
}
