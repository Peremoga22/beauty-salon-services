using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuatySalonWebApp.Migrations
{
    public partial class NailsAndCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryAllNails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryAllNails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NailsAllModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    LongDescription = table.Column<string>(nullable: false),
                    ShortDescription = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NailsAllModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NailsAllModelCategories",
                columns: table => new
                {
                    NailsAllModelId = table.Column<Guid>(nullable: false),
                    CategoryAllNailsModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NailsAllModelCategories", x => new { x.CategoryAllNailsModelId, x.NailsAllModelId });
                    table.ForeignKey(
                        name: "FK_NailsAllModelCategories_CategoryAllNails_CategoryAllNailsModelId",
                        column: x => x.CategoryAllNailsModelId,
                        principalTable: "CategoryAllNails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NailsAllModelCategories_NailsAllModels_NailsAllModelId",
                        column: x => x.NailsAllModelId,
                        principalTable: "NailsAllModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NailsAllModelCategories_NailsAllModelId",
                table: "NailsAllModelCategories",
                column: "NailsAllModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NailsAllModelCategories");

            migrationBuilder.DropTable(
                name: "CategoryAllNails");

            migrationBuilder.DropTable(
                name: "NailsAllModels");
        }
    }
}
