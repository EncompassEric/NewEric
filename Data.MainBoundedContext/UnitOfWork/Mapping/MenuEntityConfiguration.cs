
namespace Data.MainBoundedContext.UnitOfWork.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.MainBoundedContext.MenuAgg;

    public class MenuEntityConfiguration : EntityTypeConfiguration<Menu>
	{
		public MenuEntityConfiguration()
		{
			this.HasKey(m => m.Id);

			this.Property(m => m.IsRoot).IsOptional().HasColumnName("IsRoot");
			this.Property(m => m.IsInherit).IsOptional().HasColumnName("IsInherit");
			this.Property(m => m.Enabled).IsRequired().HasColumnName("Enabled");
			this.Property(m => m.IsPublic).IsOptional().HasColumnName("IsPublich");
			this.Property(m => m.NameResourceKey).IsRequired().HasColumnName("NameResourceKey");
			this.Property(m => m.DisplayOrder).IsOptional().HasColumnName("DisplayOrder");
			
			this.HasOptional(m => m.Action).WithRequired().Map(m => m.MapKey("ActionId"));

			this.HasMany(m => m.Children).WithOptional().HasForeignKey(m => m.ParentId);
		}
	}
}
