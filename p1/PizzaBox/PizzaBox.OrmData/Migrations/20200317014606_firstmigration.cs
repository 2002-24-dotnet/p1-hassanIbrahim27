using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaBox.OrmData.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pizza",
                columns: table => new
                {
                    PizzaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizza", x => x.PizzaId);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    StoreId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(nullable: true),
                    Manager = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.StoreId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "PizzaStore",
                columns: table => new
                {
                    PizzaId = table.Column<int>(nullable: false),
                    StoreId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaStore", x => new { x.PizzaId, x.StoreId });
                    table.ForeignKey(
                        name: "FK_PizzaStore_Pizza_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizza",
                        principalColumn: "PizzaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzaStore_Store_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(nullable: false),
                    StoreId = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_Store_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PizzaOrder",
                columns: table => new
                {
                    PizzaId = table.Column<int>(nullable: false),
                    OrderId = table.Column<long>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaOrder", x => new { x.PizzaId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_PizzaOrder_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzaOrder_Pizza_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizza",
                        principalColumn: "PizzaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Pizza",
                columns: new[] { "PizzaId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Vege", 10.99m },
                    { 2, "Cheese", 11.99m },
                    { 3, "Peppe", 9.99m },
                    { 4, "Vegan", 12.99m },
                    { 5, "Sushi", 12.99m },
                    { 6, "Mini", 3.99m }
                });

            migrationBuilder.InsertData(
                table: "Store",
                columns: new[] { "StoreId", "Location", "Manager" },
                values: new object[,]
                {
                    { 1, "Down Town", "Store1" },
                    { 2, "City", "Store2" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Password", "Type", "UserName" },
                values: new object[,]
                {
                    { 637199883660451603L, "123", "U", "user1" },
                    { 637199883660469400L, "234", "U", "user2" },
                    { 637199883660469460L, "345", "U", "user3" },
                    { 637199883660469465L, "456", "U", "user4" },
                    { 637199883660469469L, "123", "S", "store1" },
                    { 637199883660469472L, "234", "S", "store2" }
                });

            migrationBuilder.InsertData(
                table: "PizzaStore",
                columns: new[] { "PizzaId", "StoreId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 100 },
                    { 2, 1, 100 },
                    { 3, 1, 0 },
                    { 4, 1, 100 },
                    { 5, 1, 100 },
                    { 6, 1, 100 },
                    { 1, 2, 100 },
                    { 2, 2, 100 },
                    { 3, 2, 100 },
                    { 4, 2, 100 },
                    { 5, 2, 0 },
                    { 6, 2, 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_StoreId",
                table: "Order",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaOrder_OrderId",
                table: "PizzaOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaStore_StoreId",
                table: "PizzaStore",
                column: "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzaOrder");

            migrationBuilder.DropTable(
                name: "PizzaStore");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Pizza");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
