using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage.Framework
{
	sealed class FilterBuilder<TEntity>
	{
		private Expression<Func<TEntity, bool>> _cache;

		public Expression<Func<TEntity, bool>> Build(Expression<Func<TEntity, bool>> expression) 
		{
			if(_cache == null) 
			{
				_cache = expression;
				return _cache;
			}

			var newExpression = Expression.AndAlso(_cache.Body, expression.Body);
			_cache = Expression.Lambda<Func<TEntity, bool>>(newExpression, _cache.Parameters);
			return _cache;
		}
	}
}
