namespace Data.Seedwork
{
    using System;
    using System.Linq.Expressions;
    using System.Text;

    public class Util
	{
		public static Expression<Func<TEntity, object>> CreateGetFuncFor<TEntity>(string propertyName)
		{
			/*	
			var parameter = Expression.Parameter(typeof(TEntity), "obj");
			var property = Expression.Property(parameter, propertyName);

			var convert = Expression.Convert(property, typeof(object));
			var lambda = Expression.Lambda<Func<TEntity, object>>( convert, parameter);

			return lambda;
			
			var parameter = Expression.Parameter(typeof(TEntity), "obj");

			var propertyInfo = typeof(TEntity).GetProperty(propertyName);
			Expression expr = null;
			expr = Expression.Property(parameter, propertyName);

			if (propertyInfo.PropertyType.IsValueType)
			{
				expr = Expression.Convert(expr, typeof(object));
			}

			var lambda = Expression.Lambda<Func<TEntity, object>>(expr, parameter);

			return lambda; * */
			throw new NotImplementedException();
		}

		public static string GetSortExpression(string fieldName, bool descending)
		{
			StringBuilder sb = new StringBuilder(fieldName);

			if (descending)
			{
				sb.AppendFormat(" {0}", Constant.Descending);
			}

			return sb.ToString();
		}
	}
}
