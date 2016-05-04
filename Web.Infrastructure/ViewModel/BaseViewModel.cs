
namespace Web.Infrastructure.ViewModel
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Domain.Seedwork;

    [Serializable]
    public class BaseViewModel<TEntity> where TEntity:Entity, new()
	{
		public BaseViewModel()
		{
			
		}

		public BaseViewModel(TEntity entity)
		{
            PropertyInfo[] modelProperties = entity.GetType().GetProperties();

            PropertyInfo[] viewProperties = this.GetType().GetProperties();

			foreach (PropertyInfo viewProperty in viewProperties)
			{
				PropertyInfo property=modelProperties.FirstOrDefault(p => p.Name == viewProperty.Name);

				if (property != null)
				{
					viewProperty.SetValue(this, property.GetValue(entity));
				}
			}
		}

		public TEntity CastEntity()
		{
			TEntity entity=new TEntity();

			PropertyInfo[] modelProperties = entity.GetType().GetProperties();

			PropertyInfo[] viewProperties = this.GetType().GetProperties();

			foreach (PropertyInfo viewProperty in viewProperties)
			{
				PropertyInfo property = modelProperties.FirstOrDefault(p => p.Name == viewProperty.Name);

				if (property != null)
				{
					property.SetValue(entity, viewProperty.GetValue(this));
				}
			}

			return entity;
		}
	}
}
