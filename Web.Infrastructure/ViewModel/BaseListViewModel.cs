
namespace Web.Infrastructure.ViewModel
{
    using System.Collections.Generic;
    using Domain.Seedwork;

    public class BaseListViewModel<TEntity> where TEntity:Entity
	{
		public IEnumerable<TEntity> Entities { get; set; }

		public PagingInfo PageInfo { get; set; }
	}
}
