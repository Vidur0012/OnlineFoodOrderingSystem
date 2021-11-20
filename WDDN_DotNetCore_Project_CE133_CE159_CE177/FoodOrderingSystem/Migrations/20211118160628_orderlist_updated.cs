using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodOrderingSystem.Migrations
{
    public partial class orderlist_updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "OrderList",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "OrderList");
        }
    }
}
