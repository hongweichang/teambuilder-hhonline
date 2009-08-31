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
        public static DataActionStatus Create(CustomerGrade customerGrade)
        {
            DataActionStatus status;
            customerGrade = CommonDataProvider.Instance.CreateUpdateCustomerGrade(customerGrade, DataProviderAction.Create, out status);
            HHCache.Instance.Remove(CacheKeyManager.GetCustomerGradeKeyByCompanyID(customerGrade.CompanyID));
            return status;
        }

        /// <summary>
        /// 修改客户级别
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static DataActionStatus Update(CustomerGrade customerGrade)
        {
            DataActionStatus status;
            CommonDataProvider.Instance.CreateUpdateCustomerGrade(customerGrade, DataProviderAction.Update, out status);
            HHCache.Instance.Remove(CacheKeyManager.GetCustomerGradeKey(customerGrade.GradeID));
            HHCache.Instance.Remove(CacheKeyManager.GetCustomerGradeKeyByCompanyID(customerGrade.CompanyID));
            return status;
        }

        /// <summary>
        /// 删除客户级别
        /// </summary>
        /// <param name="gradeID"></param>
        /// <returns></returns>
        public static DataActionStatus Delete(int gradeID)
        {
            DataActionStatus status = CommonDataProvider.Instance.DeleteCustomerGrade(gradeID);
            if (status == DataActionStatus.Success)
            {
                CustomerGrade grade = Get(gradeID);
                if (grade != null)
                {
                    HHCache.Instance.Remove(CacheKeyManager.GetCustomerGradeKeyByCompanyID(grade.CompanyID));
                    HHCache.Instance.Remove(CacheKeyManager.GetCustomerGradeKey(gradeID));
                }
            }
            return status;
        }

        /// <summary>
        /// 清理客户级别
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataActionStatus Clear(int companyID)
        {
            DataActionStatus status = CommonDataProvider.Instance.ClearCustomerGrade(companyID);
            if (status == DataActionStatus.Success)
                HHCache.Instance.Remove(CacheKeyManager.GetCustomerGradeKeyByCompanyID(companyID));
            return status;
        }

        /// <summary>
        /// 获取指定客户的所有级别
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public static List<CustomerGrade> GetCustomerGrades(int companyID)
        {
            string cacheKey = CacheKeyManager.GetCustomerGradeKeyByCompanyID(companyID);

            List<CustomerGrade> userGrades = HHCache.Instance.Get(cacheKey) as List<CustomerGrade>;

            if (userGrades == null)
            {
                userGrades = CommonDataProvider.Instance.GetCustomerGradeByCompanyID(companyID);
                HHCache.Instance.Insert(cacheKey, userGrades);
            }

            return userGrades;
        }

        /// <summary>
        /// 获取指定ID的客户级别
        /// </summary>
        /// <param name="gradeID"></param>
        /// <returns></returns>
        public static CustomerGrade Get(int gradeID)
        {
            string cacheKey = CacheKeyManager.GetCustomerGradeKey(gradeID);
            CustomerGrade userGrade = HHCache.Instance.Get(cacheKey) as CustomerGrade;
            if (userGrade == null)
            {
                userGrade = CommonDataProvider.Instance.GetCustomerGrade(gradeID);
                HHCache.Instance.Insert(cacheKey, userGrade);
            }
            return userGrade;
        }
    }
}
