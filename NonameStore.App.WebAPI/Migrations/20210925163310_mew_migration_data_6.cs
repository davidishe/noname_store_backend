using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonameStore.App.WebAPI.Migrations
{
    public partial class mew_migration_data_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryMethods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(nullable: true),
                    DeliveryTime = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ByerEmail = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTimeOffset>(nullable: false),
                    ShipToAddress_FirstName = table.Column<string>(nullable: true),
                    ShipToAddress_LastName = table.Column<string>(nullable: true),
                    ShipToAddress_Street = table.Column<string>(nullable: true),
                    ShipToAddress_City = table.Column<string>(nullable: true),
                    ShipToAddress_House = table.Column<string>(nullable: true),
                    ShipToAddress_ZipCode = table.Column<string>(nullable: true),
                    DeliveryMethodId = table.Column<int>(nullable: true),
                    Subtotal = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    PaymentIntentId = table.Column<string>(nullable: true),
                    OrderNumber = table.Column<string>(nullable: true),
                    PaymentMethod = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryMethods_DeliveryMethodId",
                        column: x => x.DeliveryMethodId,
                        principalTable: "DeliveryMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Price = table.Column<int>(nullable: false),
                    PictureUrl = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ProductTypeId = table.Column<int>(nullable: false),
                    ProductRegionId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: true),
                    EnrolledDate = table.Column<DateTime>(nullable: false),
                    GuId = table.Column<int>(nullable: false),
                    IsSale = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Regions_ProductRegionId",
                        column: x => x.ProductRegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemOrdered_ProductItemId = table.Column<int>(nullable: true),
                    ItemOrdered_Name = table.Column<string>(nullable: true),
                    ItemOrdered_PictureUrl = table.Column<string>(nullable: true),
                    ItemOrdered_GuId = table.Column<int>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    GuId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryMethodId",
                table: "Orders",
                column: "DeliveryMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductRegionId",
                table: "Products",
                column: "ProductRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "DeliveryMethods");
        }
    }
}
