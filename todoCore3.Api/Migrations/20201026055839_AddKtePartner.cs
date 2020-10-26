using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace todoCore3.Api.Migrations
{
    public partial class AddKtePartner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KtePartners",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WPUserId = table.Column<int>(nullable: false),
                    UserLogin = table.Column<string>(nullable: true),
                    UserNickName = table.Column<string>(nullable: true),
                    UserEmail = table.Column<string>(nullable: true),
                    PartnerName = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KtePartners", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KtePartners");
        }
    }
}
