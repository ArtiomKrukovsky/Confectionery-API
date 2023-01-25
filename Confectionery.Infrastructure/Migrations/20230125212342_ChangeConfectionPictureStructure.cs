using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Confectionery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeConfectionPictureStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "ConfectionPicture");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "ConfectionPicture");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "ConfectionPicture",
                type: "nvarchar(2050)",
                maxLength: 2050,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "ConfectionPicture");

            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "ConfectionPicture",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "ConfectionPicture",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
