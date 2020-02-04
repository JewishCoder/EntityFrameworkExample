using DataStorage.Providers;
using DataStorage.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
	public sealed class RepositoryFactory
	{
		private readonly Dictionary<Type, object> _cache;

		public RepositoryFactory(IConnectionFactory connectionFactory)
		{
			_cache = new Dictionary<Type, object>
			{
				[typeof(IServerIpV4Repository)] = new ServerIpV4Repository(connectionFactory),
			};
		}

		public T GetRepository<T>() 
		{
			if(_cache.TryGetValue(typeof(T), out var repository)) 
			{
				return (T)repository;
			}

			throw new InvalidOperationException("Repository not found");
		}
	}
}
