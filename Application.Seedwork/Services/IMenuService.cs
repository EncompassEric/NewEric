namespace Application.Seedwork.Services
{
    using System.Collections.Generic;
    using Domain.MainBoundedContext.MenuAgg;

    public interface IMenuService:IService<Menu>
	{
		IEnumerable<Menu> GetRootMenus();
		IEnumerable<Menu> GetSubMenus(string resourceKey);
	}
}
