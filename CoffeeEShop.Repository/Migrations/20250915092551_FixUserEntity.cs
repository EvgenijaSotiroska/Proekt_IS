using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeEShop.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FixUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Orders_UserOrderId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserOrderId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserOrderId",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserOrderId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserOrderId",
                table: "Orders",
                column: "UserOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Orders_UserOrderId",
                table: "Orders",
                column: "UserOrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
