using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fumble.Catalog.Database.Migrations
{
    public partial class Alter_Categories_Unique_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "UQ_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_Categories_Name",
                table: "Categories");
        }
    }
}
