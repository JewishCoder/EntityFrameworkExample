namespace DataStorage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServerMetadata",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        HostName = c.String(nullable: false),
                        IpV4 = c.String(),
                        PortV4 = c.Int(),
                        IpV6 = c.String(),
                        PortV6 = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Servers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        MetadataId = c.Long(nullable: false),
                        IpV4MetaDataId = c.Long(nullable: false),
                        IpV6MetaDataId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServerMetadata", t => t.IpV4MetaDataId)
                .ForeignKey("dbo.ServerMetadata", t => t.IpV6MetaDataId)
                .ForeignKey("dbo.ServerMetadata", t => t.MetadataId, cascadeDelete: true)
                .Index(t => t.MetadataId)
                .Index(t => t.IpV4MetaDataId)
                .Index(t => t.IpV6MetaDataId);
            
            CreateTable(
                "dbo.VideoChannels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ServerId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Servers", t => t.ServerId)
                .Index(t => t.Order)
                .Index(t => new { t.Order, t.ServerId }, unique: true)
                .Index(t => t.CreatedDate);
            
            CreateTable(
                "dbo.RecognitionLog",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Plate = c.String(nullable: false, maxLength: 50),
                        TimeStamp = c.DateTime(nullable: false),
                        RecognitionStatus = c.Int(nullable: false),
                        ChannelDirection = c.Int(nullable: false),
                        DeletedDate = c.DateTime(),
                        VideoChannelId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VideoChannels", t => t.VideoChannelId)
                .Index(t => t.Plate)
                .Index(t => t.TimeStamp)
                .Index(t => t.RecognitionStatus, name: "IX_ChannelDirection")
                .Index(t => t.VideoChannelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecognitionLog", "VideoChannelId", "dbo.VideoChannels");
            DropForeignKey("dbo.VideoChannels", "ServerId", "dbo.Servers");
            DropForeignKey("dbo.Servers", "MetadataId", "dbo.ServerMetadata");
            DropForeignKey("dbo.Servers", "IpV6MetaDataId", "dbo.ServerMetadata");
            DropForeignKey("dbo.Servers", "IpV4MetaDataId", "dbo.ServerMetadata");
            DropIndex("dbo.RecognitionLog", new[] { "VideoChannelId" });
            DropIndex("dbo.RecognitionLog", "IX_ChannelDirection");
            DropIndex("dbo.RecognitionLog", new[] { "TimeStamp" });
            DropIndex("dbo.RecognitionLog", new[] { "Plate" });
            DropIndex("dbo.VideoChannels", new[] { "CreatedDate" });
            DropIndex("dbo.VideoChannels", new[] { "Order", "ServerId" });
            DropIndex("dbo.VideoChannels", new[] { "Order" });
            DropIndex("dbo.Servers", new[] { "IpV6MetaDataId" });
            DropIndex("dbo.Servers", new[] { "IpV4MetaDataId" });
            DropIndex("dbo.Servers", new[] { "MetadataId" });
            DropTable("dbo.RecognitionLog");
            DropTable("dbo.VideoChannels");
            DropTable("dbo.Servers");
            DropTable("dbo.ServerMetadata");
        }
    }
}
