using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagementSystemUsingMVC.Migrations
{
    /// <inheritdoc />
    public partial class seedOrAddDataToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BookData",
                columns: new[] { "Id", "Author", "BookTitle", "Genre", "Status", "Year" },
                values: new object[,]
                {
                    { 1, "F.Scott Fiztgerald", "The Great Gatsby", "Fiction", true, 1925 },
                    { 2, "Gorge Overwall", "1984", "Science Fiction", false, 1950 },
                    { 3, "Harper Lee", "To Kill a Mockingbird", "Fiction", true, 1960 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookData",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookData",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookData",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
