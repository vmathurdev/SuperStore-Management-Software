using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperStoreManagement.Migrations
{
    public partial class cartmodelfkremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_product_ProductID",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_user_UserID",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_ProductID",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_UserID",
                table: "Cart");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cart_ProductID",
                table: "Cart",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserID",
                table: "Cart",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_product_ProductID",
                table: "Cart",
                column: "ProductID",
                principalTable: "product",
                principalColumn: "prodID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_user_UserID",
                table: "Cart",
                column: "UserID",
                principalTable: "user",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
