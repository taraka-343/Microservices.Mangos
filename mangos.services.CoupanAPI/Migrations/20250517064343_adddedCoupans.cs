using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mangos.services.CoupanAPI.Migrations
{
    /// <inheritdoc />
    public partial class adddedCoupans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupans",
                columns: table => new
                {
                    coupanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    coupanCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    discountAmount = table.Column<double>(type: "float", nullable: false),
                    minAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupans", x => x.coupanId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupans");
        }
    }
}
