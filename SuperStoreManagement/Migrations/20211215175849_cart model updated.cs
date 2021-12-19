using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperStoreManagement.Migrations
{
    public partial class cartmodelupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_product_ProductprodID",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_user_UserId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_ProductprodID",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "ProductprodID",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Cart",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                newName: "IX_Cart_UserID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ProductID",
                table: "Cart",
                column: "ProductID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Cart",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_UserID",
                table: "Cart",
                newName: "IX_Cart_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Cart",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductprodID",
                table: "Cart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ProductprodID",
                table: "Cart",
                column: "ProductprodID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_product_ProductprodID",
                table: "Cart",
                column: "ProductprodID",
                principalTable: "product",
                principalColumn: "prodID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_user_UserId",
                table: "Cart",
                column: "UserId",
                principalTable: "user",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
