using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataStorage.Entities
{
	public class ServerMetadata : IEntity
	{
		#region Helpers

		internal sealed class Configuration : EntityTypeConfiguration<ServerMetadata>
		{
			public Configuration()
			{
				ToTable("ServerMetadata");

				HasKey(x => x.Id);

				Property(x => x.Id)
					.HasColumnName("Id")
					.IsRequired()
					.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

				Property(x => x.HostName)
					.HasColumnName("HostName")
					.IsRequired();
			}
		}

		#endregion

		public long Id { get; set; }


		public string HostName { get; set; }
		

		internal static Configuration GetConfiguration() => new Configuration();

	}
}
