using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Musichord.Migrations
{
    /// <inheritdoc />
    public partial class HandleSwitch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_ReceiverId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_SenderId",
                table: "Friendships");

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Friendships",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverId",
                table: "Friendships",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverHandle",
                table: "Friendships",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderHandle",
                table: "Friendships",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_ReceiverId",
                table: "Friendships",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Handle");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_SenderId",
                table: "Friendships",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Handle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_ReceiverId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_SenderId",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "ReceiverHandle",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "SenderHandle",
                table: "Friendships");

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Friendships",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverId",
                table: "Friendships",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_ReceiverId",
                table: "Friendships",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_SenderId",
                table: "Friendships",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
