
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeEShop.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ApiIntegration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetailedProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IngredientsDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaffeineInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Served = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailedProducts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailedProducts");
        }
    }
}
