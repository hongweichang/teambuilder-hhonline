using System;
using System.Collections;
using System.Text.RegularExpressions;
using HHOnline.Cache;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    public class DisallowedNames
    {
        /// <summary>
        /// 判断名称是否被禁用
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>true:disallowed;vice versa</returns>
        public static bool NameIsDisallowed(string name)
        {
            string wildcard = ".*?";
            ArrayList bannedNames = GetNames();
            if (bannedNames == null)
                return false;
            foreach (string bannedName in bannedNames)
            {
                string pattern = "^" + bannedName.Replace("*", wildcard) + "$";
                var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                if (regex.IsMatch(name))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 获取禁用名列表
        /// </summary>
        /// <returns>当前禁用名列表</returns>
        public static ArrayList GetNames()
        {
            ArrayList namesCollection = HHCache.Instance.Get(CacheKeyManager.DisallowedNamesKey) as ArrayList;
            if (namesCollection == null)
            {
                CommonDataProvider dp = CommonDataProvider.Instance;
                namesCollection = dp.GetDisallowedNames();
                HHCache.Instance.Insert(CacheKeyManager.DisallowedNamesKey, namesCollection, 2);
            }
            return namesCollection;
        }

        public static int DeleteName(string name)
        {
            return CreateUpdateDeleteName(name, null, DataProviderAction.Delete);
        }

        public static int UpdateName(string newName, string oldName)
        {
            return CreateUpdateDeleteName(oldName, newName, DataProviderAction.Update);
        }

        public static int CreateName(string newName)
        {
            return CreateUpdateDeleteName(newName, null, DataProviderAction.Create);
        }

        private static int CreateUpdateDeleteName(string name, string replacement, DataProviderAction action)
        {
            HHCache.Instance.Remove(CacheKeyManager.DisallowedNamesKey);

            CommonDataProvider dp = CommonDataProvider.Instance;

            return dp.CreateUpdateDeleteDisallowedName(name, replacement, action);
        }
    }
}
