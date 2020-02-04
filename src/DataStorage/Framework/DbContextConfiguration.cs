using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Data.Entity.SqlServerCompact;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage.Framework
{
	/// <summary>Переопределение конфигурации EntityFramework.</summary>
	/// <remarks>Для разный СУБД требуется разная конфигурация загрузки и создания подключение к БД.</remarks>
	public class DbContextConfiguration : DbConfiguration
	{
		public DbContextConfiguration()
		{
			SetSqlServerProvider();
			SetSqlServerCeProvider();
			//SetPostgreProvider();
		}

		#region Methods

		protected virtual void SetSqlServerProvider()
		{
			const string SqlName = "System.Data.SqlClient";

			SetProviderFactory(
				providerInvariantName: SqlName,
				providerFactory: SqlClientFactory.Instance);

			SetProviderServices(
				providerInvariantName: SqlName,
				provider: SqlProviderServices.Instance);

			SetDefaultConnectionFactory(connectionFactory: new SqlConnectionFactory());
		}

		protected virtual void SetSqlServerCeProvider()
		{
			const string SqlCeName = "System.Data.SqlServerCe.4.0";

			SetProviderFactory(
				providerInvariantName: SqlCeName,
				providerFactory: SqlCeProviderFactory.Instance);

			SetProviderServices(
				providerInvariantName: SqlCeName,
				provider: SqlCeProviderServices.Instance);

			SetDefaultConnectionFactory(connectionFactory: new SqlCeConnectionFactory(SqlCeName));
		}

		//protected virtual void SetPostgreProvider()
		//{
		//	//Данные взяты из https://www.npgsql.org/ef6/index.html
		//	const string PostgreSqlName = "Npgsql";

		//	SetProviderFactory(
		//		providerInvariantName: PostgreSqlName,
		//		providerFactory: NpgsqlFactory.Instance);

		//	SetProviderServices(
		//		providerInvariantName: PostgreSqlName,
		//		provider: NpgsqlServices.Instance);

		//	SetDefaultConnectionFactory(connectionFactory: new NpgsqlConnectionFactory());
		//}

		#endregion
	}
}
