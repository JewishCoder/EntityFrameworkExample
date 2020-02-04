using DataStorage.Providers;
using System;
using System.Collections.Generic;
using System.Data.Common;

using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataStorage
{
	public sealed class DatabaseInitializationService
	{
		public static Task<bool> InitializeAsync(CancellationToken cancellationToken) 
		{
			return Task.Run(() =>
			{
				var factory = new SqlServerConnectionFactory();
				try
				{
					var context = new Context(factory.CreateConnection());
					context.Database.CreateIfNotExists();

					return true;
				}
				catch(DbException) 
				{
					return false;
				}

			}, cancellationToken);
		}
	}
}
