using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage.Providers
{
	public sealed class SqlServerConnectionFactory : IConnectionFactory
	{
		private readonly string _connectionString;

		public SqlServerConnectionFactory()
		{
			var connectionBuilder = new SqlConnectionStringBuilder
			{
				DataSource = @"DESKTOP-O2VNVEV\SQLEXPRESS",
				InitialCatalog = "RecognitionLogService",
				IntegratedSecurity = true,
			};
			_connectionString = connectionBuilder.ToString();
		}


		public DbConnection CreateConnection()
		{
			return new SqlConnection(_connectionString);
		}
	}
}
