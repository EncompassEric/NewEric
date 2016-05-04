
namespace Domain.MainBoundedContext.MenuAgg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Domain.MainBoundedContext.PrivilegeAgg;
    using Seedwork;
    using Action = ActionAgg.Action;

    public class Menu:Entity,ITree<Menu>
	{
		private string _nameResourceKey;

		[NotMapped]
		public string Name { get; set; }

		public bool? IsRoot { get; set; }

		public bool? IsInherit { get; set; }

		public List<Privilege> Privileges { get; set; }

		public Guid? ActionId { get; set; }

		public virtual Action Action { get; set; }

		public bool Enabled { get; set; }

		public bool? IsPublic { get; set; }

		public string NameResourceKey { get { return _nameResourceKey; } set { _nameResourceKey = value;
			Name = GetResourceKey(_nameResourceKey);
		}
		}

		public int? DisplayOrder { get; set; }

		public Guid? ParentId { get; set; }

		public virtual List<Menu> Children { get; set; }
 
		public IEnumerable<Menu> GetChildren()
		{
			return Children == null ? null : Children.AsEnumerable();
		}

		public virtual Menu Parent { get;set; }

		private string GetResourceKey(string key)
		{
			string value = NameResourceKey;

			Type type = typeof(Resource.Language);

			PropertyInfo property = type.GetProperty(key.Replace(".", "_"));

			if (property != null)
			{
				MemberExpression me = Expression.Property(null, property);

				value = Convert.ToString(Expression.Lambda<Func<object>>(me).Compile()());
			}

			return value;
		}
	}
}
