using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fumble.Catalog.Database.Migrations
{
    public partial class Alter_Products_Category_To_Categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Categories_CategoryId",
                table: "CategoryProduct");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "CategoryProduct",
                newName: "CategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Categories_CategoriesId",
                table: "CategoryProduct",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Categories_CategoriesId",
                table: "CategoryProduct");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "CategoryProduct",
                newName: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Categories_CategoryId",
                table: "CategoryProduct",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
