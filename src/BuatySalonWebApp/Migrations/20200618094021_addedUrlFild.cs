using Microsoft.EntityFrameworkCore.Migrations;

namespace BuatySalonWebApp.Migrations
{
    public partial class addedUrlFild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "MassageAllModels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "MassageAllModels");
        }
    }
}
