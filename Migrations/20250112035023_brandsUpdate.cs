using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Migrations
{
    /// <inheritdoc />
    public partial class brandsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_brands_BrandName",
                table: "brands",
                column: "BrandName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_brands_BrandName",
                table: "brands");
        }
    }
}
