namespace Data.MainBoundedContext.Repositories
{
    using Domain.MainBoundedContext.MenuAgg;
    using Seedwork;
    using UnitOfWork;

    public class MenuRepository:Repository<Menu>,IMenuRepository
	{
		public MenuRepository(MainBCUnitOfWork unitOfWork) : base(unitOfWork)
		{
			
		}
	}
}
