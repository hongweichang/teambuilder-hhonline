using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using HHOnline.Cache;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    public class Companys
    {
        #region GetCompany

        public static List<Company> GetCompanys(CompanyStatus comStatus, CompanyType comType, string comName)
        {
            return GetCompanys((int)comStatus,(int)comType, comName);
        }
        private static List<Company> GetCompanys(int comStatus, int comType, string comName)
        {
            List<Company> companys = CommonDataProvider.Instance.GetCompanys(comStatus, comType, comName);

            return companys;
        }
        public static Company GetCompany(int companyID)
        {
            return GetCompany(companyID, null, true);
        }

        public static Company GetCompany(string companyName)
        {
            return GetCompany(0, companyName, true);
        }
        public static Company GetCompanyByUser(int userID)
        {
            return CommonDataProvider.Instance.GetCompanyByUser(userID);
        }
        public static Company GetCompany(int companyID, string companyName, bool useCache)
        {
            Company company = null;
            string cacheKey = (companyID > 0) ? CacheKeyManager.GetCompanyKey(companyID) : CacheKeyManager.GetCompanyKey(companyName);
            if (useCache)
            {
                if (HttpContext.Current != null)
                {
                    company = HttpContext.Current.Items[cacheKey] as Company;
                }
                if (company != null)
                    return company;
                company = HHCache.Instance.Get(cacheKey) as Company;
            }
            if (company == null)
            {
                company = CommonDataProvider.Instance.GetCompany(companyID, companyName);
                if (!useCache)
                {
                    return company;
                }

                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Items[cacheKey] = company;
                }
                AddCachedCompany(company);
            }
            return company;
        }
        #endregion

        #region CreateCompany
        /// <summary>
        /// 创建公司
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static CreateCompanyStatus Create(Company company)
        {
            //触发事件

            CreateCompanyStatus status;
            CommonDataProvider dp = CommonDataProvider.Instance;
            company = dp.CreateUpdateCompany(company, DataProviderAction.Create, out status);
            if (status == CreateCompanyStatus.Success)
            {
                OnUpdated(company.CompanyID);
            }
            return status;
        }
        #endregion

        #region DeleteCompany
        /// <summary>
        /// 根据公司名删除公司
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public static bool DeleteCompany(string companyName)
        {
            return DeleteCompany(0, companyName);
        }

        public static bool DeleteCompany(int companyID)
        {
            return DeleteCompany(companyID, null);
        }

        public static bool DeleteCompany(int companyID, string companyName)
        {
            if (CommonDataProvider.Instance.DeleteCompany(companyID, companyName))
            {
                HHCache.Instance.Remove(CacheKeyManager.GetCompanyKey(companyName));
                HHCache.Instance.Remove(CacheKeyManager.GetCompanyKey(companyID));
                OnUpdated(companyID);
                return true;
            }
            return false;
        }
        #endregion

        #region Company Cache Management
        /// <summary>
        /// 刷新用户缓存信息
        /// </summary>
        /// <param name="company"></param>
        internal static void RefreshCachedCompany(Company company)
        {
            HHCache.Instance.Remove(CacheKeyManager.GetCompanyKey(company.CompanyID));
            HHCache.Instance.Remove(CacheKeyManager.GetCompanyKey(company.CompanyName));
        }

        /// <summary>
        /// 添加用户到缓存
        /// </summary>
        /// <param name="company"></param>
        internal static void AddCachedCompany(Company company)
        {
            HHCache.Instance.Insert(CacheKeyManager.GetCompanyKey(company.CompanyID), company);
            HHCache.Instance.Insert(CacheKeyManager.GetCompanyKey(company.CompanyName), company);
        }

        /// <summary>
        /// 添加用户集到缓存
        /// </summary>
        /// <param name="companys"></param>
        internal static void AddCachedCompany(List<Company> companys)
        {
            foreach (Company company in companys)
            {
                AddCachedCompany(company);
            }
        }
        #endregion

        #region UpdateCompany
        /// <summary>
        /// 根据Id更新Company信息
        /// </summary>
        /// <param name="company"></param>
        public static bool UpdateCompany(Company company)
        {
            //GlobalEvents.BeforeCompany(company, ObjectState.Update);
            CreateCompanyStatus status;
            CommonDataProvider.Instance.CreateUpdateCompany(company, DataProviderAction.Update, out status);
            //GlobalEvents.AfterCompany(company, ObjectState.Update);
            RefreshCachedCompany(company);
            OnUpdated(company.CompanyID);
            return status == CreateCompanyStatus.Success;
        }
        #endregion 
        
        #region -EventHandler-
        public static EventHandler<EventArgs> Updated;
        protected static void OnUpdated(object sender)
        {
            if (Updated != null)
            {
                Updated(sender, EventArgs.Empty);
            }
        }
        #endregion
    }
}
