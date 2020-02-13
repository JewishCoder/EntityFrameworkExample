

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataStorage.Entities
{
	public sealed class ServerIpV4 : ServerMetadata
	{
		#region Helpers

		internal sealed class IpV4Configuration : IEntityTypeConfiguration<ServerIpV4>
		{
			public void Configure(EntityTypeBuilder<ServerIpV4> builder)
			{
				builder
					.Property(x => x.IpV4)
					.HasColumnName("IpAddressV4")
					.IsRequired();

				builder
					.Property(x => x.PortV4)
					.HasColumnName("PortV4");
			}
		}

		#endregion

		public string IpV4 { get; set; }

		public int? PortV4 { get; set; }

		internal static IpV4Configuration GetIpV4Configuration() => new IpV4Configuration();
	}
}
