using DataStorage.Entities;
using DataStorage.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage.Repositories
{
	public sealed class AdoNetRepository
	{
		private readonly SqlServerConnectionFactory _connectionFactory;

		public AdoNetRepository()
		{
			_connectionFactory = new SqlServerConnectionFactory();
		}

		public IReadOnlyList<ServerMetadata> FetchMetaData() 
		{
			using(var connection = _connectionFactory.CreateConnection()) 
			{
				connection.Open();

				using(var command = connection.CreateCommand()) 
				{
					command.CommandText = "Select [Id], [HostName] from ServerMetadata";
					var reader = command.ExecuteReader();
					var result = new List<ServerMetadata>();

					while(reader.Read()) 
					{
						var entity = new ServerMetadata();
						

						entity.Id = reader.GetFieldValue<long>(0);
						entity.HostName = reader.GetFieldValue<string>(1);
						result.Add(entity);
					}
					reader.Close();

					return result;
				}
			}
		}

		public int AddServerMetadata(IReadOnlyList<ServerMetadata> entities) 
		{
			if(entities.Count == 0) return 0;

			using(var connection = _connectionFactory.CreateConnection())
			{
				connection.Open();
				using(var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted))
				{
					try
					{
						for(var i = 0; i < entities.Count; i++)
						{
							using(var command = connection.CreateCommand())
							{
								command.Transaction = transaction;
								command.CommandText = "INSERT INTO ServerMetadata (HostName, Discriminator) VALUES(@hostname, @discriminator)";

								var hostNameParameter = command.CreateParameter();
								hostNameParameter.ParameterName = "@hostname";
								hostNameParameter.Value = entities[i].HostName;

								var descrminatorParameter = command.CreateParameter();
								descrminatorParameter.ParameterName = "@discriminator";
								descrminatorParameter.Value = "ServerMetadata";

								command.Parameters.Add(hostNameParameter);
								command.Parameters.Add(descrminatorParameter);

								command.ExecuteNonQuery();
							}
						}
						transaction.Commit();
						return entities.Count;
					}
					catch(SqlException)
					{
						transaction.Rollback();
						throw;
					}
				}
			}
		}
	}
}
