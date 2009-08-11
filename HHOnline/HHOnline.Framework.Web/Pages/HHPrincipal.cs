using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Collections;
using HHOnline.Permission.Components;
using System.Text.RegularExpressions;

namespace HHOnline.Framework.Web.Pages
{
    public class HHPrincipal:IPrincipal
    {
        public HHPrincipal(HHIdentity identity)
            : this(identity, null)
        { }
        public HHPrincipal(HHIdentity identity, List<ModuleActionKeyValue> roles)
        {
            this.roles = roles;
            this.identity = identity;
            //this.permissionAction = HHConfiguration.GetConfig()["permissionAction"].ToString(); 
            //regex = new Regex("(\\w+)(" + this.permissionAction + ")", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            
        }
        #region -IPrincipal Members-
        private List<ModuleActionKeyValue> roles;
        /// <summary>
        /// 角色队列
        /// </summary>
        public List<ModuleActionKeyValue> Roles
        {
            get { return roles; }
            set { roles = value; }
        }
        private HHIdentity identity = null;
        //private string permissionAction;
        ///// <summary>
        ///// 权限操作, Add|Edit...
        ///// </summary>
        //public string PermissionAction
        //{
        //    get { return permissionAction; }
        //    set { permissionAction = value; }
        //}
        public IIdentity Identity
        {
            get { return identity; }
        }
        /// <summary>
        /// 角色是否存在
        /// </summary>
        /// <param name="role">ModuleName_ActionName组成的键值对</param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            string[] r = role.Split('-');
            if (r.Length != 2)
                return false;

            foreach (ModuleActionKeyValue kv in roles)
            {
                if (kv.ModuleName == r[0] && kv.ActionName == r[1])
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
