using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingListDemo.Migrations
{
    public partial class FixUniqueness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_scheduled_shopping_items_day",
                table: "scheduled_shopping_items");

            migrationBuilder.CreateIndex(
                name: "ix_scheduled_shopping_items_day",
                table: "scheduled_shopping_items",
                column: "day");

            migrationBuilder.CreateIndex(
                name: "ix_scheduled_shopping_items_day_shopping_item_id",
                table: "scheduled_shopping_items",
                columns: new[] { "day", "shopping_item_id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_scheduled_shopping_items_day",
                table: "scheduled_shopping_items");

            migrationBuilder.DropIndex(
                name: "ix_scheduled_shopping_items_day_shopping_item_id",
                table: "scheduled_shopping_items");

            migrationBuilder.CreateIndex(
                name: "ix_scheduled_shopping_items_day",
                table: "scheduled_shopping_items",
                column: "day",
                unique: true);
        }
    }
}
