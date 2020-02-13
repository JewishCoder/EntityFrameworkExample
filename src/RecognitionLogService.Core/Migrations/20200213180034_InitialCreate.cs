using Microsoft.EntityFrameworkCore.Migrations;

namespace RecognitionLogService.Core.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServerMetadata",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostNameTest = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    IpAddressV4 = table.Column<string>(nullable: true),
                    PortV4 = table.Column<int>(nullable: true),
                    IpV6 = table.Column<string>(nullable: true),
                    PortV6 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerMetadata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    MetadataId = table.Column<long>(nullable: false),
                    IpV4MetaDataId = table.Column<long>(nullable: false),
                    IpV6MetaDataId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servers_ServerMetadata_IpV4MetaDataId",
                        column: x => x.IpV4MetaDataId,
                        principalTable: "ServerMetadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servers_ServerMetadata_IpV6MetaDataId",
                        column: x => x.IpV6MetaDataId,
                        principalTable: "ServerMetadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servers_ServerMetadata_MetadataId",
                        column: x => x.MetadataId,
                        principalTable: "ServerMetadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servers_IpV4MetaDataId",
                table: "Servers",
                column: "IpV4MetaDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_IpV6MetaDataId",
                table: "Servers",
                column: "IpV6MetaDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_MetadataId",
                table: "Servers",
                column: "MetadataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "ServerMetadata");
        }
    }
}
