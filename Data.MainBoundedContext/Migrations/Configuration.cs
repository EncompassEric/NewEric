namespace Data.MainBoundedContext.Migrations
{
    using System.Data.Entity.Migrations;
    using UnitOfWork;

    public sealed class Configuration : DbMigrationsConfiguration<MainBCUnitOfWork>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}
	}
}
