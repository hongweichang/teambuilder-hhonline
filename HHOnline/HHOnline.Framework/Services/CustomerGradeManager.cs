using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using HHOnline.Cache;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    /// <summary>
    /// 客户级别管理
    /// </summary>
    public class CustomerGradeManager
    {
        /// <summary>
        /// 添加客户级别
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static DataActionStatus Create(CustomerGrade grade)
        {
            DataActionStatus status = DataActionStatus.Success;
            return status;
        }

        /// <summary>
        /// 修改客户级别
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static DataActionStatus Update(CustomerGrade grade)
        {
            DataActionStatus status = DataActionStatus.Success;
            return status;
        }

        /// <summary>
        /// 删除客户级别
        /// </summary>
        /// <param name="gradeID"></param>
        /// <returns></returns>
        public static DataActionStatus Delete(int gradeID)
        {
            DataActionStatus status = DataActionStatus.Success;
            return status;
        }

        /// <summary>
        /// 获取指定客户的所有级别
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public static List<CustomerGrade> GetCustomerGrades(int companyID)
        {
            return null;
        }

        /// <summary>
        /// 获取指定ID的客户级别
        /// </summary>
        /// <param name="gradeID"></param>
        /// <returns></returns>
        public static CustomerGrade Get(int gradeID)
        {
            return null;
        }
    }
}
