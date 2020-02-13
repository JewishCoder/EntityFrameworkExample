using DataStorage.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace RecognitionLogService.Core.Framework
{
	class StorageContext : DbContext
	{
		private readonly DbConnection _connection;

		public StorageContext(DbConnection connection)
		{
			_connection = connection;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_connection);
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(ServerMetadata.GetConfiguration());
			modelBuilder.ApplyConfiguration(ServerIpV4.GetIpV4Configuration());
			modelBuilder.ApplyConfiguration(ServerIpV6.GetIpV6Configuration());
			modelBuilder.ApplyConfiguration(Server.GetConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
