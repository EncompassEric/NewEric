namespace Application.Seedwork.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IService<TEntity>
	{
		void Add(TEntity entity);

		TEntity Get(Guid id);

		IEnumerable<TEntity> GetAll();

		IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount,
		                                         Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending,out int itemCount);

		void Update(TEntity entity);

		void Delete(Guid id);
	}
}
