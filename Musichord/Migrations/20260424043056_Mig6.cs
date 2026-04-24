using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Musichord.Migrations
{
    /// <inheritdoc />
    public partial class Mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackArtists");

            migrationBuilder.AddColumn<int>(
                name: "ArtistId",
                table: "Tracks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_ArtistId",
                table: "Tracks",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Artists_ArtistId",
                table: "Tracks",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Artists_ArtistId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_ArtistId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Tracks");

            migrationBuilder.CreateTable(
                name: "TrackArtists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArtistId = table.Column<int>(type: "INTEGER", nullable: false),
                    TrackId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackArtists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackArtists_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackArtists_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackArtists_ArtistId",
                table: "TrackArtists",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackArtists_TrackId",
                table: "TrackArtists",
                column: "TrackId");
        }
    }
}
