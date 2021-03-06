﻿namespace Data.Seedwork
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using Crosscutting.Logging;
    using Domain.Seedwork;

    /// <summary>
	/// Repository base class
	/// </summary>
	/// <typeparam name="TEntity">The type of underlying entity in this repository</typeparam>
	public class Repository<TEntity> : IRepository<TEntity>
		where TEntity : Entity
	{
		#region Members

		IQueryableUnitOfWork _UnitOfWork;

		#endregion

		#region Constructor

		/// <summary>
		/// Create a new instance of repository
		/// </summary>
		/// <param name="unitOfWork">Associated Unit Of Work</param>
		public Repository(IQueryableUnitOfWork unitOfWork)
		{
			if (unitOfWork == (IUnitOfWork)null)
				throw new ArgumentNullException("unitOfWork");

			_UnitOfWork = unitOfWork;
		}

		#endregion

		#region IRepository Members

		/// <summary>
		/// <see cref="IRepository{TEntity}"/>
		/// </summary>
		public IUnitOfWork UnitOfWork
		{
			get
			{
				return _UnitOfWork;
			}
		}

		/// <summary>
		/// <see cref="IRepository{TEntity}"/>
		/// </summary>
		/// <param name="item"><see cref="IRepository{TEntity}"/></param>
		public virtual void Add(TEntity item)
		{

			if (item != (TEntity)null)
				GetSet().Add(item); // add new item in this set
			else
			{
				LoggerFactory.CreateLog()
						  .LogInfo("Cannot add null entity into {0} repository", typeof(TEntity).ToString());

			}

		}
		/// <summary>
		/// <see cref="IRepository{TEntity}"/>
		/// </summary>
		/// <param name="item"><see cref="IRepository{TEntity}"/></param>
		public virtual void Remove(TEntity item)
		{
			if (item != (TEntity)null)
			{
				//attach item if not exist
				_UnitOfWork.Attach(item);

				//set as "removed"
				GetSet().Remove(item);
			}
			else
			{
				LoggerFactory.CreateLog()
						  .LogInfo("Cannot remove null entity into {0} repository", typeof(TEntity).ToString());
			}
		}

		/// <summary>
		/// <see cref="IRepository{TEntity}"/>
		/// </summary>
		/// <param name="item"><see cref="IRepository{TEntity}"/></param>
		public virtual void TrackItem(TEntity item)
		{
			if (item != (TEntity)null)
				_UnitOfWork.Attach<TEntity>(item);
			else
			{
				LoggerFactory.CreateLog()
						  .LogInfo("Cannot track null item into {0} repository", typeof(TEntity).ToString());
			}
		}

		/// <summary>
		/// <see cref="IRepository{TEntity}"/>
		/// </summary>
		/// <param name="item"><see cref="IRepository{TEntity}"/></param>
		public virtual void Modify(TEntity item)
		{
			if (item != (TEntity)null)
				_UnitOfWork.SetModified(item);
			else
			{
				LoggerFactory.CreateLog()
						  .LogInfo("Cannot modify null item into {0} repository", typeof(TEntity).ToString());
			}
		}

		/// <summary>
		/// <see cref="IRepository{TEntity}"/>
		/// </summary>
		/// <param name="id"><see cref="IRepository{TEntity}"/></param>
		/// <returns><see cref="IRepository{TEntity}"/></returns>
		public virtual TEntity Get(Guid id)
		{
			if (id != Guid.Empty)
				return GetSet().Find(id);
			else
				return null;
		}
		/// <summary>
		/// <see cref="IRepository{TEntity}"/>
		/// </summary>
		/// <returns><see cref="IRepository{TEntity}"/></returns>
		public virtual IEnumerable<TEntity> GetAll()
		{
			return GetSet();
		}
		/// <summary>
		/// <see cref="IRepository{TEntity}"/>
		/// </summary>
		/// <param name="specification"><see cref="IRepository{TEntity}"/></param>
		/// <returns><see cref="IRepository{TEntity}"/></returns>
		public virtual IEnumerable<TEntity> AllMatching(Domain.Seedwork.Specification.ISpecification<TEntity> specification)
		{
			return GetSet().Where(specification.SatisfiedBy());
		}

		public virtual IEnumerable<TEntity> GetMatchingPaged(
			Domain.Seedwork.Specification.ISpecification<TEntity> specification, int pageIndex, int pageCount,
			string orderByExpression,out int itemCount)
		{
			var set = GetSet();

			var result= set.Where(specification.SatisfiedBy()).OrderUsingSortExpression(orderByExpression);

			itemCount = result.Count();

			return result.Skip(pageCount*(pageIndex-1))
				.Take(pageCount);

		}
		/// <summary>
		/// <see cref="IRepository{TEntity}"/>
		/// </summary>
		/// <typeparam name="S"><see cref="IRepository{TEntity}"/></typeparam>
		/// <param name="pageIndex"><see cref="IRepository{TEntity}"/></param>
		/// <param name="pageCount"><see cref="IRepository{TEntity}"/></param>
		/// <param name="orderByExpression"><see cref="IRepository{TEntity}"/></param>
		/// <param name="ascending"><see cref="IRepository{TEntity}"/></param>
		/// <returns><see cref="IRepository{TEntity}"/></returns>
		public virtual IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount, System.Linq.Expressions.Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending,out int itemCount)
		{
			var set = GetSet();

			itemCount = set.Count();

			if (ascending)
			{
				return set.OrderBy(orderByExpression)
						  .Skip(pageCount * (pageIndex - 1))
						  .Take(pageCount);
			}
			else
			{
				return set.OrderByDescending(orderByExpression)
						  .Skip(pageCount * (pageIndex-1))
						  .Take(pageCount);
			}
		}
		/// <summary>
		/// <see cref="IRepository{TEntity}"/>
		/// </summary>
		/// <param name="filter"><see cref="IRepository{TEntity}"/></param>
		/// <returns><see cref="IRepository{TEntity}"/></returns>
		public virtual IEnumerable<TEntity> GetFiltered(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
		{
			return GetSet().Where(filter);
		}

		/// <summary>
		/// <see cref="IRepository{TEntity}"/>
		/// </summary>
		/// <param name="persisted"><see cref="IRepository{TEntity}"/></param>
		/// <param name="current"><see cref="IRepository{TEntity}"/></param>
		public virtual void Merge(TEntity persisted, TEntity current)
		{
			_UnitOfWork.ApplyCurrentValues(persisted, current);
		}
        /// <summary>
        /// <see cref="IRepository{TEntity}"/>
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameters"></param>
        public virtual int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
           return _UnitOfWork.ExecuteCommand(sqlCommand, parameters);
        }

		#endregion

		#region IDisposable Members

		/// <summary>
		/// <see cref="M:System.IDisposable.Dispose"/>
		/// </summary>
		public void Dispose()
		{
			if (_UnitOfWork != null)
				_UnitOfWork.Dispose();
		}

		#endregion

		#region Private Methods

		IDbSet<TEntity> GetSet()
		{
			try
			{
				return _UnitOfWork.CreateSet<TEntity>();
			}
			catch (Exception ex)
			{
				LoggerFactory.CreateLog().LogError(ex.Message);
				throw new EntityException();
			}
		}
		#endregion
	}
}
