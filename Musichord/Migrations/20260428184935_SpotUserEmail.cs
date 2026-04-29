using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Musichord.Migrations
{
    /// <inheritdoc />
    public partial class SpotUserEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpotEmail",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpotUser",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpotEmail",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SpotUser",
                table: "AspNetUsers");
        }
    }
}
