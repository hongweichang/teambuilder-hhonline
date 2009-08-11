using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using HHOnline.Cache;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    /// <summary>
    /// 公司资质文件服务类
    /// </summary>
    public class CompanyQualifications
    {
        public static string FileStoreKey = "CompanyQualification";

        #region GetCompanyQualifacations
        public static CompanyQualification GetCompanyQualification(int qualificationID)
        {
            return GetCompanyQualification(qualificationID, true);
        }

        public static CompanyQualification GetCompanyQualification(int qualificationID, bool useCache)
        {
            CompanyQualification qualification = null;
            string cacheKey = CacheKeyManager.GetQualificationKey(qualificationID);
            if (useCache)
            {
                if (HttpContext.Current != null)
                {
                    qualification = HttpContext.Current.Items[cacheKey] as CompanyQualification;
                }
                if (qualification != null)
                    return qualification;
                qualification = HHCache.Instance.Get(cacheKey) as CompanyQualification;
            }
            if (qualification == null)
            {
                qualification = CommonDataProvider.Instance.GetQualification(qualificationID);
                if (!useCache)
                {
                    return qualification;
                }
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Items[cacheKey] = qualification;
                }
                HHCache.Instance.Insert(cacheKey, qualification);
            }
            return qualification;
        }
        #endregion

        #region AddFile
        public static void AddFile(CompanyQualification qualification, Stream contentStream)
        {
            qualification.QualificationName = GlobalSettings.EnsureHtmlEncoded(qualification.QualificationName);

            qualification.QualificationFile = MakePath(qualification.CompanyID);
            //事件触发
            FileStorageProvider fs = new FileStorageProvider(FileStoreKey);

            fs.AddUpdateFile(MakePath(qualification.CompanyID), qualification.QualificationName, contentStream);

            qualification.QualificationID = CommonDataProvider.Instance.CreateQualification(qualification);

            HHCache.Instance.Remove(CacheKeyManager.GetQualificationKey(qualification.QualificationID));

            //事件触发
        }
        #endregion

        /// <summary>
        /// 根据公司ID获取资质文件
        /// </summary>
        /// <param name="companyID">companyID</param>
        /// <returns></returns>
        public static List<CompanyQualification> GetCompanyQualifications(int companyID)
        {
            List<CompanyQualification> lstQualifications = null;
            lstQualifications = CommonDataProvider.Instance.GetQualificaionByCompanyID(companyID);
            return lstQualifications;
        }

        #region  Path Methods

        public static string MakePath(int companyId)
        {
            return GlobalSettings.MakePath(companyId);
        }

        public static string MakePath(Guid guid)
        {
            return FileStorageProvider.MakePath(guid.ToString().Split(new char[] { '-' }));
        }

        #endregion
    }
}
