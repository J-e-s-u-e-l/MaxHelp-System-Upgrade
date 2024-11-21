using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaxHelp_System_Upgrade.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedInventoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductNumber",
                table: "Inventory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductNumber",
                table: "Inventory");
        }
    }
}
