using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace DataStorage.Entities
{
	public sealed class VideoChannel
	{
		#region Helpers

		internal sealed class Configuration : EntityTypeConfiguration<VideoChannel>
		{
			public Configuration()
			{
				ToTable("VideoChannels");

				HasKey(x => x.Id);

				Property(x => x.Id)
					.HasColumnName("Id")
					.IsRequired()
					.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

				Property(x => x.Name)
					.HasColumnName("Name")
					.IsRequired();

				Property(x => x.Order)
					.HasColumnName("Order")
					.IsRequired();

				Property(x => x.CreatedDate)
					.HasColumnName("CreatedDate")
					.IsRequired();

				Property(x => x.ServerId)
					.HasColumnName("ServerId")
					.IsRequired();

				HasRequired(x => x.Server)
					.WithMany()
					.HasForeignKey(x => x.ServerId)
					.WillCascadeOnDelete(false);

				HasIndex(x => x.CreatedDate)
					.HasName("IX_CreatedDate");

				HasIndex(x => x.Order)
					.HasName("IX_Order");

				HasIndex(x => new { x.Order, x.ServerId })
					.HasName("IX_Order_ServerId")
					.IsUnique(true);

			}
		}

		#endregion

		internal long Id { get; set; }

		public int Order { get; set; }

		public string Name { get; set; }

		public DateTime CreatedDate { get; set; }

		internal long ServerId { get; set; }

		public Server Server { get; set; }

		internal static Configuration GetConfiguration() => new Configuration();
	}
}
