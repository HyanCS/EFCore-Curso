using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FuscaFilmes.Repo.Migrations
{
    /// <inheritdoc />
    public partial class M2MSeedCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DiretoresFilmes",
                columns: new[] { "DiretorId", "FilmeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 4 },
                    { 3, 5 },
                    { 4, 6 },
                    { 4, 7 },
                    { 5, 8 },
                    { 5, 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 4, 7 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 5, 8 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 5, 9 });
        }
    }
}
