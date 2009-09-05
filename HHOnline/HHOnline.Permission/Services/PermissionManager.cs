using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Permission.Components;
using HHOnline.Cache;
using HHOnline.Permission.Providers;
using HHOnline.Framework;

namespace HHOnline.Permission.Services
{
    /// <summary>
    /// 权限管理模块
    /// </summary>
    public class PermissionManager
    {
        private static string _RolesManagerCacheKey = "HHOnline/Permission/";
        

        private PermissionManager() {
            _RolesManagerCacheKey = CacheKeyManager.PemissionPrefix;
        }

        #region -CommonMethod-
        private delegate void CacheDelegate(ref object list, params object[] args);
        private static List<T> CacheInstance<T>(CacheDelegate cacheDelegate, string cacheKey, params object[] args)
            where T : new()
        {
            cacheKey = _RolesManagerCacheKey + cacheKey;
            object instances = HHCache.Instance.Get(cacheKey);
            if (instances == null)
            {
                cacheDelegate(ref instances, args);
                HHCache.Instance.Max(cacheKey, instances);
            }
            return instances as List<T>;
        }
        #endregion

        #region -Public Methods-
        /// <summary>
        /// 批量添加用户到角色
        /// </summary>
        /// <param name="userIdList"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        public static bool AddUsersToRole(string userIdList, UserRole userRole)
        {
            return PermissionDataProvider.Instance.AddUsersToRole(userIdList, userRole);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public static RoleOpts DeleteRole(int roleID)
        {
            RoleOpts result = PermissionDataProvider.Instance.DeleteRole(roleID);
            if (result==RoleOpts.Success)
                HHCache.Instance.Remove(_RolesManagerCacheKey + "AllRoles");
            return result;
        }
        /// <summary>
        /// 修改用户角色
        /// </summary>
        /// <param name="role"></param>
        /// <param name="moduleActionId"></param>
        /// <returns></returns>
        public static RoleOpts EditRole(Role role, string moduleActionId)
        {
            RoleOpts result = PermissionDataProvider.Instance.EditRole(role, moduleActionId);
            if (result == RoleOpts.Success)
                HHCache.Instance.Remove(_RolesManagerCacheKey + "AllRoles");
            return result;
        }
        /// <summary>
        /// 根据角色ID获取关联模块信息
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public static List<ModuleAction> LoadModuleAction(int roleID)
        {
            return PermissionDataProvider.Instance.LoadModuleAction(roleID);
        }
        /// <summary>
        /// 根据ID获取角色信息
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public static Role SelectRole(int roleID)
        {
            return PermissionDataProvider.Instance.SelectRole(roleID);
        }
        /// <summary>
        /// 根据userName获取公司内部用户权限队列
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static List<ModuleActionKeyValue> ModuleActionKeyValues(string userName)
        {
            string key = _RolesManagerCacheKey + "ModuleAction/" + userName;
            List<ModuleActionKeyValue> dics = HHCache.Instance.Get(key) as List<ModuleActionKeyValue>;
            if (dics == null)
            {
                dics = PermissionDataProvider.Instance.ModuleActionKeyValues(userName);
                HHCache.Instance.Insert(key, dics);
            }
            return dics;
        }
        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="moduleActionId"></param>
        /// <returns></returns>
        public static RoleOpts AddRole(Role role, string moduleActionId)
        {
            RoleOpts result =PermissionDataProvider.Instance.AddRole(role, moduleActionId);
            if (result == RoleOpts.Success)
                HHCache.Instance.Remove(_RolesManagerCacheKey + "AllRoles");
            return result;
        }
        /// <summary>
        /// 获取所有权限操作
        /// </summary>
        /// <returns></returns>
        public static List<Action> LoadAllActions()
        {
            return CacheInstance<Action>(_LoadAllActions, "AllActions");
        }
        /// <summary>
        /// 获取所有权限角色
        /// </summary>
        /// <returns></returns>
        public static List<Role> LoadAllRoles()
        {
            return CacheInstance<Role>(_LoadAllRoles, "AllRoles");
        }
        /// <summary>
        /// 根据ModuleID获取ModuleAction
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        public static List<ModuleAction> LoadModuleActions(int ModuleID)
        {
            return CacheInstance<ModuleAction>(_LoadModuleActions, "ModuleAction" + ModuleID.ToString(), ModuleID);
        }
        /// <summary>
        /// 角色名是否已存在
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static bool IsRoleExist(string roleName)
        {
            return PermissionDataProvider.Instance.IsRoleExist(roleName);
        }
        /// <summary>
        /// 获取所有的ModuleAction集合
        /// </summary>
        /// <returns></returns>
        public static List<Module> LoadAllModulesFromModuleAction()
        {
            return CacheInstance<Module>(_LoadAllModulesFromModuleAction, "AllModuleActions");
        }
        /// <summary>
        /// 根据ID获取Module详细信息
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        public static Module SelectModule(int ModuleID)
        {
            return PermissionDataProvider.Instance.SelectModule(ModuleID);
        }
        /// <summary>
        /// 根据ModuleID获取所有的Action列表
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        public static List<Action> SelectActionsByModuleID(int ModuleID)
        {
            return CacheInstance<Action>(_SelectActionsByModuleID, "Action_" + ModuleID.ToString(), ModuleID);
        }
        #endregion

        #region -Private Methods-
        static void _LoadModuleActions(ref object list, params object[] ModuleID)
        {
            list = PermissionDataProvider.Instance.LoadModuleActions((int)ModuleID[0]);
        }
        static void _LoadAllActions(ref object list, params object[] ActionID)
        {
            list = PermissionDataProvider.Instance.LoadAllActions();
        }
        static void _LoadAllRoles(ref object list, params object[] RoleID)
        {
            list = PermissionDataProvider.Instance.LoadAllRoles();
        }
        static void _LoadAllModulesFromModuleAction(ref object list, params object[] ModuleActionID)
        {
            list = PermissionDataProvider.Instance.LoadAllModulesFromModuleAction();
        }
        static void _SelectActionsByModuleID(ref object list, params object[] ModuleID)
        {
            list = PermissionDataProvider.Instance.SelectActionsByModuleID((int)ModuleID[0]);
        }
        #endregion
    }
}
