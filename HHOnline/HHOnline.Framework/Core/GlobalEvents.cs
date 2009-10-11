using System;


namespace HHOnline.Framework
{
    public class GlobalEvents
    {
        /// <summary>
        /// 引发BeforeUser事件 Create/Update
        /// </summary>
        /// <param name="user"></param>
        /// <param name="state"></param>
        public static void BeforeUser(User user, ObjectState state)
        {
            GlobalApplication.Instance.ExecutePreUserUpdate(user, state);
        }

        /// <summary>
        /// 引发AfterUser事件 Create/Update
        /// </summary>
        /// <param name="user"></param>
        /// <param name="state"></param>
        public static void AfterUser(User user, ObjectState state)
        {
            GlobalApplication.Instance.ExecutePostUserUpdate(user, state);
        }

        /// <summary>
        /// 引发UserValidated时间 ValidateUser
        /// </summary>
        /// <param name="user"></param>
        public static void ValidatedUser(User user)
        {
            GlobalApplication.Instance.ExecuteUserValidated(user);
        }

        /// <summary>
        /// 引发用户查询事件
        /// </summary>
        /// <param name="searchWord"></param>
        public static void UserSearch(string searchWord)
        {
            GlobalApplication.Instance.ExecuteUserSearch(searchWord);
        }
    }
}
