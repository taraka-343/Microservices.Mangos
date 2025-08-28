using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace mangos.services.CoupanAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedCouponData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupans",
                columns: new[] { "coupanId", "coupanCode", "discountAmount", "minAmount" },
                values: new object[,]
                {
                    { 1, "10OFF", 10.0, 20 },
                    { 2, "20OFF", 20.0, 40 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupans",
                keyColumn: "coupanId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coupans",
                keyColumn: "coupanId",
                keyValue: 2);
        }
    }
}
