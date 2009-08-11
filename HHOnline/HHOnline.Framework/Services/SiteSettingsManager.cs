using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Cache;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    /// <summary>
    /// 站点配置管理
    /// </summary>
    public class SiteSettingsManager
    {

        private SiteSettingsManager()
        {

        }

        /// <summary>
        /// 获取站点配置
        /// </summary>
        /// <returns></returns>
        public static SiteSettings GetSiteSettings()
        {
            SiteSettings settings = HHCache.Instance.Get(CacheKeyManager.SiteSettingsKey) as SiteSettings;
            if (settings == null)
            {
                settings = CommonDataProvider.Instance.LoadSiteSettings();
                HHCache.Instance.Max(CacheKeyManager.SiteSettingsKey, settings);
            }
            return settings;
        }

        /// <summary>
        /// 保持站点配置
        /// </summary>
        /// <param name="settings"></param>
        public static void Save(SiteSettings settings)
        {
            CommonDataProvider.Instance.SaveSiteSettings(settings);
            HHCache.Instance.Remove(CacheKeyManager.SiteSettingsKey);
        }
    }
}
