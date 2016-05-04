namespace Web.Infrastructure.Cache
{
	/// <summary>
    /// 缓存扫描清理接口
    /// </summary>
    public interface IScavenge
    {
        void DoScavenge();
    }
}
