using System;

namespace HHOnline.Framework
{
    /// <summary>
    /// 创建用户状态
    /// </summary>
    public enum CreateUserStatus
    {
        Success = 0,

        DisallowedUsername = 5,

        DuplicateUserName = 6,

        DuplicateEmail = 7,    
        
        UnknownFailure=99,


    }
}
