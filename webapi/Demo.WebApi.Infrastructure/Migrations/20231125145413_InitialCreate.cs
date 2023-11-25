using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.WebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT COLLATE NOCASE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppClients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AddressLine = table.Column<string>(type: "TEXT COLLATE NOCASE", nullable: false),
                    IsUSA = table.Column<bool>(type: "INTEGER", nullable: false),
                    ZipCode = table.Column<string>(type: "TEXT COLLATE NOCASE", nullable: true),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppAddresses_AppClients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AppClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppAddresses_ClientId",
                table: "AppAddresses",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppAddresses");

            migrationBuilder.DropTable(
                name: "AppClients");
        }
    }
}
