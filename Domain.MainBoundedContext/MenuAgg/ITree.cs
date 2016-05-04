
namespace Domain.MainBoundedContext.MenuAgg
{
    using System;
    using System.Collections.Generic;
    using Domain.Seedwork;

    public interface ITree
	{
		Guid? ParentId { get; set; }
	}

	public interface ITree<out T>:ITree where T:Entity
	{
		IEnumerable<T> GetChildren();

		T Parent { get; }
	}
}
