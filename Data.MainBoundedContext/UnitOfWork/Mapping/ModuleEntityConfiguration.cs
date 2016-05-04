namespace Data.MainBoundedContext.UnitOfWork.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.MainBoundedContext.ModuleAgg;

    public class ModuleEntityConfiguration : EntityTypeConfiguration<Module>
	{
		public ModuleEntityConfiguration()
		{
			this.HasKey(m => m.Id);

			this.Property(m => m.ClassName).IsRequired().HasMaxLength(128).HasColumnName("ClassName");
			this.Property(m => m.NameSpace).IsRequired().HasMaxLength(128).HasColumnName("NameSpace");
		}
	}
}
