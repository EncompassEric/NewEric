namespace Application.MainBoundedContext.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Domain.Seedwork;
    using Seedwork;
    using Seedwork.Services;

    public abstract class DefaultService<TEntity,TRepository>:IService<TEntity> where TEntity:Entity
								where TRepository:IRepository<TEntity>
	{
		private readonly TRepository _repository;

		protected TRepository Repository
		{
			get { return _repository; }
		}

		public DefaultService(TRepository repository)
		{
			_repository = repository;
		}

		public void Add(TEntity entity)
		{
			if (entity == null || entity.Id == Guid.Empty)
			{
				throw new ArgumentException();
			}

			Save(entity);
		}

		public TEntity Get(Guid id)
		{
			TEntity entity = _repository.Get(id);

			return entity;
		}

		public IEnumerable<TEntity> GetAll()
		{
			IEnumerable<TEntity> entities = _repository.GetAll();

			return entities;
		}

		public IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending,out int itemCount)
		{
			IEnumerable<TEntity> entities = _repository.GetPaged(pageIndex, pageCount, orderByExpression, ascending,out itemCount);

			return entities;
		}

		public void Update(TEntity entity)
		{
			if (entity == null || entity.Id == Guid.Empty)
			{
				throw new ArgumentException();
			}

			TEntity persisted = _repository.Get(entity.Id);

			_repository.Merge(persisted, entity);

			_repository.UnitOfWork.Commit();
		}

		public void Delete(Guid id)
		{
			TEntity entity = _repository.Get(id);

			_repository.Remove(entity);

			_repository.UnitOfWork.Commit();
		}

		protected virtual void Save(TEntity entity)
		{
            //var validator = EntityValidatorFactory.CreateValidator();

            //if (validator.IsValid(entity))
            //{
				_repository.Add(entity);
				_repository.UnitOfWork.Commit();
            //}
            //else
            //{
            //    throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<TEntity>(entity));
            //}
		}
	}
}
