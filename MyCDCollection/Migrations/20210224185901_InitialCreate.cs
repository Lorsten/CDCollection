using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCDCollection.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    ArtistID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.ArtistID);
                });

            migrationBuilder.CreateTable(
                name: "CD",
                columns: table => new
                {
                    CDID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Album = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArtistNameID = table.Column<int>(type: "int", nullable: false),
                    Lended = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CD", x => x.CDID);
                    table.ForeignKey(
                        name: "FK_CD_Artist_ArtistNameID",
                        column: x => x.ArtistNameID,
                        principalTable: "Artist",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LendedCDS",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Lended = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CDSID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LendedCDS", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_LendedCDS_CD_CDSID",
                        column: x => x.CDSID,
                        principalTable: "CD",
                        principalColumn: "CDID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CD_ArtistNameID",
                table: "CD",
                column: "ArtistNameID");

            migrationBuilder.CreateIndex(
                name: "IX_LendedCDS_CDSID",
                table: "LendedCDS",
                column: "CDSID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LendedCDS");

            migrationBuilder.DropTable(
                name: "CD");

            migrationBuilder.DropTable(
                name: "Artist");
        }
    }
}
