using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaxHelp_System_Upgrade.Migrations
{
    /// <inheritdoc />
    public partial class AddedThreholdToInventoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReorderThreshold",
                table: "Inventory",
                type: "int",
                nullable: false,
                defaultValue: 30);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReorderThreshold",
                table: "Inventory");
        }
    }
}
