using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuatySalonWebApp.Migrations
{
    public partial class ContactUsContactUsInfoContactUsIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    LongDescription = table.Column<string>(nullable: false),
                    ShortDescription = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactUsIcons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icon = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUsIcons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactUsInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ShortDescription = table.Column<string>(nullable: false),
                    ContactUsIconId = table.Column<int>(nullable: true),
                    ContactUsId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUsInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactUsInfos_ContactUsIcons_ContactUsIconId",
                        column: x => x.ContactUsIconId,
                        principalTable: "ContactUsIcons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactUsInfos_ContactUs_ContactUsId",
                        column: x => x.ContactUsId,
                        principalTable: "ContactUs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactUsInfos_ContactUsIconId",
                table: "ContactUsInfos",
                column: "ContactUsIconId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUsInfos_ContactUsId",
                table: "ContactUsInfos",
                column: "ContactUsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactUsInfos");

            migrationBuilder.DropTable(
                name: "ContactUsIcons");

            migrationBuilder.DropTable(
                name: "ContactUs");
        }
    }
}
