using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebChild.Migrations
{
    public partial class updatesale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ProductPriceSale",
                table: "Products",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPriceSale",
                table: "Products");
        }
    }
}
