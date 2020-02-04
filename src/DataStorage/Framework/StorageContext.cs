using DataStorage.Entities;
using DataStorage.Framework;
using System.Data.Common;
using System.Data.Entity;

namespace DataStorage
{
	[DbConfigurationType(typeof(DbContextConfiguration))]
	public class Context : DbContext
	{
		public Context(DbConnection connection) : base(connection, true)
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Configurations.Add(ServerMetadata.GetConfiguration());
			modelBuilder.Configurations.Add(ServerIpV4.GetIpV4Configuration());
			modelBuilder.Configurations.Add(ServerIpV6.GetIpV6Configuration());
			modelBuilder.Configurations.Add(Server.GetConfiguration());
			modelBuilder.Configurations.Add(VideoChannel.GetConfiguration());
			modelBuilder.Configurations.Add(RecognitionLog.GetConfiguration());
		}
	}
}
