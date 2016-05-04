namespace Application.MainBoundedContext.Services
{
    using System.Collections.Generic;
    using Domain.MainBoundedContext.MenuAgg;
    using Domain.Seedwork.Specification;
    using Seedwork.Services;

    public class MenuService:DefaultService<Menu,IMenuRepository>,IMenuService
	{
		public MenuService(IMenuRepository repository) : base(repository)
		{
			
		}

		public IEnumerable<Menu> GetRootMenus()
		{
			ISpecification<Menu> spec = MenuSpecification.RootMenu();

			IEnumerable<Menu> menus = Repository.AllMatching(spec);

			return menus;
		}

		public IEnumerable<Menu> GetSubMenus(string resourceKey)
		{
			ISpecification<Menu> spec = MenuSpecification.SubMenuByResourceKey(resourceKey);

			IEnumerable<Menu> menus = Repository.AllMatching(spec);

			return menus;
		}
	}
}
