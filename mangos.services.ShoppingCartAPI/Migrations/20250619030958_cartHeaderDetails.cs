using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mangos.services.ShoppingCartAPI.Migrations
{
    /// <inheritdoc />
    public partial class cartHeaderDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cartHeaders",
                columns: table => new
                {
                    cartHeaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    coupanCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartHeaders", x => x.cartHeaderId);
                });

            migrationBuilder.CreateTable(
                name: "cartDeatails",
                columns: table => new
                {
                    cartDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cartHeaderId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartDeatails", x => x.cartDetailsId);
                    table.ForeignKey(
                        name: "FK_cartDeatails_cartHeaders_cartHeaderId",
                        column: x => x.cartHeaderId,
                        principalTable: "cartHeaders",
                        principalColumn: "cartHeaderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cartDeatails_cartHeaderId",
                table: "cartDeatails",
                column: "cartHeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cartDeatails");

            migrationBuilder.DropTable(
                name: "cartHeaders");
        }
    }
}
