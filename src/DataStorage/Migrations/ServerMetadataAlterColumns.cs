namespace DataStorage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServerMetadataAlterColumns : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ServerMetadata", name: "HostName", newName: "HostNameTest");
            RenameColumn(table: "dbo.ServerMetadata", name: "IpV4", newName: "IpAddressV4");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.ServerMetadata", name: "IpAddressV4", newName: "IpV4");
            RenameColumn(table: "dbo.ServerMetadata", name: "HostNameTest", newName: "HostName");
        }
    }
}
