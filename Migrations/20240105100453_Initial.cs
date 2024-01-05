using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingListDemo.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shopping_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shopping_categories", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shopping_schedules",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    day = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shopping_schedules", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shopping_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    currency = table.Column<string>(type: "varchar(65)", maxLength: 65, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    shopping_category_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shopping_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_shopping_items_shopping_categories_shopping_category_id",
                        column: x => x.shopping_category_id,
                        principalTable: "shopping_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shopping_item_shopping_schedule",
                columns: table => new
                {
                    shopping_items_id = table.Column<int>(type: "int", nullable: false),
                    shopping_schedules_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shopping_item_shopping_schedule", x => new { x.shopping_items_id, x.shopping_schedules_id });
                    table.ForeignKey(
                        name: "fk_shopping_item_shopping_schedule_shopping_items_shopping_item",
                        column: x => x.shopping_items_id,
                        principalTable: "shopping_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_shopping_item_shopping_schedule_shopping_schedules_shopping_",
                        column: x => x.shopping_schedules_id,
                        principalTable: "shopping_schedules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_shopping_item_shopping_schedule_shopping_schedules_id",
                table: "shopping_item_shopping_schedule",
                column: "shopping_schedules_id");

            migrationBuilder.CreateIndex(
                name: "ix_shopping_items_shopping_category_id",
                table: "shopping_items",
                column: "shopping_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_shopping_schedules_day",
                table: "shopping_schedules",
                column: "day",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shopping_item_shopping_schedule");

            migrationBuilder.DropTable(
                name: "shopping_items");

            migrationBuilder.DropTable(
                name: "shopping_schedules");

            migrationBuilder.DropTable(
                name: "shopping_categories");
        }
    }
}
