using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Service.Catalog.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageURL", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Apple, Digital", "Apple MacBook", "", "MacBook", 999.99000000000001 },
                    { 2, "Apple, Digital", "Apple Phone", "", "IPhone 13", 1000.99 },
                    { 3, "Samsung, Digital", "Samsung Phone", "", "Samsung S23", 899.99000000000001 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);
        }
    }
}
