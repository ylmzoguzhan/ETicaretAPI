using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaretAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _20230913_AlterProduct2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductImageFile_Products_ProductID",
                table: "ProductProductImageFile");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "ProductProductImageFile",
                newName: "ProductsID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductImageFile_Products_ProductsID",
                table: "ProductProductImageFile",
                column: "ProductsID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductImageFile_Products_ProductsID",
                table: "ProductProductImageFile");

            migrationBuilder.RenameColumn(
                name: "ProductsID",
                table: "ProductProductImageFile",
                newName: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductImageFile_Products_ProductID",
                table: "ProductProductImageFile",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
