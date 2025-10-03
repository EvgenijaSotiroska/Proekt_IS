using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeEShop.Repository.Migrations
{
    /// <inheritdoc />
    public partial class APIMigrationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaffeineInfo",
                table: "DetailedProducts");

            migrationBuilder.RenameColumn(
                name: "Served",
                table: "DetailedProducts",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Origin",
                table: "DetailedProducts",
                newName: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "IngredientsDescription",
                table: "DetailedProducts",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "DetailedProducts",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "DetailedProducts",
                newName: "Served");

            migrationBuilder.RenameColumn(
                name: "Ingredients",
                table: "DetailedProducts",
                newName: "Origin");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "DetailedProducts",
                newName: "IngredientsDescription");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "DetailedProducts",
                newName: "DisplayName");

            migrationBuilder.AddColumn<string>(
                name: "CaffeineInfo",
                table: "DetailedProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
