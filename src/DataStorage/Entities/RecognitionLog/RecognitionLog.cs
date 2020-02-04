using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataStorage.Entities
{
	public sealed class RecognitionLog
	{
		#region Helpers

		internal sealed class Configuration : EntityTypeConfiguration<RecognitionLog>
		{
			public Configuration()
			{
				ToTable("RecognitionLog");

				HasKey(x => x.Id);

				Property(x => x.Id)
					.HasColumnName("Id")
					.IsRequired()
					.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

				Property(x => x.Plate)
					.HasColumnName("Plate")
					.IsRequired()
					.HasMaxLength(50);

				HasIndex(x => x.Plate)
					.HasName("IX_Plate");

				Property(x => x.TimeStamp)
					.HasColumnName("TimeStamp")
					.IsRequired();

				HasIndex(x => x.TimeStamp)
					.HasName("IX_TimeStamp");

				Property(x => x.Status)
					.HasColumnName("RecognitionStatus")
					.IsRequired();

				HasIndex(x => x.Status)
					.HasName("IX_RecognitionStatus");

				Property(x => x.ChannelDirection)
					.HasColumnName("ChannelDirection")
					.IsRequired();

				HasIndex(x => x.Status)
					.HasName("IX_ChannelDirection");

				Property(x => x.DeletedDate)
					.HasColumnName("DeletedDate")
					.IsOptional();

				Property(x => x.VideoChannelId)
					.HasColumnName("VideoChannelId")
					.IsRequired();

				HasRequired(x => x.VideoChannel)
					.WithMany()
					.HasForeignKey(x => x.VideoChannelId)
					.WillCascadeOnDelete(false);
			}
		}

		#endregion

		internal long Id { get; set; }

		public string Plate { get; set; }

		public DateTime TimeStamp { get; set; }

		public RecognitionStatus Status { get; set; }

		public ChannelDirection ChannelDirection { get; set; }

		public DateTime? DeletedDate { get; set; }

		internal long VideoChannelId { get; set; }

		public VideoChannel VideoChannel { get; set; }

		internal static Configuration GetConfiguration() => new Configuration();
	}
}
