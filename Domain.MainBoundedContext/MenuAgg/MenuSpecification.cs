namespace Domain.MainBoundedContext.MenuAgg
{
    using Seedwork.Specification;

    public static class MenuSpecification
	{
		public static Specification<Menu> SubMenuByResourceKey(string resourceKey)
		{
			Specification<Menu> spec = new DirectSpecification<Menu>(
					menu => menu.Parent.NameResourceKey == resourceKey);

			
			return spec;
		}

		public static Specification<Menu> RootMenu()
		{
			Specification<Menu> spec=new DirectSpecification<Menu>(m=>m.IsRoot??false);

			return spec;
		}
	}
}
