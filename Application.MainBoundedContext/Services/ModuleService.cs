namespace Application.MainBoundedContext.Services
{
    using Domain.MainBoundedContext.ModuleAgg;
    using Seedwork.Services;

    public class ModuleService:DefaultService<Module,IModuleRepository>,IModuleService
	{
		public ModuleService(IModuleRepository repository) : base(repository)
		{
			
		}
	}
}
