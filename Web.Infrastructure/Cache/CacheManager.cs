namespace Web.Infrastructure.Cache
{
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// 缓存管理器
    /// </summary>
    public class CacheManager
    {
        private static List<IScavenge> _ScavengePool = new List<IScavenge>();
       
        private static CacheManager _Instance = null;
        /// <summary>
        /// 单利模式实现缓存管理器
        /// </summary>
        public static CacheManager Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new CacheManager();
                return CacheManager._Instance;
            }
        }

        
        static CacheManager()
        {
            Thread thread = new Thread(new ThreadStart(ScavengeExpiredItem));
            thread.Name = "Cache Scavenge Thread";
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.Lowest;
            thread.Start();
        }
        /// <summary>
        /// 添加缓存扫描委托
        /// </summary>
        /// <param name="delegateCache"></param>
        public void Add(IScavenge delegateCache)
        {
            if (!_ScavengePool.Contains(delegateCache))
            {
                _ScavengePool.Add(delegateCache);
            }
        }
        /// <summary>
        /// 删除缓存扫描委托
        /// </summary>
        /// <param name="delegateCache"></param>
        public void Remove(IScavenge delegateCache)
        {
            if (!_ScavengePool.Contains(delegateCache))
            {
                _ScavengePool.Remove(delegateCache);
            }
        }
        /// <summary>
        /// 扫描器数量
        /// </summary>
        public int Count { get { return _ScavengePool.Count; } }

        /// <summary>
        /// 扫描清理过期缓存项
        /// </summary>
        private static void ScavengeExpiredItem()
        {
            while (true)
            {
                _ScavengePool.ForEach(i => { i.DoScavenge(); });
                Thread.Sleep(CacheSettingsSection.GetConfig().ScanvageInterval);
            }
        }

    }
}
