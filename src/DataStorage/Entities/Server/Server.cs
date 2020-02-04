using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataStorage.Entities
{
	public sealed class Server
	{
		#region Helpers

		internal sealed class Configuration : EntityTypeConfiguration<Server>
		{
			public Configuration()
			{
				ToTable("Servers");

				HasKey(x => x.Id);

				Property(x => x.Id)
					.HasColumnName("Id")
					.IsRequired()
					.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

				Property(x => x.Name)
					.HasColumnName("Name")
					.IsRequired();

				Property(x => x.MetadataId)
					.HasColumnName("MetadataId")
					.IsRequired();

				HasRequired(x => x.Metadata)
					.WithMany()
					.HasForeignKey(x => x.MetadataId)
					.WillCascadeOnDelete(true);

				Property(x => x.IpV4MetaDataId)
					.HasColumnName("IpV4MetaDataId")
					.IsRequired();

				HasRequired(x => x.IpV4MetaData)
					.WithMany()
					.HasForeignKey(x => x.IpV4MetaDataId)
					.WillCascadeOnDelete(false);

				Property(x => x.IpV6MetaDataId)
					.HasColumnName("IpV6MetaDataId")
					.IsRequired();

				HasRequired(x => x.IpV6MetaData)
					.WithMany()
					.HasForeignKey(x => x.IpV6MetaDataId)
					.WillCascadeOnDelete(false);
			}
		}

		#endregion

		internal long Id { get; set; }

		public string Name { get; set; }

		internal long MetadataId { get; set; }

		public ServerMetadata Metadata { get; set; }

		internal long IpV4MetaDataId { get; set; }

		public ServerIpV4 IpV4MetaData { get; set; }

		internal long IpV6MetaDataId { get; set; }

		public ServerIpV6 IpV6MetaData { get; set; }

		internal static Configuration GetConfiguration() => new Configuration();
	}
}
