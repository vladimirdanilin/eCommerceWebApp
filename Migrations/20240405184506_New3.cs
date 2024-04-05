using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceWebApp.Migrations
{
    /// <inheritdoc />
    public partial class New3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Checkouts_OrderId",
                table: "Checkouts");

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_OrderId",
                table: "Checkouts",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Checkouts_OrderId",
                table: "Checkouts");

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_OrderId",
                table: "Checkouts",
                column: "OrderId",
                unique: true);
        }
    }
}
