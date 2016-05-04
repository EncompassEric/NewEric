
namespace Data.MainBoundedContext.Repositories
{
    using Domain.MainBoundedContext.ModuleAgg;
    using Seedwork;
    using UnitOfWork;

    public class ModuleRepository:Repository<Module>,IModuleRepository
	{
		public ModuleRepository(MainBCUnitOfWork unitOfWork) : base(unitOfWork)
		{
			
		}
	}
}
