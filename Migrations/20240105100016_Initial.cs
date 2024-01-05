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
                name: "ShoppingCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCategories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShoppingSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Day = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingSchedules", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShoppingItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Currency = table.Column<string>(type: "varchar(65)", maxLength: 65, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShoppingCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingItems_ShoppingCategories_ShoppingCategoryId",
                        column: x => x.ShoppingCategoryId,
                        principalTable: "ShoppingCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShoppingItemShoppingSchedule",
                columns: table => new
                {
                    ShoppingItemsId = table.Column<int>(type: "int", nullable: false),
                    ShoppingSchedulesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingItemShoppingSchedule", x => new { x.ShoppingItemsId, x.ShoppingSchedulesId });
                    table.ForeignKey(
                        name: "FK_ShoppingItemShoppingSchedule_ShoppingItems_ShoppingItemsId",
                        column: x => x.ShoppingItemsId,
                        principalTable: "ShoppingItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingItemShoppingSchedule_ShoppingSchedules_ShoppingSched~",
                        column: x => x.ShoppingSchedulesId,
                        principalTable: "ShoppingSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItems_ShoppingCategoryId",
                table: "ShoppingItems",
                column: "ShoppingCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItemShoppingSchedule_ShoppingSchedulesId",
                table: "ShoppingItemShoppingSchedule",
                column: "ShoppingSchedulesId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingSchedules_Day",
                table: "ShoppingSchedules",
                column: "Day",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingItemShoppingSchedule");

            migrationBuilder.DropTable(
                name: "ShoppingItems");

            migrationBuilder.DropTable(
                name: "ShoppingSchedules");

            migrationBuilder.DropTable(
                name: "ShoppingCategories");
        }
    }
}
