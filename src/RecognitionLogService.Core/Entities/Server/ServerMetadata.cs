using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataStorage.Entities
{
	public class ServerMetadata
	{
		#region Helpers

		internal sealed class Configuration : IEntityTypeConfiguration<ServerMetadata>
		{
			public void Configure(EntityTypeBuilder<ServerMetadata> builder)
			{
				builder.ToTable("ServerMetadata");

				builder.HasKey(x => x.Id);

				builder
					.Property(x => x.Id)
					.HasColumnName("Id")
					.IsRequired()
					.ValueGeneratedOnAdd();

				builder
					.Property(x => x.HostName)
					.HasColumnName("HostNameTest")
					.IsRequired();
			}
		}

		#endregion

		public long Id { get; set; }

		public string HostName { get; set; }

		internal static Configuration GetConfiguration() => new Configuration();
	}
}
