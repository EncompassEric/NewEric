
namespace Data.MainBoundedContext.Initializer
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using Domain.MainBoundedContext.ModuleAgg;
    using UnitOfWork;

    public class StandardInitializer:CreateDatabaseIfNotExists<MainBCUnitOfWork>
	{
		public List<Module> AllModules { get; set; }

		protected override void Seed(MainBCUnitOfWork context)
		{
			#region Initialize modules

			foreach (Module module in AllModules)
			{
				context.Modules.Add(module);
			}

			context.Commit();

			#endregion
		}
	}
}
