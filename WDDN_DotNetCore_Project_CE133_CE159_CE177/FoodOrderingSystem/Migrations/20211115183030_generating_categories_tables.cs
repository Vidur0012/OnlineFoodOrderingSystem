using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodOrderingSystem.Migrations
{
    public partial class generating_categories_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chinese",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(fixedLength: true, maxLength: 20, nullable: false),
                    Price = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
                    PhotoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chinese", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ColdDrinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(fixedLength: true, maxLength: 20, nullable: false),
                    Price = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
                    PhotoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColdDrinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FastFood",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(fixedLength: true, maxLength: 20, nullable: false),
                    Price = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
                    PhotoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FastFood", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gujarati",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(fixedLength: true, maxLength: 20, nullable: false),
                    Price = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
                    PhotoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gujarati", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(fixedLength: true, maxLength: 20, nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Punjabi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(fixedLength: true, maxLength: 20, nullable: false),
                    Price = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
                    PhotoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Punjabi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SouthIndian",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(fixedLength: true, maxLength: 20, nullable: false),
                    Price = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
                    PhotoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SouthIndian", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chinese");

            migrationBuilder.DropTable(
                name: "ColdDrinks");

            migrationBuilder.DropTable(
                name: "FastFood");

            migrationBuilder.DropTable(
                name: "Gujarati");

            migrationBuilder.DropTable(
                name: "OrderList");

            migrationBuilder.DropTable(
                name: "Punjabi");

            migrationBuilder.DropTable(
                name: "SouthIndian");
        }
    }
}
