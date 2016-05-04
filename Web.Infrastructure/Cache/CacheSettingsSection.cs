namespace Web.Infrastructure.Cache
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Cache的配置信息
    /// </summary>
    public sealed class CacheSettingsSection : ConfigurationSection
    {
        /// <summary>
        /// 获取Cache的配置信息
        /// </summary>
        /// <returns>Cache的配置信息</returns>
        public static CacheSettingsSection GetConfig()
        {
            CacheSettingsSection result = (CacheSettingsSection)ConfigurationManager.GetSection("cacheSetting");

            if (result == null)
                result = new CacheSettingsSection();

            return result;
        }

        private CacheSettingsSection()
        {
        }

        /// <summary>
        /// 缺省缓存时间,以秒为单位
        /// </summary>
        [ConfigurationProperty("defaultcachetime", DefaultValue = 60)]
        public int DefaultCacheTime
        {
            get
            {
                return (int)this["defaultcachetime"];
            }
        }

        /// <summary>
        /// 清理间隔
        /// </summary>
        public TimeSpan ScanvageInterval
        {
            get
            {
                return TimeSpan.FromSeconds(this.ScanvageIntervalSeconds);
            }
        }

        [ConfigurationProperty("scanvageInterval", DefaultValue = 60)]
        private int ScanvageIntervalSeconds
        {
            get
            {
                return (int)this["scanvageInterval"];
            }
        }

        /// <summary>
        /// 缓存大小
        /// </summary>
        [ConfigurationProperty("cachesize", DefaultValue = 500)]
        public int CacheSize
        {
            get
            {
                return (int)this["cachesize"];
            }
        }
    }
}
