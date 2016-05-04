
namespace Domain.MainBoundedContext.RoleAgg
{
    using System.Collections.Generic;
    using Seedwork;
    using UserAgg;

    public class Role:Entity
	{
		public string Name { get; set; }

		public List<User> Users { get; set; }
	}
}
