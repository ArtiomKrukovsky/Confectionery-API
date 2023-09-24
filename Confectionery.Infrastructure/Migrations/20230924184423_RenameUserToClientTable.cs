using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Confectionery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameUserToClientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_User",
                table: "Order");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Order",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_UserId",
                table: "Order",
                newName: "IX_Order_ClientId");

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    FullName = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    InstagramProfile = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.CheckConstraint("CK_Client_MobileNumber", "[MobileNumber] NOT LIKE '%[^0-9]%'");
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Client",
                table: "Order",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Client",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Order",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ClientId",
                table: "Order",
                newName: "IX_Order_UserId");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Email = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: false),
                    InstagramProfile = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.CheckConstraint("CK_User_MobileNumber", "[MobileNumber] NOT LIKE '%[^0-9]%'");
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User",
                table: "Order",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
