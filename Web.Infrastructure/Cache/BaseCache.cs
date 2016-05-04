namespace Web.Infrastructure.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Collections;

    /// <summary>
    /// 缓存基类
    /// </summary>
    public abstract class BaseCache<TKey, TValue> : IScavenge, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// 缓存状态,1:正常,0:扫描,-1:清理
        /// </summary>
        private CacheState CacheState = CacheState.Running;

        protected BaseCache()
            : this(CacheSettingsSection.GetConfig().CacheSize)
        { }

        protected BaseCache(int cacheSize)
        {
            CacheSize = CacheSettingsSection.GetConfig().CacheSize;
            CacheManager.Instance.Add(this);
        }
        /// <summary>
        /// 缓存池
        /// </summary>
        protected Dictionary<TKey, CacheItem<TValue>> _Cache = new Dictionary<TKey, CacheItem<TValue>>();
        private static readonly object Synchronization = new object();

        /// <summary>
        /// 获取指定缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TValue Get(TKey key)
        {
            return Load(key);
        }

        /// <summary>
        /// 获取指定缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TValue Load(TKey key)
        {
            if (_Cache.ContainsKey(key))
            {
                CacheItem<TValue> item = _Cache[key];
                item.VisitCount++;
                item.ExpiredTime.AddSeconds(CacheSettingsSection.GetConfig().DefaultCacheTime);
                item.LastAccessTime = DateTime.Now;
                return item.CacheObject;
            }
            return default(TValue);
        }

        /// <summary>
        /// 按Key索引
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TValue this[TKey key]
        {
            get { return Load(key); }
        }

        /// <summary>
        /// 添加缓存缓存项
        /// 以默认配置缓存时间进行缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public virtual void Add(TKey key, TValue value)
        {
            this.Add(key, value, DateTime.Now.AddSeconds(CacheSettingsSection.GetConfig().DefaultCacheTime));
        }

        /// <summary>
        /// 添加缓存项
        /// 指定过期时间
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expiredTime">过期时间</param>
        public virtual void Add(TKey key, TValue value, DateTime expiredTime)
        {
            if (!_Cache.ContainsKey(key))
            {
                lock (Synchronization)
                {
                     _Cache.Add(key, new CacheItem<TValue>(value, expiredTime));
                }
            }
        }

        /// <summary>
        /// 更新缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void Update(TKey key, TValue value)
        {
            if (_Cache.ContainsKey(key))
            {
                lock (Synchronization)
                {
                    CacheItem<TValue> item = _Cache[key];
                    item.CacheObject = value;
                    _Cache[key] = item;
                }
            }
        }

        /// <summary>
        /// 添加或更新缓存项
        /// </summary>
        /// <param name="key">缓存项key</param>
        /// <param name="value">缓存项value</param>
        public virtual void AddOrUpdate(TKey key, TValue value)
        {
            if (!_Cache.ContainsKey(key))
            {
                Add(key, value);
            }
            else
            {
                Update(key, value);
            }
        }

        /// <summary>
        /// 是否包含指定缓存项
        /// </summary>
        /// <param name="key">缓存项的key</param>
        /// <returns></returns>
        public virtual bool ContainsKey(TKey key)
        {
            return _Cache.ContainsKey(key);
        }

        /// <summary>
        /// 删除指定缓存项
        /// </summary>
        /// <param name="key"></param>
        public virtual void Delete(TKey key)
        {
            lock (Synchronization)
            {
                if (_Cache.ContainsKey(key))
                    _Cache.Remove(key);               
            }
        }

        /// <summary>
        /// 清除所有缓存项
        /// </summary>
        public virtual void Clear()
        {
            lock (Synchronization)
            {
                CacheState = CacheState.Cleaning;
                _Cache.Clear();
            }
        }

        /// <summary>
        /// 扫描并删除过期项
        /// </summary>
        public virtual void DoScavenge()
        {
            lock (Synchronization)
            {
                CacheState = CacheState.Scanning;
                List<TKey> expiredItems = new List<TKey>();
                foreach (var item in _Cache)
                {
                    if (item.Value.IsExpired)
                        expiredItems.Add(item.Key);
                }
                if (expiredItems.Count > 0)
                {
                    CacheState = CacheState.Cleaning;
                    expiredItems.ForEach(i => { _Cache.Remove(i); });
                }
                CacheState = CacheState.Running;
            }
        }

        /// <summary>
        /// 缓存大小
        /// </summary>
        public int CacheSize { get; private set; }

        /// <summary>
        /// 缓存数量
        /// </summary>
        public int Count { get { return _Cache.Count; } }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var item in _Cache)
            {
                KeyValuePair<TKey, TValue> kvp = new KeyValuePair<TKey, TValue>(item.Key, item.Value.CacheObject);
                yield return kvp;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in _Cache)
            {
                KeyValuePair<TKey, TValue> kvp = new KeyValuePair<TKey, TValue>(item.Key, item.Value.CacheObject);
                yield return kvp;
            }
        }
    }
}
