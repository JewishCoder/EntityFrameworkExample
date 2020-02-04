using System.Data.Entity.ModelConfiguration;

namespace DataStorage.Entities
{
	public sealed class ServerIpV6 : ServerMetadata
	{
		#region Helpers

		internal sealed class IpV6Configuration : EntityTypeConfiguration<ServerIpV6>
		{
			public IpV6Configuration()
			{
				Property(x => x.IpV6)
					.HasColumnName("IpV6")
					.IsRequired();

				Property(x => x.PortV6)
					.HasColumnName("PortV6")
					.IsOptional();
			}
		}

		#endregion

		public string IpV6 { get; set; }

		public string PortV6 { get; set; }

		internal static IpV6Configuration GetIpV6Configuration() => new IpV6Configuration();
	}
}
