using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using HHOnline.Common;
using HHOnline.Framework;
using HHOnline.Permission.Components;
using HHOnline.Cache;

namespace HHOnline.Permission.Providers
{
    /// <summary>
    /// 权限驱动
    /// </summary>
    public abstract class PermissionDataProvider
    {
        #region -Constructor-
        private static readonly PermissionDataProvider _instance = null;

        static PermissionDataProvider()
        {
            _instance = HHContainer.Create().Resolve<PermissionDataProvider>();
        }
        /// <summary>
        /// 获取权限驱动
        /// </summary>
        public static PermissionDataProvider Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        public abstract List<Role> LoadAllRoles();
        public abstract List<Action> LoadAllActions();
        public abstract bool IsRoleExist(string roleName);
        public abstract List<Module> LoadAllModulesFromModuleAction();
        public abstract List<ModuleAction> LoadModuleActions(int ModuleID);
        public abstract Module SelectModule(int ModuleID);
        public abstract List<Action> SelectActionsByModuleID(int ModuleID);
        public abstract RoleOpts AddRole(Role role, string moduleActionId);
        public abstract RoleOpts EditRole(Role role, string moduleActionId);
        public abstract List<ModuleActionKeyValue> ModuleActionKeyValues(string userName);
        public abstract Role SelectRole(int roleID);
        public abstract List<ModuleAction> LoadModuleAction(int roleID);
        public abstract RoleOpts DeleteRole(int roleID);
        public abstract bool AddUsersToRole(string userIdList, UserRole userRole);
        #region -Public Methods-
        public static Role ConvertDataReader2RoleEntity(IDataReader reader)
        {
            return new Role(DataRecordHelper.GetInt32(reader, "RoleID"),
                                    DataRecordHelper.GetString(reader, "RoleName"),
                                    DataRecordHelper.GetString(reader, "RoleDesc"),
                                    DataRecordHelper.GetString(reader, "RoleMemo"),
                                    DataRecordHelper.GetInt32(reader, "RoleStatus"),
                                    DataRecordHelper.GetDateTime(reader, "CreateTime"),
                                    DataRecordHelper.GetInt32(reader, "CreateUser"),
                                    DataRecordHelper.GetDateTime(reader, "UpdateTime"),
                                    DataRecordHelper.GetInt32(reader, "UpdateUser"));
        }
        public static Action ConvertDataReader2ActionEntity(IDataReader reader)
        {
            return new Action(DataRecordHelper.GetInt32(reader, "ActionID"),
                                    DataRecordHelper.GetString(reader, "ActionName"),
                                    DataRecordHelper.GetString(reader, "ActionDesc"));
        }
        public static ModuleAction ConvertDataReader2ModuleActionEntity(IDataReader reader)
        {
            return new ModuleAction(DataRecordHelper.GetInt32(reader, "ModuleActionID"),
                                    DataRecordHelper.GetInt32(reader, "ModuleID"),
                                    DataRecordHelper.GetInt32(reader, "ActionID"),
                                    DataRecordHelper.GetString(reader, "ModuleActionName"),
                                    DataRecordHelper.GetString(reader, "ModuleActionDesc"));
        }
        public static Module ConvertDataReader2ModuleEntity(IDataReader reader)
        {
            return new Module(DataRecordHelper.GetInt32(reader, "ModuleID"),
                                    DataRecordHelper.GetString(reader, "ModuleShortName"),
                                    DataRecordHelper.GetString(reader, "ModuleName"),
                                    DataRecordHelper.GetString(reader, "ModuleDesc"));
        }
        #endregion
    }
}
