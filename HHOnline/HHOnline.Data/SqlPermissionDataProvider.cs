using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.AOP;
using System.Data;
using HHOnline.Permission.Components;
using HHOnline.Common;

namespace HHOnline.Data
{
    public class SqlPermissionDataProvider : Permission.Providers.PermissionDataProvider
    {
        public override List<Role> LoadAllRoles()
        {
            using (IDataReader reader = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Permission_GetAllRoles"))
            {
                List<Role> roles = new List<Role>();
                Role r = null;
                while (reader.Read())
                {
                    r = ConvertDataReader2RoleEntity(reader);
                    roles.Add(r);
                }
                return roles;
            }
        }

        public override RoleOpts DeleteRole(int roleID)
        {
            return (RoleOpts)DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_Permission_DeleteRole", new ELParameter("RoleID", DbType.Int32, roleID));
        }

        public override bool AddUsersToRole(string userIdList,UserRole userRole)
        {
            return ((int)DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_Permission_AddUsersToRole", new ELParameter[]{
                new ELParameter("UserIDList",DbType.String,userIdList),
                new ELParameter("RoleID",DbType.Int32,userRole.RoleID),
                new ELParameter("CreateTime",DbType.DateTime,userRole.CreateTime),
                new ELParameter("CreateUser",DbType.Int32,userRole.CreateUser),
                new ELParameter("UpdateTime",DbType.DateTime,userRole.UpdateTime),
                new ELParameter("UpdateUser",DbType.Int32,userRole.UpdateUser)
            }) != 0);
        }

        public override List<Action> LoadAllActions()
        {
            using (IDataReader reader = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Permission_GetAllActions"))
            {
                List<Action> actions = new List<Action>();
                Action a = null;
                while (reader.Read())
                {
                    a = ConvertDataReader2ActionEntity(reader);
                    actions.Add(a);
                }
                return actions;
            }
        }

        public override bool IsRoleExist(string roleName)
        {
            object result = DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_permission_isroleexist", 
                new ELParameter("RoleName",DbType.String,roleName));
            if ((int)result > 0)
                return true;
            else
                return false;
        }

        public override List<Module> LoadAllModulesFromModuleAction()
        {
            using (IDataReader reader = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Permission_GetAllModulesFromModuleAction"))
            {
                List<Module> modules = new List<Module>();
                Module m = null;
                while (reader.Read())
                {
                    m = ConvertDataReader2ModuleEntity(reader);
                    modules.Add(m);
                }
                return modules;
            }
        }

        public override Module SelectModule(int ModuleID)
        {
            using (IDataReader reader = DataHelper.ExecuteReader(CommandType.StoredProcedure, 
                        "sp_Permission_SelectModule", 
                        new ELParameter("ModuleID", DbType.Int32, ModuleID)))
            { 
                Module module=null;
                while (reader.Read())
                {
                    module = ConvertDataReader2ModuleEntity(reader);
                }
                return module;
            }
        }

        public override List<Action> SelectActionsByModuleID(int ModuleID)
        {
            using (IDataReader reader = DataHelper.ExecuteReader(CommandType.StoredProcedure,
                "sp_Permission_SelectActionsByMOduleID",
                new ELParameter("ModuleID", DbType.Int32, ModuleID)))
            {
                Action a = null;
                List<Action> actions = new List<Action>();
                while (reader.Read())
                {
                    a = ConvertDataReader2ActionEntity(reader);
                    actions.Add(a);
                }
                return actions;
            }
        }

        public override List<ModuleAction> LoadModuleActions(int ModuleID)
        {
            using (IDataReader reader = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Permission_GetModuleActionByModuleID", new ELParameter("ModuleID", DbType.Int32, ModuleID)))
            {
                ModuleAction ma = null;
                List<ModuleAction> mas = new List<ModuleAction>();
                while (reader.Read())
                {
                    ma = ConvertDataReader2ModuleActionEntity(reader);
                    mas.Add(ma);
                }
                return mas;
            }
        }

        public override RoleOpts AddRole(Role role, string moduleActionId)
        {
            return (RoleOpts)DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_Permission_AddRole", new ELParameter[]{
                new ELParameter("CreateTime",DbType.DateTime,role.CreateTime),
                new ELParameter("CreateUser",DbType.Int32,role.CreateUser),
                new ELParameter("RoleDesc",DbType.String,role.Description),
                new ELParameter("RoleMemo",DbType.String,role.Memo),
                new ELParameter("RoleName",DbType.String,role.RoleName),
                new ELParameter("RoleStatus",DbType.Int32,role.RoleStatus),
                new ELParameter("UpdateTime",DbType.DateTime,role.UpdateTime),
                new ELParameter("UpdateUser",DbType.Int32,role.UpdateUser),
                new ELParameter("ModuleActionID",DbType.String,moduleActionId)
            });
        }
        public override RoleOpts EditRole(Role role, string moduleActionId)
        {
            return (RoleOpts)DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_Permission_EditRole", new ELParameter[]{
                new ELParameter("RoleDesc",DbType.String,role.Description),
                new ELParameter("RoleMemo",DbType.String,role.Memo),
                new ELParameter("RoleName",DbType.String,role.RoleName),
                new ELParameter("UpdateTime",DbType.DateTime,role.UpdateTime),
                new ELParameter("UpdateUser",DbType.Int32,role.UpdateUser),
                new ELParameter("RoleID",DbType.Int32,role.RoleID),
                new ELParameter("ModuleActionID",DbType.String,moduleActionId)
            });
        }


        public override List<ModuleActionKeyValue> ModuleActionKeyValues(string userName)
        {
            using (IDataReader reader = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Permission_ModuleActionValues", new ELParameter("UserName", DbType.String, userName)))
            {
                List<ModuleActionKeyValue> dics = new List<ModuleActionKeyValue>();
                ModuleActionKeyValue makv = null;
                while (reader.Read())
                {
                    makv = new ModuleActionKeyValue(DataRecordHelper.GetString(reader, "ModuleName"),
                                 DataRecordHelper.GetString(reader, "ActionName"));
                    dics.Add(makv);
                }
                return dics;
            }
        }

        public override Role SelectRole(int roleID)
        {
            using (IDataReader reader = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Permission_SelectRole", new ELParameter("RoleID", DbType.Int32, roleID)))
            {
                Role role = null;
                while (reader.Read())
                {
                    role= ConvertDataReader2RoleEntity(reader);
                }
                return role;
            }
        }

        public override List<ModuleAction> LoadModuleAction(int roleID)
        {
            using (IDataReader reader = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Permission_SelectModuleAction", new ELParameter("RoleID", DbType.Int32, roleID)))
            {
                List<ModuleAction> mas = new List<ModuleAction>();
                while (reader.Read())
                {
                    mas.Add(ConvertDataReader2ModuleActionEntity(reader));
                }
                return mas;
            }
        }
    }
}
