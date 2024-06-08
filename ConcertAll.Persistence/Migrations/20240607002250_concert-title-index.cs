using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcertAll.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class concerttitleindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Musicals");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "Genre",
                newSchema: "Musicals");

            migrationBuilder.RenameTable(
                name: "Concert",
                newName: "Concert",
                newSchema: "Musicals");

            migrationBuilder.CreateIndex(
                name: "IX_Concert_Title",
                schema: "Musicals",
                table: "Concert",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Concert_Title",
                schema: "Musicals",
                table: "Concert");

            migrationBuilder.RenameTable(
                name: "Genre",
                schema: "Musicals",
                newName: "Genre");

            migrationBuilder.RenameTable(
                name: "Concert",
                schema: "Musicals",
                newName: "Concert");
        }
    }
}
