using System;


namespace HHOnline.Framework
{
    public enum ExceptionType
    {
        #region -数据访问异常0~31-
        /// <summary>
        /// 连接数据库异常
        /// </summary>
        DataProvider = 0,
        /// <summary>
        /// 访问被拒绝
        /// </summary>
        AccessDenied = 1,

        /// <summary>
        /// 商品重复
        /// </summary>
        ProductDuplicate = 2,

        /// <summary>
        /// 商品未找到
        /// </summary>
        ProductNotFound = 3,

        /// <summary>
        /// 商品分类未找到
        /// </summary>
        ProductCategoryNotFound = 4,
        /// <summary>
        /// 资讯未找到
        /// </summary>
        NewNotFound = 5,

        /// <summary>
        /// 文件未找到
        /// </summary>
        FileNotFound = 6,

        /// <summary>
        /// 查找无结果
        /// </summary>
        SearchNoResults = 7,

        /// <summary>
        /// 查找未知错误
        /// </summary>
        SearchUnknownError = 8,
        #endregion

        #region -账户异常32~63-
        /// <summary>
        /// 用户账号已存在
        /// </summary>
        UserAccountCreated = 32,

        /// <summary>
        /// 用户账号正在审核
        /// </summary>
        UserAccountPending = 33,

        /// <summary>
        /// 用户账号自动激活
        /// </summary>
        UserAccountCreatedAuto = 34,

        /// <summary>
        /// 用户账号未通过审核
        /// </summary>
        UserAccountDisapproved = 35,

        /// <summary>
        /// 用户账号被禁止
        /// </summary>
        UserAccountBanned = 36,

        /// <summary>
        /// 用户密码修改失败
        /// </summary>
        UserPasswordChangeFailed = 38,

        /// <summary>
        /// 用户登录失败
        /// </summary>
        UserInvalidCredentials = 39,

        /// <summary>
        /// 用户账号非法
        /// </summary>
        UserAccountInvalid = 40,

        /// <summary>
        /// 用户未找到
        /// </summary>
        UserNotFound = 41,

        /// <summary>
        /// 用户未登录
        /// </summary>
        UserUnAuthenticated = 42,
        #endregion

        #region -Email 异常64~127-
        /// <summary>
        /// Email发送失败
        /// </summary>
        EmailUnableToSend = 64,

        /// <summary>
        /// Email模板未找到
        /// </summary>
        EmailTemplateNotFound = 65,
        #endregion

        #region -功能异常128~255-
        /// <summary>
        /// Module 未找到
        /// </summary>
        ModuleNotFount = 128,

        /// <summary>
        /// 模块初始化异常
        /// </summary>
        ModuleInitFail = 129,

        /// <summary>
        /// Type 未找到
        /// </summary>
        TypeNotFount = 130,

        /// <summary>
        /// 类型初始化异常
        /// </summary>
        TypeInitFail =131,
        /// <summary>
        /// 操作失败
        /// </summary>
        OperationError=132,
        #endregion

        #region -信息提示512~1023-
        /// <summary>
        /// 操作成功
        /// </summary>
        Success = 512,
        /// <summary>
        /// 操作失败
        /// </summary>
        Failed = 513,
        /// <summary>
        /// 操作被终止
        /// </summary>
        Abored = 514,
        /// <summary>
        /// 用户密码修改成功
        /// </summary>
        UserPasswordChangeSuccess = 515,
        #endregion

        #region -其他异常1024+-
        /// <summary>
        /// Application Start
        /// </summary>
        ApplicationStart = 1024,

        /// <summary>
        /// Application Stop
        /// </summary>
        ApplicationStop = 1025,

        /// <summary>
        /// 未知错误
        /// </summary>
        UnknownError = 1026,
        /// <summary>
        /// 不使用模板页的消息提示
        /// </summary>
        NoMasterError=1027
        #endregion
    }
}
