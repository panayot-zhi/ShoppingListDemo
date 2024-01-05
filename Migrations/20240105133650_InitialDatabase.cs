using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingListDemo.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shopping_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    order = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shopping_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "shopping_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    shopping_category_id = table.Column<int>(type: "INTEGER", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "scheduled_shopping_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    day = table.Column<DateTime>(type: "TEXT", nullable: false),
                    bought = table.Column<bool>(type: "INTEGER", nullable: false),
                    shopping_item_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scheduled_shopping_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_scheduled_shopping_items_shopping_items_shopping_item_id",
                        column: x => x.shopping_item_id,
                        principalTable: "shopping_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_scheduled_shopping_items_day",
                table: "scheduled_shopping_items",
                column: "day",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_scheduled_shopping_items_shopping_item_id",
                table: "scheduled_shopping_items",
                column: "shopping_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_shopping_categories_name",
                table: "shopping_categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_shopping_categories_order",
                table: "shopping_categories",
                column: "order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_shopping_items_shopping_category_id",
                table: "shopping_items",
                column: "shopping_category_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "scheduled_shopping_items");

            migrationBuilder.DropTable(
                name: "shopping_items");

            migrationBuilder.DropTable(
                name: "shopping_categories");
        }
    }
}
