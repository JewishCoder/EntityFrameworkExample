using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataStorage.Entities
{
	public sealed class ServerIpV6 : ServerMetadata
	{
		#region Helpers

		internal sealed class IpV6Configuration : IEntityTypeConfiguration<ServerIpV6>
		{
			public void Configure(EntityTypeBuilder<ServerIpV6> builder)
			{
				builder
					.Property(x => x.IpV6)
					.HasColumnName("IpV6")
					.IsRequired();

				builder
					.Property(x => x.PortV6)
					.HasColumnName("PortV6");
			}
		}

		#endregion

		public string IpV6 { get; set; }

		public string PortV6 { get; set; }

		internal static IpV6Configuration GetIpV6Configuration() => new IpV6Configuration();
	}
}
