using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaxHelp_System_Upgrade.Migrations
{
    /// <inheritdoc />
    public partial class AddedUpdatedSalesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Inventory_ProductId",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Sales",
                newName: "InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_ProductId",
                table: "Sales",
                newName: "IX_Sales_InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Inventory_InventoryId",
                table: "Sales",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Inventory_InventoryId",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "InventoryId",
                table: "Sales",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_InventoryId",
                table: "Sales",
                newName: "IX_Sales_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Inventory_ProductId",
                table: "Sales",
                column: "ProductId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
