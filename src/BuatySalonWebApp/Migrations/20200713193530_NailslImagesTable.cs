using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuatySalonWebApp.Migrations
{
    public partial class NailslImagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NailsImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nmae = table.Column<string>(nullable: true),
                    NailsAllModelId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NailsImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NailsImages_NailsAllModels_NailsAllModelId",
                        column: x => x.NailsAllModelId,
                        principalTable: "NailsAllModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NailsImages_NailsAllModelId",
                table: "NailsImages",
                column: "NailsAllModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NailsImages");
        }
    }
}
