using DataStorage.Providers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
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
					var connection = factory.CreateConnection();
					var context = new Context(connection);
					context.Database.CreateIfNotExists();

					var migrationConfig = new Migrations.Configuration
					{
						TargetDatabase = new DbConnectionInfo(connection.ConnectionString, "System.Data.SqlClient")
					};
					var migrator = new DbMigrator(migrationConfig);
					var migrations = migrator.GetPendingMigrations();
					if(migrations.Any())
					{
						migrator.Update();
					}

					connection.Dispose();
					context.Dispose();
					return true;
				}
				catch(DbException exc) 
				{
					return false;
				}

			}, cancellationToken);
		}
	}
}
