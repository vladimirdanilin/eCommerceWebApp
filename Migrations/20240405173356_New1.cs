using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceWebApp.Migrations
{
    /// <inheritdoc />
    public partial class New1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CheckoutId",
                table: "Addresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Checkouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checkouts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CheckoutId",
                table: "Addresses",
                column: "CheckoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_OrderId",
                table: "Checkouts",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Checkouts_CheckoutId",
                table: "Addresses",
                column: "CheckoutId",
                principalTable: "Checkouts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Checkouts_CheckoutId",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "Checkouts");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CheckoutId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CheckoutId",
                table: "Addresses");
        }
    }
}
