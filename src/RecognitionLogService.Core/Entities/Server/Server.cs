using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataStorage.Entities
{
	public sealed class Server
	{
		#region Helpers

		internal sealed class Configuration : IEntityTypeConfiguration<Server>
		{
			public void Configure(EntityTypeBuilder<Server> builder)
			{
				builder.ToTable("Servers");

				builder.HasKey(x => x.Id);

				builder
					.Property(x => x.Id)
					.HasColumnName("Id")
					.IsRequired()
					.ValueGeneratedOnAdd();

				builder
					.Property(x => x.Name)
					.HasColumnName("Name")
					.IsRequired();

				builder
					.Property(x => x.MetadataId)
					.HasColumnName("MetadataId")
					.IsRequired();

				builder
					.HasOne(x => x.Metadata)
					.WithMany()
					.HasForeignKey(x => x.MetadataId)
					.IsRequired()
					.OnDelete(DeleteBehavior.Cascade);

				builder
					.Property(x => x.IpV4MetaDataId)
					.HasColumnName("IpV4MetaDataId")
					.IsRequired();

				builder
					.HasOne(x => x.IpV4MetaData)
					.WithMany()
					.HasForeignKey(x => x.IpV4MetaDataId)
					.IsRequired()
					.OnDelete(DeleteBehavior.Restrict);

				builder
					.Property(x => x.IpV6MetaDataId)
					.HasColumnName("IpV6MetaDataId")
					.IsRequired();

				builder
					.HasOne(x => x.IpV6MetaData)
					.WithMany()
					.HasForeignKey(x => x.IpV6MetaDataId)
					.IsRequired()
					.OnDelete(DeleteBehavior.Restrict);
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
