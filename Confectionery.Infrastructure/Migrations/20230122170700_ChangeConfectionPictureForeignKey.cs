using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Confectionery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeConfectionPictureForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfectionPicture_Confection",
                table: "ConfectionPicture");

            migrationBuilder.AlterColumn<string>(
                name: "Extension",
                table: "ConfectionPicture",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ConfectionPicture_ConfectionId",
                table: "ConfectionPicture",
                column: "ConfectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfectionPicture_Confection",
                table: "ConfectionPicture",
                column: "ConfectionId",
                principalTable: "Confection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfectionPicture_Confection",
                table: "ConfectionPicture");

            migrationBuilder.DropIndex(
                name: "IX_ConfectionPicture_ConfectionId",
                table: "ConfectionPicture");

            migrationBuilder.AlterColumn<string>(
                name: "Extension",
                table: "ConfectionPicture",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfectionPicture_Confection",
                table: "ConfectionPicture",
                column: "Id",
                principalTable: "Confection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
