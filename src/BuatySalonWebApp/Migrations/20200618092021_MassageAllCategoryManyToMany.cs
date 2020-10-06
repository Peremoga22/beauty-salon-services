using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuatySalonWebApp.Migrations
{
    public partial class MassageAllCategoryManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryMassageModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMassageModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MassageAllModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    LongDescription = table.Column<string>(nullable: false),
                    ShortDescription = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MassageAllModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MassageAllModelCategoryAllMassageModels",
                columns: table => new
                {
                    MassageAllModelId = table.Column<Guid>(nullable: false),
                    CategoryMassageModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MassageAllModelCategoryAllMassageModels", x => new { x.CategoryMassageModelId, x.MassageAllModelId });
                    table.ForeignKey(
                        name: "FK_MassageAllModelCategoryAllMassageModels_CategoryMassageModels_CategoryMassageModelId",
                        column: x => x.CategoryMassageModelId,
                        principalTable: "CategoryMassageModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MassageAllModelCategoryAllMassageModels_MassageAllModels_MassageAllModelId",
                        column: x => x.MassageAllModelId,
                        principalTable: "MassageAllModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MassageAllModelCategoryAllMassageModels_MassageAllModelId",
                table: "MassageAllModelCategoryAllMassageModels",
                column: "MassageAllModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MassageAllModelCategoryAllMassageModels");

            migrationBuilder.DropTable(
                name: "CategoryMassageModels");

            migrationBuilder.DropTable(
                name: "MassageAllModels");
        }
    }
}
