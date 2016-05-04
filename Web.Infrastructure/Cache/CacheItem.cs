namespace Web.Infrastructure.Cache
{
    using System;

    /// <summary>
    /// 缓存项
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CacheItem<T> : IComparable<CacheItem<T>>
    {
        
        /// <summary>
        /// 缓存对象
        /// </summary>
        public T CacheObject { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cacheObject"></param>
        public CacheItem(T cacheObject, DateTime expiredTime)
        {
            CacheObject = cacheObject;
            ExpiredTime = expiredTime;           
            CreateTime = DateTime.Now;
            LastModifyTime = CreateTime;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cacheObject"></param>
        public CacheItem(T cacheObject)
            : this(cacheObject, DateTime.Now.AddSeconds(CacheSettingsSection.GetConfig().DefaultCacheTime))
        {            
        }

        /// <summary>
        /// 创建事件
        /// </summary>
        public DateTime CreateTime { get; private set; }

        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime LastAccessTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifyTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiredTime { get; set; }

        /// <summary>
        /// 是否过期
        /// </summary>
        public bool IsExpired { get { return DateTime.Now > ExpiredTime; } }

        /// <summary>
        /// 访问次数
        /// </summary>
        public int VisitCount { get; set; }

        public int CompareTo(CacheItem<T> other)
        {
            return this.VisitCount - other.VisitCount;
        }
    }
}
