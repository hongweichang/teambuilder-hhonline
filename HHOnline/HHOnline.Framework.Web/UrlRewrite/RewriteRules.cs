using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using HHOnline.Cache;
using System.Text.RegularExpressions;

namespace HHOnline.Framework.Web.UrlRewrite
{
    /// <summary>
    /// 地址重写规则
    /// </summary>
    public class RewriteRules
    {
        private static readonly int _MAXCACHE = 1024;
        private static List<UrlRewriteSection> rules = UrlRewriteConfig.LoadSections();
        private static NameValueCollection cache = new NameValueCollection(_MAXCACHE);
        /// <summary>
        /// 重写Url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Parse(string url)
        {
            string key = GlobalSettings.Encrypt(url);
            string newUrl = LoadCache(key);
            if (!string.IsNullOrEmpty(newUrl)) { return newUrl; }
           
            #region -Ignore-
            //Match match = null;
            //Regex regex = null;
            //string[] newUrls = null;
            #endregion

            string relative = GlobalSettings.RelativeWebRoot;
            RegexOptions ro = RegexOptions.None;
            foreach (UrlRewriteSection r in rules)
            {
                #region -Ignore-
                //regex = new Regex(relative + r.VirtualUrl, (r.IgnoreCase ? RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace : RegexOptions.None));
                //match = regex.Match(url);
                //if (match.Success)
                //{
                //    GroupCollection gc = match.Groups;
                //    newUrls = new string[gc.Count];
                //    int i = 0;
                //    foreach (Group g in gc)
                //    {
                //        newUrls[i++] = g.Value;
                //    }
                //    newUrl = String.Format(r.DestinationUrl, newUrls);
                //    UpdateCache(key, newUrl);
                //    return newUrl;
                //}
                #endregion

                if (r.IgnoreCase) ro = (RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                if (Regex.IsMatch(url, relative + r.VirtualUrl, ro))
                {
                    return Regex.Replace(url, relative + r.VirtualUrl, r.DestinationUrl, ro);
                }
            }
            return null;
        }

        #region -Private Method-
        private static string LoadCache(string key)
        {
            if (cache[key] != null)
                return cache[key];
            else
                return null;
        }

        private static void UpdateCache(string key, string val)
        {
            if (cache.Count >=_MAXCACHE)
            {
                cache.Remove(cache.GetKey(0));
            }
            cache.Add(key, val);
        }
        #endregion
    }
}
