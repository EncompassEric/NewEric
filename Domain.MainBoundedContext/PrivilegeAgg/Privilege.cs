
namespace Domain.MainBoundedContext.PrivilegeAgg
{
    using System;
    using RoleAgg;
    using Seedwork;
    using UserAgg;
    using MenuAgg;

    public class Privilege:Entity
	{
		public Guid? RoleId { get; set; }

		public Role Role { get; set; }

		public Guid? UserId { get; set; }

		public User User { get; set; }

		public Guid MenuItemId { get; set; }

		public Menu MenuItem { get; set; }

		public bool? IsAllowed { get; set; }
	}
}
