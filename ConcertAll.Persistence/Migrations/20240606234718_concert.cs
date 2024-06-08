using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcertAll.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class concert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genre");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre",
                table: "Genre",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Concert",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Place = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false),
                    DateEvent = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    ImageUrl = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    TicketsQuantity = table.Column<int>(type: "int", nullable: false),
                    Finalized = table.Column<bool>(type: "bit", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concert", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Concert_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Concert_GenreId",
                table: "Concert",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Concert");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre",
                table: "Genre");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "Genres");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "Id");
        }
    }
}
