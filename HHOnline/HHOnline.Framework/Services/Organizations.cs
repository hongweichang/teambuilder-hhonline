using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using HHOnline.Cache;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    /// <summary>
    /// 组织机构
    /// </summary>
    public class Organizations
    {
        #region GetOrganization
        /// <summary>
        /// 通过ID获取组织机构
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public static Organization GetOrganization(int organizationID)
        {
            List<Organization> organizations = GetAllOrganizations();
            if (organizations != null)
            {
                foreach (Organization organization in organizations)
                {
                    if (organization.OrganizationID == organizationID)
                    {
                        return organization;
                    }
                }
            }
            return null;
        }
        #endregion

        #region GetOrganizations
        /// <summary>
        /// 返回所有组织机构
        /// </summary>
        /// <returns></returns>
        public static List<Organization> GetAllOrganizations()
        {
            List<Organization> organizations = null;
            string cacheKey = CacheKeyManager.OrganizationKey;
            if (HttpContext.Current != null)
                organizations = HttpContext.Current.Items[cacheKey] as List<Organization>;

            if (organizations != null)
                return organizations;

            organizations = HHCache.Instance.Get(cacheKey) as List<Organization>;
            if (organizations == null)
            {
                organizations = CommonDataProvider.Instance.GetOrganizations();
                HHCache.Instance.Max(cacheKey, organizations);
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Items[cacheKey] = organizations;
                }
            }
            return organizations;
        }

        /// <summary>
        /// 获取直接子组织机构
        /// </summary>
        /// <returns></returns>
        public static List<Organization> GetChildOrganizations(int organizationID)
        {
            List<Organization> organizations = GetAllOrganizations();
            List<Organization> childs = new List<Organization>();
            foreach (Organization organization in organizations)
            {
                if (organization.ParentID == organizationID)
                    childs.Add(organization);
            }
            return childs;
        }

        /// <summary>
        /// 获取所有子组织机构
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public static List<Organization> GetAllChildOrganizations(int organizationID)
        {
            Organization organization = GetOrganization(organizationID);
            List<Organization> childs = new List<Organization>();
            if (organization != null)
                RecursiveAllChild(organization, ref childs);
            return childs;
        }

        private static void RecursiveAllChild(Organization parent, ref List<Organization> child)
        {
            List<Organization> childOrganization = GetChildOrganizations(parent.OrganizationID);
            {
                foreach (Organization organization in childOrganization)
                {
                    child.Add(organization);
                    RecursiveAllChild(organization, ref child);
                }
            }
        }
        #endregion

        #region Create/Update/Delete
        /// <summary>
        /// 更新组织机构organization.OrganizationID为-1时表示已存在同名组织
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public static void UpdateOrganization(Organization organization)
        {
            CommonDataProvider.Instance.CreateUpdateOrganization(organization, DataProviderAction.Update);
            HHCache.Instance.Remove(CacheKeyManager.OrganizationKey);
        }

        /// <summary>
        /// 添加组织机构 organization.OrganizationID为-1时表示已存在同名组织
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public static Organization CreateOrganization(Organization organization)
        {
            organization.OrganizationID = CommonDataProvider.Instance.CreateUpdateOrganization(organization, DataProviderAction.Create);
            HHCache.Instance.Remove(CacheKeyManager.OrganizationKey);
            return organization;
        }

        /// <summary>
        /// 删除组织机构
        /// </summary>
        /// <param name="organizationID"></param>
        public static bool DeleteOrganization(int organizationID)
        {
            if (GetChildOrganizations(organizationID).Count > 0)
                return false;
            bool flag = CommonDataProvider.Instance.DeleteOrganization(organizationID);
            if (flag)
                HHCache.Instance.Remove(CacheKeyManager.OrganizationKey);
            return flag;
        }

        /// <summary>
        /// 删除组织机构
        /// <remarks>
        /// 1. DataActionStatus.Success: 成功删除所有
        /// 2. DataActionStatus.UnknownFailure 删除失败
        /// 3. DataActionStatus.RelationshipExist 删除时组织结构下存在关联用户(无关联用户部门继续删除)
        /// 即 部门组织结构下存在关联用户的，将无法被删除！
        /// </remarks>
        /// </summary>
        /// <param name="organizationIDList">组织结构ID列，如"1,12,34"</param>
        /// <returns></returns>
        public static DataActionStatus DeleteOrganization(string organizationIDList)
        {
            DataActionStatus s = CommonDataProvider.Instance.DeleteOrganization(organizationIDList);
            if (s == DataActionStatus.Success || s == DataActionStatus.RelationshipExist)
            {
                HHCache.Instance.Remove(CacheKeyManager.OrganizationKey);
            }
            return s;
        }

        /// <summary>
        /// 删除组织机构
        /// <summary>
        /// <param name="organizationIDList">组织结构ID列表</param>
        /// <returns></returns>
        public static DataActionStatus DeleteOrganization(List<string> organizationIDList)
        {
            return DeleteOrganization(string.Join(",", organizationIDList.ToArray()));
        }
        #endregion
    }
}
