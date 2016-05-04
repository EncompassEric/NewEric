using System.Data.Entity.Validation;

namespace Data.MainBoundedContext.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Crosscutting.Logging;
    using Domain.MainBoundedContext.ModuleAgg;
    using Mapping;
    using Seedwork;

    public partial class MainBCUnitOfWork
		: DbContext, IQueryableUnitOfWork
	{
		public MainBCUnitOfWork()
			: base("Data.MainBoundedContext.UnitOfWork")
		{

		}
		#region IDbSet Members

		#endregion

		#region IQueryableUnitOfWork Members

		public DbSet<TEntity> CreateSet<TEntity>()
			where TEntity : class
		{
			return base.Set<TEntity>();
		}

		public void Attach<TEntity>(TEntity item)
			where TEntity : class
		{
			//attach and set as unchanged
			base.Entry<TEntity>(item).State = System.Data.EntityState.Unchanged;
		}

		public void SetModified<TEntity>(TEntity item)
			where TEntity : class
		{
			//this operation also attach item in object state manager
			base.Entry<TEntity>(item).State = System.Data.EntityState.Modified;
		}
		public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current)
			where TEntity : class
		{
			//if it is not attached, attach original and set current values
			base.Entry<TEntity>(original).CurrentValues.SetValues(current);
		}

		public void Commit()
		{
			try
			{
				base.SaveChanges();
			}
			catch (Exception ex)
			{
				LoggerFactory.CreateLog().LogError(ex.Message);
				throw new EntityException(ex.Message, ex);
			}
		}

		public void CommitAndRefreshChanges()
		{
			bool saveFailed = false;

			do
			{
				try
				{
					base.SaveChanges();

					saveFailed = false;

				}
				catch (DbUpdateConcurrencyException ex)
				{
					saveFailed = true;

					ex.Entries.ToList()
							  .ForEach(entry =>
							  {
								  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
							  });

				}
			} while (saveFailed);

		}

		public void RollbackChanges()
		{
			// set all entities in change tracker 
			// as 'unchanged state'
			base.ChangeTracker.Entries()
							  .ToList()
							  .ForEach(entry => entry.State = System.Data.EntityState.Unchanged);
		}

		public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
		{
			return base.Database.SqlQuery<TEntity>(sqlQuery, parameters);
		}

		public int ExecuteCommand(string sqlCommand, params object[] parameters)
		{
			return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
		}

		#endregion

		#region Init menu
		public static void Init(List<Module> allModules)
		{
			
		}
		#endregion
	}
}
