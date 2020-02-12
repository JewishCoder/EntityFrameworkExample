using System.Data.Entity.ModelConfiguration;

namespace DataStorage.Entities
{
	public sealed class ServerIpV4 : ServerMetadata
	{
		#region Helpers

		internal sealed class IpV4Configuration : EntityTypeConfiguration<ServerIpV4>
		{
			public IpV4Configuration()
			{
				Property(x => x.IpV4)
					.HasColumnName("IpAddressV4")
					.IsRequired();

				Property(x => x.PortV4)
					.HasColumnName("PortV4")
					.IsOptional();
			}
		}

		#endregion

		public string IpV4 { get; set; }

		public int? PortV4 { get; set; }

		internal static IpV4Configuration GetIpV4Configuration() => new IpV4Configuration();
	}
}
