using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Musichord.Migrations
{
    /// <inheritdoc />
    public partial class Mig12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "AppUserId", table: "FavoriteTracks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteTracks_AspNetUsers_UserId",
                table: "FavoriteTracks");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteTracks_Tracks_TrackId",
                table: "FavoriteTracks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Artists_ArtistId",
                table: "Tracks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tracks",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_SpotifyId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteTracks_UserId",
                table: "FavoriteTracks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Artists",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_SpotifyArtistId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Artists");

            migrationBuilder.AlterColumn<string>(
                name: "ArtistId",
                table: "Tracks",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "TrackId",
                table: "FavoriteTracks",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "FavoriteTracks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tracks",
                table: "Tracks",
                column: "SpotifyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artists",
                table: "Artists",
                column: "SpotifyArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteTracks_AppUserId",
                table: "FavoriteTracks",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteTracks_AspNetUsers_AppUserId",
                table: "FavoriteTracks",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteTracks_Tracks_TrackId",
                table: "FavoriteTracks",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "SpotifyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Artists_ArtistId",
                table: "Tracks",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "SpotifyArtistId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
